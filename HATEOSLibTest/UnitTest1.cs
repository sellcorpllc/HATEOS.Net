using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttributeTestLib;
using HATEOS_Lib.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System.Data.Linq;
using System.Linq;


namespace HATEOSLibTest
{
    //Let UT designate a unit test and CT represent continuous integration test
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ResourceAttributeItem_UT()
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

        [TestMethod]
        public void ResourceAttributeItemForObject_UT()
        {
            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            string resourceName = "";

            Resource r = testItem.GetType().GetCustomAttribute<Resource>();
            resourceName = r.resourceName;
            Assert.IsTrue(resourceName.CompareTo("item") == 0);
        }


        [TestMethod]
        public void ResourceKey_UT()
        {
            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            //Get the propertylist for the item
            List<PropertyInfo> propertyList = new List<PropertyInfo>(testItem.GetType().GetProperties());

            //Generate a list of properties where an attribute of ResourceKey is defined for the property and take the first 
            PropertyInfo p = propertyList.Where(prop => Attribute.IsDefined(prop, typeof(ResourceKey))).FirstOrDefault();

            // Couple of different way to get the value of the property that has a resource key attribute
            string valueOfResourceKey1 = p.GetValue(testItem, null).ToString();
            string valueOfResourceKey2 =  testItem.GetType().GetProperty(p.Name).GetValue(testItem, null).ToString();
           
            
            //The ultimate statement of enlightenment I have been searching for
            //Get the type for the item, then properters which have an attirbute of resource key, take the first and only one and then get its value for the object of the item
            string everythingTogether = testItem.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ResourceKey))).FirstOrDefault().GetValue(testItem, null).ToString();

            
            Assert.IsTrue(everythingTogether.CompareTo("4")==0);
           
        }


        [TestMethod]
        public void urlItemClass_UT()
        {
            string baseUrl = @"/api";
            string expectEdUrl = @"/api/item/4";

            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            string resourceName = testItem.GetType().GetCustomAttribute<Resource>().resourceName;
            string resourceKey = testItem.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ResourceKey))).FirstOrDefault().GetValue(testItem, null).ToString();

            string testUrl = String.Format(baseUrl + "/{0}/{1}", resourceName, resourceKey);

            Assert.IsTrue(expectEdUrl.CompareTo(testUrl) == 0);
        }

        [TestMethod]
        public void urlForOrderLine_UT()
        {

            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            OrderLine testOrderLine = new OrderLine();
            testOrderLine.OrderLineId = 182;
            testOrderLine.OrderItem = testItem;

            string baseUrl = @"/api";
            string expectEdUrl = @"/api/orderlines/182";
            string resourceName = testOrderLine.GetType().GetCustomAttribute<Resource>().resourceName;
            string resourceKey = testOrderLine.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ResourceKey))).FirstOrDefault().GetValue(testOrderLine, null).ToString();

            string testUrl = String.Format(baseUrl + "/{0}/{1}", resourceName, resourceKey);



            Assert.IsTrue(expectEdUrl.CompareTo(testUrl) == 0);

        }

        [TestMethod]
        public void urlTestFactoryOrderLine_UT()
        {
           

        }

        [TestMethod]
        public void urlForRelatedResource_UT()
        {

            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            OrderLine testOrderLine = new OrderLine();
            testOrderLine.OrderLineId = 182;
            testOrderLine.OrderItem = testItem;

            List<PropertyInfo> propList = testOrderLine.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(RelatedResource))).ToList<PropertyInfo>();

            foreach (PropertyInfo p in propList)
            {
                p.GetValue(testOrderLine, null);

            }


        }


    }
}
