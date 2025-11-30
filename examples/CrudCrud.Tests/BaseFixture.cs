using System.Text.RegularExpressions;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using CrudCrud.API;
using Karpatium.Core.RestApi;
using NUnit.Framework;

[assembly:LevelOfParallelism(5)]

namespace CrudCrud.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.Fixtures)]
public abstract class BaseFixture
{
    protected CrudCrudApis CrudCrudApis = null!;
    
    [AllureBefore]
    [OneTimeSetUp]
    public async Task BaseOneTimeSetUp()
    {
        RestApiResponse response = await new RestApiClient("https://crudcrud.com").GetAsync(new RestApiRequest("/"));
        
        Match baseUrlMatch = Regex.Match(response.Content ?? throw new InvalidOperationException("Response content is null."),
            @"https://crudcrud\.com/api/[0-9a-fA-F]+",
            RegexOptions.IgnoreCase);

        if (!baseUrlMatch.Success)
        {
            throw new InvalidOperationException("Base URL not found.");
        }
        
        CrudCrudApis = new CrudCrudApis(baseUrlMatch.Value);
    }
}