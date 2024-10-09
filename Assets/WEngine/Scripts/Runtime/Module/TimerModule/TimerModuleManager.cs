using System;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 计时器模块管理器。
    /// </summary>
    [DisallowMultipleComponent]//禁止同一个对象身上挂载两个本组件
    public sealed partial class TimerModuleManager : ModuleManagerBase
    {
        private TimerModule _timerModule;

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            _timerModule = ModuleSystem.Get<TimerModule>();
            if (_timerModule == null)
            {
                Log.Fatal("TimerModule is invalid.");
            }
        }

        /// <summary>
        /// 添加计时器。
        /// </summary>
        /// <param name="callback">计时器回调。</param>
        /// <param name="time">计时器间隔。</param>
        /// <param name="isLoop">是否循环。</param>
        /// <param name="isUnscaled">是否不收时间缩放影响。</param>
        /// <param name="args">传参。(避免闭包)</param>
        /// <returns>计时器Id。</returns>
        public int AddTimer(TimerHandler callback, float time, bool isLoop = false, bool isUnscaled = false, params object[] args)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }

            return _timerModule.AddTimer(callback, time, isLoop, isUnscaled, args);
        }

        /// <summary>
        /// 暂停计时器。
        /// </summary>
        /// <param name="timerId">计时器Id。</param>
        public void Stop(int timerId)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }

            _timerModule.Stop(timerId);
        }
        
        /// <summary>
        /// 恢复计时器。
        /// </summary>
        /// <param name="timerId">计时器Id。</param>
        public void Resume(int timerId)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }

            _timerModule.Resume(timerId);
        }
        
        /// <summary>
        /// 计时器是否在运行中。
        /// </summary>
        /// <param name="timerId">计时器Id。</param>
        /// <returns>否在运行中。</returns>
        public bool IsRunning(int timerId)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }

            return _timerModule.IsRunning(timerId);
        }

        /// <summary>
        /// 获得计时器剩余时间。
        /// </summary>
        public float GetLeftTime(int timerId)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }

            return _timerModule.GetLeftTime(timerId);
        }
        
        /// <summary>
        /// 重置计时器,恢复到开始状态。
        /// </summary>
        public void Restart(int timerId)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }

            _timerModule.Restart(timerId);
        }

        /// <summary>
        /// 重置计时器。
        /// </summary>
        public void ResetTimer(int timerId, TimerHandler callback, float time, bool isLoop = false, bool isUnscaled = false)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }
            
            _timerModule.Reset(timerId,callback,time,isLoop,isUnscaled);
        }

        /// <summary>
        /// 重置计时器。
        /// </summary>
        public void ResetTimer(int timerId, float time, bool isLoop, bool isUnscaled)
        {
            if (_timerModule == null)
            {
                throw new Exception("TimerMgr is invalid.");
            }
            
            _timerModule.Reset(timerId, time,isLoop,isUnscaled);
        }

        /// <summary>
        /// 移除计时器。
        /// </summary>
        /// <param name="timerId">计时器Id。</param>
        public void RemoveTimer(int timerId)
        {
            if (_timerModule == null)
            {
                Log.Fatal("TimerMgr is invalid.");
                throw new Exception("TimerMgr is invalid.");
            }

            _timerModule.RemoveTimer(timerId);
        }
        
        /// <summary>
        /// 移除所有计时器。
        /// </summary>
        public void RemoveAllTimer()
        {
            if (_timerModule == null)
            {
                Log.Fatal("TimerMgr is invalid.");
                throw new Exception("TimerMgr is invalid.");
            }

            _timerModule.RemoveAllTimer();
        }
    }
}