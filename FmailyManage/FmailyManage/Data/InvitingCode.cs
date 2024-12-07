using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using FamilyManage.Data;

namespace FamilyManage.Server.Data;

//存放内存里面，节约时间


public class InvitingCode
{
	private const int DaysOff = -1;
	private const int Key0 = 0;

	private DateTime DeadTime { get => DateTime.Now.AddDays(DaysOff); }

	//private Dictionary<string, InvitingCodeData> codes { get; set; } = new Dictionary<string, InvitingCodeData>();
	private List<InvitingCodeData> codes { get; set; } = new List<InvitingCodeData>();

	public InvitingCode()
	{

	}

	public InvitingCodeData genCode()
	{
		removeOld();
		InvitingCodeData code = new InvitingCodeData();
		codes.Add(code);
		return code;
	}

	/// <summary>
	/// 一次性代码，检测的同时就删除
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public InvitingCodeData? getCode(string key)
	{
		removeOld();
		InvitingCodeData? cd = codes.Find(x => x.Key == key);
		if (cd != null)
			codes.Remove(cd);
		return cd;
	}

	/// <summary>
	/// 每10小时检测一下是否有老化的数据
	/// </summary>
	/// <returns></returns>
	private void removeOld()
	{
		codes.RemoveAll(x => x.Date < DeadTime);
	}
}
