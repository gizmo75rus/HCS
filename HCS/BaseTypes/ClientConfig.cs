using HCS.Globals;

namespace HCS.BaseTypes {
    /// <summary>
    /// Конфигурация клиента
    /// </summary>
    public class ClientConfig {
        /// <summary>
        /// Идентификатор поставщика данных ГИС
        /// </summary>
        public string OrgPPAGUID { get; set; }

        /// <summary>
        /// Идентификатор организации в ГИС
        /// </summary>
        public string OrgEntityGUID { get; set; }

        /// <summary>
        /// Отпечаток сертификата
        /// </summary>
        public string CertificateThumbprint { get; set; }

        /// <summary>
        /// Признак - указывает на то, что используется внешний туннель (stunnel)
        /// </summary>
        public bool UseTunnel { get; set; }

        /// <summary>
        /// true - использовать адреса ППАК стенда иначе СИТ
        /// </summary>
        public bool IsPPAK { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public OrganizationRoles Role { get; set; }
    }
}
