# netcore6-console-app

.Net Core 6 でコンソールアプリケーションのサンプル

# 動作環境

| 名称          | バージョン | 備考                 |
| ------------- | ---------- | -------------------- |
| .NET Core SDK | 6.0.2XX    | `dotnet --list-sdks` |

# 実行手順

## 開発中の実行

- root コマンド

```bash
cd src/Netcore6ConsoleApp
dotnet run
```

- サブコマンド

以下は`zip`というサブコマンドの例

```bash
cd src/Netcore6ConsoleApp
dotnet run -- zip 123-4567
```
