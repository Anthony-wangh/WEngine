using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WEngineSetting", menuName = "WEngine/WEngineSetting")]
public class WEngineSetting : ScriptableObject
{
    [Header("资源管理配置")]
    [SerializeField]
    public AssetLoaderSetting AssetLoadSetting;

    [Header("UI配置")]
    [SerializeField]
    public UISetting UISetting;
}

/// <summary>
/// 资源加载配置
/// </summary>
[Serializable]
public class AssetLoaderSetting
{
    [Header("是否从服务器加载")]
    public bool IsLoadFromHost = false;
    [Header("资源包名")]
    public string PackageName = "DefaultPackage";
    [Header("服务器加载地址")]
    public string HostUrl;
    [Header("本地加载地址--StreamingAssets目录以下，包名以上的部分")]
    public string LoacalPath;
}


[Serializable]
public class UISetting
{
    [Header("是否开启自动生成模板")]
    [Tooltip("开启之后对于UI面板预制体(名称以“View”结尾)可通过鼠标右击来选择不同的自动代码模板生成")]
    public bool EnableAutocode;
    [Header("UI组件自动识别标识")]
    public List<UIDesignComponentKey> ComponentRecognitions;
}


[Serializable]
public class UIDesignComponentKey
{
    [SerializeField]
    [Header("UI组件名称")]
    public string ComponentName;
    [SerializeField]
    [Header("UI组件标识")]
    public string Key;
}

