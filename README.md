# CustomSongTimeEvents

このBeatSaberプラグインは、Custom Sabers(Saber Factory), Custom Avatars, Custom Platformsの3Dモデルデータに、譜面の曲時間に合わせたイベントを追加します。

Custom SabersやCustom Platformsなら曲に合わせたエフェクトを出したり、Custom Avatarsなら曲に合わせて表情制御したりできます。

Custom Avatars, Custom Platformsなど同時使用も可能です。

# インストール方法

## BeatSaber本体側

1. [リリースページ](https://github.com/rynan4818/CustomSongTimeEvents/releases)から最新のCustomSongTimeEvents BeatSaber本体用のリリースをダウンロードします。

    (CustomSongTimeEvents-*.*.*-bs*.**.*-*******.zip の方を入れて下さい。 CustomSongTimeEvents-*.*.*-UnityEditor.zipのCustomSongTimeEvents.dllを入れると動かないので注意して下さい)

2. ダウンロードしたzipファイルを`Beat Saber`フォルダに解凍して、`Plugin`フォルダに`CustomSongTimeEvents.dll`ファイルをコピーします。

## Unity側

1. [リリースページ](https://github.com/rynan4818/CustomSongTimeEvents/releases)から最新のCustomSongTimeEvents UnityEditor用のリリースをダウンロードします。

    (CustomSongTimeEvents-*.*.*-UnityEditor.zip のCustomSongTimeEvents.dllを入れて下さい。 間違えると動かないので注意して下さい)

2. ダウンロードしたzipファイル解凍して、`CustomSongTimeEvents.dll`をイベントを追加したいCustom Sabers, Custom Avatars, Custom Platformsの3Dモデル用のUnityプロジェクトのAssets フォルダに配置します。

    ### Custom Sabers
    * `Hierarchy`で`Event Manager`のコンポーネントが設定してあるオブジェクト(TemplateSaberならLeftSaber, RightSaber)にInspectorウィンドウのAdd Componentで`Custom Song Time Event`を追加します。
    ### Custom Avatars
    * `Hierarchy`でアバターモデルの親オブジェクト(Avatar Descriptor や Event Manager を設定してあるオブジェクト)にInspectorウィンドウのAdd Componentで`Custom Song Time Event`を追加します。
    ### Custom Platforms
    * `Hierarchy`でプラットフォームモデルの親オブジェクト(Custom Platform を設定してあるオブジェクト)にInspectorウィンドウのAdd Componentで`Custom Song Time Event`を追加します。

# 使い方
`Custom Song Time Event`には以下の設定項目があります。

## Unity
### Song Time Enable()
譜面のスタート時(音楽開始時)にCustomSongTimeEventsが有効な譜面の時にイベント実行されます。
### Song Time Disable()
譜面のスタート時(音楽開始時)にCustomSongTimeEventsが無効な譜面の時にイベント実行されます。
### Event Name
曲時間で実行するイベント名です。必要な数をSizeで設定して、Element0～にスクリプトから呼ぶイベント名を設定して下さい。
### Event
Event Nameで設定したイベントが呼ばれた時に実行するイベントです。Event NameのElement0～に対応します。SizeはEvent Nameで設定した値と同じにして下さい。

## BeatSaber

譜面フォルダに`CustomSongTimeEvents.json`と言う名前で以下の様なスクリプトのJSONファイルを作成します。

    {
      "TimeScript" :[
        {
          "SongTime" : "2.5",
          "Event" : "SaberLeft"
        },
        {
          "SongTime" : "5.5",
          "Event" : "SongTimeSmile"
        },
        {
          "SongTime" : "7.5",
          "Event" : "SongTimeDefault"
        },
        {
          "SongTime" : "10",
          "Event" : "TestRotation"
        },
        {
          "SongTime" : "12.5",
          "Event" : "SaberRight"
        },
        {
          "SongTime" : "15",
          "Event" : "SongTimeSmile"
        },
        {
          "SongTime" : "18",
          "Event" : "SongTimeDefault"
        },
        {
          "SongTime" : "20",
          "Event" : "TestRotation"
        }
      ]
    }

### JSONファイルの説明
- **TimeScript** : 曲時間ごとに送信するキーのリストを配列にします。
- **SongTime** : キー送信したい曲時間(秒)です。
    - 現状は00:50 のような表記に対応していませんので1:10.5の場合は70.5と指定して下さい。
- **Evnet** : 3Dモデルに設定した`Custom Song Time Event`の`Event Name`を指定します。
    - 同じ名前のイベント名はすべて実行されます。(Custom Sabers, Custom Avatars, Custom Platformsに該当するものがあれば全て)

# Custom Platforms を使った事例

1. まず、ProjectのAssetsに`Animator Controller`を作成して、イベントを与えたいオブジェクトのInspectorウィンドウに追加します。
2. 作成した`Animator Controller`にCreate Stateで空のStateを2つ追加します。
3. ProjectのAssetsに`Animation`を作成して、Animatorの2つ目に追加した灰色のStateのMotionに適用します。
4. 追加したAnimationに変化させたい設定をします。(この事例ではRotation yを1回転します)
5. Animatorの`Parameters`に`Trigger`を追加して名前をつけます。(この場合は、CounterRotation)
6. AnimatorのNew State(オレンジ色)からNew State0(灰色)にTransitionを追加します。
7. 6で追加したTransitionを選択して、`Has Exit Time`のチェックを外して、`Transition Durations (s)`を0にして、`Conditions`に5で追加したTriggerの名前(この場合は、CounterRotation)を選択します。
8. AnimatorのNew State0(灰色)からNew State(オレンジ色)にTransitionを追加します。
9. 8で追加したTransitionを選択して、`Exit Time`を1にして、`Transition Durations (s)`を0にします。
10. プラットフォームモデルの親オブジェクト(Custom Platform を設定してあるオブジェクト)にInspectorウィンドウのAdd Componentで`Custom Song Time Event`を追加します。
11. `Custom Song Time Event`の`Evnet Name`と`Event`の`Size`を1にします。
12. Event Nameの`Element 0`にJSONスクリプトから呼ぶイベント名(この場合は、CounterRotation)を設定します。
13. Eventの`Element 0 ()`の＋を押して、`Animator Controller`を割り当てたオブジェクト(1で設定したオブジェクト)をHierarchyから割り当てます。
14. `No Function`を`Animator.SetTrigger(string)`にして、5で設定したTriggerの名前を設定します。(この場合は、CounterRotation)
15. 譜面フォルダに`CustomSongTimeEvents.json`と言う名前で以下の様なスクリプトのJSONファイルを作成します。

        {
          "TimeScript" :[
            {
              "SongTime" : "10",
              "Event" : "CounterRotation"
            },
            {
              "SongTime" : "15.5",
              "Event" : "CounterRotation"
            },
            {
              "SongTime" : "30",
              "Event" : "CounterRotation"
            }
          ]
        }

    この場合は、10秒と15.5秒と30秒でCounterRotationがトリガーがされて、`Animator Controller`がNew State(オレンジ色)からNew State0(灰色)に遷移します。

    Custom Sabers, Custom Avatarsもやり方は基本的に同じです。

# Song Time Enable/Disable を使った事例

例えばCustom Avatarsでコンボ数等に応じた表情変化をさせている時に、曲時間制御がある譜面のときはSong Time Enableのイベントで別に遷移させて、コンボ数等のイベントを受け付けない様にできます。

1. `SongTimeEnable`のトリガーで、通常待機状態の`SongTimeDisableBlink`(瞬きアニメーション)から、曲制御用のステート`SongTimeEnableBlink`に遷移させます。
2. 曲制御のイベントは`Any State`から都度表情ごとのステートに各トリガーで遷移させます
3. 曲制御有り譜面が終わって、曲制御無し譜面になったら、`SongTimeDisable`トリガーで`Any State`から`SongTimeDisableBlink`に遷移させます。
