using System.Collections.Generic;

namespace HCS.Globals {
    public static class Constants
    {
        internal const string SignElementId = "signed-data-container";
        public static class Address
        {
            public const string UriPPAK = "api.dom.gosuslugi.ru";
            public const string UriSIT = "217.107.108.147:10081"; //SIT1
            public const string UriSIT2 = "217.107.108.156:10081"; //SIT2
            public const string UriTunnel = "127.0.0.1:8080";
        }
        public static class EndPointLocator
        {
            static Dictionary<EndPoints, string> _endPoints;
            static EndPointLocator()
            {
                if (_endPoints == null)
                    _endPoints = new Dictionary<EndPoints, string>();

                _endPoints.Add(EndPoints.BillsAsync, "ext-bus-bills-service/services/BillsAsync");
                _endPoints.Add(EndPoints.DeviceMetering, "ext-bus-device-metering-service/services/DeviceMetering");
                _endPoints.Add(EndPoints.DeviceMeteringAsync, "ext-bus-device-metering-service/services/DeviceMeteringAsync");
                _endPoints.Add(EndPoints.HouseManagement, "ext-bus-home-management-service/services/HomeManagement");
                _endPoints.Add(EndPoints.HouseManagementAsync, "ext-bus-home-management-service/services/HomeManagementAsync");
                _endPoints.Add(EndPoints.Licenses, "ext-bus-licenses-service/services/Licenses");
                _endPoints.Add(EndPoints.Nsi, "ext-bus-nsi-service/services/Nsi");
                _endPoints.Add(EndPoints.NsiAsync, "ext-bus-nsi-service/services/NsiAsync");
                _endPoints.Add(EndPoints.NsiCommon, "ext-bus-nsi-common-service/services/NsiCommon");
                _endPoints.Add(EndPoints.NsiCommonAsync, "ext-bus-nsi-common-service/services/NsiCommonAsync");
                _endPoints.Add(EndPoints.OrgRegistryCommon, "ext-bus-org-registry-common-service/services/OrgRegistryCommon");
                _endPoints.Add(EndPoints.OrgRegistryCommonAsync, "ext-bus-org-registry-common-service/services/OrgRegistryCommonAsync");
                _endPoints.Add(EndPoints.OrgRegistry, "ext-bus-org-registry-service/services/OrgRegistry");
                _endPoints.Add(EndPoints.OrgRegistryAsync, "ext-bus-org-registry-service/services/OrgRegistryAsync");
                _endPoints.Add(EndPoints.PaymentsAsync, "ext-bus-payment-service/services/PaymentAsync");
            }

            public static string GetPath(EndPoints endPoint)
            {
                return _endPoints[endPoint];
            }
        }
        public static class UserAuth
        {
            public const string Name = "sit";
            public const string Passwd = "xw{p&&Ee3b9r8?amJv*]";
        }
    }

    /// <summary>
    /// Имена конечных точек
    /// </summary>
    public enum EndPoints
    {
        OrgRegistry,
        OrgRegistryAsync,
        OrgRegistryCommon,
        OrgRegistryCommonAsync,
        NsiCommon,
        NsiCommonAsync,
        Nsi,
        NsiAsync,
        HouseManagement,
        HouseManagementAsync,
        Bills,
        BillsAsync,
        Licenses,
        LicensesAsync,
        DeviceMetering,
        DeviceMeteringAsync,
        PaymentsAsync
    }


    /// <summary>
    /// Статусы сообщения
    /// </summary>
    public enum MessageStatuses
    {
        Ready,
        SendOk,
        SendError,
        SendCriticalError,
        SendTimeout,
        GetResultOk,
        GetResultError,
        GetResultCriticalError,
        GetResultTimeout,
        EndLive,
    }

    /// <summary>
    /// Роли организаций в ГИС
    /// </summary>
    public enum OrganizationRoles
    {
        /// <summary>
        /// УК/ТСЖ/ЖСК
        /// </summary>
        UK,

        /// <summary>
        /// Ресурсоснабжающая организация
        /// </summary>
        RSO,

        /// <summary>
        /// Расчетный центр
        /// </summary>
        RC,
    }
}
