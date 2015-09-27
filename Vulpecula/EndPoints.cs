namespace Vulpecula
{
    internal static class EndPoints
    {
        private static readonly string CroudiaAPIv1 = "https://api.croudia.com/";

        private static readonly string CroudiaAPIv2 = "https://api.croudia.com/2/";

        private const string Format = ".json";

        public static readonly string OAuth2Authorize = CroudiaAPIv1 + "oauth/authorize";

        public static readonly string OAuth2Token = CroudiaAPIv1 + "oauth/token";

        public static readonly string AccountVeriryCredentials = CroudiaAPIv1 + "account/verify_credentials" + Format;

        public static readonly string StatusesPublicTimeline = CroudiaAPIv2 + "statuses/public_timeline" + Format;

        public static readonly string StatusesHomeTimeline = CroudiaAPIv2 + "statuses/home_timeline" + Format;

        public static readonly string StatusesUserTimeline = CroudiaAPIv2 + "statuses/user_timeline" + Format;

        public static readonly string StatusesMentions = CroudiaAPIv2 + "statuses/mentions" + Format;

        public static readonly string StatusesUpdate = CroudiaAPIv2 + "statuses/update" + Format;

        public static readonly string StatusesUpdateWithMedia = CroudiaAPIv2 + "statuses/update_with_media" + Format;

        public static readonly string StatusesDestroyId = CroudiaAPIv2 + "statuses/destroy/{0}" + Format;

        public static readonly string StatusesShowId = CroudiaAPIv2 + "statuses/show/{0}" + Format;

        public static readonly string SecretMails = CroudiaAPIv1 + "secret_mails" + Format;

        public static readonly string SecretMailsSent = CroudiaAPIv1 + "secret_mails/sent" + Format;

        public static readonly string SecretMailsNew = CroudiaAPIv1 + "secret_mails/new" + Format;

        public static readonly string SecretMailsNewWithMedia = CroudiaAPIv1 + "secret_mails/new_with_media" + Format;

        public static readonly string SecretMailsDestroyId = CroudiaAPIv1 + "secret_mails/destroy/{0}" + Format;

        public static readonly string SecretMailsShowId = CroudiaAPIv1 + "secret_mails/show/{0}" + Format;

        public static readonly string SecretMailsGetSecretPhoto = CroudiaAPIv1 + "secret_mails/get_secret_photo/{0}";

        public static readonly string UsersShow = CroudiaAPIv1 + "users/show" + Format;

        public static readonly string UsersLookup = CroudiaAPIv1 + "users/lookup" + Format;

        public static readonly string AccountUpdateProfileImage = CroudiaAPIv1 + "account/update_profile_image" + Format;

        public static readonly string AccountUpdateCoverImage = CroudiaAPIv1 + "account/update_cover_image" + Format;

        public static readonly string AccountUpdateProfile = CroudiaAPIv1 + "account/update_profile" + Format;

        public static readonly string FriendShipsCreate = CroudiaAPIv1 + "friendships/create" + Format;

        public static readonly string FriendShipsDestroy = CroudiaAPIv1 + "friendships/destroy" + Format;

        public static readonly string FriendShipsShow = CroudiaAPIv1 + "friendships/show" + Format;

        public static readonly string FriendShipsLookup = CroudiaAPIv1 + "friendships/lookup" + Format;

        public static readonly string FriendsIds = CroudiaAPIv1 + "friends/ids" + Format;

        public static readonly string FollowersIds = CroudiaAPIv1 + "followers/ids" + Format;

        public static readonly string FriendsList = CroudiaAPIv1 + "friends/list" + Format;

        public static readonly string FollowersList = CroudiaAPIv1 + "followers/list" + Format;

        public static readonly string Favorites = CroudiaAPIv2 + "favorites" + Format;

        public static readonly string FavoritedCreateId = CroudiaAPIv2 + "favorites/create/{0}" + Format;

        public static readonly string FavoritesDestroyId = CroudiaAPIv2 + "favorites/destroy/{0}" + Format;

        public static readonly string StatusesSpreadId = CroudiaAPIv2 + "statuses/spread/{0}" + Format;

        public static readonly string StatusesComment = CroudiaAPIv2 + "statuses/comment" + Format;

        public static readonly string StatusesCommentWithMedia = CroudiaAPIv2 + "statuses/comment_with_media" + Format;

        public static readonly string TrendsPlace = CroudiaAPIv1 + "trends/place" + Format;

        public static readonly string MutesUsersCreate = CroudiaAPIv1 + "mutes/users/create" + Format;

        public static readonly string MutesUsersDestroy = CroudiaAPIv1 + "mutes/users/destroy" + Format;

        public static readonly string MutesUsersList = CroudiaAPIv1 + "mutes/users/list" + Format;

        public static readonly string MutesUsersIds = CroudiaAPIv1 + "mutes/users/ids" + Format;

        public static readonly string BlocksCreate = CroudiaAPIv1 + "blocks/create" + Format;

        public static readonly string BlocksDestroy = CroudiaAPIv1 + "blocks/destroy" + Format;

        public static readonly string BlocksList = CroudiaAPIv1 + "blocks/list" + Format;

        public static readonly string BlocksIds = CroudiaAPIv1 + "blocks/ids" + Format;

        public static readonly string SearchVoices = CroudiaAPIv2 + "search/voices" + Format;

        public static readonly string UsersSearch = CroudiaAPIv1 + "users/search" + Format;

        public static readonly string ProfileSearch = CroudiaAPIv1 + "profile/search" + Format;

        public static readonly string SearchFavorits = CroudiaAPIv2 + "search/favorites" + Format;
    }
}