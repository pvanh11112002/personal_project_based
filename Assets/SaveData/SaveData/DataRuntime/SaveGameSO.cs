using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveGame SO", menuName = "Assets/Save Game")]
public class SaveGameSO : ScriptableObject
{
    public PlayerDataRuntime playerDataRuntime;
    public GameDataRuntime gameDataRuntime;
}
