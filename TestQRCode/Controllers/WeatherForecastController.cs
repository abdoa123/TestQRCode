using Microsoft.AspNetCore.Mvc;
using IronBarCode;
using System.Drawing;

namespace TestQRCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("GenerateQRCode")]
        public IActionResult GenerateQrCode([FromQuery] string deskNumber , string? userName = "" , string? Email = "")
        {
            if (string.IsNullOrEmpty(deskNumber))
            {
                return BadRequest("deskNumber parameter is required.");
            }
            // Create the QR code content with the deskNumber parameter
            string link = $"https://login.microsoftonline.com/{deskNumber}";

            QRCodeLogo qrCodeLogo = new QRCodeLogo("logo.png");
            GeneratedBarcode myQRCodeWithLogo = QRCodeWriter.CreateQrCodeWithLogo(link, qrCodeLogo);
            myQRCodeWithLogo.ResizeTo(150, 150).SetMargins(10).ChangeBackgroundColor(Color.Transparent).ChangeBarCodeColor(Color.Black);
            // Logo will automatically be sized appropriately and snapped to the QR grid.
            myQRCodeWithLogo.SaveAsPng("myQRWithLogo.png");

            // Generate a Simple BarCode image and save as PDF
           // QRCodeWriter.CreateQrCode(link, 150, QRCodeWriter.QrErrorCorrectionLevel.High).SaveAsPng("MyQR.png");
            return Ok();    



        }
    }
}
