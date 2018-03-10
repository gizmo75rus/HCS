namespace HCS.Globals {
    public static class Constants {
        internal const string SignElementId = "signed-data-container";
        public static class Address {
            public const string UriPPAK = "api.dom.gosuslugi.ru";
            public const string UriSIT = "217.107.108.147:10081"; //SIT1
            public const string UriSIT2 = "217.107.108.156:10081"; //SIT2
            public const string UriTunnel = "127.0.0.1:8080";
        }
        public static class EndPointPath {
            public const string OrgRegistryCommon = "ext-bus-org-registry-common-service/services/OrgRegistryCommon";
            public const string NsiCommon = "ext-bus-nsi-common-service/services/NsiCommon";
            public const string Nsi = "ext-bus-nsi-service/services/Nsi";
            public const string HouseManagement = "ext-bus-home-management-service/services/HomeManagement";
            public const string HouseManagementAsync = "ext-bus-home-management-service/services/HomeManagementAsync";
            public const string Bills = "ext-bus-bills-service/services/Bills";
            public const string BillsAsync = "ext-bus-bills-service/services/BillsAsync";
            public const string Licenses = "ext-bus-licenses-service/services/Licenses";
            public const string DeviceMetering = "/ext-bus-device-metering-service/services/DeviceMetering";
        }
        public static class UserAuth {
            public const string Name = "sit"; // lanit
            public const string Passwd = "rZ_GG72XS^Vf55ZW";// tv,n8!Ya
        }
    }

    /// <summary>
    /// Имена конечных точек
    /// </summary>
    public enum EndPointNames
    {
         OrgRegistryCommon,
         NsiCommon,
         Nsi,
         HouseManagement,
         HouseManagementAsync,
         Bills,
         BillsAsync,
         Licenses,
         DeviceMetering
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
}
