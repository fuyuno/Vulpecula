# Vulpecula.Streaming
Vulpecula.Streaming は、Vulpecula の擬似ストリーミング拡張です。  
Croudia のステータス取得系 API を、 Twitter の Streaming API のように扱えます。

Sample
```csharp
var croudia = new Croudia("xxxxx", "xxxxx", "xxxxx", "xxxxx");

// 10秒スパン(デフォルトは5秒)
CroudiaStreaming.TimeSpan = TimeSpan.FromSeconds(10); 

// foreach
foreach (var status in croudia.Statuses.GetPublicTimelineAsStreaming())
{
    Console.WriteLine($"{status.User.Name} @{status.User.ScreenName}");
    Console.WriteLine(status.Text);
    Console.WriteLine("-------------------------------------------------------------------");
}


// Reactive
var stream = croudia.Statuses.GetPublicTimelineAsObservable();
var disposable = stream.Where(w => w.Source.Name == "Croudia").Subscribe(status =>
{
    Console.WriteLine($"{status.User.Name} @{status.User.ScreenName}");
    Console.WriteLine(status.Text);
    Console.WriteLine("-------------------------------------------------------------------");
});

var task = Task.Delay(TimeSpan.FromSeconds(60));
task.Wait();
Console.WriteLine("Disconnected.");
disposable.Dispose();
```