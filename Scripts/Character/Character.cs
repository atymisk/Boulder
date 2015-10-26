﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Character : MonoBehaviour 
{
    public Slider healthBar;
    public Transform spawnPoint;
	public Rigidbody2D rigidbodyTwoD;
    public GameObject rocketPunch;
    public float maxHealth = 100;

    private float health = 0;
	private bool isFacingLeft = true;
    private bool isGrounded = true;
    private bool isMovingInState = false;
    private IEnumerator moveTimeRoutine;

    protected MoveEventHandler moveHandler;

    Transform rightArm;

    protected void Initialize()
	{
        health = maxHealth;
		moveHandler = this.gameObject.GetComponent<MoveEventHandler>();
        rigidbodyTwoD = this.gameObject.GetComponent<Rigidbody2D>();
        //rocketPunch = GameObject.Find("RocketPunch");
        rightArm = this.gameObject.transform.FindChild("torso").FindChild("upperRightArm");

        //This is backwards since our prefabis facing left by default
        if (this.transform.right.x > 0)
		{
			isFacingLeft = true;
		}
		else
		{
			isFacingLeft = false;
		}
	}

	virtual public void NormalMoveAlpha()
	{
        if(rightArm.gameObject.activeSelf)
		    moveHandler.OnNormalAlphaStart();
	}
	
	virtual public void NormalMoveBeta()
	{
		moveHandler.OnNormalBetaStart();
	}

	virtual public void SpecialMoveAlpha()
	{
		moveHandler.OnSpecialAlphaStart();
	}

	virtual public void LightHitStun()
	{
		moveHandler.OnLightHitStart();
	}
	
	virtual public void HeavyHitStun(float damage, Vector2 pushVelocity, float duration)
	{
		health = health - damage;
        healthBar.value = health / maxHealth * 100;
		moveHandler.OnHeavyHitStart();
        if(moveTimeRoutine != null)
        {
            StopCoroutine(moveTimeRoutine);
        }

        moveTimeRoutine = MoveOverTime(pushVelocity, duration);
        StartCoroutine(moveTimeRoutine);
    }

    public void RocketPunch()
    {
        moveHandler.OnNormalDethaStart();
    }

    public void ShootPart()
    {
        if(rightArm.gameObject.activeSelf)
        {
            //Cheesy implementation to be refactored for different parts
            rocketPunch.GetComponent<RocketPart>().owner = this;
            rocketPunch.transform.position = this.transform.position;
            Rigidbody2D rocketBody = rocketPunch.GetComponent<Rigidbody2D>();
            float direction = IsFacingLeft() ? -1 : 1;
            rocketBody.velocity = new Vector2(direction * 200, 0);
            this.rightArm.gameObject.SetActive(false);
        }
    }

	public void Jump()
	{
		if(!moveHandler.IsBusy() && this.isGrounded)
		{
            rigidbodyTwoD.velocity = new Vector2(rigidbodyTwoD.velocity.x, 100);
        }
	}

	public void FaceLeft()
	{
		if(!isFacingLeft && !moveHandler.IsBusy())
		{
			this.transform.Rotate(new Vector3(0,180,0));
			isFacingLeft = true;
		}
	}

	public void FaceRight()
	{
		if(isFacingLeft && !moveHandler.IsBusy())
		{
			this.transform.Rotate(new Vector3(0,-180,0));
			isFacingLeft = false;
		}
	}

    public void MoveLeft()
    {
        if (!moveHandler.IsBusy())
        {
            rigidbodyTwoD.velocity = new Vector2(-50, rigidbodyTwoD.velocity.y);
        }
    }

    public void MoveRight()
    {
        if(!moveHandler.IsBusy())
        {
            rigidbodyTwoD.velocity = new Vector2(50, rigidbodyTwoD.velocity.y);
        }
    }

    public void StayStill()
    {
        if(!moveHandler.IsBusy())
        {
            rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
        }
    }

	public bool IsFacingLeft()
	{
		return isFacingLeft;
	}

	public float GetHitPoints()
	{
		return health;
	}

    //Collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
        {
            Collider2D collider = col.collider;

            Vector2 normal = col.contacts[0].normal;

            if(normal.y == 1)
            {
                isGrounded = true;
            }
        }
       
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
        {
            Collider2D collider = col.collider;

            Vector2 normal = col.contacts[0].normal;
            if (normal.y == 1)
            {
                isGrounded = false;
            }
        }
    }
    

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "DeathArea") {
            moveHandler.OnForceIdle();
            this.moveHandler.enabled = false;
			this.gameObject.SetActive(false);
			transform.position = spawnPoint.position;
            this.moveHandler.enabled = true;
            this.gameObject.SetActive(true);
		}
	}

    //CoRoutines
    IEnumerator MoveDuringState(Vector2 speed, MoveEventHandler.CharacterState state)
    {
        //yield return new WaitForEndOfFrame();
        isMovingInState = true;
        float currentTime = 0;
        while (state == moveHandler.GetCurrentState())
        {
            currentTime = currentTime + Time.deltaTime;
            float xDisplacement = speed.x * Time.deltaTime;
            float yDisplacement = speed.y * Time.deltaTime;
            this.transform.position = new Vector3(xDisplacement + this.transform.position.x,
                                                  yDisplacement + this.transform.position.y,
                                                  this.transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        isMovingInState = false;
    }

    IEnumerator MoveOverTime(Vector2 speed, float duration)
    {
        yield return new WaitForEndOfFrame();

        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime = currentTime + Time.deltaTime;
            float xDisplacement = speed.x * Time.deltaTime;
            float yDisplacement = speed.y * Time.deltaTime;
            this.transform.position = new Vector3(xDisplacement + this.transform.position.x,
                                                  yDisplacement + this.transform.position.y,
                                                  this.transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
