using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartCombatUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button startCombatButton;

    public static event EventHandler OnStartCombat;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        startCombatButton.onClick.AddListener(StartCombat);
    }

    private void StartCombat() => OnStartCombat?.Invoke(this, EventArgs.Empty);
}
