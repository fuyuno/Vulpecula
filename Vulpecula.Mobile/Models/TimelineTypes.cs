using System;

namespace Vulpecula.Mobile.Models
{
    public enum TimelineTypes
    {
        Public,

        Home,

        Mentions
    }

    public static class TimelineType
    {
        public static string GetTitle(this TimelineTypes e)
        {
            switch (e)
            {
                case TimelineTypes.Public:
                    return "Public";
                
                case TimelineTypes.Home:
                    return "Home";

                case TimelineTypes.Mentions:
                    return "Mentions";
            }
            return string.Empty;
        }

        public static string GetIcon(this TimelineTypes e)
        {
            switch (e)
            {
                case TimelineTypes.Public:
                    return "public";

                case TimelineTypes.Home:
                    return "home";

                case TimelineTypes.Mentions:
                    return "mention";
            }
            return string.Empty;
        }
    }
}

