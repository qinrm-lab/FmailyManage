using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyManage.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace FamilyManage.Shared.Auth;

public static class Permissions
{
	/// <summary>
	/// 值越小权限越高
	/// </summary>
	public static class Roles
	{
		private const string prefix = "Permission.Role.";

		public static RoleStruct AdminitratorStruct = new RoleStruct("Adminitrator",100);
		public const string Adminitrator = "Permission.Role."+"Adminitrator";

		public static RoleStruct AdminTempleStruct = new RoleStruct("AdminTemple", 1000);
		public const string AdminTemple = "Permission.Role." + "AdminTemple";

		public static RoleStruct AccountTempleStruct = new RoleStruct("AccountTemple", 2000);
		public const string AccountTemple = "Permission.Role." + "AccountTemple";

		/// <summary>
		/// 普通法师
		/// </summary>
		public static RoleStruct MasterTempleStruct = new RoleStruct("MasterTemple", 3000);
		public const string MasterTemple = "Permission.Role." + "EmpMasterTemplety";

		/// <summary>
		/// 香客
		/// </summary>
		public static RoleStruct VisitorStruct = new RoleStruct("Visitor", 4000);
		public const string Visitor = "Permission.Role." + "Visitor";

		/// <summary>
		/// 只能看静态界面，不能操作,默认开放的功能
		/// </summary>
		public static RoleStruct GuestStruct = new RoleStruct("Guest", 8000);
		public const string Guest = "Permission.Role." + "Guest";

		/// <summary>
		/// 程序初始值，必须先赋值再使用
		/// </summary>
		public static RoleStruct EmptyStruct = new RoleStruct("Empty", 9999);
		public const string Empty = "Permission.Role." + "Empty";
	}
}

public class PermissionRequirement : IAuthorizationRequirement
{
	public string Permission { get; private set; }

	public PermissionRequirement(string permission)
	{
		Permission = permission;
	}
}


public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
	public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

	public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
	{
		FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
	}

	public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
	{
		return FallbackPolicyProvider.GetDefaultPolicyAsync();
	}

	public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
	{
		return FallbackPolicyProvider.GetDefaultPolicyAsync();
	}

	public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
	{
		if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
		{
			var policy = new AuthorizationPolicyBuilder();
			policy.AddRequirements(new PermissionRequirement(policyName));
			return Task.FromResult(policy.Build());
		}

		return FallbackPolicyProvider.GetPolicyAsync(policyName);
	}
}

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
	public PermissionAuthorizationHandler()
	{

	}

	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
	{
		if (context.User == null)
			return;
		var roles= context.User.Claims.Where(x => x.Type == "Role").ToList();
		var canAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");

		if (canAccess)
		{
			context.Succeed(requirement);
			return;
		}
	}
}