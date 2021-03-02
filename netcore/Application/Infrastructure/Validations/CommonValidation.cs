using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Entities.Inheritable;

namespace Application.Infrastructure.Validations
{
    /// <summary>
    /// Common validation used within the application
    /// </summary>
    public static class CommonValidation
    {
        /// <summary>
        /// Server side to check if the password is valid
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool BeAValidPassword(string password) {

            if(string.IsNullOrEmpty(password))
                return false;

            var validator = new Regex("^(?=.*[a-z])(?=.*[A-Z])(.{6,128})$");
            return validator.Match(password).Success;
        }

        /// <summary>
        /// Error message to show if the right password is not selected
        /// </summary>
        public static string ValidPasswordErrorMessage = "One lowercase and uppercase";

        /// <summary>
        /// Must be either m = male, f = female or o = other
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static bool BeAValidateGender(string gender)
        {
            return gender == "m" || gender == "f" || gender == "o";
        }

        /// <summary>
        /// Check if string is a number
        /// </summary>
        /// <param name="supposedNumber"></param>
        /// <returns></returns>
        public static bool BeAValidateNumber(string supposedNumber)
        {
            if (Int32.TryParse(supposedNumber, out int j))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Check if string is a url
        /// </summary>
        /// <param name="supposedUrl"></param>
        /// <returns></returns>
        public static bool BeAValidateUrl(string supposedUrl)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(supposedUrl, UriKind.RelativeOrAbsolute, out uriResult);
            // throw new Exception(uriResult.IsWellFormedOriginalString().ToString());
                // && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
            // return Uri.IsWellFormedUriString(supposedUrl, UriKind.RelativeOrAbsolute);
        }


        /// <summary>
        /// Check if string is an application type gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static bool BeAValidGender(string gender = "")
        {
            gender = gender.ToLower();
            return gender == "m" || gender == "f" || gender == "o";
        }

        /// <summary>
        /// Check if start date is before end date
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool StartIsBeforeEnd(DateTime startDate, DateTime endDate)
        {
            return endDate > startDate;
        }

        /// <summary>
        /// Collection should be within range
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool WithinRange(int entity, int start, int end)
        {
            if (entity < start || entity > end )
                return false;

            return true;
        }

    }
}