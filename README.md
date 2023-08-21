### Prerequisites
[.NET Core SDK 7.0](https://dotnet.microsoft.com/en-us/download)

### Running
- To run this application, go to folder [src/TaxCalculate.Api](src/TaxCalculate.Api) and execute the command:    
    `dotnet run`

### Running the tests
- Open the command prompt 
- Go to folder [test/TaxCalculate.Tests](test/TaxCalculate.Tests) 
- Execute the command:
    `dotnet test`

### Sample Requests

Calculate prices from gross value:

```json
{
    "grossValue": "12.00",
    "vatRate": "20"
}
```

Calculate prices from net value:

```json
{
    "netValue": "10.00",
    "vatRate": "20"
}
```

Calculate prices from vat value:

```json
{
    "vatValue": "2.21",
    "vatRate": "13"
}
```