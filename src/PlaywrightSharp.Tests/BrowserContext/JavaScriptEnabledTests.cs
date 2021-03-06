using System;
using System.Threading.Tasks;
using PlaywrightSharp.Tests.BaseTests;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.BrowserContext
{
    ///<playwright-file>browsercontext.spec.js</playwright-file>
    ///<playwright-describe>BrowserContext({javaScriptEnabled})</playwright-describe>
    public class JavaScriptEnabledTests : PlaywrightSharpBrowserContextBaseTest
    {
        /// <inheritdoc/>
        public JavaScriptEnabledTests(ITestOutputHelper output) : base(output)
        {
        }

        ///<playwright-file>browsercontext.spec.js</playwright-file>
        ///<playwright-describe>BrowserContext({javaScriptEnabled})</playwright-describe>
        ///<playwright-it>should work</playwright-it>
        [Fact]
        public async Task ShouldWork()
        {
            var page = await NewPageAsync(new BrowserContextOptions { JavaScriptEnabled = false });
            await page.GoToAsync("data:text/html, <script>var something = 'forbidden'</script>");

            var exception = await Assert.ThrowsAnyAsync<Exception>(async () => await page.EvaluateAsync("something"));

            if (TestConstants.IsWebKit)
            {
                Assert.Contains("Can\'t find variable: something", exception.Message);
            }
            else
            {
                Assert.Contains("something is not defined", exception.Message);
            }

            page = await NewPageAsync();
            await page.GoToAsync("data:text/html, <script>var something = 'forbidden'</script>");
            Assert.Equal("forbidden", await page.EvaluateAsync<string>("something"));
        }

        ///<playwright-file>browsercontext.spec.js</playwright-file>
        ///<playwright-describe>BrowserContext({javaScriptEnabled})</playwright-describe>
        ///<playwright-it>should be able to navigate after disabling javascript</playwright-it>
        [Fact]
        public async Task ShouldBeAbleToNavigateAfterDisablingJavascript()
        {
            var page = await NewPageAsync(new BrowserContextOptions { JavaScriptEnabled = false });
            await page.GoToAsync(TestConstants.EmptyPage);
        }
    }
}
