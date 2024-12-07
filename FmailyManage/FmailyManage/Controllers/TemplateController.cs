using FamilyManage.Server.Data;
using FamilyManage.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using FamilyManage.Shared;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Text.Json.Serialization;


namespace FamilyManage.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateController : ControllerBase
    {
        private TempleContext _context { get; init; }
        public TemplateController(TempleContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 需要重新定义ID才能入库
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        [HttpPost("save")]
        //[Microsoft.AspNetCore.Mvc.Route("/api/template/save")]
        public  async Task<IActionResult> Save([FromBody]Template template)
        {
            try
            {
                //template.Id = 0;
               // Template at = template;
               // return Ok(at);
                string name= _context.Database.ProviderName;
                //template.Creater =await _context.Accounts.FirstOrDefaultAsync();//空？
                //CommonApi.BeforeGridTableInDatabase(template.GridTable);
                await _context.Templates.AddAsync(template);
                await _context.SaveChangesAsync();
                Template? tt = await _context.Templates.FirstOrDefaultAsync(x => x.Name == template.Name);
                int aa = _context.Templates.Count();
                tt = new Template()
                {
                    Name = template.Name,
                    JsonString = template.JsonString,
                    //GridTable = template.GridTable,
                };
                return Ok(tt);
               // return CommonVars.OK;
               string st = MyJson.Serialize(tt?.GridTable);
                //WeatherForecast wf = new WeatherForecast();
                //return Ok(wf);
                string xx = tt.GridTable.Id.ToString();
                int txt = _context.Templates.Count();

                return Ok(tt);
            }
            catch (Exception ex)
            {
                return NotFound(  ex.Message);
            }
        }//Save()

        [HttpPost("test")]
        //[Microsoft.AspNetCore.Mvc.Route("/api/template/test")]
        public async Task<IActionResult> Test([FromBody] GridTable tt)
        {
            /* try
             {
                 CommonApi.BeforeGridTableInDatabase(tt.GridTable);
                 string s0 = MyJson.Serialize(tt.GridTable);

                 await _context.Templates.AddAsync(tt);
                 string s1 = MyJson.Serialize(tt.GridTable);
                 //var xx = tt.GridTable.modules[0][0].child;
                 _context.ChangeTracker.DetectChanges();
                 await _context.SaveChangesAsync();
                 Template? template = await _context.Templates.FirstOrDefaultAsync(x => x.Name == tt.Name);
                 string s2 = MyJson.Serialize(tt.GridTable);
                 return Ok(tt);
             }
             catch (Exception ex)
             {
                 return NotFound(ex.Message);
             }*/
            Template tp = new Template();
           return Ok(tp);
        }

        /// <summary>
        /// 根据模板名提取模板json字符串返回
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("/api/template/restore")]
        public async Task<Template?> Restore([FromBody] string name)
        {
            int tt = _context.Templates.Count();
            Template? tp;
            try
            {
                tp = await _context.Templates.FirstOrDefaultAsync(x => x.Name == name);
               //var tp1 = await _context.Templates.FirstOrDefaultAsync();// (x => x.Name == name);
            }
            catch (Exception ex)
            {
                tp = null;
                //return tp;
            }
            //if(tp!=null)
            //tp.GridTable = null;
            return tp;
        }
    }

}
