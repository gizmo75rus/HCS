using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GostCryptography.Cryptography;
using Xades.Implementations;

namespace Xades.Helpers
{
    public static class GostHashAlgorithmHelper
    {
        /// <summary>
        /// Расчитать HASH
        /// </summary>
        /// <param name="cryptoProviderType">Тип Критопровайдера</param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ComputeHash(CryptoProviderTypeEnum cryptoProviderType, byte[] bytes)
        {
            byte[] hashValue;
            GostCryptoConfig.ProviderType = (int)cryptoProviderType;
            var hashAlgorithm = new Gost3411HashAlgorithm();
            hashValue = hashAlgorithm.ComputeHash(bytes);
            return BitConverter.ToString(hashValue).Replace("-", "");
        }
    }
}
