using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettingsSO", menuName = "ScriptableObjects/Game/Settings/GameSettings")]

public class GameSettingsSO : ScriptableObject
{
    [Header("Settings")]
    [Range(1, 9)] public int startingMaintenanceStructures;
    [Range(50, 200)] public int startingGold;
}
