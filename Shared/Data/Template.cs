using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyManage.Data;
using Microsoft.EntityFrameworkCore;

namespace FamilyManage.Shared.Data
{
    /// <summary>
    /// 存储各种模板
    /// 数据库
    /// </summary>
    [Serializable]
    public class Template
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 模板名字
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 模板转换成json存储
        /// </summary>
        public string? JsonString { get; set; }

        public virtual GridTable? GridTable { get; set; }

       // public virtual GridModule Module { get; set; }

        public virtual AccountClass? Creater { get; set; }
    }

    /// <summary>
    /// 牌位 数据
    /// </summary>
    public class AncestralTablet
    {
        [Key]
        public int Id { get; set; }
    }

}
