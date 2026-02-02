using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Sum(decimal firstNumber, decimal secondNumber)
        {
            return Ok(_mathService.Sum(firstNumber, secondNumber));
            
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(decimal firstNumber, decimal secondNumber)
        {
            return Ok(_mathService.Subtraction(firstNumber, secondNumber));

        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(decimal firstNumber, decimal secondNumber)
        {
            return Ok(_mathService.Multiplication(firstNumber, secondNumber));

        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(decimal firstNumber, decimal secondNumber)
        {
            return Ok(_mathService.Division(firstNumber, secondNumber));

        }

        [HttpGet("square-root/{number}")]
        public IActionResult SquareRoot(decimal number)
        {
            return Ok(_mathService.SquareRoot(number));

        }

        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(decimal firstNumber, decimal secondNumber)
        {
            return Ok(_mathService.Mean(firstNumber, secondNumber));

        }




       
    }
}
