using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Extensions;
using Umbraco_V9.Core.ViewModel;

namespace Umbraco_V9.Core.Controller
{
    public class ContactController : SurfaceController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly ILogger<ContactController> _logger;

        public ContactController(
            IUmbracoContextAccessor umbracoContextAccessor, 
            IUmbracoDatabaseFactory databaseFactory, 
            ServiceContext services, 
            AppCaches appCaches, 
            IProfilingLogger profilingLogger, 
            IPublishedUrlProvider publishedUrlProvider,
            UmbracoHelper umbracoHelper,
            ILogger<ContactController> logger) 
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _umbracoHelper = umbracoHelper;
            _logger = logger;
        }

        public IActionResult RenderContactForm()
        {
            var vm = new ContactFormViewModel();
            return PartialView("~/Views/Partials/Contact Form.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Please check the form.");
                return CurrentUmbracoPage();
            }

            try
            {
                //Create a new contact form in umbraco
                var contactForms = _umbracoHelper.ContentAtRoot().DescendantsOrSelfOfType("contactForms").FirstOrDefault();

                if (contactForms != null)
                {
                    var newContact = Services.ContentService.Create("Contact", contactForms.Id, "contactForm");

                    newContact.SetValue("contactName", vm.Name);
                    newContact.SetValue("contactEmail", vm.EmailAddress);
                    newContact.SetValue("contactSubject", vm.Subject);
                    newContact.SetValue("contactComments", vm.Comment);

                    Services.ContentService.SaveAndPublish(newContact);
                }


                //Send out an email to site admin

                //Return confirmation message to user
                TempData["Success"] = "OK";

                return RedirectToCurrentUmbracoPage();
            }
            catch (Exception exc)
            {
                _logger.LogError("There was an error in the contact form submission", exc.Message);
                ModelState.AddModelError("Error", "Sorry there was a problem nothing your details. Would you please try again later?");

            }

            return CurrentUmbracoPage();
        }
    }
}
