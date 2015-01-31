using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HATEOS_Lib.Attributes
{
    //This class allows you signify the propery is for a related resource for generating links.
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=true)]
    public class RelatedResource : Attribute
    {
        public RelatedResource(string rel) { relationship = rel; }
        public string relationship;
    }
}
