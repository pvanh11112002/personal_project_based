using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableDemo_Manager : MonoBehaviour
{
    [SerializeField] readonly string playerAddress = "Assets/Demo/Addressable/Player.prefab";
    [SerializeField] AsyncOperationHandle<GameObject> playerHandle;

    [SerializeField] readonly string sceneAddress = "Assets/Demo/Addressable/AddressableScene_I_Want_To_Load.unity";
    [SerializeField] AssetReference obj;
    [SerializeField] AssetLabelReference objLabel;
    [SerializeField] List<GameObject> TempList;
    /// <summary>
    /// Nếu asset được gán sẵn: AssetReference
    /// Nếu asset cần load động: LoadAssetAsync<T>()
    /// Nếu load nhiều asset theo nhóm: LoadAssetsAsync<T>(Label)
    /// Advanced:
    /// Kết hợp LoadAssetAsync<T>() + Object Pooling để giảm lag khi instantiate nhiều object
    /// Dùng AssetReference nếu có thể, trừ khi bạn cần linh hoạt hơn
    /// </summary>
    void SpawnGameObject() 
    {
        #region Hướng dẫn cách 1: Load dự trên địa chỉ
        #region Mô tả
        //Khi nào nên dùng phương thức nào:
        //- Cần lưu prefab để tái sử dụng nhiều lần : LoadAssetAssync<T>
        //- Muốn tạo GameObject ngay lập tức khi load: InstantiateAsync<T>
        //- Muốn quản lý bộ nhớ tốt hơn: LoadAssetAssync<T> chủ động release
        //- Muốn pooling: LoadAssetAssync<T>
        // ===> Dùng LoadAssetAsync<T> nếu bạn muốn chỉ load asset vào bộ nhớ, sau đó có thể tái sử dụng với Instantiate()
        // ===> Dùng InstantiateAsync<T> nếu bạn muốn load và tạo ngay GameObject, không cần lưu asset.
        #endregion
        #region Code
        //playerHandle = Addressables.LoadAssetAsync<GameObject>(playerAddress); // Quản lý quá trình load asset
        //playerHandle.Completed += (AsyncOperationHandle<GameObject> task) =>   // Khi load thành công, thực hiện hành động sinh ra từ kết quả load được (Result)
        //{
        //    GameObject playerPrefab = task.Result;
        //    Instantiate(playerPrefab);
        //    Addressables.Release(playerHandle); // Giải phóng bộ nhớ sau khi sử dụng
        //};

        //// Cách load khác của cách load 1
        //playerHandle = Addressables.InstantiateAsync(playerAddress);    // Quản lý quá trình load asset
        //playerHandle.Completed += (AsyncOperationHandle<GameObject> task) =>    // Khi load thành công, thực hiện hành động sinh ra từ kết quả load được (Result)
        //{
        //    playerHandle.Result.name = "Cucucaca";
        //};
        #endregion
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Hướng dẫn cách 2: Load dự trên asset reference
        #region Mô tả
        // Ưu điểm:
        // Sử dụng AssetReference là một cách linh hoạt & an toàn hơn so với load bằng string address.
        // An toàn hơn: Tránh lỗi đánh sai tên address, vì bạn kéo thả trực tiếp asset vào Inspector
        // Dễ quản lý hơn: Khi rename hoặc move asset, AssetReference vẫn hoạt động bình thường (không bị mất link như string)
        // Hỗ trợ Unload dễ dàng: Có thể gọi ReleaseAsset() để giải phóng bộ nhớ
        // Tương thích tốt với Addressables: Dễ dàng dùng trong ScriptableObject, Prefab, UI mà không cần hardcode
        // 
        // Nhược điểm:
        // Không thể load ngay lập tức như LoadAssetAsync<T>(). Nó phải đợi hoàn thành bất đồng bộ
        // Không hỗ trợ tốt với Object Pooling, vì mỗi lần InstantiateAsync() đều tạo một instance mới
        // Không thể dùng trong List hoặc Dictionary trực tiếp, vì nó là một struct đặc biệt
        #endregion
        #region Code
        playerHandle = Addressables.LoadAssetAsync<GameObject>(obj);
        playerHandle.Completed += (task) =>
        {
            Instantiate(task.Result);
        };
        #endregion
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Hướng dẫn cách 3: Load với label
        #region Mô tả
        // Ưu điểm:
        // Sử dụng Label trong Addressables giúp bạn load nhiều asset cùng lúc thay vì phải quản lý từng cái bằng AssetReference hoặc string address.
        // Dùng label để load tất cả asset có cùng tag mà không cần nhớ từng tên riêng lẻ
        // Hữu ích khi bạn có nhiều asset thuộc cùng một loại (ví dụ: tất cả vũ khí, tất cả nhân vật…)
        // 
        // Nhược điểm - Không nên dùng khi
        // Khi chỉ cần load một asset duy nhất, dùng AssetReference hoặc LoadAssetAsync<T>() sẽ hiệu quả hơn
        // Khi cần Object Pooling, do LoadAssetsAsync<T>() tạo nhiều instance mới thay vì tái sử dụng object
        // Khi muốn load theo tên cụ thể, vì label không giúp phân biệt từng asset riêng lẻ.
        #endregion
        #region Code
        //Addressables.LoadAssetsAsync<GameObject>(objLabel, (GameObject result) =>
        //{
        //    Instantiate(result);
        //    TempList.Add(result);
        //});
        #endregion
        #endregion
    }
    //void LoadScene()
    //{
    //    //Addressables.LoadSceneAsync(sceneAddress);
    //    handle =  Addressables.LoadSceneAsync(sceneAddress, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    //}   
}
