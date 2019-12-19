using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XT.ConOrmlite
{
    public static class Log4netTest
    {
        public static void Test()
        {
            try
            {
                //NETCoreRepository  NETCorelog4net
                ILoggerRepository repository = LogManager.CreateRepository("Log4netTestRepository");
                XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
                ILog log = LogManager.GetLogger(repository.Name, "Log4netTest");
                for (int i = 0; i < 1000; i++)
                {
                    log.Info("NETCorelog4net log");
                    log.Error("error");
                    log.Warn("warn");
                }
            }
            catch
            {
            }
        }
    }
}
