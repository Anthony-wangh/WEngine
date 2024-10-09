using System;

namespace WEngine
{
    /// <summary>
    /// ģ���ʵ����
    /// </summary>
    public class ModuleImp : IModule
    {
        public  void Init()
        {
            OnInit();
            OnSubscribe();
        }

        public void Update()
        {
            OnUpdate();
        }

        public void Dispose()
        {
            UnSubscribe();
            OnDispose();
        }

        public virtual void OnInit()
        {
        }
        public virtual void OnUpdate()
        {
        }

        public virtual void OnDispose()
        {

        }
        public virtual void OnSubscribe()
        {
        }

        public virtual void UnSubscribe()
        {
        }

        
    }
}