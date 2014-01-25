using System.Net.Http;
using System.Threading.Tasks;

namespace AncoraMVVM.Rest
{
    /// <summary>
    /// Base IHttpService interface.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Send a given request message.
        /// </summary>
        /// <param name="req">Request message</param>
        /// <returns>Task containing the response</returns>
        /// <remarks>Don't use after disposal.</remarks>
        Task<HttpResponseMessage> Send(HttpRequestMessage req);
    }
}
