﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace YooAsset.Editor
{
    [Serializable]
    public class AssetBundleCollector
    {
        /// <summary>
        /// 收集路径
        /// 注意：支持文件夹或单个资源文件
        /// </summary>
        public string CollectPath = string.Empty;

        /// <summary>
        /// 收集器的GUID
        /// </summary>
        public string CollectorGUID = string.Empty;

        /// <summary>
        /// 收集器类型
        /// </summary>
        public ECollectorType CollectorType = ECollectorType.MainAssetCollector;

        /// <summary>
        /// 寻址规则类名
        /// </summary>
        public string AddressRuleName = nameof(AddressByFileName);

        /// <summary>
        /// 打包规则类名
        /// </summary>
        public string PackRuleName = nameof(PackDirectory);

        /// <summary>
        /// 过滤规则类名
        /// </summary>
        public string FilterRuleName = nameof(CollectAll);

        /// <summary>
        /// 资源分类标签
        /// </summary>
        public string AssetTags = string.Empty;

        /// <summary>
        /// 用户自定义数据
        /// </summary>
        public string UserData = string.Empty;


        /// <summary>
        /// 收集器是否有效
        /// </summary>
        public bool IsValid()
        {
            if (AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(CollectPath) == null)
                return false;

            if (CollectorType == ECollectorType.None)
                return false;

            if (AssetBundleCollectorSettingData.HasAddressRuleName(AddressRuleName) == false)
                return false;

            if (AssetBundleCollectorSettingData.HasPackRuleName(PackRuleName) == false)
                return false;

            if (AssetBundleCollectorSettingData.HasFilterRuleName(FilterRuleName) == false)
                return false;

            return true;
        }

        /// <summary>
        /// 检测配置错误
        /// </summary>
        public void CheckConfigError()
        {
            string assetGUID = AssetDatabase.AssetPathToGUID(CollectPath);
            if (string.IsNullOrEmpty(assetGUID))
                throw new Exception($"Invalid collect path : {CollectPath}");

            if (CollectorType == ECollectorType.None)
                throw new Exception($"{nameof(ECollectorType)}.{ECollectorType.None} is invalid in collector : {CollectPath}");

            if (AssetBundleCollectorSettingData.HasPackRuleName(PackRuleName) == false)
                throw new Exception($"Invalid {nameof(IPackRule)} class type : {PackRuleName} in collector : {CollectPath}");

            if (AssetBundleCollectorSettingData.HasFilterRuleName(FilterRuleName) == false)
                throw new Exception($"Invalid {nameof(IFilterRule)} class type : {FilterRuleName} in collector : {CollectPath}");

            if (AssetBundleCollectorSettingData.HasAddressRuleName(AddressRuleName) == false)
                throw new Exception($"Invalid {nameof(IAddressRule)} class type : {AddressRuleName} in collector : {CollectPath}");
        }

        /// <summary>
        /// 修复配置错误
        /// </summary>
        public bool FixConfigError()
        {
            bool isFixed = false;

            if (string.IsNullOrEmpty(CollectorGUID) == false)
            {
                string convertAssetPath = AssetDatabase.GUIDToAssetPath(CollectorGUID);
                if (string.IsNullOrEmpty(convertAssetPath))
                {
                    Debug.LogWarning($"Collector GUID {CollectorGUID} is invalid and has been auto removed !");
                    CollectorGUID = string.Empty;
                    isFixed = true;
                }
                else
                {
                    if (CollectPath != convertAssetPath)
                    {
                        CollectPath = convertAssetPath;
                        isFixed = true;
                        Debug.LogWarning($"Fix collect path : {CollectPath} -> {convertAssetPath}");
                    }
                }
            }

            /*
            string convertGUID = AssetDatabase.AssetPathToGUID(CollectPath);
            if(string.IsNullOrEmpty(convertGUID) == false)
            {
                CollectorGUID = convertGUID;
            }
            */

            return isFixed;
        }

        /// <summary>
        /// 获取打包收集的资源文件
        /// </summary>
        public List<CollectAssetInfo> GetAllCollectAssets(CollectCommand command, AssetBundleCollectorGroup group)
        {
            // 注意：模拟构建模式下只收集主资源
            if (command.BuildMode == EBuildMode.SimulateBuild)
            {
                if (CollectorType != ECollectorType.MainAssetCollector)
                    return new List<CollectAssetInfo>();
            }

            Dictionary<string, CollectAssetInfo> result = new Dictionary<string, CollectAssetInfo>(1000);

            // 收集打包资源
            if (AssetDatabase.IsValidFolder(CollectPath))
            {
                string collectDirectory = CollectPath;
                string[] findAssets = EditorTools.FindAssets(EAssetSearchType.All, collectDirectory);
                foreach (string assetPath in findAssets)
                {
                    if (IsValidateAsset(command, assetPath) && IsCollectAsset(group, assetPath))
                    {
                        if (result.ContainsKey(assetPath) == false)
                        {
                            var collectAssetInfo = CreateCollectAssetInfo(command, group, assetPath);
                            result.Add(assetPath, collectAssetInfo);
                        }
                        else
                        {
                            throw new Exception($"The collecting asset file is existed : {assetPath} in collector : {CollectPath}");
                        }
                    }
                }
            }
            else
            {
                string assetPath = CollectPath;
                if (IsValidateAsset(command, assetPath) && IsCollectAsset(group, assetPath))
                {
                    var collectAssetInfo = CreateCollectAssetInfo(command, group, assetPath);
                    result.Add(assetPath, collectAssetInfo);
                }
                else
                {
                    throw new Exception($"The collecting single asset file is invalid : {assetPath} in collector : {CollectPath}");
                }
            }

            // 检测可寻址地址是否重复
            if (command.EnableAddressable)
            {
                var addressTemper = new Dictionary<string, string>();
                foreach (var collectInfoPair in result)
                {
                    if (collectInfoPair.Value.CollectorType == ECollectorType.MainAssetCollector)
                    {
                        string address = collectInfoPair.Value.Address;
                        string assetPath = collectInfoPair.Value.AssetPath;
                        if (string.IsNullOrEmpty(address))
                            continue;

                        if (address.StartsWith("Assets/") || address.StartsWith("assets/"))
                            throw new Exception($"The address can not set asset path in collector : {CollectPath} \nAssetPath: {assetPath}");

                        if (addressTemper.TryGetValue(address, out var existed) == false)
                            addressTemper.Add(address, assetPath);
                        else
                            throw new Exception($"The address is existed : {address} in collector : {CollectPath} \nAssetPath:\n     {existed}\n     {assetPath}");
                    }
                }
            }

            // 返回列表
            return result.Values.ToList();
        }

        private CollectAssetInfo CreateCollectAssetInfo(CollectCommand command, AssetBundleCollectorGroup group, string assetPath)
        {
            string address = GetAddress(command, group, assetPath);
            string bundleName = GetBundleName(command, group, assetPath);
            List<string> assetTags = GetAssetTags(group);
            CollectAssetInfo collectAssetInfo = new CollectAssetInfo(CollectorType, bundleName, address, assetPath, assetTags);

            // 注意：模拟构建模式下不需要收集依赖资源
            if (command.BuildMode == EBuildMode.SimulateBuild)
                collectAssetInfo.DependAssets = new List<string>();
            else
                collectAssetInfo.DependAssets = GetAllDependencies(command, assetPath);

            return collectAssetInfo;
        }
        private bool IsValidateAsset(CollectCommand command, string assetPath)
        {
            if (assetPath.StartsWith("Assets/") == false && assetPath.StartsWith("Packages/") == false)
            {
                UnityEngine.Debug.LogError($"Invalid asset path : {assetPath}");
                return false;
            }

            // 忽略文件夹
            if (AssetDatabase.IsValidFolder(assetPath))
                return false;

            // 忽略编辑器下的类型资源
            Type assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            if (assetType == typeof(LightingDataAsset))
                return false;

            // 忽略Unity引擎无法识别的文件
            if (command.IgnoreDefaultType)
            {
                if (assetType == typeof(UnityEditor.DefaultAsset))
                {
                    UnityEngine.Debug.LogWarning($"Cannot pack default asset : {assetPath}");
                    return false;
                }
            }

            string fileExtension = System.IO.Path.GetExtension(assetPath);
            if (DefaultFilterRule.IsIgnoreFile(fileExtension))
                return false;

            return true;
        }
        private bool IsCollectAsset(AssetBundleCollectorGroup group, string assetPath)
        {
            // 根据规则设置过滤资源文件
            IFilterRule filterRuleInstance = AssetBundleCollectorSettingData.GetFilterRuleInstance(FilterRuleName);
            return filterRuleInstance.IsCollectAsset(new FilterRuleData(assetPath, CollectPath, group.GroupName, UserData));
        }
        private string GetAddress(CollectCommand command, AssetBundleCollectorGroup group, string assetPath)
        {
            if (command.EnableAddressable == false)
                return string.Empty;

            if (CollectorType != ECollectorType.MainAssetCollector)
                return string.Empty;

            IAddressRule addressRuleInstance = AssetBundleCollectorSettingData.GetAddressRuleInstance(AddressRuleName);
            string adressValue = addressRuleInstance.GetAssetAddress(new AddressRuleData(assetPath, CollectPath, group.GroupName, UserData));
            return adressValue;
        }
        private string GetBundleName(CollectCommand command, AssetBundleCollectorGroup group, string assetPath)
        {
            if (command.AutoCollectShaders)
            {
                System.Type assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
                if (assetType == typeof(UnityEngine.Shader) || assetType == typeof(UnityEngine.ShaderVariantCollection))
                {
                    // 获取着色器打包规则结果
                    PackRuleResult shaderPackRuleResult = DefaultPackRule.CreateShadersPackRuleResult();
                    return shaderPackRuleResult.GetBundleName(command.PackageName, command.UniqueBundleName);
                }
            }

            // 获取其它资源打包规则结果
            IPackRule packRuleInstance = AssetBundleCollectorSettingData.GetPackRuleInstance(PackRuleName);
            PackRuleResult defaultPackRuleResult = packRuleInstance.GetPackRuleResult(new PackRuleData(assetPath, CollectPath, group.GroupName, UserData));
            return defaultPackRuleResult.GetBundleName(command.PackageName, command.UniqueBundleName);
        }
        private List<string> GetAssetTags(AssetBundleCollectorGroup group)
        {
            List<string> tags = EditorTools.StringToStringList(group.AssetTags, ';');
            List<string> temper = EditorTools.StringToStringList(AssetTags, ';');
            tags.AddRange(temper);
            return tags;
        }
        private List<string> GetAllDependencies(CollectCommand command, string mainAssetPath)
        {
            string[] depends = AssetDatabase.GetDependencies(mainAssetPath, true);
            List<string> result = new List<string>(depends.Length);
            foreach (string assetPath in depends)
            {
                // 注意：排除主资源对象
                if (assetPath == mainAssetPath)
                    continue;

                if (IsValidateAsset(command, assetPath))
                    result.Add(assetPath);
            }
            return result;
        }
    }
}