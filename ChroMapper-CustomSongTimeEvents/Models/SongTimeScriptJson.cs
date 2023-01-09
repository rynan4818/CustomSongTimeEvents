using Newtonsoft.Json;

namespace CustomSongTimeEvents.Models
{
    [JsonObject("TimeScript")]
    public class JSONTimeScript
    {
        public string SongTime { get; set; }
        public string Event { get; set; }
    }

    public class SongTimeScriptJson
    {
        [JsonProperty("TimeScript")]
        public JSONTimeScript[] JsonTimeScript { get; set; }
    }
}
