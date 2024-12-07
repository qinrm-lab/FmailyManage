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
/// 账号权限管理用
/// 值越小，权限越大
/// </summary>
public    enum RoleEnum
{
    /// <summary>
    /// 程序员
    /// 维护用账号，添加寺庙只能用这个账号
    /// </summary>
    Adminitrator=10,

    /// <summary>
    /// 寺庙第一个操作人员
    /// 负责给下面工作人员建立账号和修改
    /// </summary>
    AdminTemple=1000,

    /// <summary>
    /// 寺庙普通操作
    /// </summary>
    AccountTemple=2000,

    /// <summary>
    /// 用户自己操作
    /// </summary>
    Client=4000,

    /// <summary>
    /// 普通香客
    /// </summary>
    Visitor=5000,

    /// <summary>
    /// 只能看部分功能
    /// </summary>
    Guest=8000,

    /// <summary>
    /// 默认值
    /// 没任何权限
    /// </summary>
    Empty=9999,
}

/// <summary>
/// 角色
/// 权限用
/// </summary>
public struct RoleStruct
{
    //public int Id;
    public string Name {  get; set; }   
    public int Value { get; set;}

    public RoleStruct()
    {
       // Name = System.Reflection.MethodBase.GetCurrentMethod(). .DeclaringType.Name;

	}
    public RoleStruct(string name,int value)=>(Name,Value)=(name,value);
}


