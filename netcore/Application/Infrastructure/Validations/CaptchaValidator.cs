using System;
using System.Net.Http;
using Application.Exceptions;
using Application.Interfaces.IServices;
using FluentValidation;
using Microsoft.Extensions.Configuration;
// using Newtonsoft.Json.Linq;

namespace Application.Infrastructure.Validations
{

    /// <summary>
    /// Contains the neccessary information for verifying the captcah
    /// </summary>
    public class CaptchaCredentials
    {

        /// <summary>
        /// The google captcha response
        /// </summary>
        /// <value></value>
        public string GoogleCaptcha { get; set; }
    }

    /// <summary>
    /// Fluent validation for Captcha
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CaptchaValidator<T> : AbstractValidator<T> where T : CaptchaCredentials
    {
        // private ICaptchaService _captcha;

        /// <summary>
        /// COnstructor
        /// </summary>
        /// 
        public CaptchaValidator(IConfiguration configuration)
        {

            // RuleFor(x => x.Id).NotEmpty().WithMessage("Captcha Id required");

            // RuleFor(x => x.Code).NotEmpty().WithMessage("Captcha is required")
            //                     .Must((x, code) => _captcha.Ishuman(x.Id, x.Code)).WithMessage("Invalid Captcha");

            RuleFor(x => x.GoogleCaptcha).NotEmpty().WithMessage("Captcha is required")
                                .Must((x, code) => CaptchaValitions.GoogleReCaptchaPassed(configuration, x.GoogleCaptcha)).WithMessage("Invalid Captcha");
        }
    }

    /// <summary>
    /// Static class that performs the actual verification
    /// </summary>
    public static class CaptchaValitions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="gRecaptchaResponse"></param>
        /// <returns></returns>
        public static bool GoogleReCaptchaPassed(IConfiguration configuration, string gRecaptchaResponse)
        {

            var envSecret = Environment.GetEnvironmentVariable("GOOGLE_RECAPTCHA_SECRET");

            var secret = envSecret == null ? configuration.GetSection("GoogleReCaptcha:secret").Value : envSecret;

            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            string JSONres = res.Content.ReadAsStringAsync().Result;

            // throw new CustomMessageException(JSONres);

            // dynamic JSONdata = JObject.Parse(JSONres);

            // if (JSONdata.success != "true")
            // {
            //     return false;
            // }

            return true;
        }

    }
}