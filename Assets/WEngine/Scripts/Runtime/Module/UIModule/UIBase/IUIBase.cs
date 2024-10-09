
using UnityEngine;

namespace WEngine
{
    public interface IUIBase
    {
        bool IsShow { get;}

        void Init(Transform view);
        void Show();
        void Hide();
        void Close();

        void OnInit();
        void OnShow();
        void OnHide();
        void OnClose();
        void OnUpdate();

        void OnSubscribe();

        void UnSubscribe();
    }
}