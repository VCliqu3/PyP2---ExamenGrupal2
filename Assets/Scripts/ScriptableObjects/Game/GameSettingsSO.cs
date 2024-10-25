using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettingsSO", menuName = "ScriptableObjects/Game/Settings/GameSettings")]

public class GameSettingsSO : ScriptableObject
{
    [Header("Settings")]
    [Range(1, 9)] public int startingMaintenanceStructures;
    [Range(50, 200)] public int startingGold;
    [Space]
    [Range(1f, 2.5f)] public float enemySpawnInterval;
    [Space]
    [Range(0f, 1f)] public float startingEnemySoldierGenerationOdds;
    [Range(0f, 1f)] public float startingTankSoldierGenerationOdds;
    [Range(0f, 1f)] public float startingHelicopterSoldierGenerationOdds;
    [Space]
    [Range(0f, 1f)] public float finalEnemySoldierGenerationOdds;
    [Range(0f, 1f)] public float finalTankSoldierGenerationOdds;
    [Range(0f, 1f)] public float finalHelicopterSoldierGenerationOdds;
    [Space]
    [Range(5, 10)] public int waveToReachFinalOdds;
}
