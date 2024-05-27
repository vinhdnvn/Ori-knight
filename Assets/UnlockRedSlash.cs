using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class UnlockRedSlash : MonoBehaviour
{
    // didrection
    [SerializeField] GameObject particle;
    [SerializeField] UnlockSkill unlockUI;
    public bool isUsed;


    // create on collision damage to enemy and destroy when hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isUsed = true;

            collision.GetComponent<PlayerController>().UnlockRedSlash();
            Destroy(gameObject);
        }

    }

    // onCollider 2d to player then unlock red slash and destroy this object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isUsed = true;
            GameObject _particle = Instantiate(particle, transform.position, Quaternion.identity);

            // turn on unlock ui
            unlockUI.ShowUnlockUI();

            Destroy(_particle, 0.5f);
            collision.collider.GetComponent<PlayerController>().UnlockRedSlash();

            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // get PlayerController
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        if (playerController.unlockRedSlash)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
