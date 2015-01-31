using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HATEOS_Lib
{
    public class Response<T> where T : class
    {
        #region Fields
        private int _totalRecords;
        private int _returnedRecords;
        private List<T> _result;
        private Link _nextLink;
        private Link _thisLink;
        private List<Link> _relatedLinks;
        #endregion

        #region Properties
        public int TotalRecords
        {
            get { return _totalRecords; }
            set { _totalRecords = value; }
        }

        public int ReturnedRecords
        {
            get { return _returnedRecords; }
            set { _returnedRecords = value; }
        }

        public List<T> Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public Link NextLink
        {
            get { return _nextLink; }
            set { _nextLink = value; }
        }

        public Link ThisLink
        {
            get { return _thisLink; }
            set { _thisLink = value; }
        }

        public List<Link> RelatedLinks
        {
            get { return _relatedLinks; }
            set { _relatedLinks = value; }
        }
        #endregion

    }
}
