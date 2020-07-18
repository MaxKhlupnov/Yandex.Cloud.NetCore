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
    public class MembersManager : IDisposable
    {
        protected AuthContext Context { get; }
        private bool _disposed;

        public MembersManager(AuthContext context)
        {
            Context = context;
        }

        public List<Claim> GetUserClaims(Member member)
        {

            var claims = new List<Claim>
            {
                new Claim("phoneNumber", member.PhoneNumber),
                new Claim("email", member.Email),
                new Claim("role", member.MemberRole)
            };

            return claims;
        }

        public async Task<Member> FindMemberAsync(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException(nameof(login));

            return await Context.Members.FirstOrDefaultAsync(
                u => u.UserName == login);
        }

        public async Task CreateMemberAsync(Member newMember, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException(nameof(password));

            newMember.PasswordHash = GeneretePwdHash(password);

            Context.Members.Add(newMember);
            await Context.SaveChangesAsync();
        }

        public bool CheckPassword(Member member, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException(nameof(password));


            var hash = GeneretePwdHash(password);
            var success = member.PasswordHash.Equals(hash);
            if (!success)
            {
                member.AccessFailedCount++;
            }

            return success;
        }

        private string GeneretePwdHash(string password)
        {
            return AuthCrypto.GenerateSha256String(password + AuthCrypto.InnerSalt);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
             Context.Dispose();
        }

        
    }
}
