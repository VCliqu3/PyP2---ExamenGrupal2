using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour,IHasHealth
{
    [Header("Settings")]
    [SerializeField] private UnitSO entitySO;

    private int health;

    public event EventHandler<OnHealthEventArgs> OnHealhIncreased;
    public event EventHandler<OnHealthEventArgs> OnHealhDecreased;

    public static event EventHandler<OnAnyHealthEventArgs> OnAnyHealhIncreased;
    public static event EventHandler<OnAnyHealthEventArgs> OnAnyHealhDecreased;

    public event EventHandler OnEntityDeath;
    public static event EventHandler<OnAnyEntityDeathEventArgs> OnAnyEntityDeath;

    public class OnHealthEventArgs : EventArgs
    {
        public int health;
        public int quantity;
    }

    public class OnAnyHealthEventArgs : EventArgs
    {
        public int health;
        public int quantity;
        public EntityHealth entity;
    }

    public class OnAnyEntityDeathEventArgs : EventArgs
    {
        public EntityHealth entity;
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        health = entitySO.health;
    }

    public int GetHealth() => health;
    public int GetMaxHealth() => entitySO.health;

    public void IncreaseHealth(int quantity)
    {
        int previousHealth = health;

        health = health + quantity > GetMaxHealth() ? GetMaxHealth() : health + quantity;

        if (previousHealth == health) return;

        OnHealhIncreased?.Invoke(this, new OnHealthEventArgs { health = health, quantity = health - previousHealth });
        OnAnyHealhIncreased?.Invoke(this, new OnAnyHealthEventArgs { health = health, quantity = health - previousHealth, entity = this });

        if (IsAlive()) return;

        OnEntityDeath?.Invoke(this, EventArgs.Empty);
        OnAnyEntityDeath?.Invoke(this, new OnAnyEntityDeathEventArgs { entity = this });
    }

    public void TakeDamage(int quantity)
    {
        int previousHealth = health;

        health = health - quantity < 0 ? 0 : health - quantity;

        if (previousHealth == health) return;

        OnHealhDecreased?.Invoke(this, new OnHealthEventArgs { health = health, quantity = previousHealth - health });
        OnAnyHealhDecreased?.Invoke(this, new OnAnyHealthEventArgs { health = health, quantity = previousHealth - health, entity = this });
    }

    public bool IsAlive() => health > 0;

    public Transform GetTransform() => transform;
}
