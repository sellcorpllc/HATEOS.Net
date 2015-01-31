using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HATEOS_Lib
{
    public class Link
    {
        #region Fields
        private string _method;
        private string _relation;
        private string _url;
        #endregion

        #region Properties
        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }  

        public string Relation
        {
            get { return _relation; }
            set { _relation = value; }
        }
        
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        #endregion


    }
}
