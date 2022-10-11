using Aspose.Pdf;
using Microsoft.AspNetCore.Mvc;
using PdfEditor.Models;
using System.Diagnostics;
using System.Text;

namespace PdfEditor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public FileResult Index(string editor1)
        {
            // criar um nome de arquivo exclusivo
            string fileName = Guid.NewGuid() + ".pdf";

            // converter texto HTML em fluxo
            byte[] byteArray = Encoding.UTF8.GetBytes(editor1);

            // gerar PDF a partir do HTML
            MemoryStream stream = new MemoryStream(byteArray);
            HtmlLoadOptions options = new HtmlLoadOptions();
            Document pdfDocument = new Document(stream, options);

            // criar fluxo de memória para o arquivo PDF
            Stream outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);

            // retornar arquivo PDF gerado
            return File(outputStream, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}