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

# 使い方
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
- **TimeScript** : 曲時間ごとに送信するキーのリストを配列にします。
- **SongTime** : イベント発生したい曲時間(秒)です。
    - 現状は00:50 のような表記に対応していませんので1:10.5の場合は70.5と指定して下さい。
- **Evnet** : 3Dモデルに設定した`Custom Song Time Event`の`Event Name`を指定します。
    - 同じ名前のイベント名はすべて実行されます。(Custom Sabers, Custom Avatars, Custom Platformsに該当するものがあれば全て)

## ChroMapper

CustomSongTimeEventsを適用したCustom Platforms, Custom AvatarsをChroMapperで読み込んだ時に、譜面フォルダにある`CustomSongTimeEvents.json`を読み込んでイベントをプレビューします。

Custom Platformsは譜面のSong InfoのCUSTOM PLATFORMで設定します。

Custom Avatarsは[ChroMapper-CameraMovement](https://github.com/rynan4818/ChroMapper-CameraMovement)で読み込みます。

譜面エディタと言う特性上、逆方向にプレビューが可能なためモデルのAnimator Controllerの作りによっては正しく動作しない可能性があります。

再生時間が戻った場合はスクリプトを0から実行するため、再生時間まで高速にイベントが発生します。また、一時停止した場合も、ChroMapperが一番近いグリッドにスナップするため、逆方向に戻る場合があります。

Animator Controllerの設定をSong Time Enableイベントでリセットするような作りにすると上手くいくと思います。

スクリプト`CustomSongTimeEvents.json`ファイルが更新されると、自動で再読み込みします。

# Custom Platforms を使った事例

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

# Song Time Enable/Disable を使った事例

例えばCustom Avatarsでコンボ数等に応じた表情変化をさせている時に、曲時間制御がある譜面のときはSong Time Enableのイベントで別に遷移させて、コンボ数等のイベントを受け付けない様にできます。

1. `SongTimeEnable`のトリガーで、通常待機状態の`SongTimeDisableBlink`(瞬きアニメーション)から、曲制御用のステート`SongTimeEnableBlink`に遷移させます。
2. 曲制御のイベントは`Any State`から都度表情ごとのステートに各トリガーで遷移させます
3. 曲制御有り譜面が終わって、曲制御無し譜面になったら、`SongTimeDisable`トリガーで`Any State`から`SongTimeDisableBlink`に遷移させます。

    ![image](https://user-images.githubusercontent.com/14249877/211198226-4d3e7856-11be-485e-810b-ca178f9054b8.png)
