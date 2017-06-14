using Org.BouncyCastle.Asn1.X509;
using System;
using System.Linq;
using System.Text;

namespace Xades.Helpers
{
    public static class X509NameHelper
    {
        /// <summary>
        /// Исправить строку X509IssuerName для рукожопых пейсателей из Ланита
        /// </summary>
        /// <param name="x509Name">Исходная строка из сертификата</param>
        /// <returns>Исправленная строка, чтобы ее понимал сервер ГИС ЖКХ</returns>
        public static string ToX509IssuerName(this X509Name x509Name) {
            string x509IssuerName = x509Name.ToString();
            var pairs = x509IssuerName
                    .Replace("\\,", "^_^")
                    .Split(',')
                    .Select(part => part.Split('='))
                    .Select(lrParts => new ReplacementPair {
                Key = lrParts[0],
                Value = lrParts.Length == 2 ? lrParts[1] : string.Empty
            }).ToList();

            var nCount = pairs.Count;
            var result = new StringBuilder();
            var i = 0;
            // замена
            foreach (var pair in pairs)
            {
                switch (pair.Key.ToLower())
                {
                    case "t":
                    case "title":
                        pair.Key = "2.5.4.12";
                        break;

                    case "g":
                    case "givenname":
                        pair.Key = "2.5.4.42";
                        break;
                    case "e":
                        pair.Key = "1.2.840.113549.1.9.1";
                        break;
                    case "sn":
                    case "surname":
                        pair.Key = "2.5.4.4";
                        break;

                    case "ou":
                    case "orgunit":
                        pair.Key = "2.5.4.11";
                        break;

                    case "unstructured-name":
                    case "unstructuredname":
                        pair.Key = "1.2.840.113549.1.9.2";
                        break;
                }

                result.Append($"{pair.Key}={pair.Value}{(i != (nCount - 1) ? ", " : string.Empty)}");
                i++;
            } 
            return result.ToString().Replace("^_^", "\\,");
        }
    }

    internal class ReplacementPair {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
