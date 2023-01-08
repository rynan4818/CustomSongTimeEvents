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
    }
    public class SongTimeData : IInitializable
    {
        public string scriptPath = "";
        public List<SongTimeScript> _timeScript = new List<SongTimeScript>();
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
            _timeScript.Clear();
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
            foreach (JSONTimeScript jsonTimeScript in songTimeScriptJson.JsonTimeScript)
            {
                SongTimeScript newScript = new SongTimeScript();
                newScript.SongTime = float.Parse(jsonTimeScript.SongTime.Contains(sepCheck) ? jsonTimeScript.SongTime.Replace(sepCheck, sep) : jsonTimeScript.SongTime);
                newScript.Event = jsonTimeScript.Event;
                _timeScript.Add(newScript);
            }
            _timeScript = _timeScript.OrderBy(x => x.SongTime).ToList();
            return true;
        }
        public bool LoadSongTimeData(string path = null)
        {
            if (path == null)
                path = PluginConfig.Instance.songTimeScriptPath;
            if (!File.Exists(path))
                return false;
            if (scriptPath == path)
                return true;
            string jsonText = File.ReadAllText(path);
            if (!LoadFromJson(jsonText))
                return false;
            if (_timeScript.Count == 0)
            {
                Plugin.Log.Notice("No Song TIme Event data!");
                return false;
            }
            Plugin.Log.Notice($"Found {_timeScript.Count} entries in: {path}");
            scriptPath = path;
            return true;
        }
        public void ResetEventID()
        {
            eventID = 0;
        }
        public SongTimeScript UpdateEvent(float songtime)
        {
            if (eventID >= _timeScript.Count)
                return null;
            if (_timeScript[eventID].SongTime <= songtime)
            {
#if DEBUG
                Plugin.Log.Info($"EventID:{eventID}");
#endif
                return _timeScript[eventID++];
            }
            else
                return null;
        }
    }
}
