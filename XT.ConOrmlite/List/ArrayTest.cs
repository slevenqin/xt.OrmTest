using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XT.ConOrmlite.List
{
    public static class ArrayTest
    {
        public static void BigDataLoop()
        {
            ILoggerRepository repository = LogManager.CreateRepository("Log4netTestRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(repository.Name, "ArrayTest");

            /*
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();  //开始监视代码运行时间
                //需要测试的代码
                watch.Stop();  //停止监视
                TimeSpan timespan = watch.Elapsed;  //获取当前实例测量得出的总时间
                System.Diagnostics.Debug.WriteLine("打开窗口代码执行时间：{0}(毫秒)", timespan.TotalMilliseconds);  //总毫秒数
             */

            int[] p = new int[499999999];

            int[] q = new int[499999999];

            for (int i = 0; i < 499999999; i++)
            {
                p[i] = i;
            }
            for (int i = 0; i < 499999999; i++)
            {
                q[i] = i;
            }

            long sum = 0;

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < 499999999; i++)
            {
                sum += p[i];
                sum += q[i];
            }
            watch.Stop();  //停止监视
            TimeSpan timespan = watch.Elapsed;  //获取当前实例测量得出的总时间
            log.Info($"第一次时间:{timespan.TotalMinutes}; sum:{sum}");

            watch.Start();
            for (int i = 0; i < 499999999; i++)
            {
                sum += p[i];
            }
            for (int i = 0; i < 499999999; i++)
            {
                sum += q[i];
            }
            watch.Stop();  //停止监视
            timespan = watch.Elapsed;  //获取当前实例测量得出的总时间
            log.Info($"第二次时间:{timespan.TotalMinutes}; sum:{sum}");
        }
    }
}
