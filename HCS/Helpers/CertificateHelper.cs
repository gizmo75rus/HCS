using GostCryptography.Cryptography;
using System;
using System.Security.Cryptography.X509Certificates;

namespace HCS.Helpers {
    public static class CertificateHelper {
        public static bool IsGostPrivateKey(this X509Certificate2 certificate) {
            try
            {
                if (certificate.HasPrivateKey)
                {
                    var cspInfo = certificate.GetPrivateKeyInfo();
                    if (cspInfo.ProviderType == 75 || cspInfo.ProviderType == 2)
                        return true;
                    else
                        return false;

                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static Tuple<int, string> GetProviderType(this X509Certificate2 certificate) {
            try
            {
                if (certificate.HasPrivateKey)
                {
                    var cspInfo = certificate.GetPrivateKeyInfo();
                    return new Tuple<int, string>(cspInfo.ProviderType, cspInfo.ProviderName);
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
