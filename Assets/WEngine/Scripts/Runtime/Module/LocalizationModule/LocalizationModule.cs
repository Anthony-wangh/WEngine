using UnityEngine.Localization.Settings;
namespace WEngine
{
    public interface ILocalization
    {
        string GetString(string key);
        void SetTable(string tableName);
    }

    /// <summary>
    /// 本地化多语言模块
    /// </summary>
    public class LocalizationModule : ModuleImp, ILocalization
    {
        private string TextTableName = "Text";
        private string AssetTableName = "Asset";
        public override void OnInit()
        {
            
        }
        public override void OnSubscribe()
        {
            base.OnSubscribe();
        }

        public override void OnDispose()
        {
            base.OnDispose();
        }
        /// <summary>
        /// 设置表格
        /// </summary>
        /// <param name="tableName"></param>
        public void SetTable(string tableName)
        {
            TextTableName = tableName;
        }
        /// <summary>
        /// 获取本地化文本
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            var entry= LocalizationSettings.StringDatabase.GetTableEntry(TextTableName,key).Entry;
            if (entry == null)
                return key;
            return  entry.GetLocalizedString();
        }


    }
}