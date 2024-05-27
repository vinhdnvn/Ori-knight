using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkillProvider : MonoBehaviour
{

    [SerializeField] UnlockSkill unlockUI;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Unlocking skill");
            unlockUI.ShowUnlockUI();
        }
    }


}
