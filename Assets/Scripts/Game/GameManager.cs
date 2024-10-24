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
        
    }

    private void OnDisable()
    {
       
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

   
}
