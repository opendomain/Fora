using Fora.Services;
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

        public EdgarCompanyData(long cik, string? entityName)
        {
            this.Cik = cik;
            this.EntityName = entityName;
            Usd = new List<InfoFactUsGaapIncomeLossUnitsUsd>();
        }

        // TODO: Make Read only once set by contructor
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Cik { get; set; }

        public string? EntityName { get; set; }
        public List<InfoFactUsGaapIncomeLossUnitsUsd> Usd { get; set; }

        public DateTime? Updated { get; set; }

        internal void ImportFromEdgar(EdgarCompanyInfo? edgarCompanyInfo)
        {
            List<Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd> infoFactUsGaapIncomeLossUnitsUsdList = new List<Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd>();

            if (edgarCompanyInfo != null)
            {
                this.EntityName = edgarCompanyInfo.EntityName;

                var usdList10k = edgarCompanyInfo?.Facts?.UsGaap?.NetIncomeLoss?.Units?.Usd?.Where(u => u.IsGoodForm);

                if (usdList10k != null)
                {
                    foreach (Model.EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd usditem in usdList10k)
                    {
                        int year = usditem.Year;
                        if (year > 0)
                        {
                            Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd = new Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd();
                            infoFactUsGaapIncomeLossUnitsUsd.Form = usditem.Form;
                            infoFactUsGaapIncomeLossUnitsUsd.Year = year;
                            infoFactUsGaapIncomeLossUnitsUsd.Val = usditem.Val;

                            infoFactUsGaapIncomeLossUnitsUsdList.Add(infoFactUsGaapIncomeLossUnitsUsd);
                        }
                    }
                }
            }

            this.Usd.Clear();
            foreach (Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd in infoFactUsGaapIncomeLossUnitsUsdList)
            {
                this.Usd.Add(infoFactUsGaapIncomeLossUnitsUsd);
            }
        }

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
            public int Year { get; set; }

            /// <summary>
            /// The income/loss amount.
            public decimal Val { get; set; }
        }
    }
}
