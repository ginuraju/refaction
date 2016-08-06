using System;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;


namespace refactor_me.Infrastructure.Logging
{
    [EventSource(Name = "Api")]
    public class ApiEventSource : EventSource
    {
        public static class Keywords
        {
            public const EventKeywords Exception = (EventKeywords)1;
            public const EventKeywords Error = (EventKeywords)2;
        }

        private ApiEventSource()
        {
            EventSourceAnalyzer.InspectAll(this);
        }

        public static ApiEventSource Log { get; } = new ApiEventSource();

        [NonEvent]
        internal void UnexpectedWebRequestException(Uri endpoint, Exception ex)
        {
            UnexpectedWebRequestExceptionEvent(endpoint?.ToString(), ex?.ToString());
        }

        [Event(1, Level = EventLevel.Critical, Keywords = Keywords.Exception | Keywords.Error, Message = "An unexpected exception occured while processing a web request. Endpoint: {0}, Error: {1}")]
        private void UnexpectedWebRequestExceptionEvent(string endpoint, string exceptionDetail)
        {
            WriteEvent(1, endpoint, exceptionDetail);
        }
    }
}
