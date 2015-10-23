using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour 
{
	public Transform spawnPoint;
	public Rigidbody2D rigidbodyTwoD;

	public float health = 100;

	private bool isFacingLeft = true;
    private bool isGrounded = true;

	protected float hitPoints = 100;
	protected MoveEventHandler moveHandler;

	protected void Initialize(float hitPoints)
	{
		this.hitPoints = hitPoints;
		moveHandler = this.gameObject.GetComponent<MoveEventHandler>();

		//This is backwards since our prefabis facing left by default
		if(this.transform.right.x > 0)
		{
			isFacingLeft = true;
		}
		else
		{
			isFacingLeft = false;
		}

		this.rigidbodyTwoD = this.gameObject.GetComponent<Rigidbody2D>();
	}

	virtual public void NormalMoveAlpha()
	{
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
	
	virtual public void HeavyHitStun(Vector2 pushVelocity)
	{
		health = health - 5;
		moveHandler.OnHeavyHitStart();
        StartCoroutine(MoveDuringState(pushVelocity, MoveEventHandler.CharacterState.HeavyFlinch));
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
		return hitPoints;
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
            Debug.Log("exit " + normal);
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
        yield return new WaitForEndOfFrame();

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
    }
}
