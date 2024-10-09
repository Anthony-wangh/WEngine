
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// UI模块管理器
    /// </summary>
    public class UIModuleManager : ModuleManagerBase
    {
        private  UIModule _uiModule;
        private UIModule UIModule=> _uiModule??= ModuleSystem.Get<UIModule>();

        private UIEventModule _uiEventModule;
        public UIEventModule UIEventModule => _uiEventModule ??= ModuleSystem.Get<UIEventModule>();
        public Canvas Canvas;
        public Camera UICamera;

        /// <summary>
        /// UI事件监听
        /// 点击，拖拽
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public T GetUiEvent<T>(GameObject go) where T: UIEventBase
        {
            return UIEventModule.Get<T>(go);
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public  void Show<T>() where T: IUIBase,new()
        {
            UIModule.Show<T>();
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ShowAsync<T>() where T : IUIBase, new()
        {
            UIModule.ShowAsync<T>();
        }
        /// <summary>
        /// 隐藏界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Hide<T>() where T : IUIBase
        {
            UIModule.Hide<T>();
        }
        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Close<T>() where T : IUIBase
        {
            UIModule.Close<T>();
        }

        public void DestroyView(UIBase ui)
        {
            UIModule.DestroyView(ui);
        }
    }
}