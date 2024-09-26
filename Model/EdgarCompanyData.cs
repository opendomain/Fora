using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Fora.Model
{
    public class EdgarCompanyData
    {
        public EdgarCompanyData(int Cik)
        {
            this.Cik = Cik;
            this.EntityName = null;
            Usd = new List<InfoFactUsGaapIncomeLossUnitsUsd>();
        }

        public EdgarCompanyData(int Cik, string entityName)
        {
            this.Cik = Cik;
            this.EntityName = entityName;
            Usd = new List<InfoFactUsGaapIncomeLossUnitsUsd>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Cik { get; set; }

        public string? EntityName { get; set; }
        public List<InfoFactUsGaapIncomeLossUnitsUsd> Usd { get; set; }

        public class InfoFactUsGaapIncomeLossUnitsUsd
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }

            /// <summary>
            /// Possibilities include 10-Q, 10-K,8-K, 20-F, 40-F, 6-K, and
            /// their variants.YOU ARE INTERESTED ONLY IN 10-K DATA!
            /// </summary>
            public string Form { get; set; }
            
            /// <summary>
            /// For yearly information, the format is CY followed by the year
            /// number.For example: CY2021.YOU ARE INTERESTED ONLY IN YEARLY INFORMATION
            /// WHICH FOLLOWS THIS FORMAT!
            /// </summary>
            public string Frame { get; set; }

            /// <summary>
            /// The income/loss amount.
            public decimal Val { get; set; }
        }
    }
}
