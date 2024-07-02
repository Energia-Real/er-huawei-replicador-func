using er.huawei.replicador.func.Application.BussinesLogic;
using er.huawei.replicador.func.Domain.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace er.huawei.replicador.func.Functions;

public class Replicador
{
    private readonly ILogger _logger;
    private static IBrandFactory _inverterFactory;
    private static IGigawattLogic bussineslogic;

    public Replicador(ILoggerFactory loggerFactory,
        IBrandFactory inverterFactory,
        IGigawattLogic gigawattLogic)
    {
        _logger = loggerFactory.CreateLogger<Replicador>();
        _inverterFactory = inverterFactory;
        bussineslogic = gigawattLogic;
    }

    [Function("Replicador")]
    public async Task RunAsync([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        var replicateResult = await bussineslogic.ReplicateToMongoDb();

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

}
