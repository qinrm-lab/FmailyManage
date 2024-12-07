using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore.tools;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Proxies;
using FamilyManage.Data;
using FamilyManage.Shared.Data;
using FamilyManage.Shared;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Data.SqlTypes;

namespace FamilyManage.Server.Data;

/// <summary>
/// 寺庙业务数据库
/// </summary>
public class TempleContext : DbContext
{


    /// <summary>
    /// 香客资料表
    /// </summary>
    public DbSet<Person> Persons { get; set; }

    public Person Personss { get; set; }
    /// <summary>
    /// 家族名字
    /// </summary>
    public DbSet<Family> Families { get; set; }

    /// <summary>
    /// 操作人员账号
    /// </summary>
    public DbSet<AccountClass> Accounts { get; set; }

    /// <summary>
    /// 各种选项列表，
    /// 例如身份证，军官证
    /// </summary>
    public DbSet<KeyValue> KeyValues { get; set; }

    /// <summary>
    /// 寺庙
    /// </summary>
    public DbSet<Temple> Temples { get; set; }

    /// <summary>
    /// 资金出入
    /// </summary>
    public DbSet<Fund> Money { get; set; }

    /// <summary>
    /// 牌位模板
    /// </summary>
    public DbSet<Template> Templates { get; set; }


    /// <summary>
    /// 寺庙操作人员的各种操作记录
    /// </summary>
    public DbSet<Operation> Operations { get; set; }

   public DbSet<GridTable> GridTables { get; set; }

    public DbSet<GridModule> GridModules { get; set; }
    //==========************************************************************************************************************************===================

    private IConfiguration Configuration { get; set; }
    public TempleContext(DbContextOptions options) : base(options)
    {
        //base(options);
        //Configuration = configuration;
        // int i = 1 + 9;
        //base(options);
        int i = 0;
       // options.
    }

  /*  public TempleContext() : base()
    {
        //int i = 1 + 9;
    }*/

    /// <summary>
    /// 数据库关系初始化
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        
        builder.Entity<Person>()
             .HasOne(p => p.From)
             .WithMany(x => x.To)
             .OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Person>()
            .Navigation(x => x.To)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Entity<Person>()
            .Navigation(x => x.From)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Entity<NameClass>();

        builder.Entity<AccountClass>()
            .HasMany(x => x.Operations).WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Family>()
            // .wi(x=>x.Operations)
            .HasMany(x => x.Operations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Fund>()
            .HasMany(x => x.Operations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<GridTable>()
            .HasOne(x=>x.father)
            .WithOne(module=>module.Child)
            .HasForeignKey<GridTable>(x=>x.ParentId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        builder.Entity<GridTable>()
            .Navigation(x => x.ModulesList)
            .AutoInclude();

        //避免null引用的excetion
        builder.Entity<GridTable>()
            .HasKey(e => e.Id);

        //可以为null，不是必须有Child
        builder.Entity<GridModule>()
            .HasOne(x=>x.Parent)
            .WithMany(table=>table.ModulesList)
            .HasForeignKey(t=>t.ParentId)
            .OnDelete(deleteBehavior:DeleteBehavior.NoAction);
       
        /*builder.Entity<GridModule>()
            .Navigation(e => e.Child)
            .HasField("_Child")
            .UsePropertyAccessMode(PropertyAccessMode.Property);*/

        /*builder.Entity<GridModule>()
            .Navigation(x => x.Child)
            .AutoInclude();*/

        /* builder.Entity<Operation>()
             .HasOne(x => x.Operator);
            /* .WithMany()
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);


         /* builder.Entity<Person>()
              .HasIndex(x => x.Name.LastName + x.Name.FirstName);*/

        /*builder.Entity<AccountClass>()//MySha256.Crypt( "bb").Substring(0,16)
			.HasData(new AccountClass() { Id = 1, Name = "aa", Password = MySha256.Crypt("bb1234"), Role=RoleEnum.AccountTemple});*/
       /* builder.Entity<Temple>()
             .HasMany(p => p.p)
             .WithOne()
             .IsRequired(false);*/
    }

    /*protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("Server=(local)\\FAMILY;Database=fucktest;User Id=sa;Password=abcd1234;TrustServerCertificate=true;"));
        //.UseNpgsql(Configuration.GetConnectionString(connectionString));
        base.OnConfiguring(options);
    }*/

    //===========================================================================================================================================================
    /// <summary>
    /// 检查客户端返回的数据有没给修改
    /// </summary>
    public int SetChecking(string id)
    {
        string salt = "意难平" + Configuration.GetConnectionString("TempleContextPostgresql") + id;

        return salt.GetHashCode();
    }

    /// <summary>
    /// 检查是否合法
    /// 避免客户端给乱改了数据还随便写进去
    /// </summary>
    /// <param name="id"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public bool IsValid(string id, int code)
    {
        return code == SetChecking(id);
    }


}

public class ValidCheck
{
    public ValidCheck(IConfiguration configuration)
    {

    }
}


