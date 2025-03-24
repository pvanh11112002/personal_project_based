using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Edit Tools/Player Data Editor", fileName = "Player Data Editor", order = 0)]
public class PlayerDataEditorSO : ScriptableObject
{
    public PlayerDataRuntime playerDataRuntime;

    public void SaveUserData()
    {
        if (EditorUtility.DisplayDialog("Save Player Data", "Are you sure you wanna save this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.SaveData(playerDataRuntime, DataManager.PLAYER_DATA_RUNTIME_FILE_NAME, DataManager.DataPersistentDirectoryPath);
        }
    }
    public void ReloadUserData()
    {
        playerDataRuntime = SimpleDataSave.LoadData<PlayerDataRuntime>(Path.Combine(DataManager.DataPersistentDirectoryPath, DataManager.PLAYER_DATA_RUNTIME_FILE_NAME));
    }
    public void DeleteUserData()
    {
        if (EditorUtility.DisplayDialog("Delete Player Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<PlayerDataRuntime>(DataManager.PLAYER_DATA_RUNTIME_FILE_NAME, DataManager.DataPersistentDirectoryPath);
            playerDataRuntime = new PlayerDataRuntime();
        }
    }
}
#endif