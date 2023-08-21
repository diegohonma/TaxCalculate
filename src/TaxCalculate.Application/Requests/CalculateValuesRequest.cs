namespace TaxCalculate.Application.Requests
{
    public class CalculateValuesRequest
    {
        public CalculateValuesRequest(string? grossValue, string? netValue, string? vatValue, string? vatRate)
        {
            GrossValue = grossValue;
            NetValue = netValue;
            VatValue = vatValue;
            VatRate = vatRate;
        }

        public string? GrossValue { get; }

        public string? NetValue { get; }
        
        public string? VatValue { get; }
        
        public string? VatRate { get; }
    }
}
