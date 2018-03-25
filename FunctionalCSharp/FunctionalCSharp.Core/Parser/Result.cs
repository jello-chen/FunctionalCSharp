namespace FunctionalCSharp.Core.Parser
{
    public class Result<TInput, TValue>
    {
        public TValue Value { get; }
        public TInput Rest { get; }

        public Result(TValue value, TInput rest)
        {
            Value = value;
            Rest = rest;
        }
    }

    public delegate Result<TInput, TValue> Parser<TInput, TValue>(TInput input);
}
