using UnityEngine.UI;
using UnityEngine;
namespace WEngine.Start
{
   public partial class StartView 
   {
        public Text Test_Txt;
        public Image Test_Img;
        public Button My_Btn;

        public override void AutoGetComponent()
        {
            Test_Txt = ViewObj.FindChild<Text>("Bg/Test_Txt");
            Test_Img = ViewObj.FindChild<Image>("Bg/Test_Img");
            My_Btn = ViewObj.FindChild<Button>("Bg/My_Btn");
        }

   }
}
