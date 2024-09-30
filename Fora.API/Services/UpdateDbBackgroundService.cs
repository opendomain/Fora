
using Fora.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Fora.Model.EdgarCompanyData;
using static Fora.Model.EdgarCompanyInfo;

namespace Fora.Services
{
    /// <summary>
    /// Hosted background service to Call Edgar HTTP and then update the database
    /// </summary>
    public class UpdateDbBackgroundService : BackgroundService
    {
        // The number of records to retrieve and update at one time
        private const int MAX_NUM_UPDATE = 1;
        // The number of seconds befre starting the update process
        private const int DELAY = 10;
        private readonly TimeSpan _delayStart = TimeSpan.FromSeconds(DELAY);

        private readonly ILogger<UpdateDbBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UpdateDbBackgroundService(IServiceProvider serviceProvider, ILogger<UpdateDbBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Create timer to delay Update
            PeriodicTimer timer = new PeriodicTimer(_delayStart);
            await timer.WaitForNextTickAsync(stoppingToken);
            timer.Dispose();

            _logger.LogInformation("*** UpdateDbBackgroundService - START ***");

            // This flag is unset when done or there is an error,.  This allows the process to stop
            bool restartFlag = true;

            while (!stoppingToken.IsCancellationRequested && restartFlag)
            {
                // TODO: Make this configurable
                int hrs = 0;
                int min = 0;
                int sec = 3;

                try
                {
                    ICrudDbService crudDbService = _serviceProvider.GetRequiredService<ICrudDbService>();
                    List<EdgarCompanyData>? allEdgarCompanyData = await crudDbService.GetAllCompanyData(onlyGetUpdatedFlag: false, onlyGetValidNames: false);

                    if (allEdgarCompanyData != null)
                    {
                        List<EdgarCompanyData>? emptyEdgarCompanyData = allEdgarCompanyData.Where(x => x.EntityName == null).Take(MAX_NUM_UPDATE).ToList();

                        if (emptyEdgarCompanyData.Count == 0)
                        {
                            // No more records to update. Exit process
                            restartFlag = false;
                            _logger.LogInformation("*** UpdateDbBackgroundService - DONE ***");
                        }
                        else
                        {
                            EdgarCompanyInfo? edgarCompanyInfo;
                            ICallEdgarService callEdgarService = _serviceProvider.GetRequiredService<ICallEdgarService>();

                            // TODO: Make this parallel request for faster processing
                            foreach (EdgarCompanyData edgarCompanyData in emptyEdgarCompanyData)
                            {
                                // The WHERE above should filter out Null EntityNames, this is just to double check
                                if (edgarCompanyData.EntityName == null)
                                {
                                    edgarCompanyInfo = await callEdgarService.GetEdgarInfo(edgarCompanyData.Cik);
                                    // TODO: what to do if NULL?  Throw exception?
                                    if (edgarCompanyInfo != null) {
                                        edgarCompanyData.ImportFromEdgar(edgarCompanyInfo);
                                        var result = await crudDbService.UpdateCompanyData(edgarCompanyData.Cik, edgarCompanyInfo?.EntityName);
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError ($"Error in UpdateDbBackgroundService " + ex.Message);
                    restartFlag = false;
                }

                finally
                {
                    if (restartFlag)
                    {
                        // time to sleep before retrieved next record.  Used to throttle rquests to Edgar
                        await Task.Delay(new TimeSpan(hrs, min, sec));
                    }
                }

            }
        }
    }

}
