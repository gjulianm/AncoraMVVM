using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AncoraMVVM.Rest
{
    /// <summary>
    /// Wrapper for Microsoft's HttpClient.
    /// </summary>
    public class HttpService : IHttpService, IDisposable
    {
        private HttpClient client;
        private bool disposed;

        /// <summary>
        /// Create the HttpService with the default HttpClient.
        /// </summary>
        public HttpService()
        {
            client = new HttpClient();
        }

        /// <summary>
        /// Create HttpService with a given HttpClient. 
        /// </summary>
        /// <param name="client">HttpClient.</param>
        public HttpService(HttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Send a given request message.
        /// </summary>
        /// <param name="req">Request message</param>
        /// <returns>Task containing the response</returns>
        /// <remarks>Don't use after disposal.</remarks>
        public async Task<HttpResponseMessage> Send(HttpRequestMessage req)
        {
            if (disposed)
                throw new ObjectDisposedException("HttpService/HttpClient was disposed.");

            return await client.SendAsync(req);
        }

        /// <summary>
        /// Dispose the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these  
            // operations, as well as in your methods that use the resource. 
            if (!disposed)
            {
                if (disposing && client != null)
                    client.Dispose();

                // Indicate that the instance has been disposed.
                client = null;
                disposed = true;
            }
        }
    }
}
