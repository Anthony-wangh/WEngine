namespace WEngine.Start
{
   public class StartControl : ControlBase
   {
        private StartModel _model;
        public override void OnInit()
        {
            _model = CoreEngine.MC.GetModel<StartModel>();
        }

        public override void OnSubscribe()
        {
            Subscribe<StartEvent>(OnShowUI);
        }

        private void OnShowUI(StartEvent obj)
        {
            if(obj.IsOpen)
               CoreEngine.UI.Show<StartView>();
            else
               CoreEngine.UI.Close<StartView>();
        }

       
   }
}
