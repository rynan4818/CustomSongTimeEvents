using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace CustomSongTimeEvents.Models
{
    public class SongTimeScript
    {
        public float SongTime;
        public string Event;
    }
    public class SongTimeData
    {
        public string scriptPath = "";
        public List<SongTimeScript> _timeScript = new List<SongTimeScript>();
        public int eventID;
        public DateTime _scriptWriteTime = DateTime.MinValue;
        public bool _ScriptOK = false;

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
                Debug.LogError($"CustomSongTimeEvents JSON file Error:{ex.Message}");
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
            if (!File.Exists(path))
                return false;
            var scriptTime = File.GetLastWriteTime(path);
            if (scriptPath == path && _scriptWriteTime == scriptTime)
                return _ScriptOK;
            _scriptWriteTime = scriptTime;
            scriptPath = path;
            _ScriptOK = false;
            string jsonText = File.ReadAllText(path);
            if (!LoadFromJson(jsonText))
                return false;
            if (_timeScript.Count == 0)
            {
                Debug.Log("No Song TIme Event data!");
                return false;
            }
            Debug.Log($"Found {_timeScript.Count} entries in: {path}");
            _ScriptOK = true;
            eventID = 0;
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
                Debug.Log($"EventID:{eventID}");
#endif
                return _timeScript[eventID++];
            }
            else
                return null;
        }
    }
}
