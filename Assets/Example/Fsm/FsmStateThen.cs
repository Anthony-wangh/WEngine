using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEngine
{
    internal class FsmStateThen : TestFsmStateBase
    {
        protected internal override void OnEnter(IFsm<TestFsmManager> fsm)
        {
            base.OnEnter(fsm);
            Log.Debug("Enter:---------- FsmStateThen");
        }

        protected internal override void OnLeave(IFsm<TestFsmManager> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Debug("Leave:---------- FsmStateThen");
        }

        protected internal override void OnUpdate(IFsm<TestFsmManager> fsm)
        {
            base.OnUpdate(fsm);
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeState<FsmStateEnd>(fsm);
            }
        }
    }
}