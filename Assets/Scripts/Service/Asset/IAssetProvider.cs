using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Service.Asset
{
    public interface IAssetProvider
    {
        Task<GameObject> Instantiate(string address, Vector3 position, Transform parent = null);
        Task<GameObject> Instantiate(string address);
        GameObject Instantiate(GameObject template);
        Task<T> Load<T>(string address) where T : class;
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void Cleanup();
        void Initialize();
    }
}