using HATEOS_Lib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTestLib
{
    [Resource("orderlines")]
    public class OrderLine
    {
        
        int _orderLineId;
        Item _orderItem;

        public OrderLine() : this(new Item())
        {

        }

        public OrderLine(Item aItem)
        {
            _orderItem = aItem;
        }

        [RelatedResource("field")]
        public Item OrderItem
        {
            get { return _orderItem; }
            set { _orderItem = value; }
        }


        [ResourceKey()]
        public int OrderLineId
        {
            get { return _orderLineId; }
            set { _orderLineId = value; }
        }

    }
}
