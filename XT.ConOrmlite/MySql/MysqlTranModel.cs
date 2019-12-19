using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XT.ConOrmlite.mysql
{
    /*
         var dbFactory = new OrmLiteConnectionFactory(
    connectionString,  
    SqlServerDialect.Provider);

             */
    public static class MysqlTranModel
    {
        public static void Test()
        {
            bool tast = false;
            bool tast2 = false;
            int connectId = 0;
                
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection("server=192.168.199.11;PORT=3308;database=test;User ID=zhidianlife;Password=zdsh123456;Allow User Variables=True"))
                    {
                        connectId = connection.GetHashCode();
                        connection.Open();
                        for (int i = 0; i < 30000; i++)
                        {
                            using (var traction = connection.BeginTransaction())
                            {
                                var cmd = connection.CreateCommand();
                                cmd.CommandText = $" update Stock set Stock1 = Stock1 + 1 where StorageId = 1 and ProductId = 1; ";
                                cmd.ExecuteNonQuery();

                                var cmd2 = connection.CreateCommand();
                                cmd2.CommandText = $" update Stock2 set Stock1 = Stock1 + 1 where StorageId = 1 and ProductId = 1; ";
                                cmd2.ExecuteNonQuery();
                                traction.Commit();
                            }
                        }
                    }
                    Console.WriteLine($"hashcode;{connectId}执行完成");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"hashcode;{connectId}异常{ex.Message}");
                }
                tast = true;
            });


            int connectId2 = 0;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection("server=192.168.199.11;PORT=3308;database=test;User ID=zhidianlife;Password=zdsh123456;Allow User Variables=True"))
                    {
                        connectId2 = connection.GetHashCode();
                        connection.Open();
                        for (int i = 0; i < 20000; i++)
                        {
                            using (var traction = connection.BeginTransaction())
                            {
                                var cmd = connection.CreateCommand();
                                cmd.CommandText = $" update Stock2 set Stock1 = Stock1 + 1 where StorageId = 1 and ProductId = 1; ";
                                cmd.ExecuteNonQuery();

                                var cmd2 = connection.CreateCommand();
                                cmd2.CommandText = $" update Stock set Stock1 = Stock1 + 1 where StorageId = 1 and ProductId = 2; ";
                                cmd2.ExecuteNonQuery();

                                traction.Commit();
                            }
                        }
                    }
                    Console.WriteLine($"hashcode;{connectId2}执行完成");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"hashcode;{connectId2}异常{ex.Message}");
                }
                tast2 = true;
            });
            //Task.Factory.StartNew(() => {
            //    using (MySqlConnection connection = new MySqlConnection("server=192.168.199.11;PORT=3308;database=test;User ID=zhidianlife;Password=zdsh123456;Allow User Variables=True"))
            //    {
            //        connection.Open();
            //        for (int i = 0; i < 10000; i++)
            //        {
            //            using (var traction = connection.BeginTransaction())
            //            {
            //                var cmd = connection.CreateCommand();
            //                cmd.CommandText = $" update Stock set Stock1 = Stock1 + 1 where StorageId = 1 and ProductId = 1; ";
            //                cmd.ExecuteNonQuery();
            //                traction.Commit();
            //            }
            //        }
            //    }
            //});
            //Console.WriteLine("任务执行中...");
            //while (!tast || !tast2)
            //{
            //    Thread.Sleep(100);
            //}
            //Console.WriteLine("任务执行完成");
        }
    }
}
