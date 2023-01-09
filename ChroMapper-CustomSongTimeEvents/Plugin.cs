using UnityEngine;
using UnityEngine.SceneManagement;
using CustomSongTimeEvents.Component;

namespace CustomSongTimeEvents
{
    [Plugin("Custom Song Time Events")]
    public class Plugin
    {
        public static SongTimeController songTimeController;
        [Init]
        private void Init()
        {
            Debug.Log("CustomSongTimeEvents Plugin has loaded!");
            SceneManager.sceneLoaded += SceneLoaded;
        }
        [Exit]
        private void Exit()
        {
            Debug.Log("RCustomSongTimeEvents Application has closed!");
        }
        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex != 3) // Mapper scene
                return;
            if (songTimeController != null && songTimeController.isActiveAndEnabled)
                return;
            songTimeController = new GameObject("SongTimeController").AddComponent<SongTimeController>();
        }
    }
}
