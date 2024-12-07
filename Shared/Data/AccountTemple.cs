using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace FamilyManage.Data;

/// <summary>
/// 寺庙操作员,香客，都是一个账户格式
/// </summary>
public class AccountClass
{
    /// <summary>
    /// 主键Hello Hello can use 
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 角色
    /// 权限用
    /// </summary>
    [Required]
    public RoleEnum Role { get; set; } = RoleEnum.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 密码,哈希存储，防止泄密
    /// </summary>
    [Required(ErrorMessage ="必须有密码")]
    [DataType(DataType.Password)]
    //[StringLength(maximumLength:16, MinimumLength =6,ErrorMessage ="密码长度{2}--{1}")]
    public string Password   { get; set; }

    /// <summary>
    /// 个人详细信息
    /// </summary>
   public virtual Person PersonInfo { get; set; }

    /// <summary>
    /// 创建该用户的操作员
    /// </summary>
    public virtual AccountClass? CreatedBy { get; set; }

   // [ForeignKey("Id")]
    //public int CreatedById { get; set; }

    [Required]
    public virtual List<Operation> Operations { get; set; }=new List<Operation>();

    public UserStausEnum UserStaus { get; set; } = UserStausEnum.Valid;

    //==================================================================
}

//===============================================================================================
//===============================================================================================
//===============================================================================================
//===============================================================================================


public enum UserStausEnum
{
    /// <summary>
    /// 正在用
    /// </summary>
    Valid=0,

    /// <summary>
    /// 临时停用
    /// </summary>
    Pause=100,

    /// <summary>
    /// 不再使用
    /// </summary>
    NotUsed=200,
}