using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockItemPower : MonoBehaviour
{
    [SerializeField] GameObject particle;
    public bool isUsed;
    // create on collision damage to enemy and destroy when hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isUsed = true;


            // collision.GetComponent<PlayerController>().UnlockTrippleJump();
            // collision.GetComponent<PlayerController>().jumpLeft = 3;
            Destroy(gameObject);
        }

    }

    // onCollider 2d to player then unlock red slash and destroy this object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isUsed = true;
            collision.collider.GetComponent<PlayerController>().UnlockPowerForBoss();
            GameObject _particle = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(_particle, 0.5f);
            collision.collider.GetComponent<PlayerController>().timeToHeal = 0.3f;
            collision.collider.GetComponent<PlayerController>().sprintSpeed = 15.0f;
            collision.collider.GetComponent<PlayerController>().mana = 25.0f;
            collision.collider.GetComponent<PlayerController>().manaGain = 0.3f;
            Destroy(gameObject);
            // StartCoroutine(showUnlockUI());

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        if (playerController.unlockTrippleJump)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
