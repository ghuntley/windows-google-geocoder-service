using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

using NLog;

namespace GoogleGeocoderService
{
    internal static class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The return code (aka %ERRORLEVEL%) on application exit
        /// </summary>
        private enum ReturnCode
        {
            Ok = 0,
            Error = 1
        }

        /// <summary>
        /// register the service as a service on the system.
        /// </summary>
        private static void InstallService()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
                Log.Info("Service has been installed");
            }
            catch (Exception ex)
            {
                Log.FatalException("Service install has failed", ex);
                Environment.Exit((int)ReturnCode.Error);
            }
        }

        /// <summary>
        /// main application startup
        /// </summary>
        private static void Main(string[] args)
        {
            var service = new Service();

            // The default way to debug Windows Service is to install the service into a system
            // then attach a debugger to it. Here we detect if the debugger is attached and instead
            // run the service as a console application so it can be debugged without having to
            // jump through those hoops - i.e. can debug without installing as service directly
            // from visual studio.
            if (Debugger.IsAttached)
            {
                Log.Info("Service running in foreground as console application (Visual Studio debugger is attached)");
                RunInteractive(service, args);
                Environment.Exit((int) ReturnCode.Ok);
            }

            // If visual studio debugger is not found then this is the default application logic.
            var arguments = string.Concat(args);
            switch (arguments)
            {
                case "--console":
                    Log.Info("Service running in foreground as console application (activated by --console)");
                    RunInteractive(service, args);
                    break;
                case "--install":
                    Log.Info("Installing service. (activated by --install)");
                    InstallService();
                    break;
                case "--uninstall":
                    Log.Info("Uninstalling service. (activated by --uninstall)");
                    UninstallService();
                    break;
                default:
                    try
                    {
                        if (Environment.UserInteractive)
                        {
                            Log.Info("Service running in foreground as console application");
                            RunInteractive(service, args);
                        }
                        else
                        {
                            Log.Info("Service running in background as Windows Service");
                            ServiceBase.Run(service);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.FatalException("Service startup has failed", ex);
                        Environment.Exit((int)ReturnCode.Error);
                    }
                    break;
            }
        }

        /// <summary>
        /// invoke the service in the foreground as a console application.
        /// </summary>
        private static void RunInteractive(Service service, string[] args)
        {
            try
            {
                service.InteractiveStart(args);
                Log.Info("Press any key to terminate the application");
                Console.Read();
                service.InteractiveStop();
            }
            catch (Exception ex)
            {
                Log.FatalException("Service startup has failed", ex);
                Environment.Exit((int) ReturnCode.Error);
            }
        }

        /// <summary>
        /// unregister the service from the system.
        /// </summary>
        private static void UninstallService()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                Log.Info("Service has been removed.");
            }
            catch (Exception ex)
            {
                Log.FatalException("Service uninstall has failed", ex);
                Environment.Exit((int)ReturnCode.Error);
            }
        }
    }
}