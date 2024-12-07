using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyManage.Data;


namespace FamilyManage.Shared;

public class WebApiAddress
{
    public const string FamilyBase = "Family";
    public static string Family0(OperationEnum operation) => $"{FamilyBase}/{operation}";
    public static string Family0(OperationEnum operation, string id) => $"{FamilyBase}/{operation}/{id}";

    public class Family
    {
        //public static async Task<List<Family>> Read(int id) => $"{FamilyBase}/{OperationEnum.Read}/{id}";
    }
}

/// <summary>
/// 初始化的时候的KEY是假的，只是为了确定嵌套关系用，真正的KEY是在保存的时候生成的
/// </summary>
public enum ModuleStatus
{
    /// <summary>
    /// 进入初始化状态
    /// </summary>
    Init=0,
    /// <summary>
    /// 当前是模板状态
    /// </summary>
    Template=10,
    /// <summary>
    /// 当前是用户数据状态
    /// </summary>
    User=20,
}
