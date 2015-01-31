using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HATEOS_Lib.Attributes
{
    //This is used to mark the resource key for a class.   

    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false)]
    public class ResourceKey : Attribute
    {
        //public ResourceKey(string aKey) { key = aKey; }
        //public string key;        
    }

}
