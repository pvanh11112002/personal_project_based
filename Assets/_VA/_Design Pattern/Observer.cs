using System;
using System.Collections.Generic;
using UnityEngine;

#region Description (English Below)
//---------------------------- Mô tả - Description ----------------------------
//---------------------------- Tiếng Việt----------------------------
// Mô tả:
// Observer Pattern là một mẫu thiết kế (design pattern) thuộc nhóm Behavioral.
// Nó cho phép một đối tượng (Subject) thông báo đến nhiều đối tượng khác (Observers) khi có sự thay đổi, mà không cần các đối tượng này phải liên kết chặt chẽ với nhau
// Cách thức hoạt động:
// Sử dụng một Dictionary để lưu, với key là tên sự kiện (string), value là một list các hàm lắng nghe sự kiện đó

// Add Observer (có tham số hoặc không có tham số): 
// - Nếu chưa có list chứa các hàm lắng nghe 1 sự kiện, tạo list mới
// - Nếu có rồi thì thêm hàm lắng nghe vào list sẵn có

//Remove Observer (có tham số hoặc không có tham số): 
// - Nếu list chứa các hàm lắng nghe sự kiện có chứa nhiều hơn 1 thì xóa nó đi
// - Nếu list chứa các hàm lắng nghe sự kiện trống thì xóa luôn phần tử đó đi

// Cách dùng:
//public static readonly string SCORE_UPDATE = "Score Update";
//private void OnEnable()
//{
//    Observer.AddObserver<int>(SCORE_UPDATE, ScoreUpdate);
//    Observer.AddObserver(SCORE_UPDATE, ChangeText);
//}
//private void OnDisable()
//{
//    Observer.RemoveObserver<int>(SCORE_UPDATE, ScoreUpdate);
//    Observer.RemoveObserver(SCORE_UPDATE, ChangeText);
//}
//private void ScoreUpdate(int val)
//{
//    score = score + val;
//}
//private void ChangeText()
//{
//    scoreText.text = $"Score: {score}";
//}
//private void Update()
//{
//    if (Input.GetKeyDown(KeyCode.A))
//    {
//        Observer.Notify(SCORE_UPDATE, 1);
//    }
//    else if (Input.GetKeyDown(KeyCode.S))
//    {
//        Observer.Notify(SCORE_UPDATE, 2);
//    }
//    else if (Input.GetKeyDown(KeyCode.D))
//    {
//        Observer.Notify(SCORE_UPDATE, 3);
//    }
//    else if (Input.GetKeyDown(KeyCode.F))
//    {
//        Observer.Notify(SCORE_UPDATE, 4);
//    }
//    else if (Input.GetKeyDown(KeyCode.X))
//    {
//        Observer.Notify(SCORE_RESET);
//    }
//}

//---------------------------- Tiếng Anh ----------------------------
// Description:
// The Observer Pattern is a design pattern belonging to the Behavioral group.
// It allows one object (Subject) to notify multiple other objects (Observers) when a change occurs, without requiring these objects to be tightly coupled.

// How it works:
// A Dictionary is used to store event listeners, where the key is the event name (string), and the value is a list of functions that listen for that event.

// Add Observer (with or without parameters):
// - If there is no existing list for an event, create a new list.
// - If a list already exists, add the new listener function to it.

// Remove Observer (with or without parameters):
// - If the list contains more than one listener function, remove the specified function.
// - If the list becomes empty, remove the event entry from the Dictionary.

// Useage:
//public static readonly string SCORE_UPDATE = "Score Update";
//private void OnEnable()
//{
//    Observer.AddObserver<int>(SCORE_UPDATE, ScoreUpdate);
//    Observer.AddObserver(SCORE_UPDATE, ChangeText);
//}
//private void OnDisable()
//{
//    Observer.RemoveObserver<int>(SCORE_UPDATE, ScoreUpdate);
//    Observer.RemoveObserver(SCORE_UPDATE, ChangeText);
//}
//private void ScoreUpdate(int val)
//{
//    score = score + val;
//}
//private void ChangeText()
//{
//    scoreText.text = $"Score: {score}";
//}
//private void Update()
//{
//    if (Input.GetKeyDown(KeyCode.A))
//    {
//        Observer.Notify(SCORE_UPDATE, 1);
//    }
//    else if (Input.GetKeyDown(KeyCode.S))
//    {
//        Observer.Notify(SCORE_UPDATE, 2);
//    }
//    else if (Input.GetKeyDown(KeyCode.D))
//    {
//        Observer.Notify(SCORE_UPDATE, 3);
//    }
//    else if (Input.GetKeyDown(KeyCode.F))
//    {
//        Observer.Notify(SCORE_UPDATE, 4);
//    }
//    else if (Input.GetKeyDown(KeyCode.X))
//    {
//        Observer.Notify(SCORE_RESET);
//    }
//}
#endregion

namespace VA.DesignPattern
{
    public class Observer : MonoBehaviour
    {
        static Dictionary<string, List<Delegate>> Listener = new Dictionary<string, List<Delegate>>();

        /// <summary>
        /// Đăng ký một observer cho sự kiện
        /// </summary>
        /// 
        // Đăng ký một Observer có tham số
        // Nếu tên sự kiện (name) chưa có trong Listener, hệ thống sẽ tạo mới một danh sách callback.
        // Sau đó, thêm callback vào danh sách.
        public static void AddObserver<T>(string name, Action<T> callback)
        {
            if (!Listener.ContainsKey(name))
            {
                Listener[name] = new List<Delegate>();
            }
            Listener[name].Add(callback);
        }

        /// <summary>
        /// Đăng ký một observer cho sự kiện, nhưng không cần tham số truyền vào, nó sẽ mặc định bỏ qua dữ liệu
        /// </summary>
        /// 
        // Đăng ký một Observer nhưng không có tham số
        public static void AddObserver(string name, Action callback)
        {
            AddObserver<object>(name, _ => callback?.Invoke());
        }

        /// <summary>
        /// Hủy đăng ký một observer
        /// </summary>
        /// 
        // Gỡ bỏ một Observer có tham số
        // Nếu sự kiện name không tồn tại, thoát ngay.
        // Nếu có, tìm và xóa callback ra khỏi danh sách.
        // Nếu danh sách rỗng, xóa name khỏi Listener để tiết kiệm bộ nhớ.
        public static void RemoveObserver<T>(string name, Action<T> callback)
        {
            if (!Listener.ContainsKey(name))
            {
                return;
            }
            Listener[name].Remove(callback);
            if (Listener[name].Count == 0)
            {
                Listener.Remove(name);
            }
        }

        /// <summary>
        /// Hủy đăng ký một observer, nhưng không cần tham số truyền vào
        /// </summary>
        /// 

        // Gỡ bỏ một Observer nhưng không có tham số
        public static void RemoveObserver(string name, Action callback)
        {
            RemoveObserver<object>(name, _ => callback?.Invoke());
        }

        /// <summary>
        /// Gửi thông báo đến tất cả observer đã đăng ký
        /// </summary>
        /// 
        // Kích hoạt sự kiện có tham số (Notify)
        // Kiểm tra nếu sự kiện tồn tại.
        // Lặp qua danh sách action và gọi từng callback.
        // Nếu có lỗi, hiển thị Debug.LogError để tránh lỗi runtime.
        public static void Notify<T>(string name, T data)
        {
            if (!Listener.ContainsKey(name))
            {
                return;
            }
            var observers = Listener[name];
            foreach (var action in observers)
            {
                try
                {
                    (action as Action<T>)?.Invoke(data);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error on invoke {e.Message}");
                }

            }
        }

        // Kích hoạt sự kiện nhưng không có tham số 
        public static void Notify(string name)
        {
            Notify<object>(name, null);
        }


        /// <summary>
        /// Xóa tất cả observer khi scene thay đổi (Tránh memory leak)
        /// </summary>
        private void OnDestroy()
        {
            Listener.Clear();
        }
    }

}

