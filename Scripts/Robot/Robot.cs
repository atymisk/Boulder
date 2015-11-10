using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {
    public enum CharacterState { Idle, LightFlinch, HeavyFlinch, LeftPunch, RightPunch, LeftKick, RightKick };

    //Constants
    const int PartCount = 4;
    const int LeftArm = 0;
    const int RightArm = 1;
    const int LeftLeg = 2;
    const int RightLeg = 3;

    //Public members
    public Part[] robotParts;
    public Rigidbody2D rigidbodyTwoD;

    //Private members
    private CharacterState currentState;
    private Animator anim;
    private bool isFacingLeft = false;
    private bool isGrounded = true;
    private IEnumerator moveTimeRoutine;

    void Start ()
    {
        currentState = CharacterState.Idle;
        anim = gameObject.GetComponent<Animator>();
        rigidbodyTwoD = this.gameObject.GetComponent<Rigidbody2D>();
        InitializeParts();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void InitializeParts()
    {
        robotParts = new Part[PartCount];
        Transform partsObj = this.transform.FindChild("Parts");
        robotParts[LeftArm] = partsObj.GetChild(LeftArm).GetComponent<Part>();
        robotParts[RightLeg] = partsObj.GetChild(RightLeg).GetComponent<Part>();
    }

    //Attack Moves
    public void LeftPunch()
    {
        if (!IsBusy())
        {
            currentState = CharacterState.LeftPunch;
            robotParts[LeftArm].Attack();
            anim.SetTrigger(robotParts[LeftArm].GetTrigger());
        }
    }

    public void RightKick()
    {
        if (!IsBusy())
        {
            Debug.Log("right kick");
            robotParts[RightLeg].Attack();
            anim.SetTrigger(robotParts[RightLeg].GetTrigger());

            currentState = CharacterState.RightKick;
        }
    }

    //Movement
    public void Jump()
    {
        if (!IsBusy() && this.isGrounded)
        {
            rigidbodyTwoD.velocity = new Vector2(rigidbodyTwoD.velocity.x, 100);
        }
    }

    public void FaceLeft()
    {
        if (!isFacingLeft && !IsBusy())
        {
            this.transform.Rotate(new Vector3(0, 180, 0));
            isFacingLeft = true;
        }
    }

    public void FaceRight()
    {
        if (isFacingLeft && !IsBusy())
        {
            this.transform.Rotate(new Vector3(0, -180, 0));
            isFacingLeft = false;
        }
    }

    public void MoveLeft()
    {
        if (!IsBusy())
        {
            FaceLeft();
            rigidbodyTwoD.velocity = new Vector2(-50, rigidbodyTwoD.velocity.y);
            DustEffect();
        }
    }

    public void MoveRight()
    {
        if (!IsBusy())
        {
            FaceRight();
            rigidbodyTwoD.velocity = new Vector2(50, rigidbodyTwoD.velocity.y);
            DustEffect();
        }
    }

    public void StayStill()
    {
        if (!IsBusy())
        {
            rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
        }
    }
    public bool IsFacingLeft()
    {
        return isFacingLeft;
    }

    private void DustEffect()
    {
        if (isGrounded)
        {
            /*
            GameObject dust = (GameObject)Resources.Load("Particles/Dust");
            var dustCloneLeft = Instantiate(dust, this.transform.Find("torso").Find("upperLeftLeg").Find("lowerLeftLeg").Find("leftFoot").position, Quaternion.identity);
            var dustCloneRight = Instantiate(dust, this.transform.Find("torso").Find("upperRightLeg").Find("lowerRightLeg").Find("rightFoot").position, Quaternion.identity);

            Destroy(dustCloneLeft, dust.GetComponent<ParticleSystem>().startLifetime);
            Destroy(dustCloneRight, dust.GetComponent<ParticleSystem>().startLifetime); 
            */
        }
    }

    public void HeavyHitStun(float damage, Vector2 pushVelocity, float duration)
    {
        //health = health - damage;
        //healthBar.value = health / maxHealth * 100;
        currentState = CharacterState.HeavyFlinch;
        anim.SetTrigger("OnTriggerHeavyHit");
        CancelAttacks();

        moveTimeRoutine = MoveOverTime(pushVelocity, duration);
        StartCoroutine(moveTimeRoutine);
    }

    public void EnableHitBox(int limbIndex)
    {
        robotParts[limbIndex].EnableHitBox();
    }

    public void OnMoveOrFlinchEnd()
    {
        currentState = CharacterState.Idle;
        CancelAttacks();
    }

    public bool IsBusy()
    {
        return currentState != CharacterState.Idle;
    }

    public void CancelAttacks()
    {
        robotParts[LeftArm].CancelAttack();
        robotParts[RightLeg].CancelAttack();
    }

    public CharacterState GetCurrentState()
    {
        return currentState;
    }

    //Collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
        {
            Collider2D collider = col.collider;

            Vector2 normal = col.contacts[0].normal;

            if (normal.y == 1)
            {
                isGrounded = true;
                DustEffect();
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

    //Coroutines
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
