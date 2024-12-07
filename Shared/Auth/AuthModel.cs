using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using FamilyManage.Shared;

namespace FamilyManage.Shared.Auth
{
    /// <summary>
    /// UI,传输用的
    /// </summary>
   public class LoginResult
    {
        public string Message { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string JwtBearer { get;set; }

        public DateTime Expiry { get; set; }= DateTime.Now.AddDays(1);
        public bool success { get; set; }
    }

    public class  LoginBase
    {
        [Required(ErrorMessage = "输入用户名")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 登录，UI用
    /// </summary>
    public class LoginModel:LoginBase
    {


        [Required(ErrorMessage = "输入密码")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 18, MinimumLength = 6, ErrorMessage = "密码长度需要{2}-{1}字")]
        public string Password { get; set; } = "";
    }


    /// <summary>
    /// 注册用户,UI用
    /// </summary>
    public class RegModel:LoginModel
    {
        [Required(ErrorMessage = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "两次输入密码不同")]
        public string confirmPwd { get; set; } = "";

        [Required]
        public string inviteCode { get; set; } = "123456";
    }

    /// <summary>
    /// 修改密码,UI用
    /// </summary>
	public class ChangePasswordModel:LoginBase
	{
        [Required(ErrorMessage = "输入密码")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 18, MinimumLength = 6, ErrorMessage = "密码长度需要{1}-{0}字")]
        public string passwordOld { get; set; } = "";

        [Required(ErrorMessage = "输入密码")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 18, MinimumLength = 6, ErrorMessage = "密码长度需要{1}-{0}字")]
        public string password { get; set; } = "";
	}

	public  interface IPassword// LoginInterfaceModel:LoginBase
    {
        //private string _password;
        string password { get; set; }
        string passwordOld { get; set; }
    }

    public static class IPasswordExt
    {
        public static string password="";
    }

    //===================================================================================================================================
    /// <summary>
    /// 登录，注册，修改密码，客户端跟服务端交换数据都是用这个接口
    /// </summary>
    public class LoginInterfaceModel : LoginBase,IPassword
    {
        //private string _password;
        //private string _passwordOld;
       /* public string password
        {
            get => _password;
            set => MySha256.Crypt(value);
        }

        /// <summary>
        /// 登录注册时候这里为null就可以了
        /// </summary>
        public string passwordOld
        {
            get => _passwordOld;
            set => MySha256.Crypt(value);
        }*/

        public string password { get;  set; }
        public string passwordOld { get; set; }

        public LoginInterfaceModel()
        {

        }

        public LoginInterfaceModel(LoginModel login)
        {
            Name = login.Name;
            password =MySha256.Crypt( login.Password);
            
        }

        /// <summary>
        /// 赋值，登录的
        /// </summary>
        /// <param name="reg"></param>
        public LoginInterfaceModel(RegModel reg)=>(Name, password)=(Name,reg.Password);
       
        //public LoginInterfaceModel()
    }


}
