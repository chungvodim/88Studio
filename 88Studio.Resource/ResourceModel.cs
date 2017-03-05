using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _88Studio.Resource
{
    public class ResourceModel
    {
        public int ID { get; set; }
        public string LanguageCode { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public static class LanguageCode
    {
        public static string Default { get { return Nl2dehandsCulture; } }
        //public const string EngCode = "en-us";
        public const string Fr2dehandsCulture = "fr_BE";
        public static string FrStandardCulture
        {
            get
            {
                return _2dehandsToStandard(Fr2dehandsCulture);
            }
        }
        public const string Nl2dehandsCulture = "nl_BE";
        public static string NlStandardCulture
        {
            get
            {
                return _2dehandsToStandard(Nl2dehandsCulture);
            }
        }

        public static string StandardTo2dehands(string code)
        {
            return code.Replace("-", "_");
        }

        public static string _2dehandsToStandard(string code)
        {
            return code.Replace("_", "-");
        }

        public static bool IsSupported(string languageCode)
        {
            return languageCode == Fr2dehandsCulture || languageCode == Nl2dehandsCulture;
        }

        public static string GetBeautifulName(string languageCode)
        {
            switch (languageCode)
            {
                case LanguageCode.Fr2dehandsCulture:
                    return "French";
                default:
                    return "Dutch";
            }
        }
    }
}
