using CustomSongTimeEvents.Installers;
using CustomSongTimeEvents.Configuration;
using HarmonyLib;
using SiraUtil.Zenject;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPALogger = IPA.Logging.Logger;
using System.Reflection;

namespace CustomSongTimeEvents
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        public const string HARMONY_ID = "com.github.rynan4818.CustomSongTimeEvents";
        private static Harmony _harmony;

        [Init]
        /// <summary>
        /// IPAによってプラグインが最初にロードされたときに呼び出されます。（ゲームが開始されたとき、またはプラグインが無効の状態で開始された場合は有効化されたときのいずれか）
        /// [Init]はコンストラクタのメソッド、InitWithConfig のような通常のメソッドの前に呼び出されます。
        /// [Init]は１つのコンストラクタのみを使用して下さい。
        /// </summary>
        public void Init(IPALogger logger, Config conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Info("CustomSongTimeEvents initialized.");
            PluginConfig.Instance = conf.Generated<PluginConfig>();
            Log.Debug("Config loaded");
            zenjector.Install<SongTimeAppInstaller>(Location.App);
            zenjector.Install<SongTimePlayerInstaller>(Location.Player);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            _harmony = new Harmony(HARMONY_ID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Debug("OnApplicationStart");
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            _harmony.UnpatchSelf();
            Log.Debug("OnApplicationQuit");
        }
    }
}
