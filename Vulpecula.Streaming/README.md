# Vulpecula.Streaming
Vulpecula.Streaming �́AVulpecula �̋[���X�g���[�~���O�g���ł��B  
Croudia �̃X�e�[�^�X�擾�n API ���A Twitter �� Streaming API �̂悤�Ɉ����܂��B

Sample
```csharp
var croudia = new Croudia("xxxxx", "xxxxx", "xxxxx", "xxxxx");

// 10�b�X�p��(�f�t�H���g��5�b)
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