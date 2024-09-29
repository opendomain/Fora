using Xunit;
using Fora.Model;

namespace Fora.UnitTests
{
    public class ForaModel_CheckData
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(10000000000)]
        public void InvalidCik_ForCompanyData_ThrowsException(long cik)
        {
            string entityName = string.Empty;

            EdgarCompanyData edgarCompanyData; 

            Assert.Throws<ArgumentOutOfRangeException>( () => edgarCompanyData = new EdgarCompanyData(cik, entityName) );
        }

        [Fact]
        public void EmptyInfo_ForCompanyData_ThrowsException()
        {
            long cik = 1;
            string entityName = string.Empty;

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            edgarCompanyData = new EdgarCompanyData(cik);

            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(0.0M, edgarCompanyData.specialFundableAmount);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("cy")]
        [InlineData("cy99999")]
        [InlineData("CY99A9")]
        [InlineData("CY-999")]
        [InlineData("cy0001")]
        [InlineData("CY9999")]
        public void Info_BadYears(string? frame)
        {
            Model.EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;

            infoFactUsGaapIncomeLossUnitsUsd = new Model.EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd();

            infoFactUsGaapIncomeLossUnitsUsd.Frame = frame;

            Assert.Equal(0, infoFactUsGaapIncomeLossUnitsUsd.Year);
        }

        [Theory]
        [InlineData("cy1990", 1990)]
        [InlineData("CY2001", 2001)]
        public void Info_GoodYears(string? frame, int year)
        {
            Model.EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;

            infoFactUsGaapIncomeLossUnitsUsd = new Model.EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd();

            infoFactUsGaapIncomeLossUnitsUsd.Frame = frame;

            Assert.Equal(year, infoFactUsGaapIncomeLossUnitsUsd.Year);
        }

        // TODO: Verify no need to use ByRef
        private void Info_InitalizeWholeChain(EdgarCompanyInfo edgarCompanyInfo)
        {
            edgarCompanyInfo.Facts = new EdgarCompanyInfo.InfoFact();
            edgarCompanyInfo.Facts.UsGaap = new EdgarCompanyInfo.InfoFactUsGaap();
            edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss = new EdgarCompanyInfo.InfoFactUsGaapNetIncomeLoss();
            edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units = new EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnits();
            edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd = new List<EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd>();

            Info_createAllValidYears(edgarCompanyInfo);
        }

        private void Info_createAllValidYears(EdgarCompanyInfo edgarCompanyInfo)
        {
            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;

            for (int year = 2018; year <= 2022; year++)
            {
                infoFactUsGaapIncomeLossUnitsUsd = new EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd();
                infoFactUsGaapIncomeLossUnitsUsd.Form = "10-K";  // TODO: Make this an exposed constant
                infoFactUsGaapIncomeLossUnitsUsd.Frame = "CY" + year;
                infoFactUsGaapIncomeLossUnitsUsd.Val = 0;  //TODO: must have income data for all years.  Income data of negative or ACTUAL 0 should be ok
                edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Add(infoFactUsGaapIncomeLossUnitsUsd);
            }
        }

        // Company must have had positive income in both 2021 and 2022
        private void Info_Make2021and2022Positive(EdgarCompanyInfo edgarCompanyInfo)
        {
            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;

            // TODO: Prevent direct access to List like this
            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2021).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 1;

            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2022).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 1;
        }

        [Fact]
        public void InfoWithWholeChain() {
            long cik = 1;
            string entityName = string.Empty;

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;
            

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            Info_InitalizeWholeChain(edgarCompanyInfo);

            edgarCompanyData = new EdgarCompanyData(cik);
            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(0.0M, edgarCompanyData.standardFundableAmount);
            Assert.Equal(0.0M, edgarCompanyData.specialFundableAmount);
        }

        [Fact]
        public void InfoGoodCheckLowIncome()
        {
            long cik = 1;
            string entityName = string.Empty;

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            Info_InitalizeWholeChain(edgarCompanyInfo);
            Info_Make2021and2022Positive(edgarCompanyInfo);

            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;
            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2018).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 10_000_000;

            edgarCompanyData = new EdgarCompanyData(cik);
            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(2_151_000.00M, edgarCompanyData.standardFundableAmount);
            Assert.Equal(2_151_000.00M, edgarCompanyData.specialFundableAmount);
        }


        [Fact]
        public void InfoGoodCheckLowIncome_Vowel()
        {
            long cik = 1;
            string entityName = "A";

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            Info_InitalizeWholeChain(edgarCompanyInfo);
            Info_Make2021and2022Positive(edgarCompanyInfo);

            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;
            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2018).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 10_000_000;

            edgarCompanyData = new EdgarCompanyData(cik);
            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(2_151_000.00M, edgarCompanyData.standardFundableAmount);
            Assert.Equal(2_473_650.00M, edgarCompanyData.specialFundableAmount);
        }

        [Fact]
        public void InfoGoodCheckHighIncome()
        {
            long cik = 1;
            string entityName = string.Empty;

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            Info_InitalizeWholeChain(edgarCompanyInfo);
            Info_Make2021and2022Positive(edgarCompanyInfo);

            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;
            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2018).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 10_000_000_000;

            edgarCompanyData = new EdgarCompanyData(cik);
            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(1_233_000_000.00M, edgarCompanyData.standardFundableAmount);
            Assert.Equal(1_233_000_000.00M, edgarCompanyData.specialFundableAmount);
        }

        [Fact]
        public void InfoGoodCheckHighIncome_2022WasLess()
        {
            long cik = 1;
            string entityName = string.Empty;

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            Info_InitalizeWholeChain(edgarCompanyInfo);
            Info_Make2021and2022Positive(edgarCompanyInfo);

            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;
            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2021).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 10_000_000_000;

            edgarCompanyData = new EdgarCompanyData(cik);
            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(1_233_000_000.00M, edgarCompanyData.standardFundableAmount);
            Assert.Equal(924_750_000.00M, edgarCompanyData.specialFundableAmount);
        }


        [Fact]
        public void InfoGoodCheckHighIncome_Vowel()
        {
            long cik = 1;
            string entityName = "A";

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            Info_InitalizeWholeChain(edgarCompanyInfo);
            Info_Make2021and2022Positive(edgarCompanyInfo);

            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;
            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2018).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 10_000_000_000;

            edgarCompanyData = new EdgarCompanyData(cik);
            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(1_233_000_000.00M, edgarCompanyData.standardFundableAmount);
            Assert.Equal(1_417_950_000.00M, edgarCompanyData.specialFundableAmount);
        }

        [Fact]
        public void InfoGoodCheckHighIncome_2022WasLess_Vowel()
        {
            long cik = 1;
            string entityName = "A";

            EdgarCompanyInfo edgarCompanyInfo;
            EdgarCompanyData edgarCompanyData;

            edgarCompanyInfo = new EdgarCompanyInfo(cik, entityName);
            Info_InitalizeWholeChain(edgarCompanyInfo);
            Info_Make2021and2022Positive(edgarCompanyInfo);

            EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd;
            infoFactUsGaapIncomeLossUnitsUsd = edgarCompanyInfo.Facts.UsGaap.NetIncomeLoss.Units.Usd.Where(u => u.Year == 2021).FirstOrDefault();
            infoFactUsGaapIncomeLossUnitsUsd.Val = 10_000_000_000;

            edgarCompanyData = new EdgarCompanyData(cik);
            edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);

            Assert.Equal(1_233_000_000.00M, edgarCompanyData.standardFundableAmount);
            Assert.Equal(1_109_700_000.00M, edgarCompanyData.specialFundableAmount);
        }



    }
}