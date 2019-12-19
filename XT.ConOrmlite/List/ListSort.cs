using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XT.ConOrmlite.List
{
    public static class ListSort
    {
        public static void Test()
        {
            ILoggerRepository repository = LogManager.CreateRepository("ListSortRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(repository.Name, "ListSort");

            List<SortEntity> sortList = new List<SortEntity>
            {
                new SortEntity { categoryId = 68419, productName = "山东烟台苹果" },
                new SortEntity { categoryId = 68411, productName = "测试" },
                new SortEntity { categoryId = 68416, productName = "彩云火腿肠" },
                new SortEntity { categoryId = 68416, productName = "双汇火腿肠" },
                new SortEntity { categoryId = 68419, productName = "红富士苹果" }
            };

            var categoryIds = sortList.GroupBy(s => s.categoryId).Select(s => s.Key).OrderBy(s => s);
            foreach (var item in categoryIds)
            {
                var loopList = sortList.Where(s => s.categoryId == item);
                var orderProductNames = loopList.OrderBy(s => s.productName);
                foreach (var product in orderProductNames)
                {
                    log.Info($"categoryId:{product.categoryId}; productName:{product.productName}");
                }
            }

            log.Info($"categoryId、productName 排序结束");
            log.Info($"");
            log.Info($"");

            var newList3 = sortList.OrderBy(s => s.categoryId).ThenBy(s => s.productName).ToList();
            foreach (var item in newList3)
            {
                log.Info($"categoryId:{item.categoryId}; productName:{item.productName}");
            }
        }
    }

    public class SortEntity
    {
        public int categoryId { get; set; }

        public string productName { get; set; }
    }
}
