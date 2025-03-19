using UnityEngine;
#region Description (English Below)
//---------------------------- Mô tả - Description ----------------------------
//---------------------------- Tiếng Việt----------------------------
// Tổng quá:
// Ưu điểm:
// - Đảm bảo chỉ có một instance tồn tại
// - Đảm bảo truy cập dễ dàng
// - Đảm bảo instance được gán trong Awake()
// - Đảm bảo instance tồn tại trước khi sử dụng (Instance không thể bị null)
// - Tránh lỗi gọi Instance khi game đang tắt (isShuttingDown kiểm tra trong OnApplicationQuit())
// - Tuân thủ nguyên tắc thiết kế phần mềm:
// -----DRY - Dont Repeat Yourself: Tái sử dụng code
// -----SOLID - Open/Closed(OCP): Mở rộng cho từng class riêng mà không cần sửa đổi Singleton<T>
// ---------------------------------------------------------------------------------------------------------------------------------
// Nhược điểm:
// - Chưa quyết định DontDestroyOnLoad, nếu muốn giữ gameobject không bị hủy khi load qua các scene, phải tự gọi trong hàm awake của đối tượng singleton
// ---------------------------------------------------------------------------------------------------------------------------------
// Cách dùng:
// [Trong script muốn singleton]
// public class Test : Singleton<Test>
//{
//    protected override void Awake()
//{
//    base.Awake();
//    DontDestroyOnLoad(gameObject); (optional)
//    Debug.Log("Test Initialized");
//}
//public void SomeMethod()
//{
//    Debug.Log("Some Method");
//}
// ---------------------------------------------------------------------------------------------------------------------------------
//---------------------------- Description ----------------------------
//---------------------------- English ----------------------------
// Overview:
// Advantages:
// - Ensures only one instance exists
// - Provides easy access
// - Ensures the instance is assigned in Awake()
// - Guarantees the instance exists before use (Instance cannot be null)
// - Prevents errors when accessing Instance while the game is shutting down (isShuttingDown is checked in OnApplicationQuit())
// ---------------------------------------------------------------------------------------------------------------------------------
// Disadvantages:
// - Does not automatically use DontDestroyOnLoad. If you want the GameObject to persist across scenes, you must manually call it in the Awake() method of the singleton object.
// ---------------------------------------------------------------------------------------------------------------------------------
// Usage:
// [In the script you want to be a singleton]
// public class Test : Singleton<Test>
//{
//    protected override void Awake()
//{
//    base.Awake();
//    DontDestroyOnLoad(gameObject); (optional)
//    Debug.Log("Test Initialized");
//}
//public void SomeMethod()
//{
//    Debug.Log("Some Method");
//}
#endregion

namespace VA.DesignPattern
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static bool isShuttingDown = false;
        public static T Instance
        {
            get 
            {
                if (isShuttingDown) return null;
                if (instance == null)
                {
                    GameObject singletonObj = new GameObject(typeof(T).Name);
                    instance = singletonObj.AddComponent<T>();
                }    
                return instance; 
            }
        } 
        protected virtual void Awake() 
        {
            if(instance == null)
            {
                instance = this as T;
            }  
            else if(instance != this)
            {
                Destroy(gameObject);
            }    
        }
        private void OnApplicationQuit()
        {
            isShuttingDown = true;
        }
    }
}
