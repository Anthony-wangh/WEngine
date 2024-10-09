using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 带有unity生命周期的单例
    /// </summary>
    /// <typeparam name="T">完整生命周期的类型。</typeparam>
    public abstract class BehaviourSingleton<T> : MonoBehaviour
    {
        public static BehaviourSingleton<T> Instance;
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}