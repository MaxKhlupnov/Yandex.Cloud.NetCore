using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex.Cloud.NetCore.Sample.MemberCatalogue.Models
{
    /// <summary>
    /// Модель проверки средств связи пользователя
    /// </summary>
    public class RegistrationModel
    {
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
        /// Номер телефона участника
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email адрес участника
        /// </summary>
        public string Email { get; set; }
    }
}
