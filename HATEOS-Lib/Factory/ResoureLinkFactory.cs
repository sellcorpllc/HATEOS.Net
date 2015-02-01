using HATEOS_Lib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HATEOS_Lib.Factory
{
    public class ResoureLinkFactory
    {
        private string _baseUrl;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        public ResoureLinkFactory(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

       
        
        

    }
}
