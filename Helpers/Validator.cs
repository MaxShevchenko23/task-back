using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text;
using System.Text.RegularExpressions;

namespace url_shortener_server.Helpers
{
    public static class Validator
    {
        static string linkPattern = @"^(https?:\/\/)?(www\.)?[a-zA-Z0-9@:%._\+~#?&//=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%._\+~#?&//=]*)$";
        static string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,6}$";
        static string passwordPattern = @"(?=.*[A-Z])(?=.*\W)(?=(?:.*\d){3,})";


        public static bool IsValid(this string source, ToValidate toValidate)
        {
            if (source == null || string.IsNullOrEmpty(source))
            {
                return false;
            }

            string pattern;

            switch (toValidate)
            {
                case ToValidate.Email:
                    pattern = emailPattern;
                    break;
                case ToValidate.Password:
                    pattern = passwordPattern;
                    break;
                case ToValidate.Link:
                    pattern = linkPattern;
                    break;
                default:
                    return false;
            }

            return Regex.IsMatch(source, pattern);
        }
        //public static bool IsValidLink(this string source)
        //{
        //    if (source == null || string.IsNullOrEmpty(source))
        //    {
        //        return false;
        //    }

        //    if (!Regex.IsMatch(source, linkPattern))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        //public static bool IsValidEmail(this string source)
        //{
        //    if (source == null || string.IsNullOrEmpty(source))
        //    {
        //        return false;
        //    }

        //    if (!Regex.IsMatch(source, emailPattern))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool IsValidPassword(this string source)
        //{
        //    if (source == null || string.IsNullOrEmpty(source))
        //    {
        //        return false;
        //    }

        //    if (!Regex.IsMatch(source, passwordPattern))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        public static string GetCode(this string source)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(7);
        }

    }

    public enum ToValidate 
    { 
        Email,
        Password,
        Link
    }

}
