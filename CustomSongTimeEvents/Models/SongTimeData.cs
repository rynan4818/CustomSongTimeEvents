using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using CustomSongTimeEvents.Configuration;
using Zenject;

namespace CustomSongTimeEvents.Models
{
    public class SongTimeScript
    {
        public float SongTime;
        public string Event;
        public string Object;
        public bool? ObjectActive;
        public int? ObjectLayer;
    }
    public class SongTimeData : IInitializable
    {
        public List<SongTimeScript> _timeScript = new List<SongTimeScript>();
        public Dictionary<string, string> _objectList = new Dictionary<string, string>();
        public int eventID;

        public void Initialize()
        {
            if (!File.Exists(PluginConfig.Instance.songTimeScriptPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(PluginConfig.DefaultScriptPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(PluginConfig.DefaultScriptPath));
                }
                PluginConfig.Instance.songTimeScriptPath = PluginConfig.DefaultScriptPath;
            }
        }
        public bool LoadFromJson(string jsonString)
        {
            SongTimeScriptJson songTimeScriptJson = null;
            string sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string sepCheck = (sep == "." ? "," : ".");
            try
            {
                songTimeScriptJson = JsonConvert.DeserializeObject<SongTimeScriptJson>(jsonString);
            }
            catch (Exception ex)
            {
                Plugin.Log.Error($"JSON file syntax error. {ex.Message}");
            }
            if (songTimeScriptJson == null)
                return false;
            foreach (JSONObjectList jsonObjectList in songTimeScriptJson.JsonObjectList)
            {
                if (!this._objectList.TryAdd(jsonObjectList.Name, jsonObjectList.Path))
                    Plugin.Log.Error($"ObjectList Name Duplicate Error: {jsonObjectList.Name}, {jsonObjectList.Path}");
            }
            foreach (JSONTimeScript jsonTimeScript in songTimeScriptJson.JsonTimeScript)
            {
                var newScript = new SongTimeScript
                {
                    SongTime = float.Parse(jsonTimeScript.SongTime.Contains(sepCheck) ? jsonTimeScript.SongTime.Replace(sepCheck, sep) : jsonTimeScript.SongTime),
                    Event = jsonTimeScript.Event,
                    Object = jsonTimeScript.Object,
                    ObjectActive = jsonTimeScript.ObjectActive,
                    ObjectLayer = jsonTimeScript.ObjectLayer
                };
                this._timeScript.Add(newScript);
            }
            this._timeScript = this._timeScript.OrderBy(x => x.SongTime).ToList();
            return true;
        }
        public bool LoadSongTimeData(string path = null)
        {
            this._timeScript.Clear();
            this._objectList.Clear();
            if (path == null)
                path = PluginConfig.Instance.songTimeScriptPath;
            if (!File.Exists(path))
                return false;
            string jsonText = File.ReadAllText(path);
            if (!LoadFromJson(jsonText))
                return false;
            if (this._timeScript.Count == 0)
            {
                Plugin.Log.Notice("No Song TIme Event data!");
                return false;
            }
            Plugin.Log.Notice($"Found {this._timeScript.Count} entries in: {path}");
            return true;
        }
        public void ResetEventID()
        {
            eventID = 0;
        }
        public SongTimeScript UpdateEvent(float songtime)
        {
            if (eventID >= this._timeScript.Count)
                return null;
            if (this._timeScript[eventID].SongTime <= songtime)
            {
#if DEBUG
                Plugin.Log.Info($"EventID={eventID} : {songtime}sec");
#endif
                return this._timeScript[eventID++];
            }
            else
                return null;
        }
    }
}
