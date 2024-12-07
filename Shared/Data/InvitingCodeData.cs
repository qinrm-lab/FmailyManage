using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FamilyManage.Data;

/// <summary>
/// 邀请码注册,没有邀请码也可以注册
/// 邀请码只能一次性使用
/// 有效期过后会自动删除，暂定48小时，扣除postgresql误差，大概在24-48小时之间
/// 使用过后也自动删除
/// </summary>
public class InvitingCodeData
{
	/*[Key]
public int Id { get; set; }*/

	public const string KeyDefault = "123456";

	/// <summary>
	/// 邀请码
	/// </summary>
	[Required]
	public string Key { get; set; } = ($" {DateTime.Now.Ticks}hi".GetHashCode() % 10000).ToString("D04");

	/// <summary>
	/// 创建时间
	/// postgresql没有准确时间
	/// </summary>
	public DateTime Date { get; set; } = DateTime.Now;

	/// <summary>
	/// 创建邀请码的人,
	/// 数据库里面的ID
	/// </summary>
	public int Generator { get; set; } = -1;


}
