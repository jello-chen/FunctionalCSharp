﻿namespace FunctionalCSharp.Core.Parser
{
    public class MiniMLParserFromString : MiniMLParsers<string>
    {
        public override Parser<string, char> AnyChar
        {
            get {
                return input => input.Length > 0 ? new Result<string, char>(input[0], input.Substring(1)) : null;
            }
        }
    }
}
