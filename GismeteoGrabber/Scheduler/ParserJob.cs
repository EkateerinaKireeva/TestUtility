using GismeteoGrabber.Utilities.Interfaces;
using Quartz;
using System;
using System.Threading.Tasks;


namespace GismeteoGrabber.Scheduler
{
    public class ParserJob : IJob
    {
        IParser _parser;
        bool _isRunning;

        public ParserJob(IParser parser)
        {
            _parser = parser;

            if (_parser is null)
                throw new NullReferenceException("parser can't be null");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (_isRunning)
                return;

            _isRunning = true;
            await _parser.ParseDataAsync();
            _isRunning = false;
        }
    }
}
