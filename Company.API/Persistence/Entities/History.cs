using System;

namespace Company.API.Persistence.Entities
{
    public class History
    {
        public int CompanyId { get; set; }
        public string Discriminator { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public EventType EventType { get; set; }
    }

    public class HistoryTypeOne : History
    {
        public string TypeOneData { get; set; }
    }

    public class HistoryTypeTwo : History
    {
        public string TypeTwoData { get; set; }
    }

    public enum EventType
    {
        CompanyRegistered,
        CompanyMadeInsolvent
    }
}
