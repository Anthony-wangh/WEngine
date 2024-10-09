using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEngine
{
    internal class FsmStateStart : TestFsmStateBase 
    {
        protected internal override void OnEnter(IFsm<TestFsmManager> fsm)
        {
            base.OnEnter(fsm);
            Log.Debug("Enter:---------- FsmStateStart");
        }

        protected internal override void OnLeave(IFsm<TestFsmManager> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Debug("Leave:---------- FsmStateStart");
        }

        protected internal override void OnUpdate(IFsm<TestFsmManager> fsm)
        {
            base.OnUpdate(fsm);
            Log.Debug("OnUpdate:---------- FsmStateStart");
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeState<FsmStateThen>(fsm);
            }
        }
    }
}