namespace TaxCalculate.Services.DTOs
{
    public  class CalculateValuesDto
    {
        public CalculateValuesDto(decimal? grossValue, decimal? netValue, decimal? vatValue, int vatRate)
        {
            GrossValue = grossValue;
            NetValue = netValue;
            VatValue = vatValue;
            VatRate = vatRate;
        }

        public decimal? GrossValue { get; }

        public decimal? NetValue { get; }
        
        public decimal? VatValue { get; }
        
        public int VatRate { get; }
    }
}
