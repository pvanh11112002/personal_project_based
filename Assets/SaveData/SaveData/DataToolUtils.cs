using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class DataToolUtils
{
    [MenuItem("Tools/Reset All Runtime Data")]
    public static void ResetAllData()
    {
        if (EditorUtility.DisplayDialog("Delete Runtime Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<PlayerDataRuntime>(DataManager.PLAYER_DATA_RUNTIME_FILE_NAME, DataManager.DataPersistentDirectoryPath);
            SimpleDataSave.DeleteData<GameDataRuntime>(DataManager.GAME_DATA_RUNTIME_FILE_NAME, DataManager.DataPersistentDirectoryPath);
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif
