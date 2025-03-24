using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public AssetManager assetManager;

    #region Data

    [SerializeField] private SaveGameSO defaultSaveGameFile;
    public static string DataPersistentDirectoryPath => Application.persistentDataPath + "/DT";
    public readonly static string PLAYER_DATA_RUNTIME_FILE_NAME = "PLAYER_DT.bm";
    public readonly static string GAME_DATA_RUNTIME_FILE_NAME = "GAME_DT.rs";
    public PlayerDataRuntime PlayerData { get; private set; }
    public GameDataRuntime GameData { get; private set; }
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadPlayerDataRuntime();
        LoadGameDataRuntime();
    }

    private void LoadPlayerDataRuntime()
    {
        PlayerData = SimpleDataSave.LoadData<PlayerDataRuntime>(Path.Combine(DataPersistentDirectoryPath, PLAYER_DATA_RUNTIME_FILE_NAME));
        if (PlayerData == null)
        {
            if (defaultSaveGameFile)
                PlayerData = defaultSaveGameFile.playerDataRuntime.DeepCopy();
            else
                PlayerData = new PlayerDataRuntime();
        }
    }
    private void LoadGameDataRuntime()
    {
        GameData = SimpleDataSave.LoadData<GameDataRuntime>(Path.Combine(DataPersistentDirectoryPath, GAME_DATA_RUNTIME_FILE_NAME));
        if (GameData == null)
        {
            if (defaultSaveGameFile)
                GameData = defaultSaveGameFile.gameDataRuntime.DeepCopy();
            else
                GameData = new GameDataRuntime();
        }
    }    
    private void SaveDataRuntime()
    {
        SimpleDataSave.SaveData(PlayerData, PLAYER_DATA_RUNTIME_FILE_NAME, DataPersistentDirectoryPath);
        SimpleDataSave.SaveData(GameData, GAME_DATA_RUNTIME_FILE_NAME, DataPersistentDirectoryPath);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveDataRuntime();
        }
    }
}