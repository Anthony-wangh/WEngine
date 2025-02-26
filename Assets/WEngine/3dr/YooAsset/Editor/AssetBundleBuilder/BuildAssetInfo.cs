﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YooAsset.Editor
{
    public class BuildAssetInfo
    {
        private bool _isAddAssetTags = false;
        private readonly HashSet<string> _referenceBundleNames = new HashSet<string>();

        /// <summary>
        /// 收集器类型
        /// </summary>
        public ECollectorType CollectorType { private set; get; }

        /// <summary>
        /// 资源包完整名称
        /// </summary>
        public string BundleName { private set; get; }

        /// <summary>
        /// 可寻址地址
        /// </summary>
        public string Address { private set; get; }

        /// <summary>
        /// 资源路径
        /// </summary>
        public string AssetPath { private set; get; }

        /// <summary>
        /// 资源GUID
        /// </summary>
        public string AssetGUID { private set; get; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public System.Type AssetType { private set; get; }

        /// <summary>
        /// 资源的分类标签
        /// </summary>
        public readonly List<string> AssetTags = new List<string>();

        /// <summary>
        /// 依赖的所有资源
        /// 注意：包括零依赖资源和冗余资源（资源包名无效）
        /// </summary>
        public List<BuildAssetInfo> AllDependAssetInfos { private set; get; }


        public BuildAssetInfo(ECollectorType collectorType, string bundleName, string address, string assetPath)
        {
            CollectorType = collectorType;
            BundleName = bundleName;
            Address = address;
            AssetPath = assetPath;

            AssetGUID = UnityEditor.AssetDatabase.AssetPathToGUID(assetPath);
            AssetType = UnityEditor.AssetDatabase.GetMainAssetTypeAtPath(assetPath);
        }
        public BuildAssetInfo(string assetPath)
        {
            CollectorType = ECollectorType.None;
            BundleName = string.Empty;
            Address = string.Empty;
            AssetPath = assetPath;

            AssetGUID = UnityEditor.AssetDatabase.AssetPathToGUID(assetPath);
            AssetType = UnityEditor.AssetDatabase.GetMainAssetTypeAtPath(assetPath);
        }


        /// <summary>
        /// 设置所有依赖的资源
        /// </summary>
        public void SetDependAssetInfos(List<BuildAssetInfo> dependAssetInfos)
        {
            if (AllDependAssetInfos != null)
                throw new System.Exception("Should never get here !");

            AllDependAssetInfos = dependAssetInfos;
        }

        /// <summary>
        /// 设置为统一的着色器包名
        /// </summary>
        public void SetShaderBundleName(string packageName, bool uniqueBundleName)
        {
            // 获取着色器打包规则结果
            PackRuleResult shaderPackRuleResult = DefaultPackRule.CreateShadersPackRuleResult();
            BundleName = shaderPackRuleResult.GetBundleName(packageName, uniqueBundleName);
        }

        /// <summary>
        /// 添加资源的分类标签
        /// 说明：原始定义的资源分类标签
        /// </summary>
        public void AddAssetTags(List<string> tags)
        {
            if (_isAddAssetTags)
                throw new Exception("Should never get here !");
            _isAddAssetTags = true;

            foreach (var tag in tags)
            {
                if (AssetTags.Contains(tag) == false)
                {
                    AssetTags.Add(tag);
                }
            }
        }

        /// <summary>
        /// 添加关联的资源包名称
        /// </summary>
        public void AddReferenceBundleName(string bundleName)
        {
            if (string.IsNullOrEmpty(bundleName))
                throw new Exception("Should never get here !");

            if (_referenceBundleNames.Contains(bundleName) == false)
                _referenceBundleNames.Add(bundleName);
        }

        /// <summary>
        /// 资源包名是否存在
        /// </summary>
        public bool HasBundleName()
        {
            if (string.IsNullOrEmpty(BundleName))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断是否为冗余资源
        /// </summary>
        public bool IsRedundancyAsset()
        {
            if (HasBundleName())
                return false;

            return _referenceBundleNames.Count > 1;
        }

        /// <summary>
        /// 获取关联资源包的数量
        /// </summary>
        public int GetReferenceBundleCount()
        {
            return _referenceBundleNames.Count;
        }
    }
}