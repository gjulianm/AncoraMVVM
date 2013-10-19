using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AncoraMVVM.Rest
{
    public class BaseService
    {
        public virtual string Authority { get; protected set; }
        public virtual string BasePath { get; protected set; }

        protected IHttpService Service { get; set; }
        protected ParameterCollection PersistentUrlParameters { get; private set; }


        public BaseService(IHttpService service)
        {
            Service = service;
            PersistentUrlParameters = new ParameterCollection();
        }

        protected virtual HttpRequestMessage CreateRequest(string route, HttpMethod method, ParameterCollection parameters = null)
        {
            if (parameters == null)
                parameters = new ParameterCollection();

            parameters.AddRange(PersistentUrlParameters);

            string query = "";

            if (method != HttpMethod.Post)
                query = parameters.BuildQueryString();

            var request =  new HttpRequestMessage(method, Authority + BasePath + route + query);

            if(method == HttpMethod.Post)
                request.Content = new StringContent(parameters.BuildPostContent());

            return request;
        }

        protected HttpRequestMessage CreateRequest(string route, HttpMethod method, params object[] parameters)
        {
            return CreateRequest(route, method, new ParameterCollection(parameters));
        }

        protected virtual async Task<HttpResponse> CreateAndExecute(string route, HttpMethod method, params object[] parameters)
        {
            var request = CreateRequest(route, method, parameters);

            return await Execute(request);
        }

        protected virtual async Task<HttpResponse<T>> CreateAndExecute<T>(string route, HttpMethod method, params object[] parameters)
        {
            var request = CreateRequest(route, method, parameters);

            return await Execute<T>(request);
        }

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

        protected virtual Task<string> GetErrorMessage(HttpResponseMessage response)
        {
            return TaskEx.FromResult("");
        }

        protected abstract T Deserialize<T>(string content);
    }
}
