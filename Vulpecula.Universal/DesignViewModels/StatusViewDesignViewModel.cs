using System;

using JetBrains.Annotations;

using Vulpecula.Models;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Flyouts;
using Vulpecula.Universal.ViewModels.Timelines.Primitives;

namespace Vulpecula.Universal.DesignViewModels
{
    [UsedImplicitly]
    public class StatusViewDesignViewModel
    {
        public StatusModel Model { get; }

        public bool IsShare { get; }
        public bool IsComment { get; }
        public bool IsDirectMessage { get; }
        public bool HasImage { get; }

        public string CreatedAt => Model.CreatedAt.ToString("HH:mm");

        public string Via => Model.Source == null ? "" : $"via {Model.Source.Name}";

        public UserDesignViewModel User { get; }

        public UserFlyoutViewModel UserProfile { get; }

        public UserViewModel Recipient { get; }

        public UserViewModel ShareUser { get; }

        public StatusViewDesignViewModel()
        {
            Model = new StatusModel(new Status
            {
                Id = 5119033,
                IdStr = "5119033",
                CreatedAt = new DateTime(2015, 7, 17, 19, 11, 0),
                Text = "私のところでは、7-ZipやWinRAR、Lhaplus、Explzh for Windowsではちゃんと解凍できましたが、Windows標準のものだけは中身が空っぽになっていました",
                Entities = null,
                IsFavorited = false,
                FavoritedCount = 0,
                IsSpread = false,
                SpreadCount = 0,
                InReplyToStatusId = 0,
                InReplyToStatusIdStr = "0",
                InReplyToUserId = 0,
                InReplyToUserIdStr = "0",
                InReplyToScreenName = null,
                Source = new Source { Name = "Croudia", Url = "" },
                User = new User
                {
                    Id = 43391,
                    IdStr = "43391",
                    Name = "三日月 ふゆの",
                    ScreenName = "MikazukiFuyuno",
                    ProfileImageUrlHttps = "https://img.croudia.com/profile_images/MikazukiFuyuno/pzqDtyUUiiBeX7nyvcngxhwi.png",
                    CoverImageUrlHttps = "https://img.croudia.com/profile_banners/MikazukiFuyuno/c1eBtKT53EYKeF8YPyOCTkgi.jpeg",
                    CreatedAt = new DateTime(2015, 2, 15, 3, 1, 53, DateTimeKind.Local),
                    Description = "三日月冬乃といいます。",
                    FavoritesCount = 10,
                    IsFollowRequestSent = false,
                    FollowersCount = 15,
                    IsFollowing = false,
                    FriendsCount = 2,
                    Location = string.Empty,
                    StatusesCount = 67,
                    IsProtected = false,
                    Url = "https://about.mkzk.xyz"
                },
                SpreadStatus = null,
                QuotedStatus = null
            });
            if (Model.IsDirectMessage)
            {
                IsDirectMessage = Model.IsDirectMessage;
                return;
            }

            IsShare = Model.SpreadStatus != null;
            IsComment = Model.QuotedStatus != null;
            HasImage = Model.Entities?.Media != null;

            User = new UserDesignViewModel();
            //User = new UserViewModel(IsShare ? Model.SpreadStatus?.User : Model.User);
            //Recipient = new UserViewModel(Model.Recipient);
            //ShareUser = new UserViewModel(Model.User);
            //UserProfile = new UserFlyoutViewModel(IsShare ? Model.SpreadStatus?.User : Model.User);
        }

        #region Text

        private string _text;
        public string Text => _text ?? (_text = IsShare ? Model.SpreadStatus.Text : Model.Text);

        #endregion

        #region Image

        private string _imageUrl;
        public string ImageUrl => _imageUrl ?? (_imageUrl = HasImage ? Model.Entities.Media.MediaUrlHttps : "");

        #endregion
    }
}