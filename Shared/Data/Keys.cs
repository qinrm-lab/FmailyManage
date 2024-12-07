using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FamilyManage.Data;

/// <summary>
/// 只能管理员初始化数据用
/// </summary>
/*public class KeyValuePair
{
    /// <summary>
    /// 主键,不用管
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 例如写证件
    /// </summary>
    [Required]
    public string Name { get; set; } = "证件";

    /// <summary>
    /// 例如写身份证，驾驶证
    /// </summary>
    [Required]
    public string NameValue { get; set; } = "身份证";
}*/

public enum KeyEnum
{
    身份ID=0,
    联系方式=1,
}

