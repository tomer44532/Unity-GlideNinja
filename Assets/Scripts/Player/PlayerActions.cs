using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions 
{
    private Player player;

    public PlayerActions(Player player)
    {
        this.player = player;

    }


    public void Move()
    {
        checkFinishedBounce();
        Rigidbody2D rigidbody = player.Components.Rigidbody;
        int direction;
        if (player.Stats.MovingRight)
        {
            direction = 1;

        }
        else
        {
            direction = -1;
        }
        //if(Mathf.Abs(player.Stats.StartBounceY - player.gameObject.transform.position.y) > player.Stats.CurrentBounceGameObject.GetComponent<BounceProperties>().MaxBounceHeight)
        //{
        //    player.Stats.IsBouncing = false;
        //}
        //if (!player.Stats.IsBouncing)
        //{
        //}
        //else
        //{
        //    //Debug.Log("bouncing");

        //}
        rigidbody.velocity = new Vector2(direction * player.Stats.CurrentHorizontalSpeed, player.Stats.CurrentDownMovementSpeed);

        player.Stats.LastVelocity = rigidbody.velocity;
        //Debug.Log(player.Stats.DownMovementSpeed);
        if ((rigidbody.velocity.x > 0 && player.Stats.LastMovingVelocityX < 0) || (rigidbody.velocity.x < 0 && player.Stats.LastMovingVelocityX > 0))
        {
            Flip();
        }
        if (rigidbody.velocity.x != 0)
        {
            player.Stats.LastMovingVelocityX = rigidbody.velocity.x;
        }
       

    }

    public void checkFinishedBounce()
    {
        PlayerStats stats = player.Stats;
        if (player.Stats.IsBouncing)
        {
            float curY = Mathf.Abs(stats.StartBounceY - player.gameObject.transform.position.y);
            float maxY = stats.CurrentBounceGameObject.GetComponent<BounceProperties>().MaxBounceHeight;
            float curX = Mathf.Abs(stats.StartBounceX - player.gameObject.transform.position.x);
            float maxX = stats.CurrentBounceGameObject.GetComponent<BounceProperties>().MaxBounceHeight;
            if (stats.Dashing)
            {
                maxX = maxX * (stats.DashSpeed / stats.MovementSpeed);
            }

            if ((curY > maxY || curX > maxX))//Mathf.Abs(player.Stats.StartBounceY - player.gameObject.transform.position.y) > player.Stats.CurrentBounceGameObject.GetComponent<BounceProperties>().MaxBounceHeight
            {
                StopBounce(stats);
            }
        }

    }

    private void StopBounce(PlayerStats stats)
    {
        stats.MovingRight = player.Components.MapManager.DirectionRight;
        player.Components.Rigidbody.velocity = new Vector2(player.Components.Rigidbody.velocity.x, 0);
        stats.CurrentDownMovementSpeed = stats.DownMovementSpeed;
        stats.IsBouncing = false;
    }

    public void StartFall()
    {
        Rigidbody2D rigidbody = player.Components.Rigidbody;
        if (rigidbody.velocity.y <= 0 && !player.Stats.IsBouncing)
        {
            rigidbody.velocity = Vector2.zero;
            player.Stats.CurrentDownMovementSpeed = player.Stats.FallMovementSpeed;
            //rigidbody.gravityScale = 5f;
        }
    }

    public void StopFall()
    {
        Rigidbody2D rigidbody = player.Components.Rigidbody;
        if (rigidbody.velocity.y <= 0)
        {
            //rigidbody.gravityScale = 0.1f;
            rigidbody.velocity = Vector2.zero;
            player.Stats.CurrentDownMovementSpeed = player.Stats.DownMovementSpeed;

        }
    }


    public void bounce(Collision2D col)
    {
        Debug.Log("isbouncing " + player.Stats.IsBouncing);
        //Debug.Log(player.Stats.IsBouncing);
        float jumpValue = 1;
        if(col.gameObject.name == "floor")
        {
            jumpValue = 12;
        }
        Vector3 velocity = player.Stats.LastVelocity;
        float speed = velocity.magnitude;
        Vector3 direction = Vector3.Reflect(velocity.normalized, col.contacts[0].normal);
        Vector3 newVelocity = direction * Mathf.Max(speed, 0f);
        Debug.Log("vector " + velocity + " new vector " + newVelocity);
        if(newVelocity.x < 0)
        {
            player.Stats.MovingRight = false;
        }
        else
        {
            player.Stats.MovingRight = true;
        }
        //if (col.gameObject.name == "floor")
        //{
        //    newVelocity.y = jumpValue;
        //}
        int verticalDir = 1;
        if(newVelocity.y < 0)
        {
            verticalDir = -1;
        }
        player.Stats.IsBouncing = true;
        player.Stats.StartBounceY = player.gameObject.transform.position.y;
        player.Stats.StartBounceX = player.gameObject.transform.position.x;
        player.Stats.CurrentDownMovementSpeed = player.Stats.BounceSpeed * verticalDir;
        player.Stats.CurrentBounceGameObject = col.gameObject;
        Debug.Log("last bounce " + col.gameObject);
        player.Components.SoundManager.PlaySfxMusic("bounce");
    }


    public void dash(bool dashRight)
    {
        PlayerStats stats = player.Stats;
        if (stats.CanDash && !stats.IsBouncing)// 
        {
            player.Components.UiManager.DashCastingBar();
            stats.MovingRight = dashRight;
            stats.CurrentHorizontalSpeed = player.Stats.DashSpeed;
            stats.CanDash = false;
            stats.Dashing = true;
            player.StartCoroutine(DashCooldown(stats.DashCD));
            player.StartCoroutine(finishDash(stats.DashDuration));
            player.Components.SoundManager.PlaySfxMusic("dash");

        }

    }


    private IEnumerator DashCooldown(float dashCD)
    {
        yield return new WaitForSeconds(dashCD);
        Debug.Log("finished dash");
        player.Stats.CanDash = true;
    }

    private IEnumerator finishDash(float dashDuration)
    {
        yield return new WaitForSeconds(dashDuration);
        PlayerStats stats = player.Stats;
        if (stats.Dashing)
        {
            stats.CurrentHorizontalSpeed = stats.MovementSpeed;
            stats.MovingRight = player.Components.MapManager.DirectionRight;
            stats.Dashing = false;
        }

    }


    public void handleAnimation()//no fall animation yet
    {
        if (player.Stats.Dashing)
        {
            player.Components.Animator.SetBool("dash", true);
            return;
        }
        if (player.Components.Animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            player.Components.Animator.SetBool("dash", false);
            return;
        }
        if(player.Components.Rigidbody.velocity.y > 0)
        {
            player.Components.Animator.SetBool("jump", true);
            return;
        }
        if (player.Components.Rigidbody.velocity.y < 0)
        {
            player.Components.Animator.SetBool("jump", false);
            return;
        }
    }


    public void Death()
    {
        player.Components.SoundManager.StopBgmMusic(player.Components.SoundManager.CurrentBgm);
        if (!player.Components.MapManager.IsTutorial)
        {
        }
        player.Components.SoundManager.PlaySfxMusic("gameover");
        player.StartCoroutine(DeathFreeze());

    }

    public void Respawn()
    {
        Debug.Log("tutorial death");
        player.Components.SoundManager.PlayBgmMusic(player.Components.SoundManager.CurrentBgm);
        player.Components.Rigidbody.velocity = new Vector2(0, 0);//player.Components.Rigidbody.velocity.y
        StopBounce(player.Stats);
        player.Components.MapManager.Respawn();
        ResetDash();
        player.Components.MapManager.ResetBoss();
    }
    
    public void ResetDash()
    {
        PlayerStats stats = player.Stats;
        stats.CurrentHorizontalSpeed = player.Stats.MovementSpeed;
        stats.Dashing = false;
        stats.CanDash = true;
        player.Components.UiManager.ResetDashUI();
        
    }


    public void Flip()
    {

        player.transform.localScale = new Vector3(player.transform.localScale.x * -1, 1, 1);
    }

    public IEnumerator DeathFreeze()
    {
        float pauseTime = 1.5f;
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Respawn();
    }
}
