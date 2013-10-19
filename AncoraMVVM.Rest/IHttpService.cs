using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AncoraMVVM.Rest
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Send(HttpRequestMessage req);
    }

    /// <summary>
    /// Wrapper to allow testing.
    /// </summary>
    public class HttpService : IHttpService, IDisposable
    {
        private HttpClient client;
        private bool disposed;

        public HttpService()
        {
            client = new HttpClient();
        }

        public HttpService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HttpResponseMessage> Send(HttpRequestMessage req)
        {
            if (disposed)
                throw new ObjectDisposedException("HttpService/HttpClient was disposed.");

            return await client.SendAsync(req);
        }

        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass 
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
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
