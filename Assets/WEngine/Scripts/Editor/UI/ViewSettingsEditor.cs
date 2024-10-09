using UnityEditor;
using UnityEngine;
using WEngine;
using WEngine.Editor;

/// <summary>
/// ������չ�˵�
/// </summary>
public class ViewSettingsEditor
{
    private const string CreateMVCTag = "MVC";
    private const string DesignViewTag = "Design";

    [InitializeOnLoadMethod]
    private static void SceneViewExtensions()
    {
        // ע�� hierarchyWindowItemOnGUI �Ļص�����
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        EditorApplication.projectWindowItemOnGUI += OnProjectOnGUI;
    }

    

    private static void OnProjectOnGUI(string guid, Rect selectionRect)
    {
        if (!SettingsUtil.WEngineSetting.UISetting.EnableAutocode)
            return;
        if (Event.current != null && selectionRect.Contains(Event.current.mousePosition) && 
            Event.current.type == EventType.ContextClick)
        {
            var selectedGameObject = Selection.activeGameObject;
            if (selectedGameObject == null)
                return;
            // ��ȡ��ǰ�Ҽ��������Դ·��
            if (!selectedGameObject.name.EndsWith("View")) return;
            // ���� GenericMenu
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent(CreateMVCTag), false, CreateMVC, selectedGameObject);

            var templateName= selectedGameObject.name.Replace("View", "");
            if (AutoGetUIComponents.IsContainsMVCTemplate(templateName))
                menu.AddItem(new GUIContent(DesignViewTag), false, DesignView, selectedGameObject);

            // �����λ����ʾ�˵�
            menu.ShowAsContext();
            Event.current.Use();
        }
    }
    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        if (!SettingsUtil.WEngineSetting.UISetting.EnableAutocode)
            return;
        if (Event.current != null && selectionRect.Contains(Event.current.mousePosition) &&
            Event.current.button == 1 && Event.current.type <= EventType.MouseUp)
        {
            GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (selectedGameObject && selectedGameObject.name.EndsWith("View"))
            {
                // ���� GenericMenu
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent(CreateMVCTag), false, CreateMVC, selectedGameObject);
                var templateName = selectedGameObject.name.Replace("View", "");
                if (AutoGetUIComponents.IsContainsMVCTemplate(templateName))
                    menu.AddItem(new GUIContent(DesignViewTag), false, DesignView, selectedGameObject);

                // �����λ����ʾ�˵�
                menu.ShowAsContext();
                Event.current.Use();
            }
        }
    }

    private static void CreateMVC(object obj)
    {
        GameObject selectedObject = obj as GameObject;
        if (selectedObject != null)
        {
            CreateMVCTemplate.CreateTemplate();
        }
    }

    private static void DesignView(object obj)
    {
        GameObject selectedObject = obj as GameObject;
        if (selectedObject != null)
        {
            AutoGetUIComponents.AutoGetUIComponentsWindows();
        }
    }
}
