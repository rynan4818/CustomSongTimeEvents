using UnityEngine;
using CustomSongTimeEvents.Models;
using CustomSongTimeEvents.Configuration;
using System.IO;

namespace CustomSongTimeEvents.Component
{
    public class SongTimeController : MonoBehaviour
    {
        public AudioTimeSyncController atsc;
        public SongTimeData _data;
        public bool _songTimeEnable;
        public bool _startEventSend;
        public float _beforeSeconds;
        public string _scriptFile;
        public SongTimeScript _timeEvent;

        public void Start()
        {
            _scriptFile = Path.Combine(BeatSaberSongContainer.Instance.Song.Directory, Options.Instance.scriptFileName).Replace("/", "\\");
            atsc = FindObjectOfType<AudioTimeSyncController>();
            _data = new SongTimeData();
            _songTimeEnable = false;
            _startEventSend = false;
            _beforeSeconds = 0;
            _songTimeEnable = _data.LoadSongTimeData(_scriptFile);
            if (!_songTimeEnable)
                return;
            _data.ResetEventID();
        }

        public void Update()
        {
            if (_beforeSeconds == atsc.CurrentSeconds && _timeEvent == null)
                return;
            _songTimeEnable = _data.LoadSongTimeData(_scriptFile);
            if (_beforeSeconds > atsc.CurrentSeconds)
            {
                _startEventSend = false;
                _data.ResetEventID();
            }
            _beforeSeconds = atsc.CurrentSeconds;
            if (!_startEventSend)
            {
                if (atsc.CurrentSeconds > Options.Instance.songStartTime)
                {
                    SongTimeEventTrigger.SongStartEvent(_songTimeEnable);
                    _startEventSend = true;
                    Debug.Log($"Custom Song Time Event: {_songTimeEnable}");
                }
            }
            if (!_songTimeEnable)
                return;
            _timeEvent = _data.UpdateEvent(atsc.CurrentSeconds);
            if (_timeEvent == null)
                return;
            if (_timeEvent.Event != null)
            {
                SongTimeEventTrigger.SongTimeEvnet(_timeEvent.Event);
                Debug.Log($"{atsc.CurrentSeconds}Sec: {_timeEvent.Event}");
            }
        }
    }
}
