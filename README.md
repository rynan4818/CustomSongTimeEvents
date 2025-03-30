# CustomSongTimeEvents

このBeatSaberプラグインは、Custom Sabers(Saber Factory), Custom Avatars, Custom Platformsの3Dモデルデータに、譜面の曲時間に合わせたイベントを追加します。

Custom SabersやCustom Platformsなら曲に合わせたエフェクトを出したり、Custom Avatarsなら曲に合わせて表情制御したりできます。

Custom Sabers, Custom Avatars, Custom Platformsは同時使用も可能です。

ChroMapper上でCustom Platforms, Custom Avatarsをプレビューするプラグインもあります。

※Custom AvatarsをChroMapperで表示するには、[ChroMapper-CameraMovement](https://github.com/rynan4818/ChroMapper-CameraMovement)が必要です。

## サンプル動画

https://user-images.githubusercontent.com/14249877/211197517-08ff2a55-f505-4f6a-9dd4-6fd5cd8c9cdf.mp4

下のサンプルスクリプトを実行した動画です。

* 床にあるCustom Sabersが2.5秒で左セイバーが巨大化、12.5秒で右セイバーが巨大化
* Custom Avatarsで5.5～7.5秒と15～18秒に表情制御で笑顔に
* Custom Platformsで10秒と20秒に前方のノーツ数カウンター表示が回転

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

## ChroMapper用プレビュー

1.  [リリースページ](https://github.com/rynan4818/CustomSongTimeEvents/releases)から最新のCustomSongTimeEvents ChroMapper用のリリースをダウンロードします。

    (ChroMapper-CustomSongTimeEvents-*.*.*.zip のCustomSongTimeEvents.dllを入れて下さい。 間違えると動かないので注意して下さい)

2. ダウンロードしたzipファイルを解凍してChroMapperのインストールフォルダにある`Plugins`フォルダに`CustomSongTimeEvents.dll`をコピーします。

# 使い方 (Unity イベント編)
`Custom Song Time Event`には以下の設定項目があります。

![image](https://user-images.githubusercontent.com/14249877/211197738-b24203e5-4173-498c-bb22-4aa71e0f8bd9.png)

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
- **TimeScript** : 曲時間ごとにイベントのリストを配列にします。
    - **SongTime** : 文字列 : イベント発生したい曲時間(秒)です。
        - 現状は00:50 のような表記に対応していませんので1:10.5の場合は70.5と指定して下さい。
    - **Evnet** : 文字列 : 3Dモデルに設定した`Custom Song Time Event`の`Event Name`を指定します。
        - 同じ名前のイベント名はすべて実行されます。(Custom Sabers, Custom Avatars, Custom Platformsに該当するものがあれば全て)

## ChroMapper

CustomSongTimeEventsを適用したCustom Platforms, Custom AvatarsをChroMapperで読み込んだ時に、譜面フォルダにある`CustomSongTimeEvents.json`を読み込んでイベントをプレビューします。

Custom Platformsは譜面のSong InfoのCUSTOM PLATFORMで設定します。

Custom Avatarsは[ChroMapper-CameraMovement](https://github.com/rynan4818/ChroMapper-CameraMovement)で読み込みます。

譜面エディタと言う特性上、逆方向にプレビューが可能なためモデルのAnimator Controllerの作りによっては正しく動作しない可能性があります。

再生時間が戻った場合はスクリプトを0から実行するため、再生時間まで高速にイベントが発生します。また、一時停止した場合も、ChroMapperが一番近いグリッドにスナップするため、逆方向に戻る場合があります。

Animator Controllerの設定をSong Time Enableイベントでリセットするような作りにすると上手くいくと思います。

スクリプト`CustomSongTimeEvents.json`ファイルが更新されると、自動で再読み込みします。

## Custom Platforms を使った事例

1. まず、ProjectのAssetsに`Animator Controller`を作成して、イベントを与えたいオブジェクトのInspectorウィンドウに追加します。

    ![image](https://user-images.githubusercontent.com/14249877/211197772-c30f10e9-8e46-436f-8f67-b0a72866d9c7.png)

2. 作成した`Animator Controller`にCreate Stateで空のStateを2つ追加します。

    ![image](https://user-images.githubusercontent.com/14249877/211197809-66f2c399-1a46-47a4-9fd1-53050130d65b.png)

3. ProjectのAssetsに`Animation`を作成して、Animatorの2つ目に追加した灰色のStateのMotionに適用します。

    ![image](https://user-images.githubusercontent.com/14249877/211197833-ae8fdf8d-d802-4e4f-a2cb-ee9d98df08fd.png)

4. 追加したAnimationに変化させたい設定をします。(この事例ではRotation yを1回転します)

    ![image](https://user-images.githubusercontent.com/14249877/211197849-f2cb4917-3e82-4859-b4d2-7c8f9f5319b7.png)

5. Animatorの`Parameters`に`Trigger`を追加して名前をつけます。(この場合は、CounterRotation)

    ![image](https://user-images.githubusercontent.com/14249877/211197890-28bd3b05-bd00-48a1-9811-ffed33f63a5c.png)

6. AnimatorのNew State(オレンジ色)からNew State0(灰色)にTransitionを追加します。
7. 6で追加したTransitionを選択して、`Has Exit Time`のチェックを外して、`Transition Durations (s)`を0にして、`Conditions`に5で追加したTriggerの名前(この場合は、CounterRotation)を選択します。

    ![image](https://user-images.githubusercontent.com/14249877/211197920-91dbebf2-7767-4ade-8fa5-4fc1fbb67611.png)

8. AnimatorのNew State0(灰色)からNew State(オレンジ色)にTransitionを追加します。
9. 8で追加したTransitionを選択して、`Exit Time`を1にして、`Transition Durations (s)`を0にします。

    ![image](https://user-images.githubusercontent.com/14249877/211197954-037cf1e3-c53c-4222-a81a-20061ce63e82.png)

10. プラットフォームモデルの親オブジェクト(Custom Platform を設定してあるオブジェクト)にInspectorウィンドウのAdd Componentで`Custom Song Time Event`を追加します。
11. `Custom Song Time Event`の`Evnet Name`と`Event`の`Size`を1にします。
12. Event Nameの`Element 0`にJSONスクリプトから呼ぶイベント名(この場合は、CounterRotation)を設定します。
13. Eventの`Element 0 ()`の＋を押して、`Animator Controller`を割り当てたオブジェクト(1で設定したオブジェクト)をHierarchyから割り当てます。

    ![image](https://user-images.githubusercontent.com/14249877/211198044-bdc02c3e-4ed4-4bec-9819-43e03c1b5e25.png)

14. `No Function`を`Animator.SetTrigger(string)`にして、5で設定したTriggerの名前を設定します。(この場合は、CounterRotation)

    ![image](https://user-images.githubusercontent.com/14249877/211198080-5f524d7c-a233-406e-b9c6-c1a4e8be4c9f.png)
    ![image](https://user-images.githubusercontent.com/14249877/211198094-fdf5bb1f-31f7-44d9-ba45-3dbbc9839294.png)

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

## Song Time Enable/Disable を使った事例

例えばCustom Avatarsでコンボ数等に応じた表情変化をさせている時に、曲時間制御がある譜面のときはSong Time Enableのイベントで別に遷移させて、コンボ数等のイベントを受け付けない様にできます。

1. `SongTimeEnable`のトリガーで、通常待機状態の`SongTimeDisableBlink`(瞬きアニメーション)から、曲制御用のステート`SongTimeEnableBlink`に遷移させます。
2. 曲制御のイベントは`Any State`から都度表情ごとのステートに各トリガーで遷移させます
3. 曲制御有り譜面が終わって、曲制御無し譜面になったら、`SongTimeDisable`トリガーで`Any State`から`SongTimeDisableBlink`に遷移させます。

    ![image](https://user-images.githubusercontent.com/14249877/211198226-4d3e7856-11be-485e-810b-ca178f9054b8.png)

# 使い方 (任意オブジェクト操作編)

BeatSaber本体用のCustomSongTimeEventsには、ゲーム内の任意オブジェクトのActive状態と表示レイヤーを変更する機能があります。

ゲーム内に存在するオブジェクト（アバター、セイバー、プラットフォームなど）を指定したタイミングで変更できます。

次の例はNalulunaSaberを使って、左右のセイバー表示を切り替えます（最初の5秒間は左右セイバー表示OFF, 5秒後に右セイバー表示, 10秒後に右セイバーを非表示して、左セイバーを表示）

複数のモデルとトレイルを合成したセイバーを用意すれば、個別にセイバーを切り替えることができます。

NalulunaSaberはトレイルのマテリアルをゲームオブジェクトとして`/NalulunaTrailRenderer`として作成してくれます。ただし名前が全て`NalulunaTrailRenderer`のため区別がつかないため、リネームして、このオブジェクトのActive状態を変更することで、任意のトレイルもON/OFFすることができます。

    {
      "ObjectList" :[
        {
          "Name" : "LeftSaber",
          "Path" : "/Wrapper/StandardGameplay/LocalPlayerGameCore/Origin/VRGameCore/LeftHand/LeftSaber/NalulunaSaber",
          "Active" : false
        },
        {
          "Name" : "RightSaber",
          "Path" : "/Wrapper/StandardGameplay/LocalPlayerGameCore/Origin/VRGameCore/RightHand/RightSaber/NalulunaSaber",
          "Active" : false
        },
        {
          "Name" : "LeftTrail1",
          "Path" : "/NalulunaTrailRenderer",
          "Active" : false,
          "Rename" : "TraiRenderer1"
        },
        {
          "Name" : "LeftTrail2",
          "Path" : "/NalulunaTrailRenderer",
          "Active" : false,
          "Rename" : "TraiRenderer2"
        },
        {
          "Name" : "RightTrail1",
          "Path" : "/NalulunaTrailRenderer",
          "Active" : false,
          "Rename" : "TraiRenderer3"
        },
        {
          "Name" : "RightTrail2",
          "Path" : "/NalulunaTrailRenderer",
          "Active" : false,
          "Rename" : "TraiRenderer4"
        }
      ],
      "TimeScript" :[
        {
          "SongTime" : "5",
          "Object" : "RightSaber",
          "ObjectActive" : true
        },
        {
          "SongTime" : "5",
          "Object" : "RightTrail1",
          "ObjectActive" : true
        },
        {
          "SongTime" : "5",
          "Object" : "RightTrail2",
          "ObjectActive" : true
        },
        {
          "SongTime" : "10",
          "Object" : "RightSaber",
          "ObjectActive" : false
        },
        {
          "SongTime" : "10",
          "Object" : "RightTrail1",
          "ObjectActive" : false
        },
        {
          "SongTime" : "10",
          "Object" : "RightTrail2",
          "ObjectActive" : false
        },
        {
          "SongTime" : "10",
          "Object" : "LeftSaber",
          "ObjectActive" : true
        },
        {
          "SongTime" : "10",
          "Object" : "LeftTrail1",
          "ObjectActive" : true
        },
        {
          "SongTime" : "10",
          "Object" : "LeftTrail2",
          "ObjectActive" : true
        }
      ]
    }

### JSONファイルの説明
- **ObjectList** : 操作したいオブジェクト
    - **Name** : 文字列 : 検索したオブジェクトに付ける名前で、TimeScriptの方でObjectで指定します。
        - Nameは他のNameと重複しないようにしてください。
    - **Path** : 文字列 : 検索するActiveなオブジェクトのパス名です。
        - オブジェクト名だけでも検索できますが、できるだけフルパスで記載してください。[GameObject.Find](https://www.google.com/search?q=GameObject.Find)で検索します。Active状態のオブジェクトしか検索できないので注意してください。
    - **Active** : bool値 : 見つかったオブジェクトの初期Active状態を設定します。
        - true か false で設定してください。[GameObject.SetActive](https://www.google.com/search?q=GameObject.SetActive)で設定します。Active状態は親オブジェクトを非Activeにすれば子も非Activeになります。
    - **Layer** : 数値 : 見つかったオブジェクトの初期レイヤー値を設定します。
        - 文字列("0"～"32")ではなく、整数(0～31)で設定してください。[GameObject.layer](https://www.google.com/search?q=GameObject.layer)の値を変更します。レイヤー番号は[私が以前調べた](https://github.com/rynan4818/StagePositionViewer/blob/d30c61de57cf3238fc1f92ab18ebbd8834d76b2e/StagePositionViewer/Views/StagePositionUI.cs#L321-L352)ものなどを参考にしてください。レイヤーは子オブジェクトも一つずつ変更する必要があります。
    - **Rename** : 文字列 : 見つかったオブジェクトの名前をリネームします。
        - GameObject.Findは最初に見つかったActiveなオブジェクトを１つだけ見つけられます。同じフルパス名のオブジェクトの２つ目以降を見つけられないため、見つかったオブジェクトの名前を変更して、同じPathでもう一度検索することで２つ目以降も見つけることができます。主にmodで動的にオブジェクトを作ってる物を対象にしています。もし他でオブジェクト名を手がかりにしている場合、変更することで問題が発生することもありえますが、通常は問題にならない場合が殆どだと思います。
- **TimeScript** : 文字列 : 曲時間ごとにイベントのリストを配列にします。ちなみに、Evnetも同時に指定可能です。
    - **SongTime** : イベント発生したい曲時間(秒)です。
        - 現状は00:50 のような表記に対応していませんので1:10.5の場合は70.5と指定して下さい。
    - **Object** : 文字列 : ObjectListのNameで設定した操作対象のオブジェクトを指定します。
    - **ObjectActive** : bool値 : 指定したオブジェクトのActive状態を設定します。
        - true か false で設定してください。[GameObject.SetActive](https://www.google.com/search?q=GameObject.SetActive)で設定します。Active状態は親オブジェクトを非Activeにすれば子も非Activeになります。
    - **ObjectLayer** : 数値 : 見つかったオブジェクトの初期レイヤー値を設定します。
        - 文字列("0"～"32")ではなく、整数(0～31)で設定してください。[GameObject.layer](https://www.google.com/search?q=GameObject.layer)の値を変更します。レイヤー番号は[私が以前調べた](https://github.com/rynan4818/StagePositionViewer/blob/d30c61de57cf3238fc1f92ab18ebbd8834d76b2e/StagePositionViewer/Views/StagePositionUI.cs#L321-L352)ものなどを参考にしてください。レイヤーは子オブジェクトも一つずつ変更する必要があります。

ObjectListのPathは、譜面プレイ開始時にActiveなオブジェクトを全検索します。見つからないオブジェクトがある場合は、デフォルトでは開始3フレーム目まで調査を続けます。もし、対象のmodの挙動によりもっと調査が必要な場合は、設定ファイル(UserData/CustomSongTimeEvents.json)の`endCheckFrame`の数値を1つづつ増やしてみてください。ただし、オブジェクトの全検索は重いので、あまり大きな数値を設定するとプレイに影響する可能性があります。

## オブジェクトの探し方

ObjectListのPathで指定するパス名は、BeatSaberゲームプレイ中のシーンのHierarchyの状態でのパス名になります。つまり、実際にBeatSaberが起動している状態で調べる必要があります。

調べ方はBeatSaberのmod(BSIPA)に対応した[Runtime Unity Editor](https://github.com/Caeden117/RuntimeUnityEditor)を使う必要があります。DLLはBSIPAのDiscordサーバのpc-mod-devチャンネルの[2021/12/15のCaeden117さんの投稿](https://discord.com/channels/441805394323439646/443146108420620318/920513085180104795)にあります。リンクが飛ばない場合は、`Runtime Unity Editor BSIPA 4`で検索してみて下さい。`RuntimeUnityEditor_BSIPA4.zip`のファイルがそうです。

使い方は、BeatSaberを通常状態(fpfcモードは挙動が異なるのでパスが変わる場合があり、調査時は避けてください)で起動して、NF状態で適当な譜面をプレイして`G`キーを押すと、現在のゲームオブジェクトのHierarchyが表示されます。

変更したいオブジェクト名を検索して、見つかったオブジェクトのActive状態を変更して、どれが対象か１つづず調べて行きます。

見つかったオブジェクトのフルパス名をコピーして、先頭に`/`を追加してObjectListのPathで指定してください。

![image](https://github.com/user-attachments/assets/06e94377-5ef2-474a-b80f-25c1f917508e)

# 設定ファイル(UserData/CustomSongTimeEvents.json)について

modの設定ファイルは`Beat Saber/UserData/CustomSongTimeEvents.json`にあり、挙動を変更することができます。

| 設定名 | デフォルト値 | 内容 |
| ------ | ------------ | ---- |
| songTimeScriptPath | `UserData\CustomSongTimeEvents\DefaultSongTimeEvents.json` | 曲専用スクリプトが無い場合にデフォルトで動作するスクリプト |
| songSpecificScript | true | 曲専用SongTimeEventsスクリプトの有効・無効 |
| songStartTime | 0 | SongTimeEnable/Disableイベント発生曲時間 |
| startCheckFrame | 0 | オブジェクト調査チェックをする開始フレーム遅延数 |
| endCheckFrame | 3 | 曲スタート後にオブジェクト調査チェック終了するフレーム数 |
