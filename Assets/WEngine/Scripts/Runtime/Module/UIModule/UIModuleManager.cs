
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// UIģ�������
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
        /// UI�¼�����
        /// �������ק
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public T GetUiEvent<T>(GameObject go) where T: UIEventBase
        {
            return UIEventModule.Get<T>(go);
        }

        /// <summary>
        /// �򿪽���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public  void Show<T>() where T: IUIBase,new()
        {
            UIModule.Show<T>();
        }

        /// <summary>
        /// �򿪽���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ShowAsync<T>() where T : IUIBase, new()
        {
            UIModule.ShowAsync<T>();
        }
        /// <summary>
        /// ���ؽ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Hide<T>() where T : IUIBase
        {
            UIModule.Hide<T>();
        }
        /// <summary>
        /// �رս���
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