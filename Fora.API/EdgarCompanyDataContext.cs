using Fora.Model;
using Microsoft.EntityFrameworkCore;
using static Fora.Model.EdgarCompanyData;

namespace Fora
{
    public class EdgarCompanyDataContext : DbContext
    {
        // TODO: Should we add Logger?
        public EdgarCompanyDataContext(DbContextOptions<EdgarCompanyDataContext> options) : base(options)
        {
        }

        public DbSet<EdgarCompanyData> EdgarCompanyDataList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EdgarCompanyData>().HasKey(x => x.Cik);

            List<EdgarCompanyData> EdgarCompanyDataList = new List<EdgarCompanyData>();

            EdgarCompanyDataList.Add(new EdgarCompanyData(0000018926));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000892553));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001510524));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001858912));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001828248));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001819493));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000060086));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001853630));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001761312));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001851182));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001034665));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000927628));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001125259));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001547660));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001393311));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001757143));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001958217));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000312070));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000310522));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001861841));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001037868));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001696355));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001166834));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000915912));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001085277));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000831259));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000882291));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001521036));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001824502));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001015647));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000884624));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001501103));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001397183));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001552797));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001894630));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000823277));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000021175));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001439124));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000052827));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001730773));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001867287));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001685428));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001007587));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000092103));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001641751));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000006845));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001231457));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000947263));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000895421));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001988979));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001848898));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000844790));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001541309));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001858007));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001729944));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000726958));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001691221));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000730272));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001308106));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000884144));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001108134));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001849058));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001435617));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001857518));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000064803));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001912498));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001447380));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001232384));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001141788));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001549922));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000914475));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001498382));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001400897));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000314808));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001323885));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001526520));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001550695));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001634293));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001756708));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001540159));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001076691));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001980088));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001532346));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000923796));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001849635));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001872292));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001227857));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001046311));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001710350));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001476150));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001844642));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001967078));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000014272));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000933267));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001157557));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001560293));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0000217410));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001798562));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001038074));
            EdgarCompanyDataList.Add(new EdgarCompanyData(0001843370));

            modelBuilder.Entity<EdgarCompanyData>().HasData(
                EdgarCompanyDataList
            );

            modelBuilder.Entity<InfoFactUsGaapIncomeLossUnitsUsd>().HasKey(x => x.id);
        }

    }
}
