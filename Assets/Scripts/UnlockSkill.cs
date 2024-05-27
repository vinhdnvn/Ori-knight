using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockSkill : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textGUI;
    [SerializeField] private string nameSkill;
    [SerializeField] private string howToUse;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        string text = $"Unlocked {nameSkill} skill!\n";
        if (howToUse != "")
        {
            text += $"({howToUse})";
        }
        textGUI.text = text;
    }

    public void ShowUnlockUI()
    {
        gameObject.SetActive(true);
        StartCoroutine(Hide(2f));
    }

    public IEnumerator Hide(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
