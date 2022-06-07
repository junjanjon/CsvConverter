# CsvConverter

CSV ファイルを他の形式に変換する CLI です。

NOTE: 極めて単純な CSV を TSV に変換のみ対応しています。

# 使い方

dotnet コマンドのみ。

## 動作確認

- dotnet
  - 6.0.101

## コマンド

```
dotnet run --input INPUT_FILE_PATH --output OUTPUT_FILE_PATH
```

### 引数

|引数名|値|
|:---|:---|
|--input| 変換する csv ファイルパスの指定 |
|--output| 変換後のファイルパスの指定 |

# LICENSE

[MIT](./LICENSE)
