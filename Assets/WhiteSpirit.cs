using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSpirit : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update


    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Platform"))
    //     {
    //         Destroy(gameObject);
    //     }

    // }

    // // on collision destroy
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.CompareTag("Platform"))
    //     {
    //         // collision.collider.GetComponent<EnemyController>().hurt(damage);
    //         Destroy(gameObject);
    //     }

    // }


    public void trigger()
    {
        StartCoroutine(destroyCoroutine(lifeTime));
    }
    private IEnumerator destroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
