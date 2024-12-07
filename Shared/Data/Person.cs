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
/// 个人信息
/// </summary>
#nullable enable
public class Person
{
    [Key]
    public int Id { get; set; }

    //[ForeignKey("AccountId")]
    //public int AccountId { get; set; }

    //public virtual AccountClass Creator { get; set; }

    /// <summary>
    /// 属于某个家族
    /// </summary>
    public virtual Family? Family { get; set; }

    /// <summary>
    /// 姓名,里面有细分类
    /// </summary>
    [Required]
    public virtual NameClass Name { get; set; } = new NameClass();


    /// <summary>
    /// 性别:男女
    /// </summary>
    [Required]
    public GenderEnum Gender { get; set; } = GenderEnum.男;

    /// <summary>
    /// 个人身份ID
    /// 身份证号，驾驶证号，护照之类
    /// </summary>
    public virtual KeyValue PersonalID { get; set; } = new KeyValue() { Name = "身份证", Value = "xxx" };

    /// <summary>
    ///夫妻，可以对应多个配偶
    /// </summary>
    //[Column(TypeName ="配偶")]
    public virtual List<Person>? Partners { get; set; }=new List<Person>(){ };

    /// <summary>
    /// 宗族来自哪里分支
    /// </summary>
    //[Column(TypeName ="父母")]
    public virtual Person? From { get; set; } = null;

    /// <summary>
    /// 后代，集合
    /// </summary>
    public virtual IList<Person>? To { get; set; } = new List<Person>();

    /// <summary>
    /// 联系方式,可以添加多个
    /// 例如电话,地址等
    /// </summary>
    
    public virtual IList<KeyValue>? Contact { get; set; }=new List <KeyValue>();

    /// <summary>
    /// 第几代，可以留空
    /// </summary>
    //[Column(TypeName = "代")]
    [Comment("家族第几代")]
    public int Generation { get; set; } = 0;
}

public class NameClass
{
    [Key]
    public int Id { get; set; }

   // [ForeignKey("PersonId")]
    //public int PersonId { get; set; }

    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

}

/// <summary>
/// 个人联系方式等的元数据形式
/// </summary>
public class KeyValue
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

/// <summary>
/// 性别
/// </summary>
public enum GenderEnum
{
    /// <summary>
    /// 男性
    /// </summary>
    男 = 0,
    /// <summary>
    /// 女性
    /// </summary>
    女 = 1
}