using System.Collections.ObjectModel;

using Prism.Mvvm;

using Vulpecula.Models;

namespace Vulpecula.Universal.ViewModels.Contents
{
    public class TimelineViewModel<T> : BindableBase
    {
        public User User { get; private set; }

        public ObservableCollection<T> TimelineContents { get; private set; }

        public TimelineViewModel(User user)
        {
            this.User = user;
            this.TimelineContents = new ObservableCollection<T>();
        }
    }
}