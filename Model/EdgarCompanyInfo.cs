using System.Text.Json.Serialization;

namespace Fora.Model
{
    public class EdgarCompanyInfo
    {
        public EdgarCompanyInfo(long cik, string? entityName)
        {
            this.Cik = cik;
            this.EntityName = entityName;
        }

        public long Cik { get; set; }

        public string CikFormated { 
            get
            {
                return string.Format("{0:D10}", Cik);
            }
        }

        public string? EntityName { get; set; }
        
        public InfoFact Facts { get; set; }
        public class InfoFact
        {
            [JsonPropertyName("us-gaap")]
            public InfoFactUsGaap UsGaap { get; set; }
        }
        public class InfoFactUsGaap
        {
            public InfoFactUsGaapNetIncomeLoss NetIncomeLoss { get; set; }
        }
        public class InfoFactUsGaapNetIncomeLoss
        {
            public InfoFactUsGaapIncomeLossUnits Units { get; set; }
        }
        public class InfoFactUsGaapIncomeLossUnits
        {
            public List<InfoFactUsGaapIncomeLossUnitsUsd> Usd { get; set; }
        }
        public class InfoFactUsGaapIncomeLossUnitsUsd
        {
            /// <summary>
            /// Possibilities include 10-Q, 10-K,8-K, 20-F, 40-F, 6-K, and
            /// their variants.YOU ARE INTERESTED ONLY IN 10-K DATA!
            /// </summary>
            public string? Form { get; set; }

            public bool IsGoodForm
            {
                get
                {
                    const string goodForm = "10-K";
                    string? form = this.Form?.ToUpper();
                    return (form == goodForm);
                }
            }

            /// <summary>
            /// For yearly information, the format is CY followed by the year
            /// number.For example: CY2021.YOU ARE INTERESTED ONLY IN YEARLY INFORMATION
            /// WHICH FOLLOWS THIS FORMAT!
            /// </summary>
            public string? Frame { get; set; }

            public int Year
            {
                get
                {
                    const string tokenStart = "CY";
                    const int tokenExpectedLength = 6;
                    const int minYear = 1900;
                    const int maxYear = 3000;

                    int year = 0;

                    string? frame = this.Frame?.ToUpper();
                    if (!string.IsNullOrEmpty(frame) && frame.Length == tokenExpectedLength)
                    {
                        if (frame.StartsWith(tokenStart))
                        {
                            int parsedYear = 0;
                            string yearString = frame.Substring(2, 4);
                            bool isYear = int.TryParse(yearString, out parsedYear);
                            if (isYear)
                            {
                                if (parsedYear >= minYear && parsedYear <= maxYear)  { 
                                    year = parsedYear; 
                                }
                            }
                        }
                    }

                    return year;
                }
            }

            /// <summary>
            /// The income/loss amount.
            public decimal Val { get; set; }
        }
    }
}
