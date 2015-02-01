using HATEOS_Lib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HATEOS_Lib.Factory
{
    public class UrlFactory<T> where T:class
    {
        private string _baseUrl;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        public UrlFactory(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public string generateUrl(T resourceClass)
        {
            
            string resourceName = resourceClass.GetType().GetCustomAttribute<Resource>().resourceName;
            string resourceKey = resourceClass.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ResourceKey))).FirstOrDefault().GetValue(resourceClass, null).ToString();

            string returnURl = String.Format(_baseUrl + "/{0}/{1}", resourceName, resourceKey);

            return returnURl;
        }

    }
}
