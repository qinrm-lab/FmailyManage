using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FamilyManage.Data;

/// <summary>
/// 寺庙
/// </summary>
public class Temple
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 寺庙名称
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 管理人员账号
    /// 只能暂停，不能删除
    /// </summary>
    public virtual ICollection<Temple> Accounts  { get; set; }=new List<Temple>();
}
