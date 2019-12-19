using System;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.Common;
using System.Text;
using log4net;
using log4net.Repository;
using log4net.Config;
using System.IO;
using XT.ConOrmlite.List;
using XT.ConOrmlite.ServiceStackService;

namespace XT.ConOrmlite
{
    class Program
    {
        private static readonly OrmLiteConnectionFactory Factory =
            new OrmLiteConnectionFactory("", MySqlDialect.Provider);

        //private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            //更新一个表开起事务1更新表1，等待30秒； 30秒内开启事务2更新表2
            //30秒后事务1更新表2

            //先提交事务1  测试

            //先提交事务2测试
            //using (var db = Factory.Open())
            //{
            //}
            //int i = 0;
            //while (i < 10000)
            //{
            //    Console.WriteLine(i);
            //    i++;
            //}
            //server=192.168.199.11;PORT=3308;database=Wholesale;uid=jxcW;pwd=ZDjxcW@2017
            
            try
            {
                //日志测试
                //Log4netTest.Test();

                //测试mysql连接数
                //ConnectionCountTest.Test();

                //DateTime t1 = new DateTime(2016, 9, 8);
                //DateTime t2 = new DateTime(2019, 11, 17);
                //var days = (t2 - t1).Days;
                //ListSort.Test();

                //大数组轮询相加耗时
                //ArrayTest.BigDataLoop();

                #region Divisible  整除

                /*
                 
                //Divisible  整除
                int Divisor = 87 / 10;
                Console.WriteLine($"整除Divisor:{Divisor}");

                //Remainder 余数
                int Remainder = 65 % 10;
                Console.WriteLine($"余数Remainder:{Remainder}");
                
                */

                #endregion

                string directory = Directory.GetCurrentDirectory();
                Console.WriteLine($"directory:{directory}");

                CheckValidService.CheckFieldValid(directory, "XT.ConOrmlite", null, null, "\\");

                Console.WriteLine("执行完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
    }
}
