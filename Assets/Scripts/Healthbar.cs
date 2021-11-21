using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _changeDuration = 2f;

    private Slider _slider;
    private bool _isInitialized = false;
    private Coroutine _activeCoroutine;

    private void OnEnable()
    {
        _player.HealthPercentChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthPercentChanged -= OnHealthChanged;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnHealthChanged(float newPercent)
    {
        if (newPercent < 0 || newPercent > 1)
        {
            throw new ArgumentOutOfRangeException("percent");
        }

        if (_isInitialized)
        {
            StopActiveCoroutine();
            _activeCoroutine = StartCoroutine(ChangeSliderValue(newPercent));
        }
        else
        {
            _slider.value = newPercent;
            _isInitialized = true;
        }
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