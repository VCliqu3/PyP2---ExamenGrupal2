using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Settings")]
    [SerializeField] private int waveNumber;

    public int WaveNumber => waveNumber;
    private bool spawnEnabled;

    private void OnEnable()
    {
        StartCombatUI.OnStartCombat += StartCombatUI_OnStartCombat;
        EntitiesManager.OnAnyEnemyEntityRemoved += EntitiesManager_OnAnyEnemyEntityRemoved;
    }

    private void OnDisable()
    {
        StartCombatUI.OnStartCombat -= StartCombatUI_OnStartCombat;
        EntitiesManager.OnAnyEnemyEntityRemoved -= EntitiesManager_OnAnyEnemyEntityRemoved;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
    }


    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one EnemySpawnManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        spawnEnabled = false;
        waveNumber = 0;
    }

    private void StartEnemySpawn()
    {
        spawnEnabled = true;
    }

    private void SpawnNextWave()
    {

    }

    private int GetFibonacciTerm(int termNumber)
    {
        int a0 = 0;
        int a1 = 1;
        int a2 = a0 + a1;

        if (termNumber <= 1) return a0;
        if (termNumber == 2) return a1;

        for (int i = 3; i <= termNumber; i++)
        {
            a2 = a1 + a0;

            a0 = a1;
            a1 = a2;
        }

        return a2;
    }

    #region Subscriptions
    private void StartCombatUI_OnStartCombat(object sender, System.EventArgs e)
    {
        StartEnemySpawn();
    }

    private void EntitiesManager_OnAnyEnemyEntityRemoved(object sender, EntitiesManager.OnEntityEventArgs e)
    {
        if (e.enemyEntitiesCount > 0) return;

        SpawnNextWave();
    }
    #endregion
}
