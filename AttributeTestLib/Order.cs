using HATEOS_Lib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTestLib
{
    [Resource("order")]
    public class Order
    {
        private int _orderId;
        List<OrderLine> _orderLines = new List<OrderLine>();

        [ResourceKey()]
        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        [RelatedResource("collectionField")]
        public List<OrderLine> OrderLines
        {
            get { return _orderLines; }
            set { _orderLines = value; }
        }





    }
}
