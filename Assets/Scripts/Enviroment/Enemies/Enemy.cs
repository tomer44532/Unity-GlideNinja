using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected bool isMoving;

    protected Player player;

    protected float attackTimer;

    [SerializeField]
    protected float attackCoolDown;

    protected bool canAttack = true;

    protected bool colorEnabled;

    protected Animator animator;

    protected bool facingRight = false;

    protected MapManager mapManager;

    [SerializeField]
    protected string color;

    [SerializeField]
    private float movementSpeed;

    private GameObject enemiesPathsGameObject;
    private GameObject myPathGameObject;
    private List<GameObject> pathSpots;
    private int movingCurSpot;
    private int movingNextSpot;
    private Vector3 movingDirection;





    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        if (isMoving)
        {
            setMovingPath();
        }
    }



    // Update is called once per frame
    protected virtual void Update()
    {
        LookAtTarget();
        if (isMoving)
        {
            Move();
        }
    }

    protected void LookAtTarget()
    {

        float xDir = player.gameObject.transform.position.x - transform.position.x;
        if ((xDir < 0 && facingRight) || (xDir > 0 && !facingRight))
        {
            ChangeDirection();
        }

    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }



    private void setMovingPath()
    {
        enemiesPathsGameObject = GameObject.Find("EnemiesMovementPath");
        myPathGameObject = enemiesPathsGameObject.transform.Find(gameObject.name).gameObject;
        if (myPathGameObject.transform.childCount > 1)
        {
            pathSpots = new List<GameObject>();
            for (int i = 0; i < myPathGameObject.transform.childCount; i++)
            {
                pathSpots.Add(myPathGameObject.transform.GetChild(i).gameObject);
            }
            movingNextSpot = 1;
        }
        transform.position = pathSpots[0].transform.position;
        movingDirection = (pathSpots[movingNextSpot].transform.position - pathSpots[movingCurSpot].transform.position).normalized;
    }


    public void Move()
    {
        Debug.Log("move enemy");
        if (colorEnabled)
        {
            if (pathSpots.Count > 1)
            {
                if (transform.position == pathSpots[movingNextSpot].transform.position)
                {
                    movingCurSpot = movingNextSpot;
                    if (movingCurSpot == pathSpots.Count - 1)
                    {
                        movingNextSpot = 0;
                    }
                    else
                    {
                        movingNextSpot++;
                    }
                    movingDirection = (pathSpots[movingNextSpot].transform.position - pathSpots[movingCurSpot].transform.position).normalized;

                }
            }


            transform.position = Vector2.MoveTowards(transform.position, pathSpots[movingNextSpot].transform.position, movementSpeed * Time.deltaTime);

        }
    }
}
