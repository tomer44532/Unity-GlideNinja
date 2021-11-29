using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClownBoss : Boss
{
    private Player player;
    private MapManager mapManager;
    private Rigidbody2D rigidbody;
    private float destinationX;
    private bool facingRight;
    [SerializeField]
    private float defaultMovementSpeed;
    private float currentMovementSpeed;
    [SerializeField]
    private float defaultDiveSpeed;
    private float currentDiveSpeed;
    [SerializeField]
    private float defaultGoUpSpeed;
    private float currentGoUpSpeed;
    private BossActionType eCurState;

    private Vector3 diveTarget;
    private Vector2 diveDirection;
    private bool isDiving;

    private float spawnY;

    private float zAngle;

    [SerializeField]
    private int maxBombs;

    private int curBomb;

    [SerializeField]
    private GameObject bombSpawn;

    [SerializeField]
    private GameObject bombPrefab;

    [SerializeField]
    private float diveY;

    private float flightY;

    [SerializeField]
    private GameObject bossHelathBar;

    [SerializeField]
    private GameObject bossHealthBarFill;

    [SerializeField]
    private float bossMaxHp;

    private float bossCurrentHp;

    private bool canTakeDamage = true;
    

    private enum BossActionType
    {
        Spawn,
        Moving,
        Dive,
        DropBombs
    }

    DiveActionType eCurDiveState;

    private enum DiveActionType
    {
        FindTarget,
        Dive,
        GoUp
    }


    [SerializeField]
    private GameObject bossRoom;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        flightY = transform.position.y;
        GetBorders();
        destinationX = xOriginalMin;
        currentMovementSpeed = defaultMovementSpeed;
        currentDiveSpeed = defaultDiveSpeed;
        currentGoUpSpeed = defaultGoUpSpeed;
        spawnY = transform.position.y;
        //delete later
        eCurState = BossActionType.Spawn;
        StartCoroutine(BombCoolDown());
        //StartCoroutine(DiveCoolDown());
        //////
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        HandleStates();
        Debug.Log("boss state " + eCurState);
        LookAtTarget();
        base.Update();
    }

    private void HandleStates()
    {
        switch (eCurState)
        {
            case BossActionType.Spawn:
                HandleSpawnState();
                break;
            case BossActionType.Moving:
                HandleMovingState();
                break;
            case BossActionType.Dive:
                HandleDiveState();
                break;
            case BossActionType.DropBombs:
                HandleDropBombs();
                break;
        }
    }

    private void HandleSpawnState()
    {
        bossCurrentHp = bossMaxHp;
        bossHelathBar.SetActive(true);
        UpdateHealthBar();
        eCurState = BossActionType.Moving;
    }

    private void HandleDropBombs()
    {
        if (curBomb < maxBombs)
        {
            Debug.Log("drop bomb" + curBomb);
            GameObject tmpBomb = Instantiate(bombPrefab, bombSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            tmpBomb.GetComponent<Bomb>().Initialize(gameObject,Vector2.zero, mapManager, "none");
            StartCoroutine(BombCoolDown());
            curBomb++;
        }
        else
        {
            curBomb = 0;
            StartCoroutine(DiveCoolDown());
        }
        eCurState = BossActionType.Moving;
    }

    private void HandleDiveState()
    {
        switch (eCurDiveState)
        {
            case DiveActionType.FindTarget:
                rigidbody.velocity = Vector2.zero;
                diveTarget = player.transform.position;
                //LookAtTarget();
                
                diveDirection = new Vector2(diveTarget.x - transform.position.x, diveTarget.y - transform.position.y).normalized;
                zAngle = ((float)Math.Atan2(diveDirection.y, diveDirection.x)) * 180 /(float)Math.PI ;
                if (diveDirection.x < 0)
                {
                    zAngle = zAngle + 180;
                }
                Debug.Log("move boss " + zAngle);
                transform.localRotation = Quaternion.Euler(0, 0, zAngle);
                eCurDiveState = DiveActionType.Dive;
                break;
            case DiveActionType.Dive:
                rigidbody.velocity = diveDirection * currentDiveSpeed;
                Debug.Log("boss dive " + transform.position.x +" "+xOriginalMin);
                if (Math.Abs(transform.position.y - diveY) < 1 || transform.position.x < xOriginalMin || transform.position.x > xOriginalMax)
                {
                    diveDirection = -1 * diveDirection;
                    eCurDiveState = DiveActionType.GoUp;
                }
                break;
            case DiveActionType.GoUp:
                rigidbody.velocity = diveDirection * currentGoUpSpeed;
                if(transform.position.y > spawnY)
                {
                    rigidbody.velocity = Vector2.zero;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    StartCoroutine(BombCoolDown());
                    //StartCoroutine(DiveCoolDown());
                    eCurState = BossActionType.Moving;
                }
                break;


        }
        //if (!isDiving)
        //{
        //    rigidbody.velocity = Vector2.zero;
        //    diveTarget = player.transform.position;
        //    diveDirection = new Vector2(diveTarget.x - transform.position.x , diveTarget.y - transform.position.y).normalized;

        //    isDiving = true;
        //}
        //else
        //{
        //    rigidbody.velocity = diveDirection * currentMovementSpeed;
        //}

        //if (Math.Abs(transform.position.y - diveTarget.y) < 1 || transform.position.x < xOriginalMin || transform.position.x > xOriginalMax)
        //{
        //    Debug.Log("move boss " + transform.position + " " + diveTarget + " " + diveDirection);
        //    isDiving = false;
        //}

    }

    private void HandleMovingState()
    {
        if (Math.Abs(gameObject.transform.position.x - destinationX) > 1)
        {
            Debug.Log("boss walk " + gameObject.transform.position.x + " " + destinationX);
            Walk();
        }
        else
        {
            if(destinationX == xOriginalMin)
            {
                destinationX = xOriginalMax;
            }
            else
            {
                destinationX = xOriginalMin;
            }
        }
    }

    public void GetBorders()
    {
        Debug.Log("set borders");
        XOriginalMin = bossRoom.transform.Find("left wall").position.x + 5;
        XOriginalMax = bossRoom.transform.Find("right wall").position.x - 5;

    }


    public void Walk()
    {
        int direction;
        if (destinationX - transform.position.x > 0)
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
        rigidbody.velocity = new Vector2(direction * currentMovementSpeed, rigidbody.velocity.y);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(destinationX, transform.position.y), currentDownMovementSpeed * Time.deltaTime);
    }

    protected void LookAtTarget()
    {
        if ((rigidbody.velocity.x < 0 && !facingRight) || (rigidbody.velocity.x > 0 && facingRight))
        {
            ChangeDirection();
        }


    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;


        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public IEnumerator DiveCoolDown()
    {

        yield return new WaitForSeconds(2);

        eCurState = BossActionType.Dive;
        eCurDiveState = DiveActionType.FindTarget;
    }

    public IEnumerator BombCoolDown()
    {

        yield return new WaitForSeconds(3);

        eCurState = BossActionType.DropBombs;
    }

    public IEnumerator TakeDamageCoolDown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1);
        canTakeDamage = true;
        
    }

    public float GetAngle(Vector3 source, Vector3 dest)
    {
        if (source.x != dest.x)
        {
            float m = (source.y - dest.y) / (source.x - dest.x);
            float angle = Mathf.Atan(m) * 180 / Mathf.PI;
            return angle;
        }
        return 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "damage" && eCurState == BossActionType.Dive)
        {
            TakeDamage();
            if(eCurDiveState != DiveActionType.GoUp)
            {
                diveDirection = -1 * diveDirection;
                eCurDiveState = DiveActionType.GoUp;
            }
        }
        Debug.Log("hit boss " + col.gameObject.tag + " " + eCurState);
        //eCurDiveState = DiveActionType.GoUp;

    }

    public override void TakeDamage()
    {
        if(bossCurrentHp > 0 && canTakeDamage)
        {
            Debug.Log("boss damage");
            bossCurrentHp--;
            UpdateHealthBar();
            StartCoroutine(TakeDamageCoolDown());
            base.TakeDamage();
        }
    }

     public void UpdateHealthBar()
    {
        bossHealthBarFill.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, bossCurrentHp/bossMaxHp);

    }


    public override void InitializeBoss()
    {
        
    }
}
