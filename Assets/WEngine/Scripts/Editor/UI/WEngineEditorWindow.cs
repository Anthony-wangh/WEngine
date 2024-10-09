using UnityEditor;
using UnityEngine;

namespace WEngine.Editor
{
   
    public class WEngineEditorWindow : EditorWindow
    {
        private static SerializedObject m_CustomSettings;

        [MenuItem("WEngine/WEngineSettings")]
        static void Init()
        {
            if (m_CustomSettings == null)
            {
                var setting= Resources.Load<WEngineSetting>("Setting/WEngineSetting");
                m_CustomSettings = new SerializedObject(setting);
            }
            //获取已经打开的window,如果不存在则new一个
            GetWindow(typeof(WEngineEditorWindow),true, "WEngineSetting");
        }
        private Vector2 scrollPos;
        private void OnGUI()
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("WEngine框架设置");
            scrollPos=EditorGUILayout.BeginScrollView(scrollPos);
            EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("AssetLoadSetting"));
            EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("UISetting"));
            EditorGUILayout.Space(5);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            if (!changeCheckScope.changed)
            {
                return;
            }
            m_CustomSettings.ApplyModifiedPropertiesWithoutUndo();

        }


    }
}