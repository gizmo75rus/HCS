namespace HCS.Service {
    public interface IGetStateResult {
        object[] Items { get; set; }
    }
    public interface IAckRequestAck {
        string MessageGUID { get; set; }
    }
}
