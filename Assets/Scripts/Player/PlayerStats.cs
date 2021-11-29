using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStats 
{
    private Player player;
    public void start(Player player)
    {
        this.player = player;
        downMovementSpeed = -downMovementSpeed;
        fallMovementSpeed = -fallMovementSpeed;
        currentDownMovementSpeed = downMovementSpeed;
        currentHorizontalSpeed = movementSpeed;
        respawnPosition = player.gameObject.transform.position;
    }


    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float downMovementSpeed;
    [SerializeField]
    private float fallMovementSpeed;
    [SerializeField]
    private float dashCD;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float bounceSpeed;
    private bool dashing;
    private float currentHorizontalSpeed;
    private float currentDownMovementSpeed;
    private Vector3 lastVelocity;
    private bool movingRight = true;
    private bool isBouncing = false;
    private float startBounceY;
    private float startBounceX;
    private GameObject currentBounceGameObject;
    private bool canDash = true;//cooldown
    private Vector3 respawnPosition;
    private float lastMovingVelocityX;



    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool MovingRight { get => movingRight; set => movingRight = value; }
    public float DownMovementSpeed { get => downMovementSpeed; set => downMovementSpeed = value; }
    public float FallMovementSpeed { get => fallMovementSpeed; set => fallMovementSpeed = value; }
    public float CurrentDownMovementSpeed { get => currentDownMovementSpeed; set => currentDownMovementSpeed = value; }
    public Vector3 LastVelocity { get => lastVelocity; set => lastVelocity = value; }
    public bool IsBouncing { get => isBouncing; set => isBouncing = value; }
    public float StartBounceY { get => startBounceY; set => startBounceY = value; }
    public GameObject CurrentBounceGameObject { get => currentBounceGameObject; set => currentBounceGameObject = value; }
    public float DashCD { get => dashCD; set => dashCD = value; }
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float DashDuration { get => dashDuration; set => dashDuration = value; }
    public float CurrentHorizontalSpeed { get => currentHorizontalSpeed; set => currentHorizontalSpeed = value; }
    public bool CanDash { get => canDash; set => canDash = value; }
    public bool Dashing { get => dashing; set => dashing = value; }
    public Vector3 RespawnPosition { get => respawnPosition; set => respawnPosition = value; }
    public float StartBounceX { get => startBounceX; set => startBounceX = value; }
    public float LastMovingVelocityX { get => lastMovingVelocityX; set => lastMovingVelocityX = value; }
    public float BounceSpeed { get => bounceSpeed; set => bounceSpeed = value; }
}
