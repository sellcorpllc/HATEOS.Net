using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HATEOS_Lib.Attributes
{
    //This attribute defines resourcename used in a class for rest resource
    //Inherted objects shold not be accessed through the same resource key and should be redfined
    //There should only be one resouce on a class
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=false,Inherited=false)]
    public class Resource : Attribute
    {
        //Constructor
        public Resource(string rName) { resourceName = rName; }

        //The resounce name for url: baseurl../{resourceName}/{resourceKey}
        public string resourceName;
    }
}
