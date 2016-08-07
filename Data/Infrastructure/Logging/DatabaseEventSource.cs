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
            RetrieveProductsCompleted(elapsedTime.ToString());
        }

        [Event(1000,
            Level = EventLevel.Informational,
            Keywords = Keywords.Information,
            Message = "{0}  Producst Retrieved")]
        private void RetrieveProductsCompleted(string eventTiming)
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

        [NonEvent]
        internal void RetrieveProductsByNameCompleted(TimeSpan elapsedTime)
        {
            RetrieveProductsByNameCompleted(elapsedTime.ToString());
        }

        [Event(1002,
            Level = EventLevel.Informational,
            Keywords = Keywords.Information,
            Message = "{0}  Producst Retrieved by name")]
        private void RetrieveProductsByNameCompleted(string eventTiming)
        {
            WriteEvent(1002, eventTiming);
        }

        [NonEvent]
        internal void RetrieveProductsByNameFailed(TimeSpan elapsedTime, Exception ex)
        {
            RetrieveProductsByNameFailed(elapsedTime.ToString(), ex.Message);
        }

        [Event(1003,
            Level = EventLevel.Error,
            Keywords = Keywords.Error,
            Message = "{0}  Producst Retrieval by name failed")]
        private void RetrieveProductsByNameFailed(string eventTiming, string exception)
        {
            WriteEvent(1003, eventTiming, exception);
        }

        [NonEvent]
        internal void RetrieveProductByProductIdCompleted(TimeSpan elapsedTime)
        {
            RetrieveProductByProductIdCompleted(elapsedTime.ToString());
        }

        [Event(1004,
            Level = EventLevel.Informational,
            Keywords = Keywords.Information,
            Message = "{0}  Product Retrieved by product Id")]
        private void RetrieveProductByProductIdCompleted(string eventTiming)
        {
            WriteEvent(1004, eventTiming);
        }

        [NonEvent]
        internal void RetrieveProductByProductIdFailed(TimeSpan elapsedTime, Exception ex)
        {
            RetrieveProductByProductIdFailed(elapsedTime.ToString(), ex.Message);
        }

        [Event(1005,
            Level = EventLevel.Error,
            Keywords = Keywords.Error,
            Message = "{0}  Product Retrieval by Product Id failed")]
        private void RetrieveProductByProductIdFailed(string eventTiming, string exception)
        {
            WriteEvent(1005, eventTiming, exception);
        }

        [NonEvent]
        internal void CreateProductCompleted(TimeSpan elapsedTime)
        {
            CreateProductCompleted(elapsedTime.ToString());
        }

        [Event(1006,
            Level = EventLevel.Informational,
            Keywords = Keywords.Information,
            Message = "{0}  Create Product succeeded")]
        private void CreateProductCompleted(string eventTiming)
        {
            WriteEvent(1006, eventTiming);
        }

        [NonEvent]
        internal void CreateProductFailed(TimeSpan elapsedTime, Exception ex)
        {
            CreateProductFailed(elapsedTime.ToString(), ex.Message);
        }

        [Event(1005,
            Level = EventLevel.Error,
            Keywords = Keywords.Error,
            Message = "{0} Create  Product failed")]
        private void CreateProductFailed(string eventTiming, string exception)
        {
            WriteEvent(1005, eventTiming, exception);
        }



        public static class Keywords
        {
            public const EventKeywords Information = (EventKeywords)1;
            public const EventKeywords Exception = (EventKeywords)2;
            public const EventKeywords Error = (EventKeywords)4;
        }
    }
}