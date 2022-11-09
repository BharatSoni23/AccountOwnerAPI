using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        public IRepositoryWrapper _repoWrapper;


        public WeatherForecastController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var domesticAccounts = _repoWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
            var owners = _repoWrapper.Owner.FindAll();
            //_logger.LogInfo("Here is info message from the controller.");
            //_logger.LogDebug("Here is info message from the controller. ");
            //_logger.LogWarn("Here is info message form the controller. ");
            //_logger.LogError("Here is error message forom the controller. ");
            return new string[] { "value1", "value2" };

        }


     
    }
}