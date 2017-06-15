using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xades.Implementations;

namespace HCS {
    /// <summary>
    /// Конфигурация ServicePointManager для работы с tls
    /// </summary>
    public static class ServicePointConfig {
        internal static string CertificateThumbPrint;
        internal static string PrivateKeyPassword;
        internal static CryptoProviderTypeEnum CryptoProviderType;

        /// <summary>
        /// Первичная инициализация
        /// </summary>
        /// <param name="certificateThumbPrint">отпечаток сертификата</param>
        /// <param name="privateKeyPassword">пароль от контейнера закрытого ключа</param>
        /// <param name="cryptoProviderType">тип крипто провайдера</param>
        public static void InitConfig(string certificateThumbPrint, string privateKeyPassword, int cryptoProviderType) {
            CertificateThumbPrint = certificateThumbPrint;
            PrivateKeyPassword = privateKeyPassword;
            CryptoProviderType = (CryptoProviderTypeEnum)cryptoProviderType;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            ServicePointManager.Expect100Continue = false;
        }

    }
}
