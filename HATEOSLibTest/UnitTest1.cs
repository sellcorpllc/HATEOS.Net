using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttributeTestLib;
using HATEOS_Lib.Attributes;
using System.Collections.Generic;

namespace HATEOSLibTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ResourceAttributeTest()
        {        
            //Get a list
            List<Attribute> attrs = new List<Attribute>(Attribute.GetCustomAttributes(typeof(Item)));
            string resourceName = "";
            foreach (Attribute attr in attrs)
            {
                if (attr is Resource)
                {
                    Resource a = (Resource)attr;
                    resourceName = a.resourceName;
                }
            }
            Assert.IsTrue(resourceName.CompareTo("item")==0);
        }

        public void Resou
    }
}
