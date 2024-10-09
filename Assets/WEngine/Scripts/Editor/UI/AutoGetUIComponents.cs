using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace WEngine.Editor
{
    /// <summary>
    /// 自动生成UI组件缓存脚本
    /// </summary>
    public class AutoGetUIComponents
    {
        public static GameObject m_viewGoRoot;
        public static List<string> m_properityList = new List<string>();
        public static List<string> m_getCompentList = new List<string>();

        private static UISetting _uIDesignSetting;
        private static string _templateText;

        private static string _path;

        public static bool IsContainsMVCTemplate(string templateName)
        {            
            var path = EditorGlobal.MVCPath + "/" + templateName;
            if (!Directory.Exists(path))
            {
                return false;
            }
            return true;
        }

        //[MenuItem("GameObject/MVC/UIDesign", priority = 2)]
        public static void AutoGetUIComponentsWindows()
        {
            var selectObj = Selection.activeObject;
            if (selectObj == null || !(selectObj is GameObject gameObj) || !gameObj.name.Contains("View"))
            {
                EditorUtility.DisplayDialog("警告！", "请先选中界面预制体！", "确定");
                return;
            }
            _templateText = selectObj.name.Replace("View", "");

            _path = EditorGlobal.MVCPath + "/" + _templateText;
            if (!Directory.Exists(_path))
            {
                EditorUtility.DisplayDialog("警告！", "生成UI组件设计脚本之前请先创建MVC模板！", "确定");
                return;
            }
            m_viewGoRoot = (GameObject)selectObj;

            if (_uIDesignSetting == null)
            {
                _uIDesignSetting = SettingsUtil.WEngineSetting.UISetting;
            }

            Generate(m_viewGoRoot);
        }


        private static void Generate(GameObject root)
        {
            m_viewGoRoot = root;
            if (root == null)
                return;
            var children = root.GetComponentsInChildren<Transform>(true);
            if (children == null || children.Length == 0)
                return;
            foreach (var item in children)
            {
                TsNeedAddInViewElement(item);
            }

            CreateDesignTemplate(_templateText);
            AssetDatabase.Refresh();
        }

        private static string GetComponenetName(Transform child)
        {
            if (_uIDesignSetting == null||_uIDesignSetting.ComponentRecognitions==null)
                return "";
            foreach (var item in _uIDesignSetting.ComponentRecognitions)
            {
                if (child.name.Contains(item.Key))
                {
                    return item.ComponentName;
                }
            }

            return "";
        }

        static void TsNeedAddInViewElement(Transform childts)
        {
            var tempgetCompentstr = GetComponenetName(childts);
            if (tempgetCompentstr == "")
                return;
            var properitystr = $"public {tempgetCompentstr} {childts.name};";
            if (!string.IsNullOrEmpty(properitystr))
            {
                m_properityList.Add(properitystr);
                string path = childts.GetPath(m_viewGoRoot.transform);
                string tempgetCompentNameStr = 
                    string.Format(childts.name + " = ViewObj.FindChild<{0}>(" + '"' + path + '"' + ");", tempgetCompentstr);
                m_getCompentList.Add(tempgetCompentNameStr);
            }

        }

        public static void CreateDesignTemplate(string templateName)
        {
            Debug.Log("创建Design模版");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using UnityEngine;");

            sb.AppendLine(CreateMVCTemplate.GetNamespace(templateName));
            sb.AppendLine("{");

            sb.AppendFormat("   public partial class {0} ", templateName + "View");
            sb.AppendLine();
            sb.AppendLine("   {");
            foreach (var item in m_properityList)
            {
                sb.AppendLine("        " + item);
            }
            sb.AppendLine();
            sb.AppendLine("        public override void AutoGetComponent()");
            sb.AppendLine("        {");
            foreach (var item in m_getCompentList)
            {
                sb.AppendLine("            " + item);
            }
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("   }");
            sb.AppendLine("}");

            CreateScript(_path + @"\" + templateName + "View.Design.cs", sb.ToString());
        }
        /// <summary>保存脚本到View目录下</summary>
        private static void CreateScript(string path, string msg)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            sw.Write(msg);
            sw.Flush();
            sw.Close();
        }
    }
}
