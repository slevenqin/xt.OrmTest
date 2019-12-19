using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ServiceStack.DataAnnotations;

namespace XT.ConOrmlite.ServiceStackService
{
    /// <summary>
    /// 检查服务类
    /// </summary>
    public static class CheckValidService
    {
        /// <summary>
        /// 检查字段合法性
        /// </summary>
        /// <param name="applicationPath">程序运行路径[全路径]</param>
        /// <param name="assemblyPrefix">严格包含有表别名([Alias("Test")])程序集前缀</param>
        /// <param name="specificAssemplys">特定指定的model程序集</param>
        /// <param name="aliasNullAssemplys">不严格包含别名的model程序集</param>
        /// <param name="separated">分隔符</param>
        public static void CheckFieldValid(string applicationPath, string assemblyPrefix, string[] specificAssemplys, string[] aliasNullAssemplys, string separated)
        {
            //.dll为后缀的文件都加载进来
            DirectoryInfo root = new DirectoryInfo(applicationPath);
            FileInfo[] files = root.GetFiles();
            if (files == null || files.Length == 0)
            {
                return;
            }

            Assembly modelAssembly = null;
            string attributeFullName = $"ServiceStack.DataAnnotations.AliasAttribute";

            List<Type> tableAliasNameTypes = new List<Type>();
            string newAssemblyPrefix = assemblyPrefix.ToLower();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                string fileName = file.Name.ToLower();
                if (!fileName.StartsWith(newAssemblyPrefix) || !fileName.EndsWith(".dll"))
                {
                    continue;
                }
                string fullAssemblyName = $"{applicationPath}{separated}{file.Name}";  //AssemblyName
                modelAssembly = Assembly.LoadFile(fullAssemblyName);
                if (modelAssembly == null) continue;
                Type[] asemplyTypes = modelAssembly.GetTypes();
                if (asemplyTypes == null || asemplyTypes.Length == 0) continue;
                tableAliasNameTypes.AddRange(asemplyTypes.ToList());
            }

            List<TableEntityClass> tableEntitys = new List<TableEntityClass>();
            if (tableAliasNameTypes.Count > 0)
            {
                foreach (var type in tableAliasNameTypes)
                {
                    IEnumerable<Attribute> attributes = type.GetCustomAttributes();
                    if (attributes == null || attributes.Count() == 0
                        || !attributes.Where(a => a.GetType().FullName.Equals(attributeFullName)).Any()
                    ) continue;
                    Attribute attr = attributes.FirstOrDefault(a => a.GetType().FullName.Equals(attributeFullName));
                    var alias = attr as AliasAttribute;
                    tableEntitys.Add(new TableEntityClass { FullName = type.FullName, Name = type.Name, TableName = alias.Name });
                }
            }

            //项目以前缀开头的所有dll

            //FileInfo

            //遍历出所有的标注表的类

            #region 注释

            /*

            List<Type> types = new List<Type>();
            for (int i = 0; i < aliasNullAssemplys.Length; i++)
            {
                modelAssembly = Assembly.LoadFile(aliasNullAssemplys[i]);
                if (modelAssembly == null) continue;
                Type[] asemplyTypes = modelAssembly.GetTypes();
                if (asemplyTypes == null || asemplyTypes.Length == 0) continue;
                types.AddRange(asemplyTypes.ToList());
            }

            foreach (var type in types)
            {
                IEnumerable<Attribute> attributes = type.GetCustomAttributes();
                if (attributes == null || attributes.Count() == 0) return;

                AliasAttribute alias = null;
                string tableName = string.Empty;
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType().FullName.Equals(attributeFullName))
                    {
                        alias = attribute as AliasAttribute;
                        
                        break;
                    }
                }
            }

            */

            #endregion
        }
    }

    /// <summary>
    /// 表实体类
    /// </summary>
    public class TableEntityClass
    {
        /// <summary>
        /// 实体类全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 实体类简称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
    }
}
