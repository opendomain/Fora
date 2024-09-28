using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Fora.Model
{
    public class EdgarCompanyData
    {
        public EdgarCompanyData(long cik)
        {
            this.Cik = cik;
            this.EntityName = null;
            Usd = new List<InfoFactUsGaapIncomeLossUnitsUsd>();
        }

        public EdgarCompanyData(long cik, string entityName)
        {
            this.Cik = cik;
            this.EntityName = entityName;
            Usd = new List<InfoFactUsGaapIncomeLossUnitsUsd>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Cik { get; set; }

        public string? EntityName { get; set; }
        public List<InfoFactUsGaapIncomeLossUnitsUsd> Usd { get; set; }

        public DateTime? Updated { get; set; }

        public class InfoFactUsGaapIncomeLossUnitsUsd
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long id { get; set; }

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
