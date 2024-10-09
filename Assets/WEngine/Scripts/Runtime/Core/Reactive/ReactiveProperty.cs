using System;
using UnityEngine;

namespace WEngine
{   
    /// <summary>
    /// 响应式属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReactiveProperty<T> : IEventObject
    {
        public ReactiveProperty(T defaultValue = default)
        {
            _value = defaultValue;
        }
        private Action<T> _onValueChanged;
        protected T _value;
        public T Value
        {
            get => GetValue();
            set
            {
                if (value == null && GetValue() == null) return;
                if (value != null && value.Equals(GetValue())) return;

                SetValue(value);
                _onValueChanged?.Invoke(value);
            }
        }

        public void UnSubscribeOnDestroy()
        {
            _onValueChanged = null;
        }
        protected virtual void SetValue(T newValue)
        {
            _value = newValue;
        }

        protected virtual T GetValue()
        {
            return _value;
        }

        public void SetValueWithoutPublish(T newValue)
        {
            SetValue(newValue);
        }

        public void SetValueWithPublish(T newValue)
        {
            SetValue(newValue);
            _onValueChanged?.Invoke(newValue);
        }

        
        public IEventObject SubscribeWithPublish(Action<T> onValueChanged)
        {
            _onValueChanged += onValueChanged;
            onValueChanged?.Invoke(GetValue());
            return this;
        }

        public void Unsubscribe(Action<T> onValueChanged)
        {
            _onValueChanged -= onValueChanged;
        }

        public IEventObject Subscribe(Action<T> onValueChanged)
        {
            _onValueChanged += onValueChanged;
            return this;
        }


    }

}
