using System;
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
    public class MemberRegistrationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;
        private readonly MembersManager _membersManager;

        /// <summary>
        /// Первичная верификация и регистрация клиента
        /// </summary>
        public MemberRegistrationController(IMapper mapper, ApplicationContext context, MembersManager membersManager)
        {
            this._mapper = mapper;
            this._context = context;
            this._membersManager = membersManager;
        }

        /// <summary>
        /// Verify user communication (send email, sms)
        /// </summary>
        [Route("createLogin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetVerificationCode([FromBody] CommunicationVeryfyModel verifyCommunicationModel)
        {
            var generator = new Random();
            var verificationCode = generator.Next(1000, 9999);
            var sessionToken = AuthCrypto.GetConnectionToken();
            var newMember = _mapper.Map<Member>(verifyCommunicationModel);
            newMember.Login = verifyCommunicationModel.PhoneNumber != null ? verifyCommunicationModel.PhoneNumber : verifyCommunicationModel.Email;
            newMember.VerificationCode = verificationCode;

            var existingMember = _membersManager.FindMemberAsync(verifyCommunicationModel.PhoneNumber);

            return Ok(sessionToken);
        }


    }
}
