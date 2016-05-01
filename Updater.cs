using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Squirrel;

namespace DecodedConf.HelloWorld
{
    public class Updater
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly string UpdatePath = ConfigurationManager.AppSettings["updateDropLocation"];
        private static readonly int UpdateCheckFrequencyInMs = int.Parse(ConfigurationManager.AppSettings["updateCheckFreqencyInMinutes"]) * 60 * 1000;

        public static Task Create(CancellationToken token)
        {
            return new Task(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Logger.Debug("Checking for updates...");
                    Updater.CheckAndApplyUpdates();

                    Thread.Sleep(UpdateCheckFrequencyInMs);
                }
            }, token, TaskCreationOptions.LongRunning);
        }

        private static void CheckAndApplyUpdates()
        {
            try
            {
                bool shouldRestart = false; 
                using (var mgr = new UpdateManager(UpdatePath))
                {
                    var updateInfo = mgr.CheckForUpdate().Result;
                    Logger.Debug($"Installed Version: {updateInfo.CurrentlyInstalledVersion.Version}. Latest Version: {updateInfo.FutureReleaseEntry.Version}");
                    if (updateInfo.CurrentlyInstalledVersion.Version < updateInfo.FutureReleaseEntry.Version)
                    {
                        shouldRestart = true;
                        Logger.Debug($"Downloading Update...");
                        mgr.DownloadReleases(updateInfo.ReleasesToApply).Wait();
                        Logger.Debug($"Applying Update...");
                        mgr.ApplyReleases(updateInfo).Wait();
                    }
                }

                if (shouldRestart)
                {
                    Logger.Debug($"Restarting...");
                    UpdateManager.RestartApp();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}
