using System;
using System.Collections.Generic;

namespace WEngine
{
    /// <summary>
    /// ÏìÓ¦Ê½ÈÝÆ÷
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReactiveCollection<T> : IEventObject
    {
        public List<T> Value
        {
            set
            {
                _value = value;
                OnUpdate?.Invoke(Value);
            }
            get { return _value; }
        }
        private List<T> _value;
        public Action<T> OnAddItem;
        public Action<int> OnRemoveItem;
        public Action<int, T> OnReplace;
        public Action<List<T>> OnUpdate;

        public ReactiveCollection(List<T> list)
        {
            _value = list;
        }


        public void AddItem(T t, bool isPublish = false)
        {
            if (_value == null)
                _value = new List<T>();
            _value.Add(t);
            if (isPublish)
                OnAddItem?.Invoke(t);
        }

        public void RemoveItem(int index, bool isPublish = false)
        {
            if (_value.IsNullOrEmpty())
                return;
            if (index < 0 || index > _value.Count - 1)
                return;
            _value.RemoveAt(index);
            if (isPublish)
                OnRemoveItem?.Invoke(index);
        }

        public void Replace(int index, T t, bool isPublish = false)
        {
            if (_value.IsNullOrEmpty())
                return;
            if (index < 0 || index > _value.Count - 1)
                return;
            _value[index] = t;
            if (isPublish)
                OnReplace?.Invoke(index, t);
        }

        public void SetValue(List<T> values, bool isPublish = false)
        {
            _value = values;
            if (isPublish)
                OnUpdate?.Invoke(values);
        }

        //¶©ÔÄ
        public void Subscribe(Action<List<T>> action, bool isPublish = false)
        {
            OnUpdate += action;
            if (isPublish)
                OnUpdate?.Invoke(_value);
        }

        public void UnSubscribeOnDestroy()
        {
            OnAddItem = null;
            OnRemoveItem = null;
            OnReplace = null;
            OnUpdate = null;
        }
    }
}