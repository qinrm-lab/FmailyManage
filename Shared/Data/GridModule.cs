using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using FamilyManage.Data;
using System.Text.Json;

using FamilyManage.Shared;
using Npgsql.Replication;
using Microsoft.EntityFrameworkCore;

using Mykey= System.Guid;

namespace FamilyManage.Shared.Data
{


    //GridTable可以包括多个GridModule
    //GridModule只可以包括1个GridTable


    /// <summary>
    /// css,
    /// DIV排版方式：从左到右，中间对齐等
    /// </summary>
    public class Style
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 例如：writing-mode
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// 例如:vertical-lr
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 例如横排竖排,就是中文操作提示
        /// </summary>
        public string Prompt { get; init; }
        /// <summary>
        /// 选择哪个
        /// 客户端界面选择用，数据库里存储无意义,所以不需要 get,set属性
        /// </summary>
        public List<StyleSelect>? Selects ;//{ get; set; }
        public override string ToString() => $"{Name}:{Value};";
    }

    public class StyleList
    {
        [Key]
        public int Id { get; set; }

        public virtual List<Style> Styles { get; set; } = new List<Style>();

        /// <summary>
        /// 转换成CSS
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "";
            foreach (Style style in Styles)
                s += style.ToString();
            return s;
        }

      public Style? GetStyle(string name)=> Styles.FirstOrDefault(x => x.Name == name);
        
    }

    [JsonSerializable(typeof(GridTable))]
    public class GridTable
    {
        [Key]
        //[DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public Mykey Id { get;set; }=Mykey.NewGuid();

        /// <summary>
        /// 用户无需理会，编程确定Id状态的，因为Id需要在进入数据库的时候重新生成一个可用的
        /// </summary>
        public ModuleStatus status { get; set; } = ModuleStatus.Init;

        public string sLen { get; set; }

        public int nRows { get; set; } = 1;

        public int nColumns { get; set; } = 1;

        //public List<GridModule> modules { get; set; }= new List<GridModule>();

        private GridModule[][] _modules;
        /// <summary>
        /// 表格单元
        /// 方便使用，但efcore不支持存储在数据库
        /// </summary>
        [NotMapped]
        public GridModule[][] modules 
        {
            get
            {
                if(_modules!=null)
                    return _modules;
                else
                {
                    if (ModulesList?.Count != 0)
                    {
                        ModulesList.Sort((a, b) => a.i * 100 + a.j - b.i * 100 - b.j);
                        //再把模块按照i,j分组
                        var groups = ModulesList.GroupBy(a => a.i);
                        //再把分组的结果转换成二维数组
                        _modules = groups.Select(a => a.ToArray()).ToArray();
                        return _modules;
                    }
                    else
                        return null;
                }    
            }
            //private set;
        }
        

        /// <summary>
        /// 存储用的表格单元
        /// 相当于GridTable的Children
        /// </summary>
       // [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ChildModules ModulesList { get; set; } = new ChildModules();

        //private Guid? _fatherId = null;
        private GridModule? _father = null;
        /// <summary>
        /// 父节点
        /// </summary>
        public virtual GridModule? father 
        { 
            get=>_father;
            set
            {
                _father = value;
                if (value != null)
                {
                    ParentId = value.id;
                    //value.setChild(this);
                }
                else
                    ParentId = null;
            }
        }

        /// <summary>
        /// 外键
        /// GridModule的主键
        /// </summary>
        [ForeignKey(nameof(GridModule.id))]
        public Mykey? ParentId { get;private set; }
       /*     get => _fatherId;
            private set { _fatherId = value; } 
        }*/



        public GridTable()
        {

        }

        //[JsonConstructor]
        public GridTable(string slen, GridModule? father = null)
        {
            this.sLen = slen;
            int len = sLen.Length;
            this.father = father;
            _modules = new GridModule[len][];
            string[] ar = sLen.Select(x => x.ToString()).ToArray();
            //int[] intStr = ar.ToArray<int>();
           //GridModule gm;
            for (int i = 0; i < len; i++)
            {
                int n = int.Parse(ar[i]);
                _modules[i] = new GridModule[n];
                for (int j = 0; j < modules[i].Length; j++)
                {
                    _modules[i][j] = new GridModule() { i = i, j = j, };
                    setChild(_modules[i][j]);
                    //gm= new GridModule() { i = i, j = j, };
                    //ModulesList.Add(_modules[i][j]);
                    //_modules[i][j].p
                }
            }
        }



        public void Init()
        {
            int len = sLen.Length;
            //modules = new GridModule[len][];
            string[] ar = sLen.Select(x => x.ToString()).ToArray();
            //int[] intStr = ar.ToArray<int>();
            for (int i = 0; i < len; i++)
            {
                int n = int.Parse(ar[i]);
                modules[i] = new GridModule[n];
                for (int j = 0; j < modules[i].Length; j++)
                {
                    modules[i][j] = new GridModule() { i = i, j = j, };
                }
            }
        }



          public void setChild(GridModule? child=null)
          {
            if (child != null)
            {
                ModulesList.Add(child);
                //child.ParentId = Id;
                child.Parent = this;
            }
          }

        /// <summary>
        /// 序列化
        /// 级联的也要一起序列化
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve };
            return JsonSerializer.Serialize<GridTable>(this, options);
        }

    }//GridTable

    /// <summary>
    /// HTML CSS style选择
    /// 左到右，居中，垂直等
    /// </summary>
    public class StyleSelect
    {
        public string mode { get; set; }
        public string css { get; set; }

        public StyleSelect()
        {
            mode = "";
            css = "";
        }

        public StyleSelect(string mode, string css)
        {
            this.mode = mode;
            this.css = css;
        }
    }

    /// <summary>
    /// GridTable的子节点有多个GridModule
    /// </summary>
    public class ChildModules:List<GridModule>
    {
        public new void Add(GridModule module)
        {
            base.Add(module);
        }
    }

    /// <summary>
    /// GridTable下面有多个GridModule
    /// GridModule下面有一个GridTable
    /// </summary>
    [JsonSerializable(typeof(GridModule))]
    public class GridModule
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public Mykey id { get; set; } = Mykey.NewGuid();


        /// <summary>
        /// 第i行
        /// </summary>
        public int i { get; set; } = 0;



        /// <summary>
        /// 第j列
        /// </summary>
        public int j { get; set; } = 0;

        /// <summary>
        /// 显示与否。不显示可能需要占位，可能不占位
        /// </summary>
        public bool view { get; set; }

        /// <summary>
        /// css,该字段的CSS
        /// </summary>
        public string css => styles.ToString();

        /// <summary>
        /// 编辑测试用字
        /// </summary>
        public string words { get; set; } = Guid.NewGuid().ToString().Substring(0,5);

        /// <summary>
        /// 竖排横批，从右到左等
        /// </summary>
        //public string style { get; set; } = "writing-mode:vertical-lr;text-align:center";
        public virtual StyleList styles { get; set; } = new StyleList()
        {
            Styles = new List<Style>()
              {
                 new Style(){Name="writing-mode",Value="vertical-lr",Prompt="书写方式"},
                   new Style(){Name="text-align",Value="center",Prompt="对齐方式"}
              },
        };


        private GridTable? _Parent = null;
        public virtual GridTable? Parent 
        { 
            get=>_Parent;
            set
            {
                if (_Parent == value) return;
                _Parent = value;
                if(_Parent != null)
                {
                    this.ParentId = _Parent.Id;
                }
                else
                    ParentId = null;
            }
        }

        [ForeignKey(nameof(GridTable.Id))]
        public Guid? ParentId { get;private set; } 

        //public 

        //private GridTable? _Child = null;

        //public Guid ChildId { get; private set; }=Guid.NewGuid();
        //[BackingField(nameof(_Child))]
        /// <summary>
        /// 子套件
        /// </summary>
        public virtual GridTable? Child  { get; set; }
       /* {
            get=>_Child;
            set
            {
                _Child = value;
                if(value != null)
                {
                    _Child.father=this;
                    //_Child.fatherId = id;
                }
            }
        } */
        //public string myid { get; set; } = MyJson.GetID();
        //public Guid? childid { get; set; }

        // public GridTable? father { get; set; } = null;

        public GridTable setChild(GridTable? table = null)
        {
            this.Child = table;
            /*if (table != null)
            {
                //childid = table.Id;
                //table.fatherid = myid;
                table.father = this;
                table.fatherId = this.id;
                
            }*/
            return table;
        }

    }//GridModule

    public static class MyJson
    {
        private static Dictionary<Mykey, object> dic = new Dictionary<Mykey, object>();
        public static string GetID() => Guid.NewGuid().ToString();

        public static string Serialize(GridTable? table)
        {
            if (table == null)
                return "";
            JsonSerializerOptions options = new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve };
            return JsonSerializer.Serialize<GridTable>(table, options);
        }
        public static GridTable? Deserialize(string jsonString)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve };
            return JsonSerializer.Deserialize<GridTable>(jsonString, options);
        }

        public static GridTable? DeserializeOld(string template)
        {
            GridTable table = null;
            dic.Clear();
            try
            {
                JsonSerializerOptions setting = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = false,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                    
                };
                table = JsonSerializer.Deserialize<GridTable?>(template,setting);

                //init(table);
            }
            catch (Exception ex)
            {
                ;
            }
            return table;
        }

        private static void init(GridTable table)
        {
            dic.Add(table.Id, table);
            for (int i = 0; i < table.modules.Length; i++)
            {
                GridModule[] modules = table.modules[i];
                for (int j = 0; j < modules.Length; j++)
                {
                    dic.Add(modules[j].id, modules[j]);
                    if (modules[j].Child != null)
                    {
                        GridTable tt = modules[j].Child;
                        modules[j].Child = tt;
                        tt.father = modules[j];
                        init(tt);
                    }
                }
            }
        }

    }


}



