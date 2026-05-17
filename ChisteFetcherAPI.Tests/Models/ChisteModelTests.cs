using ChisteFetcherAPI.Models;

namespace ChisteFetcherAPI.Tests.Models;

public class ChisteModelTests
{
    [Fact]
    public void ChisteModel_StoresPropertyValues()
    {
        var model = new ChisteModel
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
