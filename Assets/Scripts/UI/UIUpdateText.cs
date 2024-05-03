using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdateText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void UpdateText(string text) => _text.SetText(text);
}
