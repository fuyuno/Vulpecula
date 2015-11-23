using System.Collections.Generic;

namespace Vulpecula.Mobile.Models.Interfaces
{
    public interface IPasswordVault
    {
        void Add(IPasswordCredentials credentials);

        IReadOnlyList<IPasswordCredentials> FindAllByUserName(string username);

        // Cannot used
        // IReadOnlyList<IPasswordCredentials> FindAllByResource(string resource);

        void Remove(IPasswordCredentials credentials);

        void Update(IPasswordCredentials oldCredentials, IPasswordCredentials newCredentials);
    }
}