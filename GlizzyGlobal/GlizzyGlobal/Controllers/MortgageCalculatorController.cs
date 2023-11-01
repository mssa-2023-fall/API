using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MortgageCalculatorLibrary;
using System.Collections.Generic;

namespace GlizzyGlobal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[ServiceFilter(typeof(MortgageLoggingFilter))]
    public class MortgageController : ControllerBase
    {
        private readonly ILogger<MortgageController> _logger;

        #region Waiting for MortgageCalculatorLibrary 1.1 release
        //private readonly ICurrencyConverter _currencyConverter;
        #endregion 

        //Added this for logging, don't know what its doing. Came from WeatherForeCastController.cs(null)
        public MortgageController(ILogger<MortgageController> logger)
        {
            _logger = logger;
          //_currencyConverter = currencyConverter ?? throw new ArgumentNullException(nameof(currencyConverter));
        }

        //Endpoint to create a new mortgage.
        [HttpGet("Calculate Your Payment Here")]
        public ActionResult<decimal> CalculateMonthlyPayments(
           [FromQuery] decimal interestRate,
           [FromQuery] decimal principalAmount,
           [FromQuery] DateTime originationDate,
           [FromQuery] int loanTerm,
           [FromQuery] decimal monthlyEscrow)
        {
            var mortgage = new Mortgage(interestRate, principalAmount, originationDate, loanTerm, monthlyEscrow);
            _logger.LogInformation($"Inputs - InterestRate: {interestRate}, PrincipalAmount: {principalAmount}, " +
                $"OriginationDate: {originationDate}, LoanTerm: {loanTerm}, " +
                $"MonthlyEscrow: {monthlyEscrow}");
            return Ok(mortgage.CalculateMonthlyPayment());
        }
      /*  #region Waiting for MortgageCalculatorLibrary 1.1 release
        [HttpGet("Convert Global Currency Rates:")]
        public ActionResult<Money> ConvertCurrency(
            [FromQuery] string convertFromThisCurrency,
            [FromQuery] string convertToThisCurrency,
            [FromQuery] decimal amount)
        {
            _logger.LogInformation($"Converting {amount} from {convertFromThisCurrency}, to {convertToThisCurrency}");
            var convertedAmount = _currencyConverter.Convert(convertFromThisCurrency, convertToThisCurrency);
            return Ok(convertedAmount);
        }
        #endregion
      */

        [HttpPost("Create a mortgage (deprecated)")]
        public ActionResult<Mortgage> CreateMortgage(Mortgage mortgage)
        {
            
            return Ok(mortgage);
        }
    }









    //Added this for logging, don't know what its doing.
    public class MortgageLoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        public MortgageLoggingFilter(ILogger<MortgageLoggingFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Executing a mortgage action!");
        }

    }

}
