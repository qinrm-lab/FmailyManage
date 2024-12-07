using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FamilyManage.Data;

/// <summary>
/// 流水
/// 资金流动
/// </summary>
public class Fund
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 收支
    /// +入账
    /// -出账
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; } = 0;

    [Required]
    public virtual IList<Operation> Operations { get; set; }= new List<Operation>();

    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime DateTime { get; set; }
}

