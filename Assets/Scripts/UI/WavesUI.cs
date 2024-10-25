using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI wavesText;

    private void OnEnable()
    {
        EnemySpawnManager.OnWaveStartSpawn += EnemySpawnManager_OnWaveStartSpawn;
    }

    private void OnDisable()
    {
        EnemySpawnManager.OnWaveStartSpawn -= EnemySpawnManager_OnWaveStartSpawn;
    }

    private void UpdateWavesText(int waveNumber) => wavesText.text = $"Wave {waveNumber}";

    #region EnemySpawnerManager Subscriptions

    private void EnemySpawnManager_OnWaveStartSpawn(object sender, EnemySpawnManager.OnWaveEventArgs e)
    {
        UpdateWavesText(e.waveNumber);
    }
    #endregion
}
