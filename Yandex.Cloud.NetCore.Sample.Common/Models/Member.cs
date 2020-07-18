using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Yandex.Cloud.NetCore.Sample.Common.Models
{
    public static class MemberRole
    {
        public const string ROLE_ADMIN = "admin";
        public const string ROLE_MANAGER = "coordinator";
        public const string ROLE_CONTRIBUTOR = "contributor";
        public const string ROLE_MEMBER = "member";
    }


    /**
     * Информация об участнике
     */
    public class Member : IdentityUser
    {
        /// <summary>
        /// ID участника
        /// </summary>
        public Guid MemberId { get; set; }

        /// <summary>
        /// Фамилия участника
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя участника
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Адрес участника
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Роль участника - "Администратор","Координатор","Волонтер","Участник"
        /// </summary>
        public string MemberRole { get; set; }

        /// <summary>
        /// Номер телефона участника
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email адрес участника
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Код проверки первичной регистрации
        /// </summary>
        public int VerificationCode { get; set; }
    }
}
