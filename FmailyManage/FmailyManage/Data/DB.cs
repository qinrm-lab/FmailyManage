using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorApptestdb
{
    // 数据库上下文
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MyModel> MyModels { get; set; }
    }

    // 数据模型
    public class MyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // 视图
}