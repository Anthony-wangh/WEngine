using UnityEngine.Localization.Settings;
namespace WEngine
{
    public interface ILocalization
    {
        string GetString(string key);
        void SetTable(string tableName);
    }

    /// <summary>
    /// ���ػ�������ģ��
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
        /// ���ñ��
        /// </summary>
        /// <param name="tableName"></param>
        public void SetTable(string tableName)
        {
            TextTableName = tableName;
        }
        /// <summary>
        /// ��ȡ���ػ��ı�
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