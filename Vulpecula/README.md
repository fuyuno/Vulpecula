# Vulpecula
Vulpecula �́ACroudia API�̃��b�p�[���C�u�����ł��B  
�����y�є񓯊��̗�����(�ꉞ)�T�|�[�g���Ă��܂��B

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
�Ȃ��A�������\�b�h�̎g�p�͔񐄏��ł���A�܂������I�ɂ͔񓯊����\�b�h���g�p���Ă��܂��B