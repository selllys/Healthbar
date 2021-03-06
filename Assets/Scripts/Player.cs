using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField, Min(1)] private int _maxHealth = 100;

    private int _health;

    public event UnityAction<float> HealthChanged;

    private enum HealthChangeMode
    {
        Increase,
        Decrease
    }

    public int Health => _health;

    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void Heal(int amount)
    {
        ChangeHealthBy(amount);
    }

    public void TakeDamage(int amount)
    {
        ChangeHealthBy(amount, HealthChangeMode.Decrease);
    }

    private void ChangeHealthBy(int amount, HealthChangeMode mode = HealthChangeMode.Increase)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException("amount");
        }

        if (mode == HealthChangeMode.Decrease)
        {
            amount = -amount;
        }

        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
        HealthChanged?.Invoke(_health);
    }
}