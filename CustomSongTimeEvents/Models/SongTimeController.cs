using CustomSongTimeEvents.HarmonyPatches;
using CustomSongTimeEvents.Configuration;
using System;
using UnityEngine;
using Zenject;

namespace CustomSongTimeEvents.Models
{
    /// <summary>
    /// Monobehaviours（スクリプト）はGameObjectsに追加されます。
    /// Monobehaviourがゲームから受け取ることのできるメッセージの全リストは、https://docs.unity3d.com/ScriptReference/MonoBehaviour.html を参照してください。
    /// </summary>

    public class SongTimeController : MonoBehaviour
    {
        private IAudioTimeSource _audioTimeSource;
        public SongTimeData _data;
        public bool _songTimeEnable;
        public bool _startEventSend;

        [Inject]
        private void Constractor(IAudioTimeSource audioTimeSource, SongTimeData _data)
        {
            this._audioTimeSource = audioTimeSource;
            this._data = _data;
        }
        public void Awake()
        {
            _songTimeEnable = false;
            _startEventSend = false;
            if (PluginConfig.Instance.songSpecificScript && CustomPreviewBeatmapLevelPatch.customLevelPath != String.Empty)
                _songTimeEnable = _data.LoadSongTimeData(CustomPreviewBeatmapLevelPatch.customLevelPath);
            else
                _songTimeEnable = _data.LoadSongTimeData();
            if (!_songTimeEnable)
                return;
            _data.ResetEventID();
            Plugin.Log.Info("Custom Song Time Event OK");
        }

        public void Update()
        {
            if (!_startEventSend)
            {
                if (_audioTimeSource.songTime > PluginConfig.Instance.songStartTime)
                {
                    SongTimeEventTrigger.SongStartEvent(_songTimeEnable);
#if DEBUG
                    Plugin.Log.Info($"Custom Song Time Event: {_songTimeEnable}");
#endif
                    _startEventSend = true;
                }
            }
            var timeEvent = _data.UpdateEvent(_audioTimeSource.songTime);
            if (timeEvent == null)
                return;
            if (timeEvent.Event != null)
            {
                SongTimeEventTrigger.SongTimeEvnet(timeEvent.Event);
#if DEBUG
                Plugin.Log.Info($"{_audioTimeSource.songTime}: {timeEvent.Event}");
#endif
            }
        }
    }
}
