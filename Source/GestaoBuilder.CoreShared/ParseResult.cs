using GestaoBuilder.Shared.Contracts;

namespace GestaoBuilder.CoreShared
{
    public class ParseResult<T> : IParseResult<T>
    {
        public bool IsParseSucess { get; set; }
        public T Result { get; set; }
    }
}