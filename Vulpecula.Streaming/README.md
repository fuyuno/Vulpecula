# Vulpecula.Streaming
Vulpecula.Streaming �́AVulpecula �̋[���X�g���[�~���O�g���ł��B  
Croudia �̃X�e�[�^�X�擾�n API ���A Twitter �� Streaming API �̂悤�Ɉ����܂��B

Sample
```csharp
var croudia = new Croudia("xxxxx", "xxxxx", "xxxxx", "xxxxx");

// 10�b�X�p��
CroudiaStreaming.TimeSpan = TimeSpan.FromSeconds(10); 

// foreach
foreach(var status = croudia.Statuses.GetPublicTimelineAsStreaming())
    Console.WriteLine(status.ToString());

// use parameters
foreach(var status = croudia.Statuses.GetPublicTimelineAsStreaming(trim_user => true))
    Console.WriteLine(status.ToString());
```
