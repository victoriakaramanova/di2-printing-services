namespace Di2.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.IO;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Di2.Common;
    using Di2.Services;
    using Di2.Services.Data;
    using Di2.Services.Messaging;
    using Di2.Web.ViewModels;
    using Di2.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Sandbox;
    using System.Text;
    using Di2.Web.ViewModels.Materials.ViewModels;
    using System;
    using System.Net.Http;
    using System.Net;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly IMaterialsService materialsService;
        private readonly Cloudinary cloudinary;
        private readonly IEmailSender sender;

        public HomeController(
            IMaterialsService materialsService,
            Cloudinary cloudinary,
            IEmailSender sender)
        {
            this.materialsService = materialsService;
            this.cloudinary = cloudinary;
            this.sender = sender;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                Materials = this.materialsService
                    .GetAllMaterials<IndexMaterialViewModel>(),
            };

            StringBuilder sb = new StringBuilder();
            IEnumerable<IndexMaterialViewModel> materials = this.materialsService.GetAllMaterials<IndexMaterialViewModel>();
            foreach (var material in materials)
            {
                sb.AppendFormat(
                    "{0};{1};{2};{3};{4}",
                    material.Name,
                    material.Description,
                    material.ExtraInfo,
                    material.SubCategory.Name,
                    Environment.NewLine);
            }
            /*MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(sb.ToString());
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = $"materials-{DateTime.UtcNow}" };
            //result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
            var content = await result.Content.ReadAsByteArrayAsync();
           */

            var data = Encoding.UTF8.GetBytes(sb.ToString());
            var res = Encoding.UTF8.GetPreamble().Concat(data).ToArray();

            //var content = System.IO.File.ReadAllBytes(@"..\\..\\Book1.csv");
            var attachmentFileName = $"materials-{DateTime.UtcNow.ToShortDateString()}.csv";
            var mimeType = "text/csv"; // charset=UTF-8
            
            var attch = new EmailAttachment
            {
                MimeType = mimeType,
                FileName = attachmentFileName,
                Content = res,
            };
            var attchList = new List<EmailAttachment>();
            attchList.Add(attch);

            //UNMARK HERE TO SEND EMAIL!
            // await this.sender.SendEmailAsync(GlobalConstants.Email, "Я сега", "victoriakaramanova@gmail.com", $"Поръчка за {DateTime.UtcNow.ToShortDateString()}", $"Здравейте, приложена е поръчката. Поздрави - {GlobalConstants.SystemName}", attchList);



            /*var content = System.IO.File.ReadAllBytes(@"..\\..\\Book1.csv");
            var attachmentFileName = "Book1.csv";
            var memeType = "text/csv";
            var attch = new EmailAttachment
            {
                Content = result.Content,
                FileName = attachmentFileName,
                MimeType = memeType,
            };
            var attchList = new List<EmailAttachment>();
            attchList.Add(attch);

            await this.sender.SendEmailAsync(GlobalConstants.Email, "Познай от кого", "e_karamanova@abv.bg", ":)", "<li>А аз съм на върха на света :D!</li>", attchList);
            */

            return this.View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var result = await CloudinaryExtension.Upload(this.cloudinary, files);

            this.ViewBag.Links = result;
            return this.Redirect("/");
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
