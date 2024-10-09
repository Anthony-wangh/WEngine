using System.Threading.Tasks;
using YooAsset;

namespace WEngine
{
    /// <summary>
    /// 资源包状态事件
    /// </summary>
    public struct AssetPackageEvent : IEvent
    {
        public EOperationStatus State;
        public float Progress;
    }

    /// <summary>
    /// 资源加载模块
    /// </summary>
    public class AssetLoadModule : ModuleImp, IAssetLoadModule
    {
        public AssetLoaderSetting AssetLoadSetting;
        private bool _isInitialize;
        private ResourcePackage _package;
        private InitializationOperation _async;
        private AssetPackageEvent _assetPackageEvent;

        public void InitPackage()
        {
            if (AssetLoadSetting == null)
            {
                Log.Fatal("AssetLoadSetting is Null");
                return;
            }
            YooAssets.Initialize();
            // 创建默认的资源包
            string packageName = AssetLoadSetting.PackageName;
            _package = YooAssets.TryGetPackage(packageName);
            if (_package == null)
            {
                _package = YooAssets.CreatePackage(packageName);
                _async = _package.InitializeAsync(GetInitializeParameters());
                _async.Completed += OnCompleted;
                YooAssets.SetDefaultPackage(_package);
            }
        }


        private InitializeParameters GetInitializeParameters()
        {
            if (AssetLoadSetting == null)
                return null;
            if (AssetLoadSetting.IsLoadFromHost)
            {
                var hostPlayModeParameters = new HostPlayModeParameters();
                hostPlayModeParameters.RemoteServices = new RemoteServices();
                hostPlayModeParameters.DecryptionServices = new GameDecryptionServices();
                hostPlayModeParameters.BuildinQueryServices = new BuiltinQueryServices();
                hostPlayModeParameters.DeliveryQueryServices = new DefaultDeliveryQueryServices();
                hostPlayModeParameters.DeliveryLoadServices = new DeliveryLoadServices();
                return hostPlayModeParameters;
            }
            return new OfflinePlayModeParameters();
        }

        private void OnCompleted(AsyncOperationBase obj)
        {
            _assetPackageEvent.State = obj.Status;
            _assetPackageEvent.Progress = obj.Progress;
            CoreEngine.Event.Publish(_assetPackageEvent);

            if (obj.Status == EOperationStatus.Succeed)
            {
                _isInitialize = true;
            }
                
        }
        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <returns></returns>
        public T Load<T>(string location) where T : UnityEngine.Object
        {
            if (!_isInitialize) return null;
            var handle= _package.LoadAssetSync<T>(location);

            return handle.AssetObject as T;
        }
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<T> LoadAsync<T>(string location) where T : UnityEngine.Object
        {
            if (!_isInitialize) return null;
            var handle= _package.LoadAssetAsync<T>(location);
            await handle.Task;
            return handle.AssetObject as T;
        }

        public override void OnDispose()
        {
            _package.UnloadUnusedAssets();
        }

    }
}