using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string victoryScene;
    [SerializeField] private string defeatScene;

    private void OnEnable()
    {
        EntitiesManager.OnAnyAllyEntityRemoved += EntitiesManager_OnAnyAllyEntityRemoved;
        UnitMovement.OnUnitEndMovement += UnitMovement_OnUnitEndMovement;
    }

    private void OnDisable()
    {
        EntitiesManager.OnAnyAllyEntityRemoved -= EntitiesManager_OnAnyAllyEntityRemoved;
        UnitMovement.OnUnitEndMovement -= UnitMovement_OnUnitEndMovement;
    }

    private void Awake()
    {
        SetSingleton();
    }
    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one GameManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void GoToVictoryScene() => SceneManager.LoadScene(victoryScene);
    private void GoToDefeatScene() => SceneManager.LoadScene(defeatScene);

    private void CheckNoAllyStructures()
    {
        if (EntitiesManager.Instance.GetAllyStructuresCount() > 0) return;

        GoToDefeatScene();      
    }

    private void CheckAllyUnitReachedEnemyBase(Entity entity, NodePosition nodePosition)
    {
        if (nodePosition.Node != NodeManager.Instance.GetEnemyBase()) return;
        if (!entity.IsAlied) return;

        GoToVictoryScene();
    }

    private void EntitiesManager_OnAnyAllyEntityRemoved(object sender, EntitiesManager.OnEntityEventArgs e)
    {
        CheckNoAllyStructures();
    }

    private void UnitMovement_OnUnitEndMovement(object sender, UnitMovement.OnUnitEndMovementEventArgs e)
    {
        CheckAllyUnitReachedEnemyBase(e.unitMovement.Entity, e.nodePosition);
    }


}
