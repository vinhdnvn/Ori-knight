using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlash : MonoBehaviour
{

    // didrection
    public Vector2 direction;
    public float speed;
    public float lifeTime;
    public int damage;

    // create on collision damage to enemy and destroy when hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().hurt(damage);
            Destroy(gameObject);
        }

    }

    // onCollider 2d damage to enemy and destroy when hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyController>().hurt(damage);
            Destroy(gameObject);
        }

    }

    public void trigger()
    {
        Vector2 newVelocity = direction.normalized * speed;
        gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;

        StartCoroutine(destroyCoroutine(lifeTime));
    }

    // destroy after a certain time
    private IEnumerator destroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
