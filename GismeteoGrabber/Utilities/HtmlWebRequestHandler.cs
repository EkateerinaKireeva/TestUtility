using GismeteoGrabber.Utilities.Interfaces;
using GismeteoGrabber.Utilities.Primitives;
using HtmlAgilityPack;
using System.Diagnostics;

namespace GismeteoGrabber.Utilities
{
    class HtmlWebRequestHandler : IRequestHandler
    {
        private readonly HtmlWeb _htmlWeb = new HtmlWeb();
        private readonly object _locker = new object();

        public JobResult<HtmlDocument> Load(string url)
        {
            HtmlDocument document = null;
            try
            {
                lock (_locker)
                {
                    Debug.WriteLine($"{nameof(HtmlWebRequestHandler)} start load data url:{url}");
                    document = _htmlWeb.Load(url);
                    Debug.WriteLine($"{nameof(HtmlWebRequestHandler)} end load data url:{url}");
                }
            }
            catch
            {
                Debug.WriteLine($"{nameof(HtmlWebRequestHandler)} catch exception");
            }

            if (document is null)
                return JobResult<HtmlDocument>.CreateFailed();

            return JobResult<HtmlDocument>.CreateSuccessful(document);
        }
    }
}
