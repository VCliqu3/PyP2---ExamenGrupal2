using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Settings")]
    [SerializeField] private int waveNumber;

    public static event EventHandler<OnWaveEventArgs> OnWaveStartSpawn;
    public static event EventHandler<OnWaveEventArgs> OnWaveEndSpawn;

    public int WaveNumber => waveNumber;

    public class OnWaveEventArgs : EventArgs
    {
        public int waveNumber;
    }

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
        waveNumber = 0;
    }

    private void SpawnNextWave()
    {
        waveNumber++;
        int numberOfEnemies = GetFibonacciTerm(waveNumber + 1);

        OnWaveStartSpawn?.Invoke(this, new OnWaveEventArgs { waveNumber = waveNumber });

        StartCoroutine(SpawnEnemiesCoroutine(numberOfEnemies));
    }

    private IEnumerator SpawnEnemiesCoroutine(int numberOfEnemies)
    {
        Node enemyBase = NodeManager.Instance.GetEnemyBase();

        for(int i=0; i<numberOfEnemies; i++)
        {
            yield return new WaitForSeconds(gameSettingsSO.enemySpawnInterval);

            EntityType chosenSpawnType = GetEnemyTypeToSpawn();
            SpawnEnemyByType(chosenSpawnType, enemyBase);
        }

        OnWaveEndSpawn?.Invoke(this, new OnWaveEventArgs { waveNumber = waveNumber });
    }

    private void SpawnEnemyByType(EntityType type, Node node)
    {
        switch (type)
        {
            case EntityType.Soldier:
            default:
                EntitySpawnerUtility.Instance.TryInstantiateEnemySoldierInNode(node);
                break;
            case EntityType.Tank:
                EntitySpawnerUtility.Instance.TryInstantiateEnemyTankInNode(node);
                break;
            case EntityType.Helicopter:
                EntitySpawnerUtility.Instance.TryInstantiateEnemyHelicopterInNode(node);
                break;
        }
    }

    private EntityType GetEnemyTypeToSpawn()
    {
        float soldierOdds = GetCurrentWaveOdd(gameSettingsSO.startingEnemySoldierGenerationOdds, gameSettingsSO.finalEnemySoldierGenerationOdds);
        float tankOdds = GetCurrentWaveOdd(gameSettingsSO.startingEnemyTankGenerationOdds, gameSettingsSO.finalEnemyTankGenerationOdds);
        float helicopterOdds = GetCurrentWaveOdd(gameSettingsSO.startingEnemyHelicopterGenerationOdds, gameSettingsSO.finalEnemyHelicopterGenerationOdds);

        float oddsAccumulator = soldierOdds + tankOdds + helicopterOdds;

        float randomOdd = UnityEngine.Random.Range(0f, oddsAccumulator);

        if (randomOdd < soldierOdds)
        {
            return EntityType.Soldier;
        }

        if (randomOdd < soldierOdds + tankOdds)
        {
            return EntityType.Tank;
        }

        return EntityType.Helicopter;
    }

    private float GetCurrentWaveOdd(float startingOdd, float finalOdd)
    {
        float t = (waveNumber-1)/(gameSettingsSO.waveToReachFinalOdds-1);

        return Mathf.Lerp(startingOdd, finalOdd, t);
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
        SpawnNextWave();
    }

    private void EntitiesManager_OnAnyEnemyEntityRemoved(object sender, EntitiesManager.OnEntityEventArgs e)
    {
        if (e.enemyEntitiesCount > 0) return;

        SpawnNextWave();
    }
    #endregion
}
