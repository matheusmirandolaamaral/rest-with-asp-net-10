namespace RestWithAspNet10.Model
{
    public class Operation
    {
        public decimal First { get; set; }
        public decimal Second { get; set; }

        
        public Operation(decimal first, decimal second)
        {
            First = first;
            Second = second;
        }

       

        public decimal Sum()
        {
            decimal soma = First + Second;
            return soma;
        }
    }
}
