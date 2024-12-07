using FamilyManage.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyManage.Shared.Http
{
    /// <summary>
    /// GridTable在网络中传送的时候需要一些额外信息
    /// </summary>
    public class GridTableHttp
    {
        public GridTable Table { get; set; }
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public GridTableHttp(GridTable table, string name, int? parentId=null)
        {
            Table = table;
            Name = name;
            ParentId = parentId;
        }
    }

    /// <summary>
    /// GridModule在网络中传送的时候需要一些额外信息
    /// </summary>
    public class GridModuleHttp
    {  
        public GridModule Module { get; set; }
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public GridModuleHttp(GridModule module, string name, int? parentId = null)
        {
            Module = module;
            Name = name;
            ParentId = parentId;
        }
    }
}
