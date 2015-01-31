using HATEOS_Lib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTestLib
{
    [Resource("item")]
    public class Item
    {
        private int _itemId;
        private string _name;

        

        [ResourceKey()]
        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
