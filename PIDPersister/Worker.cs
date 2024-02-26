namespace PIDPersister
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bool isExecuting = false;
            
            string dir = Directory.GetCurrentDirectory();

            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            string exeBat = @"\launch_portable.bat";

            proc.StartInfo.FileName = dir + exeBat;


            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Persistência do processo em execução", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                if(!isExecuting) {

                    proc.Start();
                    isExecuting = true;
                } else { 
                
                    if (proc.HasExited)
                    {
                        proc.Start();

                        isExecuting = true;

                    }

                }
            }
        }
    }
}