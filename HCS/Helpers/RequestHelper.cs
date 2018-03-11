using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Globals;

namespace HCS.Helpers
{
    public static class RequestHelper
    {
        public static THeaderType Create<THeaderType>(string orgPPAGUID,OrganizationRoles role) where THeaderType : class
        {
            try {
                var tInstance = Activator.CreateInstance(typeof(THeaderType));

                var props = tInstance.GetType().GetProperties();

                foreach (var prop in props) {
                    switch (prop.Name) {
                        case "Item":
                            prop.SetValue(tInstance, orgPPAGUID);
                            break;
                        case "ItemElementName":
                            prop.SetValue(tInstance, Enum.Parse(prop.PropertyType, "orgPPAGUID"));
                            break;
                        case "MessageGUID":
                            prop.SetValue(tInstance, Guid.NewGuid().ToString());
                            break;
                        case "Date":
                            prop.SetValue(tInstance, DateTime.Now);
                            break;
                        case "IsOperatorSignatureSpecified":
                            if (role == OrganizationRoles.RC)
                                prop.SetValue(tInstance, true);
                            break;
                        case "IsOperatorSignature":
                            if (role == OrganizationRoles.RC)
                                prop.SetValue(tInstance, true);
                            break;
                    }
                }

                return tInstance as THeaderType;
            }
            catch (ArgumentNullException ex) {
                throw new ApplicationException($"При сборке заголовка запроса для ГИС произошла ошибка: {ex.Message}");
            }
            catch (SystemException exc) {
                throw new ApplicationException($"При сборке заголовка запроса для ГИС произошла не предвиденная ошибка {exc.GetBaseException().Message}");
            }
        }
    }
}
