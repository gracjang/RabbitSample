using Microsoft.AspNetCore.Mvc;
using RabbitSample.Banking.Application.Models;
using RabbitSample.Banking.Application.Services.Interfaces;

namespace RabbitSample.Banking.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BankingController : Controller
  {
    private readonly IAccountService _accountService;

    public BankingController(IAccountService accountService)
    {
      _accountService = accountService;
    }

    // GET api/banking
    [HttpGet]
    public IActionResult Get()
    {
      return Json(_accountService.GetAccounts());
    }

    [HttpPost]
    public IActionResult Post([FromBody] AccountTransfer accountTransfer)
    {
      _accountService.Transfer(accountTransfer);
      return Ok();
    }
  }
}