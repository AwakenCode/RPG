using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Service.Asset
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedHashes = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();
        
        public void Cleanup()
        {
            foreach (var resourceHandles in _handles.Values)
                foreach (var handle in resourceHandles)
                    Addressables.Release(handle);

            _handles.Clear();
            _completedHashes.Clear();
        }

        public void Initialize() => Addressables.InitializeAsync();

        public Task<GameObject> Instantiate(string address, Vector3 position, Transform parent) =>
            Addressables.InstantiateAsync(address, position, Quaternion.identity, parent).Task;

        public Task<GameObject> Instantiate(string address) => 
            Addressables.InstantiateAsync(address).Task;

        public GameObject Instantiate(GameObject template) => Object.Instantiate(template);

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedHashes.TryGetValue(address, out var handle))
                return handle.Result as T;
    
            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address), address);
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedHashes.TryGetValue(assetReference.AssetGUID, out var handle))
                return handle.Result as T;
            
            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string hashKey) where T : class
        {
            handle.Completed += completedHash =>
                _completedHashes[hashKey] = completedHash;

            AddHandle(hashKey, handle);
            
            return await handle.Task;
        }
        
        private void AddHandle(string key, AsyncOperationHandle handle)
        {
            if (_handles.TryGetValue(key, out var resourceHandles) == false)
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }
            
            resourceHandles.Add(handle);
        }
    }
}