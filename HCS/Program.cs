using HCS.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Service.Async.HouseManagement.v11_2_0_6;
using HCS.BaseTypes;
using System.Security.Cryptography.X509Certificates;
using HCS.Helpers;

namespace HCS {
    class Program {
        static void Main(string[] args) {

            var cert = GetCert();
            if (cert == null) return;

            Console.WriteLine("Укажите pin ЭЦП");
            string pin = Console.ReadLine();

            ServicePointConfig.InitConfig(cert.Item2, pin, cert.Item1);
            
                
            var config = new ClientConfig {
                UseTunnel = true,
                CertificateThumbprint = cert.Item2,
                OrgPPAGUID = Guid.NewGuid().ToString(),
                OrgEntityGUID = Guid.NewGuid().ToString()

            };

            var request = new importAccountDataRequest {
                RequestHeader = new RequestHeader {
                    ItemElementName = ItemChoiceType.orgPPAGUID,
                    Item = config.OrgPPAGUID,
                    Date = DateTime.Now,
                    MessageGUID = Guid.NewGuid().ToString().ToLower(),
                },
                importAccountRequest = new importAccountRequest {
                    Id = Globals.Constants.SignElementId,
                    Account = new importAccountRequestAccount[] {
                        new importAccountRequestAccount {
                                AccountNumber = "4546464646454",
                                TransportGUID = Guid.NewGuid().ToString(),
                                Accommodation = new AccountTypeAccommodation[] {
                                    new AccountTypeAccommodation {
                                        ItemElementName = ItemChoiceType9.PremisesGUID,
                                        Item = Guid.NewGuid().ToString()
                                    }
                                },
                                PayerInfo = new AccountTypePayerInfo { },
                                CreationDateSpecified = false,
                                LivingPersonsNumberSpecified = false,
                                ResidentialSquareSpecified = false,
                                TotalSquareSpecified = false,
                                HeatedAreaSpecified = false,
                                ItemElementName = ItemChoiceType8.isUOAccount,
                                Item = true
                        }
                    }
                }
            };

            var provider = new HouseManagmentProvider<importAccountDataRequest, AckRequestAck>(config);

            try
            {
                var responce = provider.Send(request);
                Console.WriteLine($"ОК");
            }catch(Exception ex)
            {
                Console.WriteLine("При отправке сообщения произошла ошибка:"+ex.GetBaseException().Message);
            }
            Console.ReadKey();
        }


        static Tuple<int,string> GetCert() {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var collection = store.Certificates
                    .OfType<X509Certificate2>()
                    .Where(x => x.HasPrivateKey && x.IsGostPrivateKey());
                var cert = X509Certificate2UI.SelectFromCollection(new X509Certificate2Collection(collection.ToArray()), "Выбор сертификата", "", X509SelectionFlag.SingleSelection)[0];
                Console.WriteLine($"Криптопровайдер:{cert.GetProviderType().Item2}");
                return new Tuple<int, string>(cert.GetProviderType().Item1,cert.Thumbprint);
            }
            catch
            {
                return null;
            }
            finally
            {
                store.Close();
            }
        }
    }

}
