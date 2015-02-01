using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttributeTestLib;
using HATEOS_Lib.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System.Data.Linq;
using System.Linq;
using HATEOS_Lib.Factory;


namespace HATEOSLibTest
{
    //Let UT designate a unit test and CT represent continuous integration test
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ResourceAttributeItem_UT()
        {        
            //Get a list of attributes
            List<Attribute> attrs = new List<Attribute>(Attribute.GetCustomAttributes(typeof(Item)));
            string resourceName = "";

            //Iterate through attributes and then find the one to produce the the resource
            foreach (Attribute attr in attrs)
            {
                if (attr is Resource)
                {
                    Resource a = (Resource)attr;
                    resourceName = a.resourceName;
                }
            }
            //Check and make sure the resourcename found is item
            Assert.IsTrue(resourceName.CompareTo("item")==0);
        }

        //This is an improved version of the above test that runs much faster
        [TestMethod]
        public void ResourceAttributeItemForObject_UT()
        {
            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            string resourceName = "";

            //Grab the resource attribute from the test item object rather than iterate through it
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


        //This code generates the url for an object given the mark up.
        [TestMethod]
        public void urlItemClass_UT()
        {
            string baseUrl = @"/api";
            string expectEdUrl = @"/api/item/4";

            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            //Get the 2 components
            string resourceName = testItem.GetType().GetCustomAttribute<Resource>().resourceName;
            string resourceKey = testItem.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ResourceKey))).FirstOrDefault().GetValue(testItem, null).ToString();

            //Bring everything together
            string testUrl = String.Format(baseUrl + "/{0}/{1}", resourceName, resourceKey);

            Assert.IsTrue(expectEdUrl.CompareTo(testUrl) == 0);
        }


        //Get the URL for orderline test object
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

        //Put the url code into a factory class and test it
        [TestMethod]
        public void urlTestFactoryOrderLine_UT()
        {
            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";
            string expectEdUrl = @"/api/item/4";

            UrlFactory urlGenerator = new UrlFactory(@"/api");
            string testUrl = urlGenerator.generateUrl(testItem);

            Assert.IsTrue(expectEdUrl.CompareTo(testUrl) == 0);

        }

        //generate the url for a relate resource
        [TestMethod]
        public void urlForRelatedResource_UT()
        {
            string expectEdUrl = @"/api/item/4";
            string testURL = "";

            Item testItem = new Item();
            testItem.ItemId = 4;
            testItem.Name = "Test item";

            OrderLine testOrderLine = new OrderLine();
            testOrderLine.OrderLineId = 182;
            testOrderLine.OrderItem = testItem;

            List<PropertyInfo> propList = testOrderLine.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(RelatedResource))).ToList<PropertyInfo>();

            foreach (PropertyInfo p in propList)
            {
                UrlFactory urlGenerator = new UrlFactory(@"/api");
                testURL =  urlGenerator.generateUrl(p.GetValue(testOrderLine, null));
                 
            }

            Assert.IsTrue(expectEdUrl.CompareTo(testURL) == 0);

        }


    }
}
