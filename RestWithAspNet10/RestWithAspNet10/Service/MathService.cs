namespace RestWithAspNet10.Service
{
    public class MathService
    {
        public decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out decimalValue)) // Br and Us
            {
                return decimalValue;
            }
            return 0;
        }

        public bool IsNumeric(string strNumber)
        {
            decimal number;
            if(decimal.TryParse(strNumber, out number))
            {
                return true;
            }
            return false;

        }
    }
}
