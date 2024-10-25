using GameJolt.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyAchieverHandler : MonoBehaviour
{
    private const int WAVE1_TROPHY_ID = 248424;
    private const int WAVE2_TROPHY_ID = 248425;
    private const int UNITS1_TROPHY_ID = 248427;
    private const int UNITS2_TROPHY_ID = 248426;
    private const int GOLD_TROPHY_ID = 248428;

    private const int WAVE1 = 5;
    private const int WAVE2 = 10;
    private const int UNITS1 = 10;
    private const int UNITS2 = 15;
    private const int GOLD = 500;

    private void OnEnable()
    {
        EnemySpawnManager.OnWaveStartSpawn += EnemySpawnManager_OnWaveStartSpawn;
        EntitiesManager.OnAnyAllyEntityAdded += EntitiesManager_OnAnyAllyEntityAdded;
        GoldManager.OnGoldIncreased += GoldManager_OnGoldIncreased;
    }
    private void OnDisable()
    {
        EnemySpawnManager.OnWaveStartSpawn -= EnemySpawnManager_OnWaveStartSpawn;
        EntitiesManager.OnAnyAllyEntityAdded -= EntitiesManager_OnAnyAllyEntityAdded;
        GoldManager.OnGoldIncreased -= GoldManager_OnGoldIncreased;
    }

    private void Start()
    {
        Trophies.Remove(WAVE1_TROPHY_ID);
        Trophies.Remove(WAVE2_TROPHY_ID);
        Trophies.Remove(UNITS1_TROPHY_ID);
        Trophies.Remove(UNITS2_TROPHY_ID);
        Trophies.Remove(GOLD_TROPHY_ID);
    }


    private void CheckUnlockWave1Trophy(int waveNumber)
    {
        if (waveNumber < WAVE1) return;
        Trophies.TryUnlock(WAVE1_TROPHY_ID);
    }

    private void CheckUnlockWave2Trophy(int waveNumber)
    {
        if (waveNumber < WAVE2) return;
        Trophies.TryUnlock(WAVE2_TROPHY_ID);
    }

    private void CheckUnlockUnits1Trophy(int units)
    {
        if(units < UNITS1) return;
        Trophies.TryUnlock(UNITS1_TROPHY_ID);
    }

    private void CheckUnlockUnits2Trophy(int units)
    {
        if (units < UNITS2) return;
        Trophies.TryUnlock(UNITS2_TROPHY_ID);
    }

    private void CheckUnlockGoldTrophy(int gold)
    {
        if(gold < GOLD) return;
        Trophies.TryUnlock(GOLD_TROPHY_ID);
    }


    private void EnemySpawnManager_OnWaveStartSpawn(object sender, EnemySpawnManager.OnWaveEventArgs e)
    {
        CheckUnlockWave1Trophy(e.waveNumber);
        CheckUnlockWave2Trophy(e.waveNumber);
    }

    private void EntitiesManager_OnAnyAllyEntityAdded(object sender, EntitiesManager.OnEntityEventArgs e)
    {
        CheckUnlockUnits1Trophy(e.allyEntitiesCount);
        CheckUnlockUnits2Trophy(e.allyEntitiesCount);
    }

    private void GoldManager_OnGoldIncreased(object sender, GoldManager.OnGoldEventArgs e)
    {
        CheckUnlockGoldTrophy(e.gold);
    }
}
