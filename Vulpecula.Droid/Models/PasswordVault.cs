using System;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Droid.Models
{
    public class PasswordVault : IPasswordVault
    {
        public void Add(IPasswordCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public IPasswordCredentials FindByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(IPasswordCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public void Update(IPasswordCredentials oldCredentials, IPasswordCredentials newCredentials)
        {
            throw new NotImplementedException();
        }
    }
}