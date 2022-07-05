using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services;
using konkeror.app.Services.Interface;
using konkeror.web.Common;

namespace konkeror.web.Controllers
{
    public class TransactionsController : ApiController
    {
        private ITransactionService TransactionService { get; }
        public TransactionsController(ITransactionService transactionService)
        {
            TransactionService = transactionService;
        }

        [HttpPost]
        [Route("api/transations/register")]
        public IHttpActionResult Register([FromBody] TransactionModel transaction)
        {
            try
            {
                var r = TransactionService.Register(transaction);
                return Ok(r);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPatch]
        [Route("api/transations/report")]
        public IHttpActionResult Report(string transactionId)
        {
            try
            {
                var r = TransactionService.Report(transactionId);
                return Ok(r);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
