using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttributeTestLib;
using HATEOS_Lib.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System.Data.Linq;
using System.Linq;
using HATEOS_Lib.Factory;
using System.Collections;


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

        //generate the urls for a relate resource for a collection
        [TestMethod]
        public void detectRelatedResouceICollection_UT ()
        {
            //Declare test items
            Item testItem1 = new Item();
            testItem1.ItemId = 4;
            testItem1.Name = "Test item";

            Item testItem2 = new Item();
            testItem2.ItemId = 777;
            testItem2.Name = "Bacon";

            //Declare OrderLines
            OrderLine ol1 = new OrderLine(testItem1);
            ol1.OrderLineId = 1;

            OrderLine ol2 = new OrderLine(testItem2);
            ol2.OrderLineId = 24;

            //Declare new order and add hte orderlines to the orderline list
            Order newOrder = new Order();
            newOrder.OrderLines.Add(ol1);
            newOrder.OrderLines.Add(ol2);

            PropertyInfo pInfo = newOrder.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(RelatedResource))).FirstOrDefault();
            Object relatedResource = pInfo.GetValue(newOrder, null);

            //There is only one related resource in order and it is a list of orderlines. Test for IEnumberable so it can be iterated over.
            Assert.IsTrue(relatedResource is IEnumerable);
        }

        [TestMethod]
        public void getUrlsforRelatedResourceCollection_UT()
        {
             //Declare test items
            Item testItem1 = new Item();
            testItem1.ItemId = 4;
            testItem1.Name = "Test item";

            Item testItem2 = new Item();
            testItem2.ItemId = 777;
            testItem2.Name = "Bacon";

            //Declare OrderLines
            OrderLine ol1 = new OrderLine(testItem1);
            ol1.OrderLineId = 1;

            OrderLine ol2 = new OrderLine(testItem2);
            ol2.OrderLineId = 24;


            //Declare new order and add hte orderlines to the orderline list
            Order newOrder = new Order();
            newOrder.OrderLines.Add(ol1);
            newOrder.OrderLines.Add(ol2);

            List<string> urlsForOrderLines = new List<string>();



            PropertyInfo pInfo = newOrder.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(RelatedResource))).FirstOrDefault();
            Object relatedResource = pInfo.GetValue(newOrder, null);

            if (relatedResource is IEnumerable)
            {
                foreach(var ele in (IEnumerable)relatedResource)
                {
                    UrlFactory urlGenerator = new UrlFactory(@"/api");
                    string url = urlGenerator.generateUrl(ele);
                    urlsForOrderLines.Add(url);

                }
            }

            foreach (string olURL in urlsForOrderLines)
            {
                string expectEdUrl1 = @"/api/orderlines/1";
                string expectEdUrl2 = @"/api/orderlines/24";

                if(olURL.CompareTo(expectEdUrl1) != 0 && olURL.CompareTo(expectEdUrl2) !=0)
                {
                    Assert.Fail("Urls are incorrect");
                }
            }
            


        }


    }
}
