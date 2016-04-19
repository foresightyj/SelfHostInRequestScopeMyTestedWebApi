using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTested.WebApi;
using SelfHostInRequestScopeMyTestedWebApi;
using SelfHostInRequestScopeMyTestedWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

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