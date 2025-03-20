using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#region Description (English Below)
//---------------------------- Mô tả - Description ----------------------------
//---------------------------- Tiếng Việt----------------------------
// HDSD:
// Load 1 prefab: GameObject obj = await CustomAddressables.Spawn<GameObject>("playerPrefab");
// Load 1 sprite: Sprite sprite = await CustomAddressables.Spawn<Sprite>("enemyIcon");
// Load 1 audio clip: AudioClip audio = await CustomAddressables.Spawn<AudioClip>("bgMusic");

#endregion

namespace VA.Addressable
{
    public class CustomAddressables : MonoBehaviour
    {
        private static List<AsyncOperationHandle> loadedHandles = new List<AsyncOperationHandle>();
        private static List<Object> spawnedInstances = new List<Object>();
        /// <summary>
        /// Spawn a object with addressables by object address
        /// </summary>
        /// <param name="objectAddress">Address of object in addressable</param>
        /// <returns>Return a game object</returns>
        public static async Task<T> Spawn<T>(string objectAddress) where T : Object
        {
            AsyncOperationHandle<T> objectHandle = Addressables.LoadAssetAsync<T>(objectAddress); // Quản lý quá trình load asset
            await objectHandle.Task;
            if (objectHandle.Status == AsyncOperationStatus.Succeeded)
            {
                T newGameObject = Object.Instantiate(objectHandle.Result);
                
                spawnedInstances.Add(newGameObject);    // Lưu lại instance
                loadedHandles.Add(objectHandle);     // Lưu lại handle
                return newGameObject;
            } 
            else
            {
                Debug.LogError($"Failed to load Addressable: {objectAddress}");
                return null;
            }                    
        }

        /// <summary>
        /// Spawn a object with addressables by object asset reference
        /// </summary>
        /// <param name="assetReference">Direct refernce, just drag and drop</param>
        /// <returns>Return a game object</returns>
        public static async Task<T> Spawn<T>(AssetReference assetReference) where T : Object
        {
            if (!assetReference.RuntimeKeyIsValid())
            {
                Debug.LogError($"Invalid AssetReference: {assetReference}");
                return null;
            }
            AsyncOperationHandle<T> objectHandle = Addressables.LoadAssetAsync<T>(assetReference);
            await objectHandle.Task;
            if (objectHandle.Status == AsyncOperationStatus.Succeeded)
            {
                T newGameObject = objectHandle.Result;
                Instantiate(newGameObject);
                spawnedInstances.Add(newGameObject);    // Lưu lại instance
                loadedHandles.Add(objectHandle);
                return newGameObject;
            }
            else
            {
                Debug.LogError($"Failed to load Addressable: {assetReference}");
                return null;
            }
        }

        /// <summary>
        /// Spawn some objects with label
        /// </summary>
        /// <param name="label">The asset label</param>
        /// <returns>Return a list of game object</returns>
        public static async Task<List<T>> Spawn<T>(AssetLabelReference label) where T : Object
        {
            List<T> result = new List<T>();
            var locationsHandle = Addressables.LoadResourceLocationsAsync(label.labelString);
            await locationsHandle.Task;
            if (!locationsHandle.IsValid() || locationsHandle.Result.Count == 0)
            {
                Debug.LogError($"No resources with this label: {label.labelString}");
                return result;
            }
            foreach (var location in locationsHandle.Result)
            {
                var handle = Addressables.LoadAssetAsync<T>(location.PrimaryKey);
                await handle.Task;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    T newInstance = Object.Instantiate(handle.Result);
                    result.Add(newInstance);
                    spawnedInstances.Add(newInstance);
                    loadedHandles.Add(handle);
                }
            }
            Addressables.Release(locationsHandle);
            return result;
        }
        public static void ReleaseMemory()
        {
            foreach (var instance in spawnedInstances)
            {
                if (instance != null)
                {
                    Object.Destroy(instance);
                }
            }
            spawnedInstances.Clear();
            foreach (var handle in loadedHandles)
            {
                Addressables.Release(handle);
            }
            loadedHandles.Clear();
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
    }
}

