using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;

namespace FamilyManage.Shared.Data
{
    public class GridTableTest
    {
        public DateTime _id { get; set; } = DateTime.Now;

        public string sLen { get; set; }

        public int nRows { get; set; } = 1;

        public int nColumns { get; set; } = 1;

       // public List<GridModuleTest> modules { get; set; }= new List<GridModuleTest>();

        /// <summary>
        /// 表格单元
        /// </summary>
        public GridModuleTest[][] modules { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public GridModuleTest? father { get; set; } = null;

        /* public GridTable(int i,int j,GridModule father=null) 
         {
             _id = DateTime.Now;
             nRows = i;
             nColumns = j;
             this.father= father;
             init();
         }*/

        public async Task fuck()
        {
           await Task.Delay(100);
        }

        public  GridTableTest(string slen, GridModuleTest? father = null)
        {
            this.sLen = slen;
            int len = sLen.Length;
            this.father = father;
            modules = new GridModuleTest[len][];
            string[] ar = sLen.Select(x => x.ToString()).ToArray();
            //int[] intStr = ar.ToArray<int>();
            for (int i = 0; i < len; i++)
            {
                int n = int.Parse(ar[i]);
                modules[i] = new GridModuleTest[n];
                for (int j = 0; j < modules[i].Length; j++)
                {
                    modules[i][j] = new GridModuleTest() { i = i, j = j, words = $"{i} {j}", father = null };

                }
            }
        }
    }

    public class GridModuleTest
    {
        /// <summary>
        /// 第i行
        /// </summary>
        public int i { get; set; } = 0;

        /// <summary>
        /// 第j列
        /// </summary>
        public int j { get; set; }

        /// <summary>
        /// 显示与否。不显示可能需要占位，可能不占位
        /// </summary>
        public bool view { get; set; }

        /// <summary>
        /// 留字，需要自己填上去
        /// </summary>
        public string words { get; set; } = " ";

        /// <summary>
        /// 竖排横批，从右到左等
        /// </summary>
        public string style { get; set; } = "writing-mode:vertical-lr;text-align:center";

        //private GridTable? _child = null;
        public GridTableTest? child { get; set; }//{ get => _child; set => _child = value; } 

        public GridTableTest? father { get; set; } = null;

        public void setChild(GridTableTest? table = null)
        {
            this.child = table;
            if (table != null)
                table.father = this;
        }

        public void setFather(GridTableTest table)
        {
            this.father = table;
        }
    }
}
