
using Fora.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fora.Services
{
    public class UpdateDbBackgroundService : BackgroundService
    {
        private const int MAX_NUM_UPDATE = 10;
        private const int DELAY = 5;

        private readonly TimeSpan _delayStart = TimeSpan.FromSeconds(DELAY);

        ILogger<UpdateDbBackgroundService> _logger = null;
        private Timer? _timer = null;

        private readonly IServiceProvider _serviceProvider;

        public UpdateDbBackgroundService(IServiceProvider serviceProvider, ILogger<UpdateDbBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // TODO: Create timer to delay Update
            PeriodicTimer timer = new PeriodicTimer(_delayStart);
            await timer.WaitForNextTickAsync(stoppingToken);
            timer.Dispose();

            bool restartFlag = true;

            while (!stoppingToken.IsCancellationRequested && restartFlag)
            {
                int hrs = 0;
                int min = 0;
                int sec = 2;

                try
                {
                    ICrudDbService crudDbService = _serviceProvider.GetRequiredService<ICrudDbService>();
                    List<EdgarCompanyData>? allEdgarCompanyData = await crudDbService.GetAllCompanyData(onlyGetUpdatedFlag: false, onlyGetValidNames: false);

                    if (allEdgarCompanyData != null)
                    {
                        List<EdgarCompanyData>? emptyEdgarCompanyData = allEdgarCompanyData.Where(x => x.EntityName == null).Take(MAX_NUM_UPDATE).ToList();

                        if (emptyEdgarCompanyData.Count == 0)
                        {
                            restartFlag = false;
                        }
                        else
                        {
                            EdgarCompanyInfo? edgarCompanyInfo;
                            ICallEdgarService callEdgarService = _serviceProvider.GetRequiredService<ICallEdgarService>();


                            // TODO: parallel
                            foreach (EdgarCompanyData edgarCompanyData in emptyEdgarCompanyData)
                            {
                                // Where above should filter out Null EntityNames
                                if (edgarCompanyData.EntityName == null)
                                {
                                    // TODO:  Call multiple parallel 
                                    edgarCompanyInfo = await callEdgarService.GetEdgarInfo(edgarCompanyData.Cik);
                                    if (edgarCompanyInfo == null) { continue; }

                                    // TODO: Is This needed to update the local?
                                    edgarCompanyData.EntityName = edgarCompanyInfo.EntityName;
                                    var result = await crudDbService.UpdateCompanyData(edgarCompanyData.Cik, edgarCompanyInfo.EntityName);

                                    if (!string.IsNullOrEmpty(edgarCompanyInfo.EntityName))
                                    {

                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Error in ExecuteAsync {ex.Message}.");
                    restartFlag = false;
                }

                finally
                {
                    if (restartFlag)
                    {
                        await Task.Delay(new TimeSpan(hrs, min, sec));
                    }
                }

            }
        }
    }

}
