
using UnityEngine;

namespace WEngine
{
    internal class FsmStateEnd : TestFsmStateBase
    {
        protected internal override void OnEnter(IFsm<TestFsmManager> fsm)
        {
            base.OnEnter(fsm);
            Log.Debug("Enter:---------- FsmStateEnd");
        }

        protected internal override void OnLeave(IFsm<TestFsmManager> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Debug("Leave:---------- FsmStateEnd");
        }

        protected internal override void OnUpdate(IFsm<TestFsmManager> fsm)
        {
            base.OnUpdate(fsm);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeState<FsmStateStart>(fsm);
            }
        }
    }
}