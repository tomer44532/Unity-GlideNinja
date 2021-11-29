using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NinjaBoss : Boss
{
    private Player player;
    private MapManager mapManager;
    private Camera camera;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private int bossStage;
    private BossActionType eCurState;
    private enum BossActionType
    {
        Spawn,
        Moving,
        walking,
        Attacking,
        GoingDown,
        GoingUp,
        FollowPlayerHeight
    }

    [SerializeField]
    private GameObject bossRoom;




    private float xMinSpawnRange;
    private float xMaxSpwanRange;
    private float yCeiling;

    private bool facingRight;

    private float destinationX;
    private float playerY;

    [SerializeField]
    private float normalDownMovementSpeed;

    [SerializeField]
    private float fastDownMovementSpeed;

    private float currentDownMovementSpeed;

    [SerializeField]
    private Transform knifePos;

    [SerializeField]
    private GameObject knifePrefab;

    [SerializeField]
    private float bossAttackDelay;

    private bool isAttackDelayOver;
    private bool canThrow = true;
    bool spawn = true;

    private float minCameraX;
    private float maxCameraX;

    [SerializeField]
    private GameObject bossDoor;

    [SerializeField]
    private int maxHealth;

    private int currentHealth;
    private int healthPerStage;
    private int stagesAmount = 3;

    private int attackNumber;






    // Start is called before the first frame update
    protected override void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        eCurState = BossActionType.Spawn;
        bossStage = 1;
        GetBorders();
        //normalDownMovementSpeed = -1 * normalDownMovementSpeed;
        //fastDownMovementSpeed = -1 * fastDownMovementSpeed;
        currentDownMovementSpeed = normalDownMovementSpeed;
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        bossDoor.GetComponent<BossDoor>().Initialize(this);
        //bossRoom.transform.Find("left wall").GetComponent<BossEntrance>().Initialize(this);
        currentHealth = maxHealth;
        healthPerStage = maxHealth / 3;
        base.Start();
    }

    // Update is called once per frame
   protected override void Update()
    {
        HandleStates();
        LookAtTarget();
        Debug.Log("boss state " + eCurState);
        base.Update();
    }

    public void GetCameraBounds()
    {
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minCameraX = lowerLeft.x + 5f;
        maxCameraX = upperRight.x - 5f;
    }

    public void GetBorders()
    {
        Debug.Log("set borders");
        XOriginalMin = bossRoom.transform.Find("left wall").position.x + 5;
        XOriginalMax = bossRoom.transform.Find("right wall").position.x - 5;
        yCeiling = bossRoom.transform.Find("ceiling").position.y+5;
    }

    protected void LookAtTarget()
    {
        if(eCurState == BossActionType.walking)
        {
            Debug.Log("boss direction "+rigidbody.velocity.x +" " + facingRight);

            if ((rigidbody.velocity.x < 0 && !facingRight)||(rigidbody.velocity.x > 0 && facingRight))
            {
                ChangeDirection();
            }
        }
        else
        {
            float xDir = player.transform.position.x - transform.position.x;
            if ((xDir < 0 && facingRight) || (xDir > 0 && !facingRight))
            {
                ChangeDirection();
            }
        }


    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }


    public void HandleStates()
    {
        switch (bossStage)
        {
            case 1:
                switch (eCurState)
                {
                    case BossActionType.Spawn:
                        HandleSpawnState();
                        break;
                    case BossActionType.Moving:
                        HandleMovingState();
                        break;
                    case BossActionType.walking:
                        HandleWalkingState();
                        break;
                    case BossActionType.GoingDown:
                        HandleGoingDownState();
                        break;
                    case BossActionType.Attacking:
                        HandleAttackState();
                        break;
                    case BossActionType.GoingUp:
                        HandleGoingUpState();
                        break;
                }
                break;
            case 2:
                switch (eCurState)
                {
                    case BossActionType.Moving:
                        HandleMovingState();
                        break;
                    case BossActionType.walking:
                        HandleWalkingState();
                        break;
                    case BossActionType.GoingDown:
                        HandleGoingDownState();
                        break;
                    case BossActionType.Attacking:
                        HandleAttackState();
                        break;
                    case BossActionType.FollowPlayerHeight:
                        HandleFollowPlayerHeight();
                        break;
                    case BossActionType.GoingUp:
                        HandleGoingUpState();
                        break;
                }
                break;
            case 3:
                switch(eCurState)
                {
                    case BossActionType.Moving:
                        HandleMovingState();
                        break;
                    case BossActionType.walking:
                        HandleWalkingState();
                        break;
                    case BossActionType.GoingDown:
                        HandleGoingDownState();
                        break;
                    case BossActionType.Attacking:
                        HandleAttackState();
                        break;
                    case BossActionType.FollowPlayerHeight:
                        HandleFollowPlayerHeight();
                        break;
                    case BossActionType.GoingUp:
                        HandleGoingUpState();
                        break;
                }
                break;
        }
    }



    public void HandleSpawnState()
    {
        //do spawn stuff
        CameraFollow cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        cameraFollow.FollowGameObject = gameObject;
        bool finished = cameraFollow.FollowObject();
        player.Components.Rigidbody.velocity = Vector2.zero;
        if (finished)
        {
            eCurState = BossActionType.GoingUp;
        }
    }

    public void HandleMovingState()
    {
        UpdateSpawnArea();
        MoveToTop();
        if (spawn)
        {
            spawn = false;
            //eCurState = BossActionType.GoingDown;
            animator.SetBool("walk", true);
            eCurState = BossActionType.walking;
        }
        else
        {
            animator.SetBool("walk", true);
            eCurState = BossActionType.walking;
        }
        //animator.SetBool("walk", true);
        //eCurState = BossActionType.walking;
    }

    public void HandleWalkingState()
    {
        Debug.Log("walking " + Math.Abs(gameObject.transform.position.x - destinationX));
        if(Math.Abs(gameObject.transform.position.x - destinationX) > 1)
        {
            Debug.Log("boss walk " + gameObject.transform.position.x+" "+ destinationX);
            Walk();
        }
        else
        {
            playerY = player.transform.position.y;
            animator.SetBool("walk", false);
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            eCurState = BossActionType.GoingDown;
        }
    }

    private void HandleGoingDownState()
    {
        Debug.Log("boss position "+ gameObject.transform.position.y+" "+ playerY);
        if (gameObject.transform.position.y > playerY)
        {
            MoveDown();
        }
        else
        {
            StopMovingVertical();
            eCurState = BossActionType.Attacking;
        }
    }

    public void HandleAttackState()
    {
        Attack();
        HandleAttackDelay();
    }

    public void HandleGoingUpState()
    {
        if (gameObject.transform.position.y < yCeiling)
        {
            MoveUp();
        }
        else
        {
            StopMovingVertical();
            //CheckStage();

            eCurState = BossActionType.Moving;
        }
    }

    public void HandleFollowPlayerHeight()
    {
        if (gameObject.transform.position.y != playerY)
        {
            MoveToPlayerHeight();
        }
        else
        {
            StopMovingVertical();
            eCurState = BossActionType.Attacking;
        }
    }

    void UpdateSpawnArea()
    {
        GetCameraBounds();
        float newX = player.transform.position.x;
        //float minX, maxX;
        //if(minCameraX > xOriginalMin)
        //{
        //    minX = minCameraX;
        //}
        //if(maxCameraX < xOriginalMax)
        //{
        //    maxX = maxCameraX;
        //}
        if (Mathf.Abs(XOriginalMin - newX) < Mathf.Abs(XOriginalMax - newX))
        {
            if (maxCameraX < XOriginalMax)
            {
                xMaxSpwanRange = maxCameraX;

            }
            else
            {
                xMaxSpwanRange = XOriginalMax;
            }

            xMinSpawnRange = newX + 8;

        }
        else
        {
            if (minCameraX > XOriginalMin)
            {
                xMinSpawnRange = minCameraX;
            }
            else
            {
                xMinSpawnRange = XOriginalMin;
            }
            xMaxSpwanRange = newX - 8;

        }
        Debug.Log("camera min " + xMinSpawnRange + " camera max " + xMaxSpwanRange);

    }


    private void MoveToTop()
    {

        destinationX = Random.Range(xMinSpawnRange, xMaxSpwanRange);
        if (spawn)
        {
            //transform.position = new Vector3(destinationX, yCeiling, 0);
            //playerY = player.transform.position.y;
        }

        //eCurState = BossActionType.GoingDown;

    }

    public void Walk()
    {
        int direction;
        if(destinationX - transform.position.x > 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        //if (!facingRight)
        //{
        //    direction = -1;
        //}
        rigidbody.velocity = new Vector2(direction * currentDownMovementSpeed, rigidbody.velocity.y);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(destinationX, transform.position.y), currentDownMovementSpeed * Time.deltaTime);
    }

    public void MoveDown()
    {
        //rigidbody.velocity = new Vector2(rigidbody.velocity.x, currentDownMovementSpeed);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x,playerY), currentDownMovementSpeed * Time.deltaTime);

    }

    public void StopMovingVertical()
    {
        //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
    }

    private void Attack()
    {
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (canThrow && playerDistance > 10f)
        {
            LookAtTarget();
            int shurikeSpeed = bossStage;
            if (bossStage == 3)
            {
                playerY = player.transform.position.y;
                GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                Vector2 target = (player.transform.position - transform.position).normalized;
                tmp.GetComponent<Shuriken>().Initialize(gameObject,target, mapManager, "none");
                tmp.GetComponent<Shuriken>().MovementSpeed = shurikeSpeed * tmp.GetComponent<Shuriken>().MovementSpeed;
                Debug.Log("shuriken direction " + player.transform.position + " normalized "+ player.transform.position.normalized);

            }
            else
            {
                if (facingRight)
                {
                    GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                    tmp.GetComponent<Shuriken>().Initialize(gameObject,Vector2.right, mapManager, "none");
                    tmp.GetComponent<Shuriken>().MovementSpeed = shurikeSpeed * tmp.GetComponent<Shuriken>().MovementSpeed;

                }
                else
                {

                    GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));

                    tmp.GetComponent<Shuriken>().Initialize(gameObject,Vector2.left, mapManager, "none");
                    tmp.GetComponent<Shuriken>().MovementSpeed = shurikeSpeed * tmp.GetComponent<Shuriken>().MovementSpeed;

                }

            }
            
            if(bossStage != 1)
            {
                attackNumber++;
            }
            animator.SetTrigger("throw");
            animator.Update(0);
            canThrow = false;
            //StartCoroutine(AttackDelay());
        }
        

       

    }

    public void HandleAttackDelay()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Glide"))
        {
            switch (bossStage)
            {
                case 1:
                    canThrow = true;
                    eCurState = BossActionType.GoingUp;
                    break;
                case 2:
                case 3:
                    Debug.Log("boss attack number " + attackNumber);
                    playerY = player.transform.position.y;
                    canThrow = true;
                    if(attackNumber != 2)
                    {
                        eCurState = BossActionType.FollowPlayerHeight;
                    }
                    else
                    {
                        attackNumber = 0;
                        eCurState = BossActionType.GoingUp;
                    }
                    break;
            }
        }
        //if (isAttackDelayOver)
        //{
        //    isAttackDelayOver = false;
        //    switch (bossStage)
        //    {
        //        case 1:
        //            canThrow = true;
        //            eCurState = BossActionType.GoingUp;
        //            break;
        //    }
        //}
    }
    
    public IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(bossAttackDelay);
        isAttackDelayOver = true;
    }

    public void MoveUp()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, yCeiling), currentDownMovementSpeed * Time.deltaTime);
    }

    //protected void LookAtDestination()
    //{

    //    float xDir = destinationX- transform.position.x;
    //    if ((xDir < 0 && facingRight) || (xDir > 0 && !facingRight))
    //    {
    //        ChangeDirection();
    //    }

    //}

    
    public void MoveToPlayerHeight()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, playerY), currentDownMovementSpeed * Time.deltaTime);
    }


    public override void TakeDamage()
    {
        Debug.Log("boss take damage");
        if(currentHealth > 0)
        {
            currentHealth--;
        }
        CheckStage();
        base.TakeDamage();
    }

    public void CheckStage()
    {
        if(currentHealth == 0)
        {
            int spritesAmount = bossDoor.GetComponent<BossDoor>().DoorSprites.Length;
            bossDoor.GetComponent<BossDoor>().ChangeSprite(spritesAmount-1);
            bossDoor.GetComponent<BossDoor>().OpenDoor();

        }
        if (currentHealth > maxHealth - healthPerStage * 1)
        {
            bossStage = 1;
            bossDoor.GetComponent<BossDoor>().ChangeSprite(bossStage-1);
            return;
        }
        if (currentHealth > maxHealth - healthPerStage * 2)
        {
            Debug.Log("boss stage 2");
            bossStage = 2;
            bossDoor.GetComponent<BossDoor>().ChangeSprite(bossStage - 1);
            return;
        }
        if (currentHealth > maxHealth - healthPerStage * 3)
        {
            bossStage = 3;
            bossDoor.GetComponent<BossDoor>().ChangeSprite(bossStage - 1);
            return;
        }

    }

    public override void InitializeBoss()
    {
        currentHealth = maxHealth;
        bossStage = 1;
        CheckStage();
        transform.position = respawnPosition;
        eCurState = BossActionType.Spawn;
        bossDoor.GetComponent<BossDoor>().CloseDoor();
    }

}
