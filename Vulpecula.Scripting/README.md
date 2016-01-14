# Vulpecula Script Library
[![License](https://img.shields.io/github/license/fuyuno/vulpecula.svg?style=flat-square)](https://github.com/fuyuno/Vulpecula/blob/develop/LICENSE.txt)  
Vulpecula Script Library は、 Vulpecula で使用しているフィルターのスクリプト実行部分です。  
SQL のような感じで記述することができます。

スクリプトは、 以下の様なものとなっています。
```csharp
// ささやき本文に "ミッシー" という単語を含んでいるものを抽出
where s.text contains "ミッシー"

// シェア回数が 5 回より多いものを抽出
where s.spreads > 5

// 画像を含んでいるものを抽出
where s contains :image
```


### ライセンス
* [The MIT License (MIT)](https://github.com/fuyuno/Vulpecula/blob/develop/LICENSE.txt)