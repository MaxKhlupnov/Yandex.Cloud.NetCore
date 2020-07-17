using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex.Cloud.NetCore.Sample.MemberCatalogue.Models
{
    /// <summary>
    /// Модель проверки средств связи пользователя
    /// </summary>
    public class CommunicationVeryfyModel
    {
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
