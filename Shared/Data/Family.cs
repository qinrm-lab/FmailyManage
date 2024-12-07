using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FamilyManage.Data;

/// <summary>
/// 家族
/// 以家族为单位登记
/// 家族名字不能重复
/// </summary>
public class Family
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 家族名字
    /// </summary>
    [Required]
    [StringLength(18,MinimumLength =4, ErrorMessage = "家族名字至少{2}个字")]
    public string Name { get; set; }

    /// <summary>
    /// 家族成员
    /// </summary>
    public virtual IList<Person> Persons   { get; set; }=new List<Person>();

   /// <summary>
   /// 操作记录
   /// </summary>
    public virtual IList<Operation> Operations { get; set; }= new List<Operation>();

    //public virtual AccountClass? Operator { get; set; }
}

