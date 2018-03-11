using HCS.Interfaces;

namespace HCS.BaseTypes
{
    public class Result<TValue> where TValue : class
    {
        public bool HasError { get; set; }
        public IFault Fault { get; set; }
        public TValue Value { get; set; }
    }
}
