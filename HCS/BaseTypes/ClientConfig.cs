namespace HCS.BaseTypes {
    public class ClientConfig {
        public string OrgPPAGUID { get; set; }
        public string OrgEntityGUID { get; set; }
        public string CertificateThumbprint { get; set; }
        public bool UseTunnel { get; set; }
        public bool IsPPAK { get; set; }
    }
}
