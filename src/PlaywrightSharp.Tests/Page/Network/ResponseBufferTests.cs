using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PlaywrightSharp.Tests.BaseTests;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.Page.Network
{
    ///<playwright-file>network.spec.js</playwright-file>
    ///<playwright-describe>Response.buffer</playwright-describe>
    public class ResponseBufferTests : PlaywrightSharpPageBaseTest
    {
        internal ResponseBufferTests(ITestOutputHelper output) : base(output)
        {
        }

        ///<playwright-file>network.spec.js</playwright-file>
        ///<playwright-describe>Response.buffer</playwright-describe>
        ///<playwright-it>should work</playwright-it>
        [Fact]
        public async Task ShouldWork()
        {
            var response = await Page.GoToAsync(TestConstants.ServerUrl + "/pptr.png");
            byte[] imageBuffer = File.ReadAllBytes("./Assets/pptr.png");
            Assert.Equal(imageBuffer, await response.GetBufferAsync());
        }

        ///<playwright-file>network.spec.js</playwright-file>
        ///<playwright-describe>Response.buffer</playwright-describe>
        ///<playwright-it>should work with compression</playwright-it>
        [Fact]
        public async Task ShouldWorkWithCompression()
        {
            Server.EnableGzip("/pptr.png");
            var response = await Page.GoToAsync(TestConstants.ServerUrl + "/pptr.png");
            byte[] imageBuffer = File.ReadAllBytes("./Assets/pptr.png");
            Assert.Equal(imageBuffer, await response.GetBufferAsync());
        }
    }
}
