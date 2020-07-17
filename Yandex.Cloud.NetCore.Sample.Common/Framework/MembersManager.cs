using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yandex.Cloud.NetCore.Sample.Common.Models;
using Yandex.Cloud.NetCore.Sample.Common.Security;


namespace Yandex.Cloud.NetCore.Sample.Common.Framework
{
    public class MembersManager
    {
        protected ApplicationContext Context { get; }

        public MembersManager(ApplicationContext context)
        {
            Context = context;
        }

        public List<Claim> GetUserClaims(Member member)
        {

            var claims = new List<Claim>
            {
                new Claim("phoneNumber", member.PhoneNumber),
                new Claim("email", member.Email),
            };

            return claims;
        }

        public async Task<Member> FindMemberAsync(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException(nameof(login));

            return await Context.Members.FirstOrDefaultAsync(
                u => u.Login == login);
        }

        public bool CheckPassword(Member member, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException(nameof(password));


            var hash = AuthCrypto.GenerateSha256String(password + AuthCrypto.InnerSalt);
            var success = member.PwdHash.Equals(hash);
            if (!success)
            {
                //todo: add log failed user pass
            }

            return success;
        }

    }
}
