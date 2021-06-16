using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Guardium.Server.Model;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Guardium.Server.UnitTest
{
    [TestClass]
    public class PageManagerTest
    {
        IAppManager appmgr;
        IAuthorizationManager authmgr;
        IPageManager pagemgr;


        [TestMethod]
        public async Task PageManager_AllowMaxElementsPerUser()
        {
            await Setup();

            var user1 = new User("user1");
            var page1 = appmgr.CreateOrGetExistingPage(user1, "uuid1");

            for(int i=0;i< authmgr.MaxElementsPerDay; i++)
                await pagemgr.AddElement(page1, user1, new ElementContent() { ElementIdentifier = $"e{i}", Content = $"Square{i}" });
            
            Assert.IsTrue(page1.ListOfElements.Count == authmgr.MaxElementsPerDay);

            try
            {
                await pagemgr.AddElement(page1, user1, new ElementContent() { ElementIdentifier = $"e100", Content = $"Square100" });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(PageManagerException));
            }

         }

       [TestMethod]
        public async Task PageManager_OnlyPageOwnerAllowDelete()
        {
            await Setup();

            var user1 = new User("user1");
            var page1 = appmgr.CreateOrGetExistingPage(user1, "uuid1");
            await pagemgr.AddElement(page1, user1, new ElementContent() { ElementIdentifier = "e1", Content = "Square" });

            Assert.IsTrue(page1.ListOfElements.Count == 1);

            var user2 = new User("user2");
            var page2 = appmgr.CreateOrGetExistingPage(user2, "uuid1");
            await pagemgr.AddElement(page2, user2, new ElementContent() { ElementIdentifier = "e2", Content = "Circle" });

            Assert.IsTrue(page2.ListOfElements.Count == 2);

            try
            {
                await pagemgr.DeleteElement(page2, user2, "e2");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(PageManagerException));
            }


            await pagemgr.DeleteElement(page2, user1, "e2");
            Assert.IsTrue(page2.ListOfElements.Count == 1);

        }



        private async Task Setup()
        {
            appmgr = new AppManager();
            authmgr = new AuthorizationManager(appmgr);
            var notifier = new Mock<INotifier>();
            notifier.Setup(p => p.NotifyClientsOnAdd("", new ElementContent())).Returns(Task.CompletedTask);
            notifier.Setup(p => p.NotifyClientsOnDelete("", "")).Returns(Task.CompletedTask);
            pagemgr = new PageManager(authmgr, notifier.Object);
        }
    }
}
