# Vulpecula.Streaming
Vulpecula.Streaming は、Vulpecula の擬似ストリーミング拡張です。  
Croudia のステータス取得系 API を、 Twitter の Streaming API のように扱えます。

Sample
```csharp
var croudia = new Croudia("xxxxx", "xxxxx", "xxxxx", "xxxxx");

// 10秒スパン
CroudiaStreaming.TimeSpan = TimeSpan.FromSeconds(10); 

// foreach
foreach(var status = croudia.Statuses.GetPublicTimelineAsStreaming())
    Console.WriteLine(status.ToString());

// use parameters
foreach(var status = croudia.Statuses.GetPublicTimelineAsStreaming(trim_user => true))
    Console.WriteLine(status.ToString());
```
