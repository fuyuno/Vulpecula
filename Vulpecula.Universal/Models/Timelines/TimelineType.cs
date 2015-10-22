using System;

namespace Vulpecula.Universal.Models.Timelines
{
    public enum TimelineType
    {
        /// <summary>
        /// Public timeline (/2/statuses/public_timeline.json)
        /// </summary>
        Public,

        /// <summary>
        /// Public timeline (all accounts)
        /// </summary>
        PublicAll,

        /// <summary>
        /// Home timeline (/2/statuses/home_timeline.json)
        /// </summary>
        Home,

        /// <summary>
        /// Mentions timeline (/2/statuses/mentions.json)
        /// </summary>
        Mentions,

        /// <summary>
        /// Mentions timeline (all accounts)
        /// </summary>
        MentionsAll,

        /// <summary>
        /// User timeline (/2/statuses/user_timeline.json)
        /// </summary>
        User,

        /// <summary>
        /// Favorite timeline (/2/statuses/favorites.json)
        /// </summary>
        [Obsolete("This timeline is disabled now.", true)]
        Favorite,

        /// <summary>
        /// Direct messages timeline (/secret_mails.json, /secret_mails/sent.json)
        /// </summary>
        DirectMessages,

        /// <summary>
        /// Direct messages timeline (all accounts)
        /// </summary>
        DirectMessagesAll,

        /// <summary>
        /// Event notification (UNDEFINED)
        /// </summary>
        Event,

        /// <summary>
        /// Event notification (all accounts)
        /// </summary>
        EventAll
    }
}