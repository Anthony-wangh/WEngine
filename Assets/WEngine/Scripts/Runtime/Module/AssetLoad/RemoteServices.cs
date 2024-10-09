using System;
using System.IO;
using UnityEngine;
using YooAsset;

namespace WEngine
{
    public class RemoteServices : IRemoteServices
    {

        public string GetRemoteMainURL(string fileName)
        {
            var assetLoadSetting = SettingsUtil.WEngineSetting.AssetLoadSetting;
            return assetLoadSetting.HostUrl + assetLoadSetting.PackageName+"/"+ fileName;
        }

        public string GetRemoteFallbackURL(string fileName)
        {
            var assetLoadSetting = SettingsUtil.WEngineSetting.AssetLoadSetting;
            return assetLoadSetting.HostUrl + assetLoadSetting.PackageName + "/" + fileName;
        }
    }

    public class BuiltinQueryServices : IBuildinQueryServices
    {
        public  bool FileExists(string packageName, string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "yoo", packageName, fileName);
            return File.Exists(filePath);
        }
        public bool Query(string packageName, string fileName, string fileCRC)
        {
            // ע�⣺fileName�����ļ���ʽ
            return FileExists(packageName, fileName);
        }
    }

    /// <summary>
    /// WebGL�����ļ���ѯ�����ࡣWebGLƽ̨����Ҫ���ò�ѯ��ֱ��ʹ��Զ���ȸ���Դ��
    /// </summary>
    public class WebGLBuiltinQueryServices : IBuildinQueryServices
    {

        public bool Query(string packageName, string fileName, string fileCRC)
        {
            return true;
        }
    }
    /// <summary>
    /// Ĭ�ϵķַ���Դ��ѯ������
    /// </summary>
    public class DefaultDeliveryQueryServices : IDeliveryQueryServices
    {

        public bool Query(string packageName, string fileName, string fileCRC)
        {
            return false;
        }

        public string GetFilePath(string packageName, string fileName)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// ��Դ�ļ����ܷ����ࡣ
    /// </summary>
    public class GameDecryptionServices : IDecryptionServices
    {
        private const byte OffSet = 32;

        public ulong LoadFromFileOffset(DecryptFileInfo fileInfo)
        {
            return OffSet;
        }

        public byte[] LoadFromMemory(DecryptFileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public Stream LoadFromStream(DecryptFileInfo fileInfo)
        {
            BundleStream bundleStream =
                new BundleStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return bundleStream;
        }

        public uint GetManagedReadBufferSize()
        {
            return 1024;
        }

        public AssetBundle LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
        {
            throw new NotImplementedException();
        }

        public AssetBundleCreateRequest LoadAssetBundleAsync(DecryptFileInfo fileInfo, out Stream managedStream)
        {
            throw new NotImplementedException();
        }
    }

    public class BundleStream : FileStream
    {
        public const byte KEY = 128;

        public BundleStream(string path, FileMode mode, FileAccess access, FileShare share) : base(path, mode, access,
            share)
        {
        }

        public BundleStream(string path, FileMode mode) : base(path, mode)
        {
        }

        public override int Read(byte[] array, int offset, int count)
        {
            var index = base.Read(array, offset, count);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] ^= KEY;
            }

            return index;
        }
    }

    public class DeliveryLoadServices : IDeliveryLoadServices
    {
        public AssetBundle LoadAssetBundle(DeliveryFileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public AssetBundleCreateRequest LoadAssetBundleAsync(DeliveryFileInfo fileInfo)
        {
            throw new NotImplementedException();
        }
    }
}