
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Edit Tools/Game Data Editor", fileName = "Game Data Editor", order = 1)]
public class GameDataEditorSO : ScriptableObject
{
    public GameDataRuntime gameDataRuntime;
    public void SaveGameData()
    {
        if (EditorUtility.DisplayDialog("Save Game Data", "Are you sure you wanna save this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.SaveData(gameDataRuntime, DataManager.GAME_DATA_RUNTIME_FILE_NAME, DataManager.DataPersistentDirectoryPath);
        }
    }
    public void ReloadUserData()
    {
        gameDataRuntime = SimpleDataSave.LoadData<GameDataRuntime>(Path.Combine(DataManager.DataPersistentDirectoryPath, DataManager.GAME_DATA_RUNTIME_FILE_NAME));
    }
    public void DeleteUserData()
    {
        if (EditorUtility.DisplayDialog("Delete Game Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<GameDataRuntime>(DataManager.GAME_DATA_RUNTIME_FILE_NAME, DataManager.DataPersistentDirectoryPath);
            gameDataRuntime = new GameDataRuntime();
        }
    }
}
#endif