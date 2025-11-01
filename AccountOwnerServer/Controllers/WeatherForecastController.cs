using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepositoryWrapper _repoWrapper;

    public WeatherForecastController(IRepositoryWrapper repoWrapper)
    {
        _repoWrapper = repoWrapper;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            // Testando sem acessar o banco de dados por enquanto
            return Ok(new
            {
                message = "Repository Pattern Working!",
                status = "Success - No Database Connection Required",
                repositoryInjected = _repoWrapper != null,
                timestamp = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("test-db")]
    public IActionResult TestDatabase()
    {
        try
        {
            // Demonstração do uso dos repositórios (só quando quiser testar com DB)
            var allOwners = _repoWrapper.Owner.FindAll(false);
            var domesticAccounts = _repoWrapper.Account.FindByCondition(a => a.AccountType.Equals("Domestic"), false);

            return Ok(new
            {
                message = "Database Connection Working!",
                ownersCount = allOwners.Count(),
                domesticAccountsCount = domesticAccounts.Count()
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Database Error: {ex.Message}");
        }
    }
}
