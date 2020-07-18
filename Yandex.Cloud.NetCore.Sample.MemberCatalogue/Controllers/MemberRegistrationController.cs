﻿using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Yandex.Cloud.NetCore.Sample.Common.Security;
using Yandex.Cloud.NetCore.Sample.Common.Models;
using Yandex.Cloud.NetCore.Sample.MemberCatalogue.Models;
using Yandex.Cloud.NetCore.Sample.Common.Framework;


namespace Yandex.Cloud.NetCore.Sample.MemberCatalogue.Controllers
{
    public class MemberCatalogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;
        private readonly MembersManager _membersManager;

        /// <summary>
        /// Первичная верификация и регистрация клиента
        /// </summary>
        public MemberCatalogController(IMapper mapper, ApplicationContext context, MembersManager membersManager)
        {
            this._mapper = mapper;
            this._context = context;
            this._membersManager = membersManager;
        }

        /// <summary>
        /// Register member and return verification code (verification send email, sms)
        /// </summary>
        [Route("Register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Register([FromBody] RegistrationModel registrationModel)
        {
            var generator = new Random();
            var verificationCode = generator.Next(1000, 9999);
            var sessionToken = AuthCrypto.GetConnectionToken();
            var newMember = _mapper.Map<Member>(registrationModel);
            newMember.Login =  registrationModel.PhoneNumber != null ?  registrationModel.PhoneNumber :  registrationModel.Email;
            newMember.VerificationCode = verificationCode;

            var existingMember = _membersManager.FindMemberAsync( registrationModel.PhoneNumber).Result;

            if (existingMember != null)
            {
                if (!string.Equals(existingMember.Email, registrationModel.Email, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new { result = "email адрес не совпадает с введенным ранее" });
                }

            }
                return Ok(sessionToken);
        }


    }
}