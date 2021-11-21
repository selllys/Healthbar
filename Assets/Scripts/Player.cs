using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField, Min(1)] private int _maxHealth = 100;

    [SerializeField] private UnityEvent<float> OnHealthPercentChanged;

    private int _health;

    public float HealthPercent => (float)_health / _maxHealth;

    private void Start()
    {
        _health = _maxHealth;
        OnHealthPercentChanged?.Invoke(HealthPercent);
    }

    public void ChangeHealthBy(int amount)
    {
        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
        OnHealthPercentChanged?.Invoke(HealthPercent);

        Debug.Log($"Current health: {_health}");
    }
}