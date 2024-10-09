using System.Threading.Tasks;
using UnityEngine;

namespace WEngine
{
    public interface IAssetLoadModule
    {
        T Load<T>(string location) where T : Object;

        Task<T> LoadAsync<T>(string location) where T : UnityEngine.Object;

    }
}