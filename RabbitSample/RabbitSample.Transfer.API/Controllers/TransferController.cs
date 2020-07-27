using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RabbitSample.Transfer.Application.Services.Interfaces;
using RabbitSample.Transfer.Domain.Models;

namespace RabbitSample.Transfer.API.Controllers
{
  [Route("api/[controller]")]
  public class TransferController : Controller
  {
    private readonly ITransferService _transferService;


    public TransferController(ITransferService transferService)
    {
      _transferService = transferService;
    }

    // GET
    [HttpGet]
    public IActionResult Index()
    {
      return Json(_transferService.GeTransferLogs());
    }
  }
}