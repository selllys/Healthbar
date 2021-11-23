using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _changeDuration = 2f;

    private Slider _slider;
    private Coroutine _activeCoroutine;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _player.Health;
    }

    private void OnHealthChanged(float newValue)
    {
        StopActiveCoroutine();
        _activeCoroutine = StartCoroutine(ChangeSliderValue(newValue));
    }

    private void StopActiveCoroutine()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }
    }

    private IEnumerator ChangeSliderValue(float targetValue)
    {
        float totalChange = Mathf.Abs(targetValue - _slider.value);
        float changePerSecond = totalChange / _changeDuration;

        while (Mathf.Approximately(_slider.value, targetValue) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, changePerSecond * Time.deltaTime);

            yield return null;
        }
    }
}