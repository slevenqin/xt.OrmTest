using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.ConOrmlite.Model
{
    /// <summary>
    /// TestEntity
    /// </summary>
    [Alias("Test")]
    public class TestEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }
    }
}
