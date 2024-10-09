

namespace WEngine
{
    /// <summary>
    /// Control��Modelģ�������
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
        /// ��ȡ�߼���ģ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetControl<T>() where T: ControlBase
        {
            return _controlModule.Get<T>();
        }

        /// <summary>
        /// ��ȡ���ݲ���ģ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModel<T>() where T : ModelBase, new()
        {
            return _modelModule.Get<T>();
        }
    }
}