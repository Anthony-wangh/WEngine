

namespace WEngine
{
    /// <summary>
    /// Control和Model模块管理器
    /// </summary>
    public class MCModuleManager : ModuleManagerBase
    {
        private  ControlModule _controlModule;
        private  ModelModule _modelModule;
        private void Start()
        {
            _controlModule = ModuleSystem.Get<ControlModule>();
            _modelModule = ModuleSystem.Get<ModelModule>();
            InitControl();
        }


        private void InitControl()
        {
            //_controlModule.Register<StartControl>();
        }

        /// <summary>
        /// 获取逻辑子模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetControl<T>() where T: ControlBase
        {
            return _controlModule.Get<T>();
        }

        /// <summary>
        /// 获取数据层子模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModel<T>() where T : ModelBase, new()
        {
            return _modelModule.Get<T>();
        }
    }
}