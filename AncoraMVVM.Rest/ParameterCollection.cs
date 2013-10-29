using System;
using System.Collections.Generic;
using System.Linq;

namespace AncoraMVVM.Rest
{
    /// <summary>
    /// A collection of parameters of a request.
    /// </summary>
    public class ParameterCollection : List<KeyValuePair<string, object>>
    {
        /// <summary>
        /// Build a parameter collection given a list of the form "key", value, "key", value, ...
        ///
        /// If the collection has an odd number of items, ignores the last item.
        ///
        /// Useful when receiving a variable parameter list.
        /// </summary>
        /// <param name="parameters">List of parameters key, value, key, value...</param>
        public ParameterCollection(object[] parameters)
            : base()
        {
            var names = parameters.Where((x, index) => index % 2 == 0).Cast<string>();
            var values = parameters.Where((x, index) => index % 2 == 1);

            var queryParams = names.Zip(values, (name, value) => new KeyValuePair<string, object>(name, value));

            this.AddRange(queryParams);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ParameterCollection()
        {
        }

        /// <summary>
        /// Adds a parameter to the collection.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Add(string key, object value)
        {
            Add(new KeyValuePair<string, object>(key, value));
        }

        /// <summary>
        /// Builds the query string for the collection of parameters. Returns an
        ///     empty string if the collection is empty.
        /// </summary>
        /// <returns>Query string (i.e., "?key1=value1&key2=value2&...")</returns>
        public string BuildQueryString()
        {
            string query = "";

            if (Count >= 1)
            {
                var queryParams = this.Select(pair =>
                    string.Format("{0}={1}",
                        Uri.EscapeDataString(pair.Key),
                        Uri.EscapeDataString(pair.Value.ToString())));
                query = "?" + string.Join("&", queryParams);
            }

            return query;
        }

        /// <summary>
        /// Builds a string to send the parameters in a POST request. Returns an
        ///     empty string if the collection is empty.
        /// </summary>
        /// <returns>Post content (i.e. "key1=value1&key2=value2&...")</returns>
        public string BuildPostContent()
        {
            string query = "";

            if (Count >= 1)
            {
                var queryParams = this.Select(pair =>
                    string.Format("{0}={1}",
                        pair.Key,
                        Uri.EscapeDataString(pair.Value.ToString())));
                query = string.Join("&", queryParams);
            }

            return query;
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return this.Select(x => x.Key);
            }
        }

        public bool ContainsKey(string key)
        {
            return this.Any(x => x.Key == key);
        }
    }
}