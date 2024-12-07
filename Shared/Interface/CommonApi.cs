using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;
using FamilyManage.Shared.Data;
using System.Collections;
using System.Xml.Linq;
using System.Text.Json;

namespace FamilyManage.Shared;

public static class CommonApi {
    public static string PasswordToDb(string password) => $"{password}秦瑞明";

    /// <summary>
    /// GridTable存入数据库之前的处理
    /// 二维数据不能直接存入数据库，要先转换成List再保存
    /// </summary>
    /// <param name="table"></param>
    public static void BeforeGridTableInDatabase0( GridTable? table)
    {
        if (table == null) 
            return;
        
        for(int i=0;i< table.modules.Length;i++)
        {
            for(int j = 0; j <table.modules[i]?.Length;j++)
            {
                GridModule gm = table.modules[i][j];
                gm.i = i;
                gm.j = j;
                table.ModulesList.Add(gm);
                //BeforeGridTableInDatabase( gm.Child);
            }
        }
    }//BeforeGridTableInDatabase

    /// <summary>
    /// 从数据库取出来后要先处理一下
    /// 把List转换成二维数组方便后续处理
    /// </summary>
    /// <param name="table"></param>
    public static void AfterGridTableOutDatabase0(GridTable? table)
    {
if (table == null)
            return;
        //先把所有的模块按照i,j排序
        //帮我找到哪里会导致table.ModulesList为null的
        if(table.ModulesList.Count == 0)
            return;
        table.ModulesList.Sort((a, b) => a.i * 100 + a.j - b.i * 100 - b.j);
        //再把模块按照i,j分组
        var groups = table.ModulesList.GroupBy(a => a.i);
        //再把分组的结果转换成二维数组
        //table.modules = groups.Select(a => a.ToArray()).ToArray();
        //最后递归处理子表
        foreach (var item in table.modules)
        {
            foreach (var gm in item)
            {
                AfterGridTableOutDatabase0(gm.Child);
            }
        }
        table.ModulesList.Clear() ;
    }//AfterGridTableOutDatabase
}



/// <summary>
/// 牌位的css style编辑
/// 可以单独一个编辑方便一些
/// </summary>
public class StyleListBase {
    public Style Style { get; set; } = new Style();
    public string Name { get; init; }

    public override string ToString() {
        return Style.ToString();
    }
}

public static class JsonHelper
{
    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    public static string Serialize<T>(T data)
    {
        return JsonSerializer.Serialize(data, options);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, options);
    }
}

public static class CommonVars {
    private static Dictionary<string, List<StyleSelect>> styleHash=new  Dictionary<string, List<StyleSelect>>(6);

    private const string writing_mode = "writing-mode";
    private const string text_align = "text-align";
    private static List<StyleSelect> writeModeSelects { get; set; } = new List<StyleSelect>()
    {
        new StyleSelect(){mode="水平排列",css="horizontal-tb"},
        new StyleSelect(){mode="竖排右左",css="vertical-rl"},
        new StyleSelect(){mode="竖排左右",css="vertical-lr"},
        new StyleSelect(){mode="垂直方向从上到下",css="sideways-rl"},
        new StyleSelect(){mode="垂直方向从下到上",css="sideways-lr"}
    };
    private static List<StyleSelect> textAlignSelects { get; set; } = new List<StyleSelect>()
            {
        new StyleSelect("左","left"),
        new StyleSelect("居中","center"),
        new StyleSelect("右","right"),
        new StyleSelect("两端对齐","justify"),
        new StyleSelect("继承","inherit")
    };



    public static StyleList styleList = new StyleList() {
        //css含义
        //https://www.runoob.com/cssref/css-pr-writing-mode.html     
        Styles = new List<Style>()
        {
             new Style() {Name=writing_mode,Value="vertical-lr",Prompt="书写方式",Selects=new List<StyleSelect>()
    {
        new StyleSelect(){mode="水平排列",css="horizontal-tb"},
        new StyleSelect(){mode="竖排右左",css="vertical-rl"},
        new StyleSelect(){mode="竖排左右",css="vertical-lr"},
        new StyleSelect(){mode="垂直方向从上到下",css="sideways-rl"},
        new StyleSelect(){mode="垂直方向从下到上",css="sideways-lr"}
    } },

            new Style(){Name=text_align,Value="center",Prompt="对齐方式",Selects=new List<StyleSelect>()
            {
        new StyleSelect("左","left"),
        new StyleSelect("居中","center"),
        new StyleSelect("右","right"),
        new StyleSelect("两端对齐","justify"),
        new StyleSelect("继承","inherit")
    } },
        }
    };

    /*private static List<StyleSelect> direction = new List<StyleSelect>()
        {
        new StyleSelect(){mode="从左到右",css="1"},
        new StyleSelect(){mode="从右到左",css="2"}
        };
    private static List<StyleSelect> writingMode = new List<StyleSelect>()
    {

        new StyleSelect("横排","horizatal-tbl"),
        new StyleSelect("竖排", "vertical-lr;")
    };
    private static List<StyleSelect> textAlign = new List<StyleSelect>()
    {
        new StyleSelect("左","left"),
        new StyleSelect("居中","center"),
        new StyleSelect("右","right")
    };*/

    private const string oK = "OK";

    private const bool dEBUG = false;

    public static string Writing_Mode_Str => writing_mode;
    public static string Text_Align_Str => text_align;

    public static StyleList StyleList { get => styleList; set => styleList = value; }
    //public static List<StyleSelect> Direction { get => direction; set => direction = value; }
    public static List<StyleSelect> WritingMode { get => writeModeSelects;  }
    public static List<StyleSelect> TextAlign { get => textAlignSelects;  }

    public static string OK => oK;

    public static bool DEBUG => dEBUG;

    //--------------------------------------------------------
    /// <summary>
    /// 静态变量难以自动化初始化
    /// 在program启动手动初始化一次
    /// </summary>
    public static void InitVars() {
        StyleList styleList = CommonVars.styleList;
        styleHash.Add(text_align, TextAlign);
        styleHash.Add("aa", WritingMode);
        styleHash.Add(writing_mode, WritingMode);
        //string name = "writing-mode";
        //Console.WriteLine($"debug now={name} {styleHash[name].Count}");
        int n = styleHash.Count;
        Console.WriteLine($"a={n}");
        foreach(var a in styleHash["aa"])
        {
            Console.WriteLine(a.css);
        }
        foreach (var style in styleHash.ToList())
        {
            Console.WriteLine($"\n{style.Key}-------{style.Value.Count}:{style.Value==writeModeSelects}||{style.Value==textAlignSelects}");
            foreach (var aa1 in style.Value)
            {
                Console.Write($"{aa1.css}=={aa1.css==null} ");
            }
        }
        
        foreach (var style in styleList.Styles) {
            string name = style.Name;
            style.Selects = GetStyleSelects(name);
          /*  if (style.Selects == null) {
                if (name == CommonVars.Text_Align_Str) {
                    style.Selects = CommonVars.TextAlign;
                    break;
                }
                if (name == CommonVars.Writing_Mode_Str) {
                    style.Selects = CommonVars.WritingMode;
                    break;
                }
            }*/
        }
    }//InitVars()

    public static List<StyleSelect>? GetStyleSelects(string name) =>styleHash[name];

}

/// <summary>
/// 简单加密一下密码再保存进数据库，防止网管窃取用户密码
/// </summary>
public static class MySha256 {
    /*  SHA256 mySha256;//= SHA256.Create("hi");

      public MySha256()
      {
          mySha256 = SHA256.Create("hi");
      }*/

    public static string Crypt(string password) {
        SHA256 sha256 = SHA256.Create();
        //sha256.Clear();
        byte[] bValue = null;
        string s = null;
        try {
            byte[] bp = Encoding.UTF8.GetBytes($"{password}秦瑞明");
            bValue = sha256.ComputeHash(bp);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bValue.Length; i++) {
                sb.Append(bValue[i].ToString("X2"));
            }
            s = sb.ToString();
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        return s == null ? "shit" : s;
    }
}


//====================================================================================================================================

public class PriviligeLevelAuthorizeAttribute : AuthorizeAttribute {
    public string Activity {
        get {
            if (!String.IsNullOrEmpty(Policy.Substring(Policy.Length))) {
                return Policy.Substring(Policy.Length);
            }
            return default(string);
        }

        set {
            Policy = $"{Policy}{value.ToString()}";
        }
    }
}

internal class OverMePolicyProvider : IAuthorizationPolicyProvider {
    const string POLICY_PREFIX = "PrivilegeLevel";

    public static string POLICY_PREFIX1 => POLICY_PREFIX;

    public async Task<AuthorizationPolicy> GetDefaultPolicyAsync() { return null; }
    //await Task.FromResult(new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.auth))


    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() {
        throw new NotImplementedException();
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName) {
        throw new NotImplementedException();
    }
}
