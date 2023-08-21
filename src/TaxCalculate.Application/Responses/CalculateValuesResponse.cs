namespace TaxCalculate.Application.Responses
{
    public class CalculateValuesResponse
    {
        public CalculateValuesResponse(decimal grossValue, decimal netValue, decimal vatValue, int vatRate)
        {
            GrossValue = grossValue;
            NetValue = netValue;
            VatValue = vatValue;
            VatRate = vatRate;
        }

        public decimal GrossValue { get; }

        public decimal NetValue { get; }
        
        public decimal VatValue { get; }
        
        public int VatRate { get; }
    }
}
