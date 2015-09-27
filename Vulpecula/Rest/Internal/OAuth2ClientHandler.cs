using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Vulpecula.Rest.Internal
{
    internal class OAuth2ClientHandler : HttpClientHandler
    {
        private readonly Croudia _croudia;

        internal OAuth2ClientHandler(Croudia croudia)
        {
            this._croudia = croudia;
        }

        /// <summary>
        /// Creates an instance of  <see cref="T:System.Net.Http.HttpResponseMessage"/> based on the information provided in the <see cref="T:System.Net.Http.HttpRequestMessage"/> as an operation that will not block.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="request">The HTTP request message.</param><param name="cancellationToken">A cancellation token to cancel the operation.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> was null.</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", "Bearer " + this._croudia.AccessToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}