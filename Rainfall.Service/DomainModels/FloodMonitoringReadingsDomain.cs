using Newtonsoft.Json;

namespace Rainfall.Service.DomainModels
{
    public class FloodMonitoringReadingsDomain
    {
        [JsonProperty("@context")]
        public string context { get; set; }
        public Meta meta { get; set; }
        public List<Item> items { get; set; }
    }

    public class Item
    {
        [JsonProperty("@id")]
        public string id { get; set; }
        public DateTime dateTime { get; set; }
        public string measure { get; set; }
        public double value { get; set; }
    }

    public class Meta
    {
        public string publisher { get; set; }
        public string licence { get; set; }
        public string documentation { get; set; }
        public string version { get; set; }
        public string comment { get; set; }
        public List<string> hasFormat { get; set; }
        public int limit { get; set; }
    }
}
