using FamilyManage.Data;
using FamilyManage.Shared.Data;


namespace FamilyManage.Server
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public static class ShareData
    {
        /// <summary>
        /// 全局引用,反正数量不多
        /// 身份证信息等公用数据
        /// </summary>
        public static List<KeyValue> Names { get; set; } = new List<KeyValue>();
 
    }
}
