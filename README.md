# UdonMonitoring
UdonSharpの状態を監視するデバッグツール。  
VRChatのワールド作成時などにご利用ください。

![image](https://github.com/tp-jp/udon-monitoring/assets/130125691/81b4a72c-f87d-4d64-a934-f7e36484223c)

## 導入方法

VCCをインストール済みの場合、以下の**どちらか一つ**の手順を行うことでインポートできます。

- [VCC Listing](https://tp-jp.github.io/vpm-repos/) へアクセスし、「Add to VCC」をクリック

- VCCのウィンドウで `Setting - Packages - Add Repository` の順に開き、 `https://tp-jp.github.io/vpm-repos/index.json` を追加

[VPM CLI](https://vcc.docs.vrchat.com/vpm/cli/) を使用してインストールする場合、コマンドラインを開き以下のコマンドを入力してください。

```
vpm add repo https://tp-jp.github.io/vpm-repos/index.json
```

VCCから任意のプロジェクトを選択し、「Manage Project」から「Manage Packages」を開きます。
一覧の中から `UdonMonitoring` の右にある「＋」ボタンをクリックするか「Installed Vection」から任意のバージョンを選択することで、プロジェクトにインポートします。

リポジトリを使わずに導入したい場合は [releases](https://github.com/tp-jp/udon-monitoring/releases) から unitypackage をダウンロードして、プロジェクトにインポートしてください。

## 使い方

1. Packages/UdonMonitoring/Runtime/Prefab/UdonMonitoring.prefab を Hierarchy にドラッグ＆ドロップします。
2. Hierarchy上の `UdonMonitoring` を選択し、Inspector を表示します。
![image](https://github.com/tp-jp/udon-monitoring/assets/130125691/3897bae9-66e7-4124-8ae9-767d774ece88)

4. Inspector上で設定を行います。
   - TargetScripts  
     監視したい `UdonSharpBehaviour` を指定します。

## 設定方法

1. ツールバーから TpLab>UdonMonitoring を選択します。
2. 表示されたウィンドウの設定を行い、設定を変更できます。  
   ![image](https://github.com/tp-jp/udon-monitoring/assets/130125691/1a522e20-a8e0-4c1d-b25a-4b22d424d2fc)

   - Is Show Owner  
     有効にするとオーナー情報が表示します。
   - Is Convert Nicify Name  
     有効にすると変数名を変換します（例. _isOnLid => Is On Lid）
   - Null Color  
     NULLの色を指定します。     
   - True Color  
     TRUEの色を指定します。
   - False Color  
     FALSEの色を指定します。
   - X Color  
     Xの色を指定します。
   - Y Color  
     Yの色を指定します。
   - Z Color  
     Zの色を指定します。
   - W Color  
     Wの色を指定します。
   - Active Line Color  
     ActiveLineの色を指定します。
   - Inactive Line Color  
     InactiveLineの色を指定します。

## 更新履歴

[CHANGELOG](CHANGELOG.md)
