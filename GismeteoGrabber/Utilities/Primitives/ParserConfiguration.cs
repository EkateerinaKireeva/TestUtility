using System;
using System.Collections.Generic;
using System.Text;

namespace GismeteoGrabber.Utilities.Primitives
{
    public class ParserConfiguration
    {
        public string Url { get; }

        public ParserConfiguration(string url)
        {
            Url = url;
        }
    }
}
