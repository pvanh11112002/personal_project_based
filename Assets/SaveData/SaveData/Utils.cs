using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string ConvertToStr(float val)
    {
        return val.ToString("F2").Replace(",", ".");
    }
    public static int ConvertDpToPx(float dp)
    {
        // Lấy DPI từ thiết bị
        float dpi = Screen.dpi;

        // Nếu DPI không hợp lệ
        if (dpi == 0)
        {
            dpi = 160; // Giả định DPI mặc định
        }

        // Tính density scale
        float densityScale = dpi / 160;

        // Chuyển đổi dp sang px
        return (int)(dp * densityScale);
    }
    //public static HighlightPlus.HighlightEffect AddHighlightEffect(GameObject obj, HighlightPlus.Visibility visibility = HighlightPlus.Visibility.AlwaysOnTop)
    //{
    //    var hl = obj.GetComponent<HighlightPlus.HighlightEffect>();
    //    if(hl == null)
    //        hl = obj.AddComponent<HighlightPlus.HighlightEffect>();

    //    hl.outline = 1f;
    //    hl.outlineWidth = 0.7f;
    //    hl.outlineColor = new Color32(45, 255, 0, 255);
    //    hl.outlineVisibility = visibility;
    //    hl.SetHighlighted(true);
    //    return hl;
    //}
    //public static HighlightPlus.HighlightEffect AddHighlightEffect2(GameObject obj, Color32 color)
    //{
    //    var hl = obj.GetComponent<HighlightPlus.HighlightEffect>();
    //    if (hl == null)
    //        hl = obj.AddComponent<HighlightPlus.HighlightEffect>();

    //    hl.outline = 1f;
    //    hl.outlineWidth = 0.7f;
    //    hl.outlineColor = color;
    //    hl.outlineVisibility = HighlightPlus.Visibility.AlwaysOnTop;
    //    hl.SetHighlighted(true);
    //    return hl;
    //}
    public static T DeepCopy<T>(this T self)
    {
        var serialized = JsonUtility.ToJson(self);
        return JsonUtility.FromJson<T>(serialized);
    }
    public static Color SetAlpha(this Color color, float a)
    {
        return new Color(color.r, color.g, color.b, a);
    }
    public static Vector3 SetX(this Vector3 vec, float x)
    {
        return new Vector3(x, vec.y, vec.z);
    }

    public static Vector3 SetY(this Vector3 vec, float y)
    {
        return new Vector3(vec.x, y, vec.z);
    }

    public static Vector3 SetZ(this Vector3 vec, float z)
    {
        return new Vector3(vec.x, vec.y, z);
    }

    /// <summary>
    /// Tính kích thước hình chữ nhất lớn nhất trong 1 ma trận
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static (int, int, int, int) MaxRectangleSize(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int[] heights = new int[cols];
        int maxArea = 0;
        int maxLeft = 0, maxTop = 0, maxWidth = 0, maxHeight = 0;

        for (int i = 0; i < rows; i++)
        {
            // Cập nhật chiều cao của mỗi cột
            for (int u = 0; u < cols; u++)
            {
                if (matrix[i, u] == 0)
                {
                    heights[u] = 0;
                }
                else
                {
                    heights[u]++;
                }
            }

            // Tìm hình chữ nhật lớn nhất từ chiều cao của các cột
            Stack<int> stack = new Stack<int>();
            int j = 0;
            while (j < cols)
            {
                if (stack.Count == 0 || heights[stack.Peek()] <= heights[j])
                {
                    stack.Push(j++);
                }
                else
                {
                    int h = heights[stack.Pop()];
                    int w = stack.Count == 0 ? j : j - stack.Peek() - 1;
                    int area = h * w;
                    if (area > maxArea)
                    {
                        maxArea = area;
                        maxLeft = stack.Count == 0 ? 0 : stack.Peek() + 1;
                        maxTop = i - h + 1;
                        maxWidth = w;
                        maxHeight = h;
                    }
                }
            }

            while (stack.Count > 0)
            {
                int h = heights[stack.Pop()];
                int w = stack.Count == 0 ? cols : cols - stack.Peek() - 1;
                int area = h * w;
                if (area > maxArea)
                {
                    maxArea = area;
                    maxLeft = stack.Count == 0 ? 0 : stack.Peek() + 1;
                    maxTop = i - h + 1;
                    maxWidth = w;
                    maxHeight = h;
                }
            }
        }

        return (maxLeft, maxTop, maxWidth, maxHeight);
    }

    /// <summary>
    /// Convert 1 mảng obj sang 1 ma trận 1,0
    /// </summary>
    /// <param name="gameObjects"></param>
    /// <param name="cellSize"></param>
    /// <returns></returns>
    public static int[,] CreateMatrix(GameObject[] gameObjects, float cellSize)
    {
        // Tìm kích thước lớn nhất của GameObjects
        float maxX = float.MinValue, maxY = float.MinValue, minX = float.MaxValue, minY = float.MaxValue;
        foreach (GameObject go in gameObjects)
        {
            float x = go.transform.position.x;
            float y = go.transform.position.z; // Sử dụng transform.position.z vì Unity sử dụng trục Z thay vì Y
            maxX = Mathf.Max(maxX, x);
            maxY = Mathf.Max(maxY, y);
            minX = Mathf.Min(minX, x);
            minY = Mathf.Min(minY, y);
        }

        // Tính kích thước của ma trận
        int matrixWidth = Mathf.CeilToInt((maxX - minX) / cellSize) + 1;
        int matrixHeight = Mathf.CeilToInt((maxY - minY) / cellSize) + 1;
        int[,] matrix = new int[matrixHeight, matrixWidth];

        // Điền giá trị 1 vào các vị trí có GameObjects
        foreach (GameObject go in gameObjects)
        {
            float x = go.transform.position.x;
            float y = go.transform.position.z;
            int col = Mathf.CeilToInt((x - minX) / cellSize);
            int row = Mathf.CeilToInt((y - minY) / cellSize);
            matrix[row, col] = 1;
        }
        return matrix;
    }
}
