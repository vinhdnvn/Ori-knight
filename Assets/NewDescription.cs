using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDescription : MonoBehaviour
{

    public GameObject textDescription;
    // Start is called before the first frame update
    void Start()
    {
        textDescription.SetActive(false);

    }

    public void ShowDescription()
    {
        textDescription.SetActive(true);
    }

    public void HideDescription()
    {
        textDescription.SetActive(false);
    }


}
