using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WEngine
{
    public class UIEventBase : MonoBehaviour
    {
    }
    /// <summary>
    /// UI事件（点击，选择，拖拽）
    /// </summary>
    public class UIEventModule : ModuleImp
    {
        public T Get<T>(GameObject go) where T : UIEventBase
        {
            T listener = go.GetComponent<T>();
            if (listener == null) listener = go.AddComponent<T>();
            return listener;
        }
    }

    public class UIClick : UIEventBase, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public Action<GameObject> OnClick;
        public Action<GameObject> OnDoubleClick;
        public Action<GameObject> OnDown;
        public Action<GameObject> OnEnter;
        public Action<GameObject> OnExit;
        public Action<GameObject> OnUp;

        /// <summary>
        /// 已经点击过了
        /// </summary>
        private bool _isOnClick;

        private bool _isDoubleClick;

        /// <summary>
        /// 最后点击时间
        /// </summary>
        private float _lastClickTime;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickTime - _lastClickTime < 0.2f && !_isDoubleClick)
            {
                _isDoubleClick = true;
                Invoke("CancelDoubleClick", 0.3f);
                OnDoubleClick?.Invoke(gameObject);
            }
            if (!_isDoubleClick)
                _lastClickTime = eventData.clickTime;

            if (_isOnClick)
                return;

            Invoke("CancelClick", 0.3f);
            _isOnClick = true;
            OnClick?.Invoke(gameObject);
        }

        private void CancelClick()
        {
            _isOnClick = false;
        }

        private void CancelDoubleClick()
        {
            _isDoubleClick = false;
        }

        public void OnPointerDown(PointerEventData eventData) => OnDown?.Invoke(gameObject);
        public void OnPointerEnter(PointerEventData eventData) => OnEnter?.Invoke(gameObject);
        public void OnPointerExit(PointerEventData eventData) => OnExit?.Invoke(gameObject);
        public void OnPointerUp(PointerEventData eventData) => OnUp?.Invoke(gameObject);

        // ReSharper disable once UnusedMember.Local
        void OnDestroy()
        {
            OnClick = null;
            OnDown = null;
            OnEnter = null;
            OnExit = null;
            OnUp = null;
        }
    }

    // ReSharper disable once InconsistentNaming
    public class UISelect : UIEventBase, ISelectHandler, IDeselectHandler, IUpdateSelectedHandler
    {
        public Action<GameObject> Select;
        public Action<GameObject> DeSelect;
        public Action<GameObject> UpdateSelect;

        public void OnSelect(BaseEventData eventData) => Select?.Invoke(gameObject);
        public void OnDeselect(BaseEventData eventData) => DeSelect?.Invoke(gameObject);
        public void OnUpdateSelected(BaseEventData eventData) => UpdateSelect?.Invoke(gameObject);

        // ReSharper disable once UnusedMember.Local
        void OnDestroy()
        {
            Select = null;
            DeSelect = null;
            UpdateSelect = null;
        }
    }

    // ReSharper disable once InconsistentNaming
    public class UIDrag : UIEventBase, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Action<PointerEventData> Drag;
        public Action<PointerEventData> BeginDrag;
        public Action<PointerEventData> EndDrag;

        public void OnDrag(PointerEventData eventData) => Drag?.Invoke(eventData);
        public void OnBeginDrag(PointerEventData eventData) => BeginDrag?.Invoke(eventData);
        public void OnEndDrag(PointerEventData eventData) => EndDrag?.Invoke(eventData);

        // ReSharper disable once UnusedMember.Local
        void OnDestroy()
        {
            Drag = null;
            BeginDrag = null;
            EndDrag = null;
        }
    }
}