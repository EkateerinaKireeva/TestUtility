
namespace GismeteoGrabber.Utilities.Primitives
{
    public class JobResult<T>
    {
        JobResult(bool isSuccessful, T content = default(T))
        {
            IsSuccessful = isSuccessful;
            Content = content;
        }

        public bool IsSuccessful { get; }

        public T Content { get; }

        public static JobResult<T> CreateSuccessful(T content) => new JobResult<T>(true, content);

        public static JobResult<T> CreateFailed() => new JobResult<T>(false);

    }

}
