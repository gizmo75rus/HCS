namespace HCS.Interaces
{
    public interface IAck
    {
        string MessageGUID { get; set; }
        string RequesterMessageGUID { get; set; }

    }
}
