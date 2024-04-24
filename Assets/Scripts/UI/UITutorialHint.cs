using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITutorialHint : MonoBehaviour
{
    [SerializeField] private TMP_Text _tutorialHint;

    public void ShowHint(string text)
    {
        _tutorialHint.SetText(text);
        _tutorialHint.enabled = true;
        StartCoroutine(HideHint());
    }

    private IEnumerator HideHint()
    {
        yield return new WaitForSeconds(2.0f);
        _tutorialHint.enabled = false;
    }
}
