using log4net;
using log4net.Config;
using log4net.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XT.ConOrmlite
{
    public class ConnectionCountTest
    {
        private static ILoggerRepository repository = LogManager.CreateRepository("ConnectionCountTestRepository");

        public static void Test()
        {
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(repository.Name, "ConnectionCountTest");

            int max = 1000;
            int loop = 0;
            log.Debug("创建1000链接开始");
            StringBuilder sdLine = new StringBuilder();
            int opening = 0;
            int closeed = 0;
            for (int i = 0; i < max; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    bool isOpen = false;
                    int hashCode = 0;
                    try
                    {
                        using (IDbConnection connection = MySqlClientFactory.Instance.CreateConnection())
                        {
                            hashCode = connection.GetHashCode();
                            connection.ConnectionString = @"server=192.168.199.11;PORT=3308;database=test;User ID=zhidianlife;Password=zdsh123456;Allow User Variables=True";
                            connection.Open();
                            isOpen = true;
                            log.Debug($"hashCode:{hashCode}_opening:{opening}_打开");
                            opening++;
                            Thread.Sleep(50000);
                            connection.Close();
                            log.Debug($"hashCode:{hashCode}_opening:{opening}_关闭");
                            closeed++;
                            loop++;
                        }
                    }
                    catch (Exception ex)
                    {
                        sdLine.AppendLine($"链接hashCode:{hashCode}异常:{ex.Message};____开启:{opening}_____关闭:{closeed}_____正在连接数{opening - closeed}");
                        loop++;
                        if (isOpen)
                        {
                            closeed++;
                        }
                    }
                });
            }
            while (loop != max - 1)
            {
                log.Debug($"当前循环数：{loop};__开启:{opening}__关闭:{closeed}__正在连接数{opening - closeed};");
                Thread.Sleep(20000);
                if (sdLine.Length > 0)
                {
                    log.Debug(sdLine.ToString());
                    break;
                }
            }
            
            Console.WriteLine("创建1000链接完成");
        }
    }
}
