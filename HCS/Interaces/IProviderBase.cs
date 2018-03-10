namespace HCS.Interaces
{
    public interface IProviderBase
    {
        IAck Send<T>(T request);

        bool GetResult(IAck ack, out IGetStateResult result);
    }
}
