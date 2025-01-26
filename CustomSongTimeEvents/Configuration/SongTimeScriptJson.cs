using Newtonsoft.Json;

namespace CustomSongTimeEvents.Configuration
{
    [JsonObject("TimeScript")]
    public class JSONTimeScript
    {
        public string SongTime { get; set; }
        public string Event { get; set; }
        public string Object { get; set; }
        public bool? ObjectActive { get; set; }
        public int? ObjectLayer { get; set; }
    }

    [JsonObject("ObjectList")]
    public class JSONObjectList
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool? Active { get; set; }
        public int? Layer { get; set; }
    }

    public class SongTimeScriptJson
    {
        [JsonProperty("ObjectList")]
        public JSONObjectList[] JsonObjectList { get; set; }
        [JsonProperty("TimeScript")]
        public JSONTimeScript[] JsonTimeScript { get; set; }
    }
}
