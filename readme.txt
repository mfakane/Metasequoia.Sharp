Metasequoia.Sharp

概要
====

3D モデリングソフト Metasequoia のプラグイン SDK を C# に移植したものです。
Metasequoia 名前空間には、元の SDK とほぼ一対一で対応したクラス群があります。
Metasequoia.Sharp 名前空間には、C# で使用するうえで便利なクラス群があります。

必要なもの
=========

* Microsoft Visual Studio 2010 以降
* .NET Framework 4.0 以降
* Metasequoia Ver2.4.9 以降
* Metasequoia Plugin SDK Rev2.49 以降
* ILMerge

事前準備
=======

1. Tools\PostBuild.bat を開き、ILMerge へのパス と DllExporter へのパス を自分の環境に合わせ修正してください。
2. Metasequoia Plugin SDK の mqsdk フォルダをソリューションディレクトリにコピーしてください。
3. Metasequoia.Sharp プロジェクトで [ソリューションエクスプローラー] > [すべてのテンプレートの変換]、もしくは [ビルド] > [すべての T4 テンプレートの変換] などで
   T4 テンプレートを実行してください。

使用方法
=======

開発
----

新しいクラスライブラリプロジェクトを作成した後、参照設定に Metasequoia.Sharp.dll または Metasequoia.Sharp プロジェクトを追加します。
その後、DllExport プロジェクトをビルドし、Tools\PostBuild.exe に配置されているかを確認します。

ビルド
-----

[プロジェクト プロパティ] の [ビルド イベント] にて、ビルド後のイベントに

cd $(TargetDir)
$(SolutionDir)Tools\PostBuild /$(ConfigurationName) $(TargetPath) /copyto:<METASEQUOIA_DIR>\Plugins\<PLUGIN_TYPE>

を指定し、[ビルド後イベントの実行] を [ビルドがプロジェクト出力を更新したとき] にします。
ちなみに、これはソリューションディレクトリに Tools\PostBuild.bat が存在する場合のものです。
また、<METASEQUOIA_DIR> は Metasequoia.exe の入っているパスに変えてください。
<PLUGIN_TYPE> は、作るプラグインの種類によって Create、Select 等に変えてください。

デバッグ
-------

[プロジェクト プロパティ] の [デバッグ] で [プログラムの開始] に Metaseq.exe を指定し、そのプロジェクトをスタートアッププロジェクトに設定すると、
そのままデバッグ実行ができます。ブレークポイントなども正常にヒットします。

注意
====

* 一度インストールされると、Metasequoia の [プラグインについて] ダイアログのインストールボタンを使用して dll を置き換えることができません。
  これは .NET の AppDomain および Assembly の仕様によるものです。ご了承ください。
* 大部分は未検証です。Station プラグイン周り、MQXmlElement 等は動くかどうかすらわかりません。
* 元 SDK のすべての機能をカバーしているわけではありません。
  (とくに MQ3DLib.h まわり)

更新履歴
=======

Version 0.0, Tue, 03 Apr 2012

	製作

連絡先
=====

mfakane <star@glasscore.net>

ライセンス
=========

本プログラムはフリーウェアです。完全に無保証で提供されるものであり
これを使用したことにより発生した、または発生させた、あるいは
発生させられたなどしたいかなる問題に関して製作者は一切の責任を負いません。
別途ライセンスが明記されている場所またはファイルを除き、使用者は本プログラムを
Do What The Fuck You Want To Public License, Version 2 (WTFPL) および自らの責任において
自由に複製、改変、再配布、などが可能です。WTFPL についての詳細は次の URL か、
以下の条文を参照してください。http://sam.zoy.org/wtfpl/

            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE 
                    Version 2, December 2004 

 Copyright (C) 2012 mfakane <star@glasscore.net>

 Everyone is permitted to copy and distribute verbatim or modified 
 copies of this license document, and changing it is allowed as long 
 as the name is changed. 

            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE 
   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION 

  0. You just DO WHAT THE FUCK YOU WANT TO.

おまけ
=====

アセンブリがネイティブへロードされる仕組み
------------------------------------

.NET の、逆 P/Invoke という仕組みの一種を使っています。
これで使っている逆 P/Invoke は、任意の静的メソッドに IL 上で .export ディレクティブを指定することで thunk が dllexport されネイティブコードから呼び出し可能になるものです。
これによりネイティブコードから呼び出しがあった場合はそのプロセスに CLR がロードされ、アセンブリが読み込まれます。

.export ディレクティブの付加は、あらかじめメソッドにカスタム属性で dllexport したいことを指定しておき、ビルド後の処理で ildasm に渡し IL コードを取り出してから
単純な文字列処理で .custom を .export へ置き換え、ilasm へ渡し再度アセンブリを生成することで実現しています。

Metasequoia へアクセスする仕組み
-----------------------------

GetModuleHandle でモジュールのハンドルを得た後、元の SDK がやっているのと同じように MQInit で GetProcAddress を使用し一つ一つ関数ポインタを取得しています。
関数ポインタに対応するデリゲートを作っておき、それと取得した関数ポインタを Marshal.GetDelegateForFunctionPointer に渡しデリゲートインスタンスとして関数を取得しています。