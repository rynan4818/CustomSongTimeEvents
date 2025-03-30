using System.IO;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomSongTimeEvents.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        /// <summary>
        /// デフォルトのSongTimeEventsスクリプトファイルパスです。
        /// </summary>
        public static readonly string DefaultScriptPath = Path.Combine(IPA.Utilities.UnityGame.UserDataPath, "CustomSongTimeEvents", "DefaultSongTimeEvents.json");

        // BSIPAが値の変更を検出し、自動的に設定を保存したい場合は、'virtual'でなければなりません。

        /// <summary>
        /// SongTimeEventsスクリプトのファイルパス
        /// </summary>
        public virtual string songTimeScriptPath { get; set; } = DefaultScriptPath;

        /// <summary>
        /// 曲専用SongTimeEventsスクリプトの有効・無効
        /// </summary>
        public virtual bool songSpecificScript { get; set; } = true;

        /// <summary>
        /// SongTimeEnable/Disableイベント発生曲時間
        /// </summary>
        public virtual float songStartTime { get; set; } = 0;

        /// <summary>
        /// オブジェクト調査チェックをする開始フレーム遅延数
        /// </summary>
        public virtual int startCheckFrame { get; set; } = 0;

        /// <summary>
        /// 曲スタート後にオブジェクト調査チェック終了するフレーム数
        /// </summary>
        public virtual int endCheckFrame { get; set; } = 3;

        /// <summary>
        /// これは、BSIPAが設定ファイルを読み込むたびに（ファイルの変更が検出されたときを含めて）呼び出されます。
        /// </summary>
        public virtual void OnReload()
        {
            // 設定ファイルを読み込んだ後の処理を行う。
        }

        /// <summary>
        /// これを呼び出すと、BSIPAに設定ファイルの更新を強制します。 これは、ファイルが変更されたことをBSIPAが検出した場合にも呼び出されます。
        /// </summary>
        public virtual void Changed()
        {
            // 設定が変更されたときに何かをします。
        }

        /// <summary>
        /// これを呼び出して、BSIPAに値を<paramref name ="other"/>からこの構成にコピーさせます。
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // このインスタンスのメンバーは他から移入されました
        }
    }
}
