using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM.Rest
{
    public class ParameterCollection : List<KeyValuePair<string, object>>
    {
        public ParameterCollection(object[] parameters)
            : base()
        {
            var names = parameters.Where((x, index) => index % 2 == 0).Cast<string>();
            var values = parameters.Where((x, index) => index % 2 == 1);

            var queryParams = names.Zip(values, (name, value) => new KeyValuePair<string, object>(name, value));

            this.AddRange(queryParams);
        }

        public ParameterCollection()
        {
        }

        public void Add(string key, object value)
        {
            Add(new KeyValuePair<string, object>(key, value));
        }

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

        public string BuildPostContent()
        {
            return BuildQueryString().TrimStart('?');
        }
    }
}
