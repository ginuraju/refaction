using System;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;

namespace Data.Infrastructure.Logging
{
    [EventSource(Name = "Database")]
    public class DatabaseEventSource : EventSource
    {
        public DatabaseEventSource()
        {
            EventSourceAnalyzer.InspectAll(this);
        }

        public static DatabaseEventSource Log { get; } = new DatabaseEventSource();

        [NonEvent]
        internal void RetrieveProductsCompleted(TimeSpan elapsedTime)
        {
            RetrieveDeploymentCompleted(elapsedTime.ToString());
        }

        [Event(1000,
            Level = EventLevel.Informational,
            Keywords = Keywords.Information,
            Message = "{0}  Producst Retrieved")]
        private void RetrieveDeploymentCompleted(string eventTiming)
        {
            WriteEvent(1000, eventTiming);
        }

        [NonEvent]
        internal void RetrieveProductsFailed(TimeSpan elapsedTime, Exception ex)
        {
            RetrieveProductsFailed(elapsedTime.ToString(), ex.Message);
        }

        [Event(1001,
            Level = EventLevel.Error,
            Keywords = Keywords.Error,
            Message = "{0}  Producst Retrieval failed")]
        private void RetrieveProductsFailed(string eventTiming, string exception)
        {
            WriteEvent(1001, eventTiming, exception);
        }

        public static class Keywords
        {
            public const EventKeywords Information = (EventKeywords) 1;
            public const EventKeywords Exception = (EventKeywords) 2;
            public const EventKeywords Error = (EventKeywords) 4;
        }
    }
}