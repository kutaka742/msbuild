# msbuild
ASP.NET Webアプリケーションのブランクプロジェクトのビルド・単体テスト自動化のサンプルプロジェクト。

## 構成
```
├─ sample - …             APのブランクプロジェクト
├─ sample.Tests - …       テストのブランクプロジェクト
├─ msbuild-sample.sln     ソリューションファイル
└─ Jenkinsfile            Jenkinsパイプライン
```
## 前提
 - `Visual Studio 2015`がインストールされている
 - MSBuildが`C:\Program Files\MSBuild\14.0\Bin\MSBuild.exe`にインストールされている
 - MSTestが`C:\Program Files\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe`にインストールされている
 - 参照するライブラリが「C:\temp\NugetPackage\」配下に格納

## Jenkinsパイプライン概要
 - 「Multibranch Pipeline」ジョブを想定したパイプライン
 - 「develop～」ブランチの場合のみ単体テストを実行(それ以外はビルドのみ実行)
 - 「sample」配下のモジュールとビルドログを保存
 -  サンプルではメール通知の処理はコメントアウト

