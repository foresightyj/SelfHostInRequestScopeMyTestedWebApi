using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTested.WebApi;
using System.Net;
using System.Net.Http;

namespace SelfHostInRequestScopeMyTestedWebApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        private IServerBuilder _server;

        [TestInitialize]
        public void Init()
        {
            _server = MyWebApi.Server().Starts<Startup>();
        }

        [TestMethod]
        public void Get()
        {
            _server
                .WithHttpRequestMessage(r => r
                    .WithMethod(HttpMethod.Get)
                    .WithRequestUri("/api/values/13")
                )
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<int>()
                .Passing(m => Assert.AreEqual(3, m))
                .WithStatusCode(HttpStatusCode.OK);
        }

        [TestCleanup]
        public void Clean()
        {
            MyWebApi.Server().Stops();
        }
    }
}
