using UnityEngine;
using ProcedureOwner = WEngine.IFsm<WEngine.TestFsmManager>;
namespace WEngine
{
    internal class TestFsmManager : FsmManager
    {
        private ProcedureOwner _fsm;

        public override void OnInit()
        {
            base.OnInit();
            _fsm= CreateFsm(this, new FsmStateStart(),new FsmStateThen(),new FsmStateEnd());
        }

        public override void OnDispose()
        {
            base.OnDispose();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.A))
            {
                _fsm.Start<FsmStateStart>();
            }
        }

    }

    internal class TestFsmStateBase: FsmState<TestFsmManager>
    {
        /// <summary>
        /// ״̬��ʼ��ʱ���á�
        /// </summary>
        /// <param name="procedureOwner">���̳����ߡ�</param>
        protected internal override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        /// <summary>
        /// ����״̬ʱ���á�
        /// </summary>
        /// <param name="procedureOwner">���̳����ߡ�</param>
        protected internal override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
        }

        /// <summary>
        /// ״̬��ѯʱ���á�
        /// </summary>
        /// <param name="procedureOwner">���̳����ߡ�</param>
        /// <param name="elapseSeconds">�߼�����ʱ�䣬����Ϊ��λ��</param>
        /// <param name="realElapseSeconds">��ʵ����ʱ�䣬����Ϊ��λ��</param>
        protected internal override void OnUpdate(ProcedureOwner procedureOwner)
        {
            base.OnUpdate(procedureOwner);
        }

        /// <summary>
        /// �뿪״̬ʱ���á�
        /// </summary>
        /// <param name="procedureOwner">���̳����ߡ�</param>
        /// <param name="isShutdown">�Ƿ��ǹر�״̬��ʱ������</param>
        protected internal override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        /// <summary>
        /// ״̬����ʱ���á�
        /// </summary>
        /// <param name="procedureOwner">���̳����ߡ�</param>
        protected internal override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }


}