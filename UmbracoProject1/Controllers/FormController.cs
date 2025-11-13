using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoProject1.Services;
using UmbracoProject1.ViewModels;
namespace UmbracoProject1.Controllers;

public class FormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, FormSubmissionsService formSubmissions, IEmailService emailService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    private readonly FormSubmissionsService _formSubmissions = formSubmissions;
    private readonly IEmailService _emailService = emailService;

    [HttpPost]
    public async Task<IActionResult> HandleCallbackForm(CallbackFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = _formSubmissions.SaveCallbackRequest(model);
        if (!result)
        {
            TempData["FormError"] = "Something went wrong while submitting your request. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }

        await _emailService.SendEmailAsync(model.Email);

        TempData["FormSuccess"] = "Thank you! Your request had been recived and we will get back to you soon";
        return RedirectToCurrentUmbracoPage();
    }

    [HttpPost]
    public async Task<IActionResult> HandleSupportForm(SupportFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = _formSubmissions.SaveSupportRequest(model);
        if (!result)
        {
            TempData["FormError"] = "Something went wrong while submitting your request. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }

        await _emailService.SendEmailAsync(model.Email);

        TempData["FormSuccess"] = "Thank you! Your request had been recived and we will get back to you soon";
        return RedirectToCurrentUmbracoPage();
    }

    [HttpPost]
    public async Task<IActionResult> HandleQuestionForm(QuestionFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = _formSubmissions.SaveQuestionRequest(model);
        if (!result)
        {
            TempData["FormError"] = "Something went wrong while submitting your request. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }

        await _emailService.SendEmailAsync(model.Email);

        TempData["FormSuccess"] = "Thank you! Your request had been recived and we will get back to you soon";
        return RedirectToCurrentUmbracoPage();
    }
}
