﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.Cloud.NetCore.Sample.Common.Models
{
    /**
     * Информация об участнике
     */
    public class Member
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
        /// Тип участника - "Волонтер" или "Нуждающийся в помощи"
        /// </summary>
        public short MemberType { get; set; }

        /// <summary>
        /// Номер телефона участника
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email адрес участника
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Код проверки первичной регистрации
        /// </summary>
        public int VerificationCode { get; set; }

        /// <summary>
        /// Password Хэш
        /// </summary>
        public string PwdHash { get; set; }

        /// <summary>
        /// Признак пройденной верификации проверочного кода
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Признак пройденной регистрации
        /// </summary>
        public bool IsRegistered { get; set; }
    }
}
