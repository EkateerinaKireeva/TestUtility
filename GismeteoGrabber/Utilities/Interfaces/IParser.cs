using GismeteoGrabber.Utilities.Primitives;
using System.Threading.Tasks;

namespace GismeteoGrabber.Utilities.Interfaces
{
    public interface IParser
    {
        ParserConfiguration Configuration { get; }

        Task ParseDataAsync();
    }
}
