# Vulpecula
Vulpecula は、Croudia APIのラッパーライブラリです。  
同期及び非同期の両方を(一応)サポートしています。

Sample
```csharp
var croudia = new Croudia("xxxxx", "xxxxx", "xxxxx", "xxxxx");

// sync
var statuses = croudia.Statuses.GetHomeTimeline();

// async
var statuses = await croudia.Statuses.GetHomeTimelineAsync();

// use parameters
var statuses = await croudia.Statuses.GetHomeTimelineAsync(trim_user => true, include_entities => false);
```
なお、同期メソッドの使用は非推奨であり、また内部的には非同期メソッドを使用しています。