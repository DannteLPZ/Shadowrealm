using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    public void UpdateHealthUI(float ratio) => _healthSlider.value = ratio;
}
