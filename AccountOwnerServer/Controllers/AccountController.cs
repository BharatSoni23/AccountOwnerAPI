using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Account;
using Octokit;
using Stripe;
using Stripe.FinancialConnections;
using System.Security.Principal;

namespace AccountOwnerServer.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AccountController(ILoggerManager logger,IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAccount()
        {
            try
            {
                 var account=_repository.Account.GetAllAccount();
                _logger.LogInfo($"Returned All Account from Database");

                var accountResult=_mapper.Map<IEnumerable<AccountDto>>(account);

                return Ok(accountResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllAccount action {ex.Message}");
                return StatusCode(500, "Internal server error");
                
            }
        
        }

        [HttpGet("{Id}")]
        public IActionResult GetAccountById(Guid Id)
        {
            try
            {
                var account= _repository.Account.GetAccountById(Id);
                if(account==null)
                {
                    _logger.LogError($"Account With Account Id: {Id}, has't found in the database. ");
                    return NotFound();
                }
                _logger.LogInfo($"Returned account with Accountid {Id}");

                var accountResult = _mapper.Map<AccountDto>(account);

                return Ok(accountResult);


            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAccountById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }





        //[HttpGet]
        //public IActionResult GetAccountsForOwner(Guid ownerId)
        //{
        //    try
        //    {
        //        var accounts = _repository.Account.GetAccountByOwner(ownerId);
        //        _logger.LogInfo($"Retured all account from database");
        //        return Ok(accounts);

        //    }
        //    catch (Exception ex) 
        //    {

        //        _logger.LogError($"Something went wrong inside GetAccountsForOwner action: {ex.Message}");
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}


    }
}
