using FamilyManage.Data;
using FamilyManage.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

using FamilyManage;
using FamilyManage.Server;

namespace FamilyManage.xxx
{
    /// <summary>
    /// app 初始化之前运行，然后马上就是app初始化
    /// </summary>
    public static class StartUp
    {
        private const string sqlString = "TempleContextSqlServer";
        private const string postgresqlString = "TempleContextPostgresql";
        private const string sqlStringPwd = "TempleContextSqlServerPwd";
        private static string keyConnectionString = sqlStringPwd;
        private static string? connectionString  ; 

        private static  IConfiguration? configuration;


        private static void testdb()
        {
            //string connectionString = "Server=QIN5521\\FAMILY;Database=fucktest;User Id=sa;Password=abcd1234;Encrypt=True;TrustServerCertificate=True;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))//"Server=qin5521\\FAMILY;Database=fucktest;User Id=sa;Password=abcd1234;Encrypt=True;TrustServerCertificate=True;"
                {
                    connection.Open();
                    Console.WriteLine("Successfully connected to the database.");
                    //connection.Database.al
                    // Your database logic here

                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Init must just before app.Build()
        /// </summary>
        /// <param name="builder"></param>
        public static void Initxx( WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            init0( services);
            //var app = builder.Build();
            //init1( app);;
        }
        public static void Init( WebApplicationBuilder builder)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            connectionString = "Server=qin5521\\FAMILY;Database=fucktest;User Id=sa;Password=abcd1234;Encrypt=True;TrustServerCertificate=True;";// configuration.GetConnectionString(keyConnectionString);

            testdb();
            //database init
            IServiceCollection services = builder.Services;
            try
            {
                //这是干嘛的？
                //services.AddSingleton(provider => configuration[connectionString] );

                Console.WriteLine($"{connectionString}");
                builder.Services.AddDbContext<TempleContext>(options=>
                options.UseSqlServer(connectionString)
                );
                //builder.Services.AddSingleton<TempleContext>();
                /*builder.Services.TryAddSingleton<DbContextOptions<TempleContext>>(options=>
                options.us
                );*/
               
            }
            catch(Exception ex)
            {

            }


            //logger init
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
            builder.Services.AddSingleton(new JsonSerializerOptions
            {
                //防止http转换json的时候循环引用
                ReferenceHandler = ReferenceHandler.Preserve,

            });

        }

        /// <summary>
        /// InitDb must just after app.Build()
        /// 数据库相关的初始化
        /// </summary>
        /// <param name="app"></param>
        public static   void InitDb( WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //配置dbcontext
                    //services.
                    var context = services.GetRequiredService<TempleContext>();
                    //context.Database.EnsureDeleted();
                    //context.Database.Migrate();
                    bool created = context.Database.EnsureCreated();
                    bool can = context.Database.CanConnect();
                    if (can)
                    {
                        int aa = context.Templates.Count();
                    }
                    else
                    {
                       // context.Database.Migrate();
                       //context.Database.en
                    }
                    seed(context);

                    ShareData.Names =  context.KeyValues.ToList();

                    //context.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                  /*  var logger = services.GetRequiredService<ILogger< Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                    string userName = Environment.UserName;//WindowsIdentity.GetCurrent().Name;
                    File.WriteAllText(@"f:\tt\log.txt", $"{userName}\n{ex.Message}");*/
                }
            }
        }

        //-------------------------------------------------

        public static void init0( IServiceCollection services)
        {
            services.AddDbContext<TempleContext>(options =>
            {
                options.UseSqlServer("Server=qin5521\\FAMILY;Database=testdb2;User Id=sa;Password=abcd1234;Encrypt=True;TrustServerCertificate=True;");
            });
        }


        public static void init1( WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                IServiceProvider sv = scope.ServiceProvider; ;
                var context = sv.GetRequiredService<TempleContext>();
                try
                {
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //--------------------------------------------------------------
        private static async void seed(TempleContext context)
        {
            //context.Database.EnsureCreated();

            if (!context.Accounts.Any())
            {
                NameClass name = new NameClass()
                {
                    FirstName = "张",
                    LastName="恨水",
                };
                Person person = new Person()
                {
                    Name= name,
                    
                };
                AccountClass account = new AccountClass()
                {
                    Name = "Admin",
                    Password = "abcd1234",
                    Role=RoleEnum.Adminitrator,
                    PersonInfo=person,
                };
                context.Accounts.Add(account);
            }
            if (!context.KeyValues.Any())
            {
                string[] values = { "身份证", "军官证", "驾驶证", "护照" };
                foreach (string s in values)
                {
                    context.KeyValues.Add(new KeyValue() { Name = KeyEnum.身份ID.ToString(), Value = s });
                }
            }

            await context.SaveChangesAsync();
            var aa=context.Accounts.FirstOrDefault();
        }

    }
}
