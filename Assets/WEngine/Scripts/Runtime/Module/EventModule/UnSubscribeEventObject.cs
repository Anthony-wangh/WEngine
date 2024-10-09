using System.Collections.Generic;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 移除游戏对象身上的事件绑定
    /// </summary>
    public class UnSubscribeEventObject : MonoBehaviour
    {
        private List<IEventObject> _events = new List<IEventObject>();

        public void Add(IEventObject eventObject)
        {
            _events.Add(eventObject);
        }
        public void Remove(IEventObject eventObject)
        {
            _events.Remove(eventObject);
        }
        private void OnDestroy()
        {
            foreach (var item in _events)
            {
                item.UnSubscribeOnDestroy();
            }
            _events.Clear();
        }

    }
}