using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WEngineSetting", menuName = "WEngine/WEngineSetting")]
public class WEngineSetting : ScriptableObject
{
    [Header("��Դ��������")]
    [SerializeField]
    public AssetLoaderSetting AssetLoadSetting;

    [Header("UI����")]
    [SerializeField]
    public UISetting UISetting;
}

/// <summary>
/// ��Դ��������
/// </summary>
[Serializable]
public class AssetLoaderSetting
{
    [Header("�Ƿ�ӷ���������")]
    public bool IsLoadFromHost = false;
    [Header("��Դ����")]
    public string PackageName = "DefaultPackage";
    [Header("���������ص�ַ")]
    public string HostUrl;
    [Header("���ؼ��ص�ַ--StreamingAssetsĿ¼���£��������ϵĲ���")]
    public string LoacalPath;
}


[Serializable]
public class UISetting
{
    [Header("�Ƿ����Զ�����ģ��")]
    [Tooltip("����֮�����UI���Ԥ����(�����ԡ�View����β)��ͨ������һ���ѡ��ͬ���Զ�����ģ������")]
    public bool EnableAutocode;
    [Header("UI����Զ�ʶ���ʶ")]
    public List<UIDesignComponentKey> ComponentRecognitions;
}


[Serializable]
public class UIDesignComponentKey
{
    [SerializeField]
    [Header("UI�������")]
    public string ComponentName;
    [SerializeField]
    [Header("UI�����ʶ")]
    public string Key;
}

