﻿using System.Collections.Generic;
using UnityEngine;
using VA.DesignPattern;
#region Description (English Below)
//---------------------------- Mô tả - Description ----------------------------
//---------------------------- Tiếng Việt----------------------------
//Mô tả tổng quát:
//- PoolingObject Object - một design pattern được sử dụng nhằm mục đích tối ưu hiệu năng trò chơi
//- Thay vì dùng Instantiate và Destroy thì chúng ta sử dụng Pooling.
//Cách thức hoạt động:
//- Tạo ra 1 gameobject mới, nhưng thay vì phá hủy nó đi khi không dùng nữa, ta tắt nó đi và tái sử dụng cho lần kế tiếp
//- Sử dụng một Dictionary với key là tên object, value là Queue chứa các object đó => Chỉ sử dụng duy nhất 1 Dictionary để lưu trữ cho các loại object khác nhau
//- Sử dụng GetObject để lấy ra gameobject từ pool của nó
//- Sử dụng ReturnObject để trả nó về pool của chính nó
//Chi tiết cách thức hoạt động:
//- Trong hàm GetObject:
//    - Case 1: Đã tồn tại 1 Queue của gameobject đó:
//        - Mini Case 1.1: Nếu Queue đó trống, gọi hàm CreateNewGameObject (ở dưới)
//        - Mini Case 1.2: Nếu Queue có tồn tại giá trị trước đó rồi, lấy ra và xóa phần tử đầu tiên trong hàng đợi
//    - Case 2: Chưa tồn tại Queue cho game object đó:
//        - Mini Case 2.1: gọi hàm CreateNewGameObject (ở dưới)
//- Trong hàm CreatNewGameObject:
//    - Tạo 1 gameobjet mới và trả về
//- Trong hàm ReturnObject:
//    - Case 1: Đã tồn tại 1 Queue của gameobject đó, thì thêm phần tử đó vào cuối hàng đợi (pool của nó)
//    - Case 2: Chưa tồn tại Queue cho game object đó, tạo mọt hàng đợi mới, thêm nó vào hàng đợi và thêm hàng đợi vào Dic tổng
//    Tắt gameObject
//Khi dùng thì cần nhớ một quy tắc, đó là gọi ra thì phải trả về

//---------------------------- Tiếng Anh ----------------------------
//Summarize:
//- PoolingObject - A Design Pattern for Game Performance Optimization
//- PoolingObject is a design pattern used to optimize game performance.
//- Instead of using Instantiate and Destroy, we utilize Pooling.

//How It Works:
//- A new GameObject is created, but instead of destroying it when not in use, we deactivate it and reuse it for the next instance.
//- A Dictionary is used, where:
//    - Key = Object name
//    - Value = A Queue that holds instances of that object
//    - Only one Dictionary is used to store different object types.
//- GetObject is used to retrieve a GameObject from its pool.
//- ReturnObject is used to return the object back to its pool.

//Detailed Functionality:
//- In the GetObject function:
//    Case 1: A Queue for the GameObject already exists:
//        Mini Case 1.1: If the Queue is empty → Call CreateNewGameObject.
//        Mini Case 1.2: If the Queue contains objects → Retrieve and remove the first element from the Queue.
//    Case 2: A Queue for the GameObject does not exist:
//        Mini Case 2.1: Call CreateNewGameObject.
//- In the CreateNewGameObject function:
//    Create a new GameObject and return it.
//- In the ReturnObject function:
//    Case 1: A Queue for the GameObject already exists → Add the object to the end of the queue (its pool).
//    Case 2: A Queue does not exist → Create a new Queue, add the object to it, and store it in the main Dictionary.
//    Finally, deactivate the GameObject.
#endregion
namespace VA.DesignPattern
{
    public static class PoolingObject
    {
        private static Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
        public static GameObject GetObject(GameObject gameObject)
        {
            if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
            {
                if (objectList.Count == 0)
                {
                    return CreatNewGameObject(gameObject);
                }
                else
                {
                    GameObject _object = objectList.Dequeue();
                    _object.SetActive(true);
                    return _object;
                }
            }
            else
            {
                return CreatNewGameObject(gameObject);
            }
        }
        private static GameObject CreatNewGameObject(GameObject gameObject)
        {
            GameObject newGameObject = Object.Instantiate(gameObject);
            newGameObject.name = gameObject.name;
            return newGameObject;
        }
        public static void ReturnObject(GameObject gameObject)
        {
            if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
            {
                objectList.Enqueue(gameObject);
            }
            else
            {
                Queue<GameObject> newGameObjectQ = new Queue<GameObject>();
                newGameObjectQ.Enqueue(gameObject);
                objectPool.Add(gameObject.name, newGameObjectQ);
            }
            gameObject.SetActive(false);
        }
    }

}

