using System;
using UnityEngine;
using UnityEngine.Events;

namespace CustomSongTimeEvents
{
    public class CustomSongTimeEvent : MonoBehaviour
    {
        [Serializable]
        public class SongTimeEvent : UnityEvent<string> { }

        public UnityEvent SongTimeEnable;
        public UnityEvent SongTimeDisable;
        public string[] EventName;
        public UnityEvent[] Event;
        public UnityEvent<String> OnSongTIme = new SongTimeEvent();

        public void OnEnable()
        {
            SongTimeEventTrigger.OnSongTimeEventTrigger += OnSongTimeEventTrigger;
            SongTimeEventTrigger.OnSongStartEventTrigger += OnSongStart;
            this.OnSongTIme.AddListener(OnSongTimeEvent);
            foreach(string eventName in EventName)
                Plugin.Log.Info($"Event Enable: {eventName}");
        }
        public void OnDisable()
        {
            SongTimeEventTrigger.OnSongTimeEventTrigger -= OnSongTimeEventTrigger;
            SongTimeEventTrigger.OnSongStartEventTrigger -= OnSongStart;
            this.OnSongTIme.RemoveListener(OnSongTimeEvent);
        }
        public void OnSongStart(bool songTimeEnable)
        {
            if (songTimeEnable)
                SongTimeEnable?.Invoke();
            else
                SongTimeDisable?.Invoke();
        }
        public void OnSongTimeEvent(string eventName)
        {
            for (int i = 0; i < EventName.Length; i++)
            {
                if (eventName == EventName[i] && Event.Length > i)
                {
                    Event[i]?.Invoke();
#if DEBUG
                    Plugin.Log.Info($"{eventName}");
#endif
                }
            }
        }
        public void OnSongTimeEventTrigger(string eventName)
        {
            OnSongTIme?.Invoke(eventName);
        }
    }
    public class SongTimeEventTrigger
    {
        public static event Action<string> OnSongTimeEventTrigger;
        public static event Action<bool> OnSongStartEventTrigger;
        public static bool SongTimeEventEnable;
        public static void SongTimeEvnet(string eventName)
        {
            OnSongTimeEventTrigger?.Invoke(eventName);
        }
        public static void SongStartEvent(bool songTimeEnable)
        {
            OnSongStartEventTrigger?.Invoke(songTimeEnable);
        }
    }
}
