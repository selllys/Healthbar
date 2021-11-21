using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour
{
    [SerializeField] private float _changeDuration = 2f;

    private Slider _slider;
    private bool _isInitialized = false;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void HandleHealthPercentChanged(float newPercent)
    {
        if (newPercent < 0 || newPercent > 1)
        {
            throw new ArgumentOutOfRangeException("percent");
        }

        if (_isInitialized)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeSliderValue(newPercent));
        }
        else
        {
            _slider.value = newPercent;
            _isInitialized = true;
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