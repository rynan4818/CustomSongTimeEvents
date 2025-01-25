using CustomSongTimeEvents.HarmonyPatches;
using CustomSongTimeEvents.Configuration;
using System;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

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
        public bool _songStartCheck;
        public Dictionary<string, (GameObject, int)> _gameObjects = new Dictionary<string, (GameObject, int)>();

        [Inject]
        private void Constractor(IAudioTimeSource audioTimeSource, SongTimeData _data)
        {
            this._audioTimeSource = audioTimeSource;
            this._data = _data;
        }
        public void Awake()
        {
            this._songTimeEnable = false;
            this._startEventSend = false;
            this._songStartCheck = false;
            if (PluginConfig.Instance.songSpecificScript && CustomPreviewBeatmapLevelPatch.customLevelPath != String.Empty)
                this._songTimeEnable = this._data.LoadSongTimeData(CustomPreviewBeatmapLevelPatch.customLevelPath);
            else
                this._songTimeEnable = this._data.LoadSongTimeData();
            if (!this._songTimeEnable)
                return;
            this._data.ResetEventID();
            Plugin.Log.Info("Custom Song Time Event OK");
        }

        public void OnDestroy()
        {
            if (!this._songTimeEnable)
                return;
            foreach (var gameObj in this._gameObjects)
            {
                var obj = gameObj.Value.Item1;
                if (obj != null)
                {
                    obj.SetActive(true);
                    obj.layer = gameObj.Value.Item2;
                }
            }
            this._gameObjects.Clear();
        }

        public void Update()
        {
            if (!this._songTimeEnable)
                return;
            if (!this._audioTimeSource.isReady)
                return;
            if (!this._songStartCheck)
                this.SongStartCheck();
            if (!this._startEventSend)
            {
                if (this._audioTimeSource.songTime > PluginConfig.Instance.songStartTime)
                {
                    SongTimeEventTrigger.SongStartEvent(this._songTimeEnable);
#if DEBUG
                    Plugin.Log.Info($"Custom Song Time Event: {_songTimeEnable}");
#endif
                    this._startEventSend = true;
                }
            }
            var timeEvent = this._data.UpdateEvent(this._audioTimeSource.songTime);
            if (timeEvent == null)
                return;
            if (timeEvent.Event != null)
            {
                SongTimeEventTrigger.SongTimeEvnet(timeEvent.Event);
#if DEBUG
                Plugin.Log.Info($"{timeEvent.Event}");
#endif
            }
            if (timeEvent.Object != null)
            {
                if (this._gameObjects.TryGetValue(timeEvent.Object, out var obj))
                {
                    if (obj.Item1 != null)
                    {
                        if (timeEvent.ObjectActive != null)
                        {
                            obj.Item1.SetActive((bool)timeEvent.ObjectActive);
#if DEBUG
                            Plugin.Log.Info($"{timeEvent.Object} : Active {timeEvent.ObjectActive}");
#endif
                        }
                        if (timeEvent.ObjectLayer != null)
                        {
                            obj.Item1.layer = (int)timeEvent.ObjectLayer;
#if DEBUG
                            Plugin.Log.Info($"{timeEvent.Object} : Layer {timeEvent.ObjectLayer}");
#endif
                        }
                    }
                }
            }
        }

        public void SongStartCheck()
        {
            this._songStartCheck = true;
            foreach (var objectList in this._data._objectList)
            {
                var obj = GameObject.Find(objectList.Value);
                if (obj == null)
                    Plugin.Log.Error($"Not Found Game Object: {objectList.Key}");
                else
                {
                    if (!this._gameObjects.TryAdd(objectList.Key, (obj, obj.layer)))
                        Plugin.Log.Error($"GameObjects Name Duplicate Error: {objectList.Key}");
#if DEBUG
                    Plugin.Log.Info($"Found: {objectList.Key}");
#endif
                }
            }
        }
    }
}
