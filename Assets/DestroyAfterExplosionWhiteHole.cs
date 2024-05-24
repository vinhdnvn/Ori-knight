using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterExplosionWhiteHole : MonoBehaviour
{
    // Start is called before the first frame update

    public float lifeTime;


    private IEnumerator destroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // on collision destroy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            // collision.collider.GetComponent<EnemyController>().hurt(damage);
            Destroy(gameObject);
        }

    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void trigger()
    {
        StartCoroutine(destroyCoroutine(lifeTime));
    }
}
