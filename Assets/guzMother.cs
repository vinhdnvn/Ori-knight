using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guzMother : MonoBehaviour
{

    // for Player 
    [Header("Player")]
    [SerializeField] GameObject playerObject;

    //For idle Stage
    [Header("Idle Stage")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirection;

    // For attack up and down stage
    [Header("Attack Up and Down Stage")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirection;

    // for attack layer stage
    [Header("Attack Layer")]
    [SerializeField] float attackPlayerSpeed;
    [SerializeField] Transform player;
    private Vector2 playerPosition;
    private bool hasPlayerPosition;
    // other
    [Header("Other")]
    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckWall;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp = true;
    private bool facingLeft = true;
    private Rigidbody2D bossRb;

    public int health;



    // death
    private Transform _transform;
    public float hurtRecoilTime;
    public Vector2 hurtRecoil;
    public Vector2 deathForce;


    private Animator enemyAnim;
    void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        bossRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();

        _transform = gameObject.GetComponent<Transform>();



    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);
        // IdleState();
        // AttackUpNDown();
        FlipTowardsPlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
            AttackPlayer();
        }




        // AttackPlayer(

    }


    void randomStatePicker()
    {
        int randomState = Random.Range(0, 2);
        if (randomState == 0)
        {
            // attackupndown animation
            enemyAnim.SetTrigger("AttackUpNDown");
        }
        else if (randomState == 1)
        {
            // attackplayer animation
            enemyAnim.SetTrigger("AttackPlayer");
        }
    }

    public void IdleState()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }

        }

        bossRb.velocity = idleMoveDirection * idleMoveSpeed;

    }

    public void AttackUpNDown()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }

        }

        bossRb.velocity = attackMoveSpeed * attackMoveDirection;

    }

    public void AttackPlayer()
    {

        if (!hasPlayerPosition)
        {
            playerPosition = player.position - transform.position;
            playerPosition.Normalize();
            hasPlayerPosition = true;
        }
        if (hasPlayerPosition)
        {
            bossRb.velocity = playerPosition * attackPlayerSpeed;
        }
        if (isTouchingWall || isTouchingUp || isTouchingDown)
        {
            bossRb.velocity = Vector2.zero;
            hasPlayerPosition = false;
            enemyAnim.SetTrigger("Slamed");


        }



    }
    public void Flip()
    {
        facingLeft = !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void FlipTowardsPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;
        if (playerDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDirection < 0 && !facingLeft)
        {
            Flip();
        }

    }
    void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);
    }


    // hit player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerController>().hurt(1);
        }
    }

    private void die()
    {
        enemyAnim.SetBool("Die", true);
        // Tắt Rigidbody
        Vector2 newVelocity;
        newVelocity.x = 0;
        newVelocity.y = 0;
        bossRb.velocity = newVelocity;

        gameObject.layer = LayerMask.NameToLayer("Decoration");

        Vector2 newForce;
        newForce.x = _transform.localScale.x * deathForce.x;
        newForce.y = deathForce.y;
        bossRb.AddForce(newForce, ForceMode2D.Impulse);


        Destroy(gameObject, 1f);


    }

    public void hurt(int damage)
    {
        health = Mathf.Max(health - damage, 0);
        if (health == 0)
        {
            die();
            return;
        }


        //  boss thụt lùi khi nhận dame


        StartCoroutine(hurtCoroutine());
    }
    private IEnumerator hurtCoroutine()
    {
        // Lưu trữ vận tốc hiện tại của boss
        Vector2 currentVelocity = bossRb.velocity;

        // Tính toán hướng thụt lùi ngược với hướng di chuyển hiện tại
        Vector2 recoilDirection = -currentVelocity.normalized;

        // Thực hiện thụt lùi trong khoảng thời gian hurtRecoilTime
        float timer = 0f;
        while (timer < hurtRecoilTime)
        {
            bossRb.velocity = recoilDirection * hurtRecoil.magnitude;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(hurtRecoilTime);

        bossRb.velocity = Vector2.zero;

    }
}
