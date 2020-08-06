using System;
using System.IO;
using System.Reflection;
using System.Threading;
using log4net;
using log4net.Config;

namespace JustChat_Center
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += GlobalExceptionEventHandler;

            var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            BasicConfigurator.Configure(logRepo);

            log.Info("Entering application.");

            var bar = new Bar();

            ThreadStart childref = bar.DoIt;
            Thread childThread = new Thread(childref);
            childThread.Start();

            log.Info("Exiting application.");

            Console.ReadKey();
        }

        public static void GlobalExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            log.Fatal(e.ExceptionObject);
        }
    }

    class Bar
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Bar));

        public void DoIt()
        {
            log.Debug("Did it again!");
            log.Fatal("test", new Exception());
            throw new Exception();
        }
    }

}
