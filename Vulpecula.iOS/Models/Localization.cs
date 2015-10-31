using System;

using Foundation;

using Vulpecula.Mobile.Models;

namespace Vulpecula.iOS
{
	public class Localization : ILocalization
	{
		public Localization ()
		{
		}

		public string GetString(string key)
		{
			return NSBundle.MainBundle.LocalizedString (key, key);
        }

		public string GetStringByFullName(string name)
		{
			return NSBundle.MainBundle.LocalizedString (name, name);
		}
	}
}