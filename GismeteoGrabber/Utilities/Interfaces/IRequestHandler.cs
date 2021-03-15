using GismeteoGrabber.Utilities.Primitives;
using HtmlAgilityPack;


namespace GismeteoGrabber.Utilities.Interfaces
{
    public interface IRequestHandler
    {
        public JobResult<HtmlDocument> Load(string url);
    }
}
