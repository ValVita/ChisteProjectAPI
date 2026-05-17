using ChisteFetcherAPI.Models;

namespace ChisteConsumerAPI.Tests.Models;

public class ChisteModelCTests
{
    [Fact]
    public void ChisteModelC_StoresPropertyValues()
    {
        var model = new ChisteModelC
        {
            Id = "id-1",
            Value = "valor",
            Url = "https://example.com"
        };

        Assert.Equal("id-1", model.Id);
        Assert.Equal("valor", model.Value);
        Assert.Equal("https://example.com", model.Url);
    }
}
