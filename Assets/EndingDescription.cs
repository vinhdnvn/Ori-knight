using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDescription : MonoBehaviour
{

    [SerializeField] GameObject EndingDescriptionUI;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndingDescriptionUI.SetActive(true);
            // set active back to false after 5 seconds
            StartCoroutine(DisableEndingDescription());

            Destroy(gameObject);


        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            EndingDescriptionUI.SetActive(true);
            // set active back to false after 5 seconds
            StartCoroutine(DisableEndingDescription());

            Destroy(gameObject);
        }
    }

    IEnumerator DisableEndingDescription()
    {
        yield return new WaitForSeconds(2);
        EndingDescriptionUI.SetActive(false);
    }


}
