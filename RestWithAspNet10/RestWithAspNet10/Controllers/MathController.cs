using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Model;
using RestWithAspNet10.Service;
using System.Reflection.Metadata;

namespace RestWithAspNet10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {

        private readonly MathService _mathService;
        
        public MathController(MathService mathService)
        {
            _mathService = mathService;
            

        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult GetSum(string firstNumber, string secondNumber)
        {
            if (_mathService.IsNumeric(firstNumber) && _mathService.IsNumeric(secondNumber))
            {
               var number1= _mathService.ConvertToDecimal(firstNumber);
               var number2= _mathService.ConvertToDecimal(secondNumber);
                Operation operation = new(number1, number2);
               var soma= operation.Sum();
                return Ok(soma);
            }

            return Ok("Invalid input");
        }




    }







}

