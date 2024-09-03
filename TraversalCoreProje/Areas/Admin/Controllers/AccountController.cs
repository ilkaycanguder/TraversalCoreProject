using BusinessLayer.Abstract.AbstractUow;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PopulateAccountDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Index(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var valueSender = _accountService.TGetByID(model.SenderID);
                var valueReceiver = _accountService.TGetByID(model.ReceiverID);

                if (valueSender != null && valueReceiver != null && valueSender.Balance >= model.Amount)
                {
                    valueSender.Balance -= model.Amount;
                    valueReceiver.Balance += model.Amount;

                    List<Account> modifiedAccounts = new List<Account>()
                {
                    valueSender,
                    valueReceiver
                };

                    _accountService.TMultiUpdate(modifiedAccounts);
                    ViewBag.Message = "Transfer successful!";
                }
                else
                {
                    ViewBag.Message = "Transfer failed. Check the account details or balance.";
                }
            }
            else
            {
                ViewBag.Message = "Please fill out the form correctly.";
            }

            PopulateAccountDropdowns();
            return View(model);
        }

        private void PopulateAccountDropdowns()
        {
            var accounts = _accountService.TGetAll(); 
            var accountSelectList = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountID.ToString(),
                Text = a.Name 
            }).ToList();

            ViewBag.Accounts = accountSelectList;
        }
    }
}
