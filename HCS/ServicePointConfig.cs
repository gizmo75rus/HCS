using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xades.Implementations;

namespace HCS {
    public static class ServicePointConfig {
        internal static string CertificateThumbPrint;
        internal static string PrivateKeyPassword;
        internal static CryptoProviderTypeEnum CryptoProviderType;

        /// <summary>
        /// Инициализировать протокол безопастности 
        /// </summary>
        public static void InitConfig(string certificateThumbPrint, string privateKeyPassword, int cryptoProviderType) {
            CertificateThumbPrint = certificateThumbPrint;
            PrivateKeyPassword = privateKeyPassword;
            CryptoProviderType = (CryptoProviderTypeEnum)cryptoProviderType;

            InitTLSConfig();

        }

        public static void InitTLSConfig() {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            ServicePointManager.Expect100Continue = false;

        }
    }
}
