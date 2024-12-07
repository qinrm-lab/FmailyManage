using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyManage.Shared.Data
{
    public class KeyValueShared
    {
        [Key]
        public int Id { set; get; }

        /// <summary>
        /// 联系方式的关键词
        /// </summary>
        public string Name { get; set; } = "电话";

        /// <summary>
        /// 号码住址之类
        /// </summary>
        public string Value { get; set; } = "";
    }
}
