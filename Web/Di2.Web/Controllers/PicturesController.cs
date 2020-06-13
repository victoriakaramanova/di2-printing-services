namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Di2.Common;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class PicturesController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IPictureService pictureService;

        public PicturesController(IPictureService pictureService, IOrderService orderService)
        {
            this.pictureService = pictureService;
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPictures(string id)
        {
            try
            {
                var serviceItem = this.orderService.GetById<OrderViewModel>(id);
                if (serviceItem == null || (serviceItem.Orderer.UserName != this.User.Identity.Name &&
                    !this.User.IsInRole(GlobalConstants.AdministratorRoleName)))
                {
                    return this.NotFound();
                }

                var uploads = await this.pictureService.Upload((ICollection<IFormFile>)this.Request.Form.Files, id);
                var urls = uploads.Select(p => p.SecureUri.AbsoluteUri).ToList();
                return this.Json(new { urls });
            }
            catch (Exception)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.NotFound();
            }
        }
    }
}
