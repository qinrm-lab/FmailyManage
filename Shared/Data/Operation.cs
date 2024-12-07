using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace FamilyManage.Data;

/// <summary>
/// 操作
/// 追责用
/// </summary>
public class Operation
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 操作员
    /// </summary>
    //[Required]
    public virtual  AccountClass? Operator { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    [Required]
    //[Column(TypeName ="Date")]
    //[DatabaseGenerated(DatabaseGeneratedOption.)]
    public DateTimeOffset Time { get; set; }

    public OperationEnum Operating { get; set; } = OperationEnum.Create;

    /// <summary>
    /// 备注
    /// </summary>
    public string Comment { get; set; } = "";
}

public enum OperationEnum
{
    Create=0,//Post
    Update=1,//Post
    Delete=2,//Post
    Read=3,//Get
}