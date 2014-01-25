using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AncoraMVVM.Rest
{
    /// <summary>
    /// Base service for the creation of REST clients.
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Authority (host) where requests will be directed. E.g.: http://twitter.com.
        /// </summary>
        public virtual string Authority { get; protected set; }

        /// <summary>
        /// Base path for all the requests. E.g.: /api/1/
        /// </summary>
        public virtual string BasePath { get; protected set; }

        /// <summary>
        /// HttpService used to send the requests.
        /// </summary>
        protected IHttpService Service { get; set; }

        /// <summary>
        /// Url parameters that will be appended to every URL.
        /// </summary>
        protected ParameterCollection PersistentUrlParameters { get; private set; }

        public BaseService(IHttpService service)
        {
            Service = service;
            PersistentUrlParameters = new ParameterCollection();
        }

        #region Request creation
        /// <summary>
        /// Creates the string content for a POST request.
        /// </summary>
        /// <param name="parameters">Parameters to be used.</param>
        /// <returns>Http content.</returns>
        protected virtual HttpContent CreateStringContentForPost(ParameterCollection parameters)
        {
            return new StringContent(parameters.BuildPostContent(), Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        /// <summary>
        /// Create a request.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="method">HTTP method.</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>A request message ready to be sent.</returns>
        protected virtual HttpRequestMessage CreateRequest(string route, HttpMethod method, ParameterCollection parameters = null)
        {
            if (parameters == null)
                parameters = new ParameterCollection();

            parameters.AddRange(PersistentUrlParameters);

            string query = method == HttpMethod.Post ?
                    "" :
                    parameters.BuildQueryString();

            var request = new HttpRequestMessage(method, Authority + (BasePath ?? "") + route + query);

            if (method == HttpMethod.Post)
                request.Content = CreateStringContentForPost(parameters);

            return request;
        }

        /// <summary>
        /// Create a request.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="method">HTTP method.</param>
        /// <param name="parameters">Parameters as params list.</param>
        /// <returns>A request message ready to be sent.</returns>
        protected HttpRequestMessage CreateRequest(string route, HttpMethod method, params object[] parameters)
        {
            return CreateRequest(route, method, new ParameterCollection(parameters));
        }
        #endregion

        #region File Requests

        /// <summary>
        /// Creates a file request for a unique file.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="file">File as stream.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="fileParamName">File parameter name.</param>
        /// <param name="parameters">Additional parameters.</param>
        /// <returns>HttpRequestMessage ready to be sent.</returns>
        protected HttpRequestMessage CreateFileRequest(string route, Stream file, string fileName, string fileParamName, params object[] parameters)
        {
            return CreateFileRequest(route, new List<HttpFileUpload> { new HttpFileUpload(file, fileName, fileParamName) }, new ParameterCollection(parameters));
        }

        /// <summary>
        /// Creates a file request for a unique file as MultipartFormData.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="files">Collection of files.</param>
        /// <param name="parameters">Additional parameters.</param>
        /// <returns>HttpRequestMessage ready to be sent.</returns>
        protected HttpRequestMessage CreateFileRequest(string route, IEnumerable<HttpFileUpload> files, params object[] parameters)
        {
            return CreateFileRequest(route, files, new ParameterCollection(parameters));
        }

        /// <summary>
        /// Creates a file request for a unique file as MultipartFormData.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="files">Collection of files.</param>
        /// <param name="parameters">Additional parameters.</param>
        protected virtual HttpRequestMessage CreateFileRequest(string route, IEnumerable<HttpFileUpload> files, ParameterCollection parameters)
        {
            var content = new MultipartFormDataContent();

            foreach (var parameter in parameters)
                content.Add(new StringContent(parameter.Value.ToString()), parameter.Key);

            foreach (var file in files)
            {
                var fileContent = CreateFileContentFor(file);
                content.Add(fileContent, file.Parameter, file.Filename);
            }

            var req = new HttpRequestMessage(HttpMethod.Post, Authority + (BasePath ?? "") + route);

            req.Content = content;

            return req;
        }

        /// <summary>
        /// Creates a HttpContent object for the given file.
        /// </summary>
        /// <param name="file">File.</param>
        /// <returns>HttpContent wrapping the file contents.</returns>
        protected virtual HttpContent CreateFileContentFor(HttpFileUpload file)
        {
            var fileContent = new ByteArrayContent(ReadStreamContents(file.FileStream));
            fileContent.Headers.ContentType = GetContentTypeFor(file.Filename);

            return fileContent;
        }

        /// <summary>
        /// Returns a content type based on the file name
        /// </summary>
        /// <param name="filename">File name.</param>
        /// <returns>Media type.</returns>
        protected virtual MediaTypeHeaderValue GetContentTypeFor(string filename)
        {
            var extension = filename.Split('.').Last();

            //TODO: Complete.
            switch (extension)
            {
                case "png":
                    return MediaTypeHeaderValue.Parse("image/png");
                case "jpg":
                    return MediaTypeHeaderValue.Parse("image/jpeg");
                default:
                    return MediaTypeHeaderValue.Parse("text/plain");

            }
        }

        private byte[] ReadStreamContents(Stream stream)
        {
            int length = (int)stream.Length;
            byte[] contents = new byte[length];

            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);

            stream.Read(contents, 0, length);

            return contents;
        }

        #endregion

        /// <summary>
        /// Creates a HttpRequestMessage for the given parameters, executes the request and returns the results.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="method">Http method.</param>
        /// <param name="parameters">Collection of parameters in the format "param1name", param1val, "param2name", param2val...</param>
        /// <returns>Task containing the response from the server.</returns>
        protected virtual async Task<HttpResponse> CreateAndExecute(string route, HttpMethod method, params object[] parameters)
        {
            var request = CreateRequest(route, method, parameters);

            return await Execute(request);
        }

        /// <summary>
        /// Creates a HttpRequestMessage for the given parameters, executes the request and returns the results deserializing the content.
        /// </summary>
        /// <typeparam name="T">Type of the content to be returned.</typeparam>
        /// <param name="route">Route.</param>
        /// <param name="method">Http method.</param>
        /// <param name="parameters">Collection of parameters in the format "param1name", param1val, "param2name", param2val...</param>
        /// <returns>Task containing the response from the server.</returns>
        protected virtual async Task<HttpResponse<T>> CreateAndExecute<T>(string route, HttpMethod method, params object[] parameters)
        {
            var request = CreateRequest(route, method, parameters);

            return await Execute<T>(request);
        }

        /// <summary>
        /// Executes a request against the server.
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Task containing the response.</returns>
        protected virtual async Task<HttpResponse> Execute(HttpRequestMessage request)
        {
            HttpResponseMessage response;

            try
            {
                response = await Service.Send(request);
            }
            catch (Exception e)
            {
                Debug.WriteLine("AncoraMVVM.Rest:BaseService.Execute - Failed with exception {0}", e);
                return new HttpResponse(null, e);
            }

            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await GetErrorMessage(response);
                return new HttpResponse(response, null, errorMsg);
            }
            else
            {
                return new HttpResponse(response);
            }
        }

        /// <summary>
        /// Executes a request against the server.
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Task containing the response.</returns>
        protected virtual async Task<HttpResponse<T>> Execute<T>(HttpRequestMessage req)
        {
            var response = await Execute(req);
            T item;

            if (response.Succeeded)
            {
                try
                {
                    var content = await response.Response.Content.ReadAsStringAsync();
                    item = Deserialize<T>(content);
                }
                catch (Exception e)
                {
                    return new HttpResponse<T>(default(T), response.Response, e);
                }
            }
            else
            {
                item = default(T);
            }

            return new HttpResponse<T>(item, response);
        }

        /// <summary>
        /// Extract the error message from a given response. Default return value is empty string.
        /// </summary>
        /// <param name="response">Response.</param>
        /// <returns>String error message.</returns>
        protected virtual Task<string> GetErrorMessage(HttpResponseMessage response)
        {
            return TaskEx.FromResult("");
        }

        /// <summary>
        /// Deserialize string into a type.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="content">String content.</param>
        /// <returns>Deserialized object.</returns>
        protected abstract T Deserialize<T>(string content);
    }
}
