using System;
using OpenQA.Selenium;

namespace Banquo.Exceptions
{
    
    internal class TimeoutException : Exception
    {
        static private string Fmt(string s, int ms) => $"Expected {s}, not found [timeout: {ms} mS].\n";
        public TimeoutException(string s, int ms, Exception e = null) : base(Fmt(s, ms), e)
        {
        }

        public TimeoutException(string s, Exception e = null) : base(s, e)
        {
        }

        public TimeoutException() : base()
        {
        }
    }
}