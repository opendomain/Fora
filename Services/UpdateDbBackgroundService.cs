
using Fora.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Fora.Model.EdgarCompanyData;
using static Fora.Model.EdgarCompanyInfo;

namespace Fora.Services
{
    public class UpdateDbBackgroundService : BackgroundService
    {
        private const int MAX_NUM_UPDATE = 1;
        private const int DELAY = 10;

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

                                    List<Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd> infoFactUsGaapIncomeLossUnitsUsdList = new List<Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd>();

                                    // TODO: Move filter algo
                                    var usdList10k = edgarCompanyInfo?.Facts?.UsGaap?.NetIncomeLoss?.Units?.Usd?.Where(u => u.Form?.ToUpper() == "10-K");

                                    if (usdList10k != null)
                                    {
                                        foreach (Model.EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd usditem in usdList10k)
                                        {
                                            string? frame = usditem?.Frame;
                                            if (!string.IsNullOrEmpty(frame) && frame.Length == 6)
                                            {
                                                if (frame.StartsWith("CY", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    string yearString = frame.Substring(2, 4);
                                                    int year;
                                                    bool isYear = int.TryParse(yearString, out year);
                                                    if (isYear)
                                                    {
                                                        Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd infoFactUsGaapIncomeLossUnitsUsd = new Model.EdgarCompanyData.InfoFactUsGaapIncomeLossUnitsUsd();
                                                        infoFactUsGaapIncomeLossUnitsUsd.Form = usditem.Form;
                                                        infoFactUsGaapIncomeLossUnitsUsd.Frame = frame;
                                                        infoFactUsGaapIncomeLossUnitsUsd.Val = usditem.Val;

                                                        infoFactUsGaapIncomeLossUnitsUsdList.Add(infoFactUsGaapIncomeLossUnitsUsd);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // TODO: Is This needed to update the local?
                                    edgarCompanyData.EntityName = edgarCompanyInfo.EntityName;
                                    var result = await crudDbService.UpdateCompanyData(edgarCompanyData.Cik, edgarCompanyInfo.EntityName);
                                    result = await crudDbService.AddUnits(edgarCompanyData.Cik, infoFactUsGaapIncomeLossUnitsUsdList);
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
