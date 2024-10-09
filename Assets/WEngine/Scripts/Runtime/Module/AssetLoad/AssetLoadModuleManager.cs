using System.Threading.Tasks;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// ��Դ����ģ�������
    /// </summary>
    public class AssetLoadModuleManager : ModuleManagerBase, IAssetLoadModule
    {

        private AssetLoadModule _assetLoad;
        private AssetLoadModule AssetLoad => _assetLoad ??= ModuleSystem.Get<AssetLoadModule>();


        private const string PrefabsGroup = "Prefabs_";
        private const string SpriteGroup = "Sprite_";
        private void Start()
        {
            AssetLoad.AssetLoadSetting = SettingsUtil.WEngineSetting.AssetLoadSetting;
            AssetLoad.InitPackage();
        }



        #region ͬ������

        /// <summary>
        /// ͬ��������Դ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <returns></returns>
        public T Load<T>(string location) where T : Object
        {
            return AssetLoad.Load<T>(location);
        }
        /// <summary>
        /// ͬ������Ԥ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        public GameObject LoadPrefabs(string name, string group = "")
        {
            var g = string.IsNullOrEmpty(group) ? PrefabsGroup : group;
            var go = Load<GameObject>(g + name);
            if (go == null)
            {
                Log.Debug("��Դ����ʧ�ܣ�" + name);
                return null;
            }
            return Instantiate(go);
        }

        /// <summary>
        /// ͬ�����ؾ���ͼ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public Sprite LoadSprite(string name, string group = "")
        {
            var g = string.IsNullOrEmpty(group) ? SpriteGroup : group;
            var asset = Load<Sprite>(g + name);
            if (asset == null)
            {
                Log.Debug("��Դ����ʧ�ܣ�" + name);
                return null;
            }
            return asset;
        }
        #endregion



        #region �첽����
        /// <summary>
        /// �첽������Դ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<T> LoadAsync<T>(string location) where T : Object
        {
            var go = await AssetLoad.LoadAsync<T>(location);
            return go;
        }

        /// <summary>
        /// �첽����Ԥ����
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="group">��Դ����</param>
        /// <returns></returns>
        public async Task<GameObject> LoadPrefabsAsync(string name, string group)
        {
            var g = string.IsNullOrEmpty(group) ? PrefabsGroup : group;
            var asset = await LoadAsync<GameObject>(g + name);
            return Instantiate(asset);
        }
        /// <summary>
        /// �첽���ؾ���ͼ
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="group">��Դ����</param>
        /// <returns></returns>
        public async Task<Sprite> LoadSpriteAsync(string name, string group)
        {
            var g = string.IsNullOrEmpty(group) ? SpriteGroup : group;
            var asset = await LoadAsync<Sprite>(g + name);
            return asset;
        }
        #endregion

        /// <summary>
        /// ж����Դ
        /// </summary>
        public void UnLoadAsset()
        {
            AssetLoad.Dispose();
        }
    }
}