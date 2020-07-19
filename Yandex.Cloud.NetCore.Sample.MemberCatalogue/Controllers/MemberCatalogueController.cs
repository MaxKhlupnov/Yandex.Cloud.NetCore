using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Yandex.Cloud.NetCore.Sample.Common.Security;
using Yandex.Cloud.NetCore.Sample.Common.Models;
using Yandex.Cloud.NetCore.Sample.MemberCatalogue.Models;
using Yandex.Cloud.NetCore.Sample.Common.Framework;


namespace Yandex.Cloud.NetCore.Sample.MemberCatalogue.Controllers
{
    public class MemberCatalogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<Member> _context;
        private readonly MembersManager _membersManager;
        private readonly UserManager<Member> userManager;

        /// <summary>
        /// Первичная верификация и регистрация клиента
        /// </summary>
        public MemberCatalogController(IMapper mapper, SignInManager<Member> signInManager,  UserManager<Member> userManager)
        {
            this._mapper = mapper;
            this._context = signInManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// Register member and return verification code (verification send email, sms)
        /// </summary>
        [Route("api/v1/MemberCatalogue/Register")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Register([FromBody] RegistrationModel registrationModel)
        {
            var generator = new Random();
            var verificationCode = generator.Next(1000, 9999);
            var sessionToken = AuthCrypto.GetConnectionToken();
            var newMember = _mapper.Map<Member>(registrationModel);
            newMember.UserName = registrationModel.Email;
            newMember.VerificationCode = verificationCode;

            var existingMember = await userManager.FindByNameAsync(newMember.UserName);
                

            if (existingMember != null)
            {
                if (!string.Equals(existingMember.Email, registrationModel.Email, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new { result = "email адрес не совпадает с введенным ранее" });
                }
                return BadRequest(new { result = $"пользователь4 с номером телефона {registrationModel.PhoneNumber} уже зарегистрирован" });
            }
            else
            {
                newMember.MemberRole = MemberRole.ROLE_MEMBER;
                // await user.CreateMemberAsync(newMember, registrationModel.Password);

                var result = await userManager.CreateAsync(newMember, registrationModel.Password);
                if (!result.Succeeded)
                {
                    IdentityError err = result.Errors.First<IdentityError>();
                    return BadRequest($"{err.Code} {err.Description}");
                }
                return Ok(sessionToken);
            }
        }


    }
}
