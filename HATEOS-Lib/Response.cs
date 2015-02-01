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
        // This list holds actions that can be done on the resource such as POST,PUT,DELETE ex. decrease value of an account
        private List<Link> _actionLinks;
       
        // This list holds links to additional resources
        private List<Link> _resourceLinks;       
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

        public List<Link> ActionLinks
        {
            get { return _actionLinks; }
            set { _actionLinks = value; }
        }

        public List<Link> ResourceLinks
        {
            get { return _resourceLinks; }
            set { _resourceLinks = value; }
        }
        #endregion

    }
}
