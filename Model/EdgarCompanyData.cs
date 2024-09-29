using Fora.Services;
using System.Collections.Generic;
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
        private long _cik;
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Cik {
            get { 
                return _cik; 
            }
            set {
                if (value <= 0 || value > 9999999999) {
                    throw new ArgumentOutOfRangeException("Cik");
                }
                _cik = value;
            } 
        }

        public string? EntityName { get; set; }
        
        // TODO: Make private/internal set ?
        public List<InfoFactUsGaapIncomeLossUnitsUsd> Usd { get; set; }

        public DateTime? Updated { get; set; }
        public decimal standardFundableAmount { get; set; }
        public decimal specialFundableAmount { get; set; }

        // TODO: Dirty flag to CalculateFundable
        internal void CalculateFundable()
        {
            decimal targetIncome = 10_000_000_000m;
            decimal stdFundableLower = 0.2151m; // 21.51%
            decimal stdFundableUpper = 0.1233m; // 12.33%

            decimal specialFundableVowel = 0.15m;
            decimal specialFundable2022isLower = 0.25m;

            standardFundableAmount = 0.0m;
            specialFundableAmount = 0.0m;

            // TODO: Verify distinct
            List<InfoFactUsGaapIncomeLossUnitsUsd> disctinctList = 
                this.Usd
                    .GroupBy(usd => usd.Year )
                    .Select(usd => usd.First())
                    .ToList();

            if (this.Usd.Count != disctinctList.Count)
            {
                Usd = new List<InfoFactUsGaapIncomeLossUnitsUsd>();
                throw new Exception("USD not distinct");
            }

            var yearsOfIncome = this.Usd.Where(usd => usd.Year >= 2018 && usd.Year <= 2022);
            if (yearsOfIncome != null && yearsOfIncome.Count() == 5)
            {
                var usd2021 = this.Usd.Where(Usd => Usd.Year == 2021).FirstOrDefault();
                if (usd2021 != null && usd2021.Val > 0.0m ) {
                    var usd2022 = this.Usd.Where(Usd => Usd.Year == 2022).FirstOrDefault();
                    if (usd2022 != null && usd2022.Val > 0.0m)
                    {
                        decimal maxIncome = yearsOfIncome.Max(usd => usd.Val);
                        if (maxIncome >= targetIncome) {
                            standardFundableAmount = maxIncome * stdFundableUpper;
                        } else
                        {
                            standardFundableAmount = maxIncome * stdFundableLower;
                        }

                        decimal specialFundableExtra = 0.0m;
                        specialFundableAmount = standardFundableAmount;

                        bool isVowel = false;
                        if (!string.IsNullOrEmpty(this.EntityName))
                        {
                            string? firstLetter = this.EntityName?.Substring(0, 1).ToUpper();
                            // Letter 'Y' at the beginning of a word is a consonant.
                            isVowel = "AEIOU".IndexOf(firstLetter) >= 0;
                        }

                        if (isVowel) {
                            specialFundableExtra = (standardFundableAmount * specialFundableVowel);
                        }

                        if (usd2021.Val > usd2022.Val)
                        {
                            specialFundableExtra -= (standardFundableAmount * specialFundable2022isLower);
                        }
                        specialFundableAmount += specialFundableExtra;
                    }
                }

            }
        }

        public void ImportFromEdgar(EdgarCompanyInfo? edgarCompanyInfo)
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

            CalculateFundable();
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
