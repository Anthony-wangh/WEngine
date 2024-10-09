using System.Threading.Tasks;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 资源加载模块管理器
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



        #region 同步加载

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <returns></returns>
        public T Load<T>(string location) where T : Object
        {
            return AssetLoad.Load<T>(location);
        }
        /// <summary>
        /// 同步加载预制体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        public GameObject LoadPrefabs(string name, string group = "")
        {
            var g = string.IsNullOrEmpty(group) ? PrefabsGroup : group;
            var go = Load<GameObject>(g + name);
            if (go == null)
            {
                Log.Debug("资源加载失败！" + name);
                return null;
            }
            return Instantiate(go);
        }

        /// <summary>
        /// 同步加载精灵图
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
                Log.Debug("资源加载失败！" + name);
                return null;
            }
            return asset;
        }
        #endregion



        #region 异步加载
        /// <summary>
        /// 异步加载资源
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
        /// 异步加载预制体
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="group">资源组名</param>
        /// <returns></returns>
        public async Task<GameObject> LoadPrefabsAsync(string name, string group)
        {
            var g = string.IsNullOrEmpty(group) ? PrefabsGroup : group;
            var asset = await LoadAsync<GameObject>(g + name);
            return Instantiate(asset);
        }
        /// <summary>
        /// 异步加载精灵图
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="group">资源组名</param>
        /// <returns></returns>
        public async Task<Sprite> LoadSpriteAsync(string name, string group)
        {
            var g = string.IsNullOrEmpty(group) ? SpriteGroup : group;
            var asset = await LoadAsync<Sprite>(g + name);
            return asset;
        }
        #endregion

        /// <summary>
        /// 卸载资源
        /// </summary>
        public void UnLoadAsset()
        {
            AssetLoad.Dispose();
        }
    }
}