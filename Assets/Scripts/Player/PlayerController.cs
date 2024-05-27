using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // ======Healing=======
    public bool healing;
    float healTimer;
    public float timeToHeal = 1f;
    // ======================
    [Header("Audio Settings")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] public AudioClip dashSound;
    [SerializeField] public AudioClip attackSound;
    [SerializeField] public AudioClip wallJumpSound;
    [SerializeField] public AudioClip walkSound;
    [SerializeField] public AudioClip hitSound;
    [SerializeField] public AudioClip redSlashSound;

    [SerializeField] public AudioClip pickUpSound;

    // background sound
    [SerializeField] public AudioClip backgroundSound;

    private AudioSource audioSource;  // Biến cho AudioSource
    private bool isJumpingSoundPlayed = false;  // Biến để kiểm tra trạng thái âm thanh nhảy

    private float footstepTimer = 0f;  // Timer for footstep sound
    private float footstepInterval = 0.5f;  // Interval between footstep sounds
    private bool isPlayingFootstepSound = false;
    // ==============Mana Settings =========
    [Header("Mana Settings")]

    [SerializeField] UnityEngine.UI.Image manaStorage;
    public float mana = 8f;
    [SerializeField] float manaDrainSpeed = 0.2f;
    [SerializeField] public float manaGain = 0.1f;
    [Space(5)]
    // ==========================
    // ===========Spell casting
    [Header("Spell Casting")]

    public bool isCasting;
    public GameObject redSlash;
    public GameObject WhiteHole;
    public GameObject WhiteSpirit;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileDamage = 1f;
    [SerializeField] float projectileManaCost = 0.2f;
    [SerializeField] float downSpellForce = 10; // desolate dive skill

    public bool unlockRedSlash;
    public bool unlockPowerForBoss;

    public bool unlockTrippleJump;

    public void UnlockRedSlash()
    {
        unlockRedSlash = true;
        audioSource.PlayOneShot(pickUpSound);
        PlayerPrefs.SetInt("UnlockRedSlash", boolToInt(unlockRedSlash));
    }

    public void UnlockPowerForBoss()
    {
        unlockPowerForBoss = true;
        audioSource.PlayOneShot(pickUpSound);
        PlayerPrefs.SetInt("UnlockPowerForBoss", boolToInt(unlockPowerForBoss));
        // powerForBoss();
    }

    public void UnlockTrippleJump()
    {
        unlockTrippleJump = true;
        audioSource.PlayOneShot(pickUpSound);
        PlayerPrefs.SetInt("UnlockTrippleJump", boolToInt(unlockTrippleJump));
    }

    public bool unlockWhiteHole;
    public void UnlockWhiteHole()
    {
        unlockWhiteHole = true;
        audioSource.PlayOneShot(pickUpSound);
        PlayerPrefs.SetInt("UnlockWhiteHole", boolToInt(unlockWhiteHole));
    }

    public bool unlockWhiteSpirit;
    public void UnlockWhiteSpirit()
    {
        unlockWhiteSpirit = true;
        audioSource.PlayOneShot(pickUpSound);
        PlayerPrefs.SetInt("UnlockWhiteSpirit", boolToInt(unlockWhiteSpirit));
    }

    [Space(5)]

    // MAP
    public GameObject mapHandler;
    bool isMapOpen;


    [SerializeField] protected GameObject orangeBlood;

    [SerializeField] public ParticleSystem dustEffect;


    public int health;
    public float moveSpeed;
    public float jumpSpeed;
    public int jumpLeft = 2;

    public Vector2 climbJumpForce;
    public float fallSpeed;
    [SerializeField] public float sprintSpeed = 10.0f;
    public float sprintTime;
    public float sprintInterval;
    public float attackInterval;

    public Color invulnerableColor;
    public Vector2 hurtRecoil;
    public float hurtTime;
    public float hurtRecoverTime;
    public Vector2 deathRecoil;
    public float deathDelay;

    public Vector2 attackUpRecoil;
    public Vector2 attackForwardRecoil;
    public Vector2 attackDownRecoil;

    public GameObject attackUpEffect;
    public GameObject attackForwardEffect;
    public GameObject attackDownEffect;
    public GameObject healingEffect;

    private bool _isGrounded;
    private bool _isClimb;
    private bool _isSprintable;
    private bool _isSprintReset;
    private bool _isInputEnabled;
    private bool _isFalling;
    private bool _isAttackable;

    private float _climbJumpDelay = 0.2f;
    private float _attackEffectLifeTime = 0.05f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    // Start is called before the first frame update
    private void Start()
    {
        _isInputEnabled = true;
        _isSprintReset = true;
        _isAttackable = true;

        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();

        audioSource = gameObject.GetComponent<AudioSource>();


        // play background sound with loop "backgroundSound" with volumn 0.5f
        // audioSource.clip = backgroundSound;
        // audioSource.loop = true;
        // audioSource.volume = 0.5f;
        // audioSource.Play();

        audioSource.PlayOneShot(backgroundSound, 0.5f);




        if (intToBool(PlayerPrefs.GetInt("UnlockTrippleJump")))
        {
            this.unlockTrippleJump = true;
        }

        if (intToBool(PlayerPrefs.GetInt("UnlockRedSlash")))
        {
            this.unlockRedSlash = true;
        }

        if (intToBool(PlayerPrefs.GetInt("UnlockWhiteHole")))
        {
            this.unlockWhiteHole = true;
        }

        if (intToBool(PlayerPrefs.GetInt("UnlockWhiteSpirit")))
        {
            this.unlockWhiteSpirit = true;
        }

        footstepTimer = footstepInterval;
        // checkItemPowerForBoss();
        if (PlayerPrefs.HasKey("UnlockPowerForBoss") && PlayerPrefs.GetInt("UnlockPowerForBoss") == 1)
        {
            unlockPowerForBoss = true;
        }
        else
        {
            unlockPowerForBoss = false;
        }



        Mana = mana;
        manaStorage.fillAmount = Mana;
    }

    // Update is called once per frame
    private void Update()
    {
        updatePlayerState();
        if (_isInputEnabled)
        {
            move();
            jumpControl();
            fallControl();
            sprintControl();
            attackControl();
            Heal();


        }
        if (unlockRedSlash)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                RedSlashSpell();
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Map");
            isMapOpen = !isMapOpen;
            ToggleMap();
        }

        // if player grounded then turn off sound Jump state PLayed
        if (_isGrounded)
        {
            isJumpingSoundPlayed = false;
        }








        // Kiểm tra xem skill WhiteSpirit đã được mở khóa và player không grounded và nhấn phím U
        if (Input.GetKeyDown(KeyCode.U) && !_isGrounded && !_isClimb)
        {
            Debug.Log("WhiteSpirit");
            WhiteSpirit.SetActive(true);
            if (Mana >= projectileManaCost)
            {


                // GameObject WHITE_SPIRIT = Instantiate(WhiteSpirit, transform.position, Quaternion.identity);

                //    Make the player falling down when using the skill Suddenly
                Vector2 newVelocity;
                newVelocity.x = 0;
                newVelocity.y = -downSpellForce;
                _rigidbody.velocity = newVelocity;

                Mana -= projectileManaCost;

                //    if grounded , destroy WhiteSpirit after 1s
                if (_isGrounded)
                {
                    WhiteSpirit.SetActive(false);
                }




                // if player grounded then WhiteSpirit is not active



            }
            else
            {
                return;
            }


        }


        //    else if player grounded then WhiteSpirit is not active


        // // if WhiteSpirit active , force player down until grounded
        // if (WhiteSpirit.activeSelf)
        // {
        //     Vector2 newVelocity;
        //     newVelocity.x = 0;
        //     newVelocity.y = -downSpellForce;
        //     _rigidbody.velocity = newVelocity;
        // }



        // if (Input.GetKeyDown(KeyCode.I) && _isGrounded && !_isClimb)
        // {
        //     Debug.Log("WhiteHole");
        //     WhiteHoleSpell();
        // }
    }

    // Check trạng thái leo tường/ wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // enter climb state
        if (collision.collider.tag == "Wall" && !_isGrounded)
        {
            _rigidbody.gravityScale = 0;

            Vector2 newVelocity;
            newVelocity.x = 0;
            newVelocity.y = -2;

            _rigidbody.velocity = newVelocity;

            _isClimb = true;
            _animator.SetBool("IsClimb", true);

            _isSprintable = true;
        }
        // //    spell cast
        // if (collision.collider.tag == "Enemy")
        // {
        //     collision.collider.GetComponent<EnemyController>().hurt(spellDamage);
        // }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall" && _isFalling && !_isClimb)
        {
            OnCollisionEnter2D(collision);
        }

    }



    public void hurt(int damage)
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerInvulnerable");

        health = Math.Max(health - damage, 0);

        if (health == 0)
        {
            die();
            return;
        }

        // enter invulnerable state
        _animator.SetTrigger("IsHurt");

        // stop player movement
        Vector2 newVelocity;
        newVelocity.x = 0;
        newVelocity.y = 0;
        _rigidbody.velocity = newVelocity;

        // visual effect
        _spriteRenderer.color = invulnerableColor;

        // death recoil
        Vector2 newForce;
        newForce.x = -_transform.localScale.x * hurtRecoil.x;
        newForce.y = hurtRecoil.y;
        _rigidbody.AddForce(newForce, ForceMode2D.Impulse);

        _isInputEnabled = false;

        StartCoroutine(recoverFromHurtCoroutine());
    }

    private IEnumerator recoverFromHurtCoroutine()
    {
        yield return new WaitForSeconds(hurtTime);
        _isInputEnabled = true;
        yield return new WaitForSeconds(hurtRecoverTime);
        _spriteRenderer.color = Color.white;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // exit climb state
        if (collision.collider.tag == "Wall")
        {
            _isClimb = false;
            _animator.SetBool("IsClimb", false);

            _rigidbody.gravityScale = 1;
        }
    }

    /* ######################################################### */

    private void updatePlayerState()
    {
        _isGrounded = checkGrounded();
        _animator.SetBool("IsGround", _isGrounded);

        float verticalVelocity = _rigidbody.velocity.y;
        _animator.SetBool("IsDown", verticalVelocity < 0);

        if (_isGrounded && verticalVelocity == 0)
        {
            _animator.SetBool("IsJump", false);
            _animator.ResetTrigger("IsJumpFirst");
            _animator.ResetTrigger("IsJumpSecond");
            _animator.SetBool("IsDown", false);

            // double jump
            // jumpLeft = 2;
            jumpLeft = unlockTrippleJump ? 3 : 2;
            _isClimb = false;
            _isSprintable = true;
        }
        else if (_isClimb)
        {
            // one remaining jump chance after climbing
            jumpLeft = 1;
        }
    }



    private void move()
    {
        // calculate movement
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;



        // set velocity
        Vector2 newVelocity;
        newVelocity.x = horizontalMovement;
        newVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = newVelocity;

        if (!_isClimb)
        {
            // the sprite itself is inversed
            float moveDirection = -transform.localScale.x * horizontalMovement;




            if (moveDirection < 0)
            {
                // flip player sprite
                Vector3 newScale;
                newScale.x = horizontalMovement < 0 ? 1 : -1;
                newScale.y = 1;
                newScale.z = 1;

                transform.localScale = newScale;
                CreateDust();

                if (_isGrounded)
                {
                    // turn back animation
                    _animator.SetTrigger("IsRotate");
                }
            }
            else if (moveDirection > 0)
            {
                // move forward
                _animator.SetBool("IsRun", true);


            }


        }

        // stop
        if (Input.GetAxis("Horizontal") == 0)
        {
            _animator.SetTrigger("stopTrigger");
            _animator.ResetTrigger("IsRotate");
            _animator.SetBool("IsRun", false);

            // Stop the dust effect when the player stops moving
            StopDust();

        }
        else
        {
            _animator.ResetTrigger("stopTrigger");
        }
    }

    private void jumpControl()
    {
        if (!Input.GetButtonDown("Jump"))
            return;

        if (_isClimb)
            climbJump();
        else if (jumpLeft > 0)
            jump();
    }

    private void fallControl()
    {
        if (Input.GetButtonUp("Jump") && !_isClimb)
        {
            _isFalling = true;
            fall();
        }
        else
        {
            _isFalling = false;
        }
    }

    private void sprintControl()
    {
        if (Input.GetKeyDown(KeyCode.K) && _isSprintable && _isSprintReset)
            sprint();
    }

    private void attackControl()
    {
        if (Input.GetKeyDown(KeyCode.J) && !_isClimb && _isAttackable)
        {
            audioSource.PlayOneShot(attackSound);
            attack();
        }
    }

    private void die()
    {
        _animator.SetTrigger("IsDead");

        _isInputEnabled = false;

        // stop player movement
        Vector2 newVelocity;
        newVelocity.x = 0;
        newVelocity.y = 0;
        _rigidbody.velocity = newVelocity;

        // visual effect
        _spriteRenderer.color = invulnerableColor;

        // death recoil
        Vector2 newForce;
        newForce.x = -_transform.localScale.x * deathRecoil.x;
        newForce.y = deathRecoil.y;
        _rigidbody.AddForce(newForce, ForceMode2D.Impulse);

        StartCoroutine(deathCoroutine());
    }

    private IEnumerator deathCoroutine()
    {
        var material = _boxCollider.sharedMaterial;
        material.bounciness = 0.3f;
        material.friction = 0.3f;
        // unity bug, need to disable and then enable to make it work
        _boxCollider.enabled = false;
        _boxCollider.enabled = true;

        yield return new WaitForSeconds(deathDelay);

        material.bounciness = 0;
        material.friction = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /* ######################################################### */

    private bool checkGrounded()
    {
        Vector2 origin = _transform.position;

        float radius = 0.2f;

        // detect downwards
        Vector2 direction;
        direction.x = 0;
        direction.y = -1;

        float distance = 0.5f;
        LayerMask layerMask = LayerMask.GetMask("Platform");

        RaycastHit2D hitRec = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
        return hitRec.collider != null;
    }

    private void jump()
    {
        Vector2 newVelocity;
        newVelocity.x = _rigidbody.velocity.x;
        newVelocity.y = jumpSpeed;

        _rigidbody.velocity = newVelocity;

        _animator.SetBool("IsJump", true);
        // double jump
        // Nhảy 1 lần trừ jumpLeft đi 1, nếu jumpLeft = 0 thì đếm đó là lần nhảy số 2
        jumpLeft -= 1;
        if (jumpLeft == 0)
        {
            _animator.SetTrigger("IsJumpSecond");
        }
        else if (jumpLeft == 1)
        {
            _animator.SetTrigger("IsJumpFirst");
        }



        audioSource.PlayOneShot(jumpSound);
        isJumpingSoundPlayed = true;

    }


    // Wall jump , nhảy tường
    private void climbJump()
    {
        Vector2 realClimbJumpForce;
        realClimbJumpForce.x = climbJumpForce.x * transform.localScale.x;
        realClimbJumpForce.y = climbJumpForce.y;
        _rigidbody.AddForce(realClimbJumpForce, ForceMode2D.Impulse);

        _animator.SetTrigger("IsClimbJump");
        _animator.SetTrigger("IsJumpFirst");

        audioSource.PlayOneShot(wallJumpSound);

        _isInputEnabled = false;
        StartCoroutine(climbJumpCoroutine(_climbJumpDelay));

    }

    private IEnumerator climbJumpCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        _isInputEnabled = true;

        _animator.ResetTrigger("IsClimbJump");

        // jump to the opposite direction
        Vector3 newScale;
        newScale.x = -transform.localScale.x;
        newScale.y = 1;
        newScale.z = 1;

        transform.localScale = newScale;
    }

    private void fall()
    {
        Vector2 newVelocity;
        newVelocity.x = _rigidbody.velocity.x;
        newVelocity.y = -fallSpeed;

        _rigidbody.velocity = newVelocity;
    }

    private void sprint()
    {
        // reject input during sprinting
        _isInputEnabled = false;
        _isSprintable = false;
        _isSprintReset = false;

        Vector2 newVelocity;
        newVelocity.x = transform.localScale.x * (_isClimb ? sprintSpeed : -sprintSpeed);
        newVelocity.y = 0;

        _rigidbody.velocity = newVelocity;

        if (_isClimb)
        {
            // sprint to the opposite direction
            Vector3 newScale;
            newScale.x = -transform.localScale.x;
            newScale.y = 1;
            newScale.z = 1;

            transform.localScale = newScale;
        }

        audioSource.PlayOneShot(dashSound);

        _animator.SetTrigger("IsSprint");
        StartCoroutine(sprintCoroutine(sprintTime, sprintInterval));
    }

    private IEnumerator sprintCoroutine(float sprintDelay, float sprintInterval)
    {
        yield return new WaitForSeconds(sprintDelay);
        _isInputEnabled = true;
        _isSprintable = true;

        yield return new WaitForSeconds(sprintInterval);
        _isSprintReset = true;
    }

    private void attack()
    {
        float verticalDirection = Input.GetAxis("Vertical");
        if (verticalDirection > 0)
            attackUp();
        else if (verticalDirection < 0 && !_isGrounded)
            attackDown();
        else
            attackForward();
    }

    private void attackUp()
    {
        _animator.SetTrigger("IsAttackUp");
        attackUpEffect.SetActive(true);

        Vector2 detectDirection;
        detectDirection.x = 0;
        detectDirection.y = 1;

        StartCoroutine(attackCoroutine(attackUpEffect, _attackEffectLifeTime, attackInterval, detectDirection, attackUpRecoil));
    }

    private void attackForward()
    {
        _animator.SetTrigger("IsAttack");
        attackForwardEffect.SetActive(true);

        Vector2 detectDirection;
        detectDirection.x = -transform.localScale.x;
        detectDirection.y = 0;

        Vector2 recoil;
        recoil.x = transform.localScale.x > 0 ? -attackForwardRecoil.x : attackForwardRecoil.x;
        recoil.y = attackForwardRecoil.y;

        StartCoroutine(attackCoroutine(attackForwardEffect, _attackEffectLifeTime, attackInterval, detectDirection, recoil));
    }

    private void attackDown()
    {
        _animator.SetTrigger("IsAttackDown");
        attackDownEffect.SetActive(true);

        Vector2 detectDirection;
        detectDirection.x = 0;
        detectDirection.y = -1;

        StartCoroutine(attackCoroutine(attackDownEffect, _attackEffectLifeTime, attackInterval, detectDirection, attackDownRecoil));
    }

    private IEnumerator attackCoroutine(GameObject attackEffect, float effectDelay, float attackInterval, Vector2 detectDirection, Vector2 attackRecoil)
    {
        Vector2 origin = _transform.position;

        float radius = 0.6f;

        float distance = 1.5f;
        LayerMask layerMask = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Trap") | LayerMask.GetMask("Switch") | LayerMask.GetMask("Projectile") | LayerMask.GetMask("Boss");

        RaycastHit2D[] hitRecList = Physics2D.CircleCastAll(origin, radius, detectDirection, distance, layerMask);

        foreach (RaycastHit2D hitRec in hitRecList)
        {
            GameObject obj = hitRec.collider.gameObject;

            string layerName = LayerMask.LayerToName(obj.layer);

            if (layerName == "Switch")
            {
                Switch swithComponent = obj.GetComponent<Switch>();
                if (swithComponent != null)
                    swithComponent.turnOn();
            }
            else if (layerName == "Enemy")
            {
                Mana += manaGain;

                EnemyController enemyController = obj.GetComponent<EnemyController>();
                if (enemyController != null)
                    enemyController.hurt(1);
                GameObject _orangeBlood = Instantiate(orangeBlood, obj.transform.position, Quaternion.identity);
                Destroy(_orangeBlood, 2.5f);

                audioSource.PlayOneShot(hitSound);



            }
            else if (layerName == "Boss")
            {
                Mana += manaGain;
                guzMother guzMother = obj.GetComponent<guzMother>();
                if (guzMother != null)
                {
                    Debug.Log("guzMother is detected");
                    guzMother.hurt(1);
                }
                else
                {
                    Debug.Log("guzMother is null");
                }
                audioSource.PlayOneShot(hitSound);
            }
            else if (layerName == "Projectile")
            {
                Destroy(obj);
            }
        }

        if (hitRecList.Length > 0)
        {
            _rigidbody.velocity = attackRecoil;
        }

        yield return new WaitForSeconds(effectDelay);

        attackEffect.SetActive(false);

        // attack cool down
        _isAttackable = false;
        yield return new WaitForSeconds(attackInterval);
        _isAttackable = true;
    }

    protected virtual void Death(float _destroyTime)
    {
        Destroy(gameObject, _destroyTime);
    }

    // function to Heal using Input.GetButton
    public void Heal()
    {

        if (Input.GetKey(KeyCode.Mouse1) && health < 5 && Mana > 0 && !_animator.GetBool("IsJump") && !_animator.GetBool("IsSprint")
        // and isGrounded
        )
        {



            Debug.Log("Healing Effect is on");
            healingEffect.SetActive(true);
            healing = true;
            healTimer += Time.deltaTime;
            Debug.Log("Is Healing");

            if (healTimer >= timeToHeal)
            {
                health++;
                healTimer = 0;
            }
            // drain mana
            Mana -= Time.deltaTime * manaDrainSpeed;
        }
        else
        {
            healingEffect.SetActive(false);
            // healing effact off

            healing = false;
            healTimer = 0;

        }



    }
    // function Mana
    public float Mana
    {
        get { return mana; }
        set
        {
            // if mana stats change
            if (mana != value)
            {
                mana = Mathf.Clamp(value, 0, 1);
                manaStorage.fillAmount = Mana;
            }
        }
    }


    void RedSlashSpell()
    {
        if (Mana >= projectileManaCost)
        {

            // get the position current of player



            audioSource.PlayOneShot(redSlashSound);

            GameObject redSlashInstance = Instantiate(redSlash, transform.position, Quaternion.identity);

            if (transform.localScale.x < 0)
            {
                redSlashInstance.transform.rotation = Quaternion.Euler(0, -200, 0);

            }
            else if (transform.localScale.x > 0)
            {
                redSlashInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            // Đặt hướng của đòn tấn công dựa trên hướng hiện tại của người chơi
            Vector2 direction;
            direction.x = -transform.localScale.x; // Hướng của đòn tấn công
            direction.y = 0;
            redSlashInstance.GetComponent<RedSlash>().direction = direction;

            // Lấy thành phần RedSlash từ instance vừa tạo
            RedSlash REDSLASH = redSlashInstance.GetComponent<RedSlash>();

            // Thiết lập các thuộc tính của đòn tấn công
            REDSLASH.speed = projectileSpeed;       // Tốc độ di chuyển của đòn tấn công
            REDSLASH.damage = (int)projectileDamage; // Sát thương gây ra
            REDSLASH.lifeTime = 3f;                 // Thời gian tồn tại của đòn tấn công

            // Kích hoạt đòn tấn công
            REDSLASH.trigger();

            Mana -= projectileManaCost;
        }
        else
        {
            return;
        }
    }

    void WhiteHoleSpell()
    {
        if (Mana >= projectileManaCost)
        {
            // WhiteHole.SetActive(true);
            GameObject whiteHoleInstance = Instantiate(WhiteHole, transform.position, Quaternion.identity);

            DestroyAfterExplosionWhiteHole WHITEHOLE = whiteHoleInstance.GetComponent<DestroyAfterExplosionWhiteHole>();

            WHITEHOLE.lifeTime = 1f;

            WHITEHOLE.trigger();
            Mana -= projectileManaCost;
        }
        else
        {
            return;
        }
    }

    void WhiteSpiritSpell()
    {
        if (Mana >= projectileManaCost)
        {


            Instantiate(WhiteSpirit, transform.position, Quaternion.identity);

            //    Make the player falling down when using the skill Suddenly
            Vector2 newVelocity;
            newVelocity.x = 0;
            newVelocity.y = -downSpellForce;
            _rigidbody.velocity = newVelocity;





            // if player grounded then WhiteSpirit is not active



            Mana -= projectileManaCost;
        }
        else
        {
            return;
        }

    }

    void CreateDust()
    {
        Debug.Log("Creating dust");
        dustEffect.Play();
    }
    void StopDust()
    {
        // Debug.Log("Stopping dust");
        dustEffect.Stop();
    }

    void ToggleMap()
    {

    }


    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

    // void powerForBoss()
    // {
    //     // unlockRedSlash = true;
    //     // unlockTrippleJump = true;
    //     // unlockWhiteHole = true;
    //     // unlockWhiteSpirit = true;
    //     // set healtimer
    //     healTimer = 0.3f;
    //     sprintSpeed = 15.0f;

    // }

    void checkItemPowerForBoss()
    {
        if (PlayerPrefs.HasKey("UnlockPowerForBoss") && PlayerPrefs.GetInt("UnlockPowerForBoss") == 1)
        {
            unlockPowerForBoss = true;
        }
        else
        {
            unlockPowerForBoss = false;
        }
    }

    public void setInputEnabled(bool value)
    {
        _isInputEnabled = value;
    }
}
