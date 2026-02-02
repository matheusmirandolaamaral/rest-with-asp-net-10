using Microsoft.AspNetCore.Http.HttpResults;

namespace RestWithAspNet10.Service
{
    public class MathService
    {
        public decimal Sum(decimal firstNumber,  decimal secondNumber)
        {
            return firstNumber + secondNumber; 
        }
        public decimal Subtraction(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber - secondNumber;
        }
        public decimal Multiplication(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber * secondNumber;
        }
        public decimal Division(decimal firstNumber, decimal secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new DivideByZeroException("Operation invalid");
            }
            else
            {
                return firstNumber / secondNumber;
            }
        }
        public double SquareRoot(decimal number)
        {
            if(number < 0)
            {
                throw new ArgumentOutOfRangeException("Operation invalid");
            }
            else
            {
                return Math.Sqrt((double)number);
            }
        }
        public decimal Mean(decimal firstNumber, decimal secondNumber)
        {
            return (firstNumber + secondNumber) / 2;
        }


    }
}
