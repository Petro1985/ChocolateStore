using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ChocolateBackEnd.APIStruct;
using FluentAssertions;
using Xunit;

namespace ChocolateBackEnd.Tests;

public class FullStackTest
{
    [Fact]
    public async Task Test1()
    {
        var chocolateApp = new MyWebSite();
        var serviceProvider = chocolateApp.Services;

        var appClient = chocolateApp.CreateDefaultClient();

        var productList = new List<ProductAddRequest>
        {
            new ProductAddRequest
            {
                Description = "The TANK!!!!!", Price = 888, TimeToMakeInHours = 12
            },
            new ProductAddRequest
            {
                Description = "The TANK 2!!!!!", Price = 333, TimeToMakeInHours = 23
            },
            new ProductAddRequest
            {
                Description = "The TANK 3!!!!!", Price = 66666, TimeToMakeInHours = 342
            },
            new ProductAddRequest
            {
                Description = "The TANK 4!!!!!", Price = 999, TimeToMakeInHours = 48
            },
        };
        
        foreach (ProductAddRequest productAddRequest in productList)
        {
            var httpAddResponse = await appClient.PostAsJsonAsync("Products", productAddRequest);
            httpAddResponse.Should().Be200Ok();
        }
        
        var httpResponse = await appClient.GetAsync("Products");

        httpResponse.Should().Be200Ok();

        var content = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<ProductResponse>>();
        
        content!.Should().HaveCount(4);
    }
}