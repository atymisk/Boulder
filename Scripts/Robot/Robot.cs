using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Robot : MonoBehaviour {
    public enum CharacterState { Idle, Run, Hang, Blocking, BlockStun, LightFlinch, HeavyFlinch, LeftPunch, RightPunch, LeftKick, RightKick, Pickup };

    //Constants
    const int PartCount = 4;
    const int LeftArm = 0;
    const int RightArm = 1;
    const int LeftLeg = 2;
    const int RightLeg = 3;

    //Public members
	public int playerNum = 1;
    public string mytag = "null";
    public Part[] robotParts;
    public Rigidbody2D rigidbodyTwoD;
    public float maxHealth = 200f;
    public float currentHealth = 200f;
    public int currnumparts = 4;
	//public Text healthNum;
	public Slider healthBar;
	public Image Fill;
    public string prefabName;

	public GameObject buttonRefls;
	public GameObject charUI;


    //Private members
    private GameManager gm;
    private CharacterState currentState;
    private Animator anim;
    private bool isFacingLeft = false;
    private bool isGrounded = true;
    private bool triggered = false;
	private bool comboState = false;
	private bool drop = false;
	private Collider2D previousPlatform = null;
	private Transform partsHolder;
	private RobotHurtBox hurtBox;
    private PickupBox pickupBox;
    private IEnumerator moveTimeRoutine;
    private IEnumerator delayedJump;

	private Text healthNum;
	//private Slider healthBar;
	//private Image Fill;
	private Image LeftArmUI;
	private Image RightArmUI;
	private Image LeftLegUI;
	private Image RightLegUI;

    void Start ()
    {
        currentState = CharacterState.Idle;
        anim = gameObject.GetComponent<Animator>();
        rigidbodyTwoD = this.gameObject.GetComponent<Rigidbody2D>();
		hurtBox = this.transform.FindChild ("HurtBox").GetComponent<RobotHurtBox>();
        pickupBox = this.transform.FindChild("PickupBox").GetComponent<PickupBox>();
        gm = (GameManager)GameObject.Find("GameManager").GetComponent<GameManager>();

		healthNum = charUI.transform.Find ("healthText").GetComponent<Text> ();
		LeftArmUI = charUI.transform.Find ("leftArm").GetComponent<Image> ();
		RightArmUI = charUI.transform.Find ("RightArm").GetComponent<Image> ();
		LeftLegUI = charUI.transform.Find ("leftLeg").GetComponent<Image> ();
		RightLegUI = charUI.transform.Find ("RightLeg").GetComponent<Image> ();

        InitializeParts();

        if (this.transform.right.x < 0)
        {
            isFacingLeft = true;


        }
        else
        {
            isFacingLeft = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		healthBar.value = currentHealth / maxHealth * 100;
		healthNum.text = healthBar.value.ToString ();

		Color darkYellow = new Color (255, 241, 0);

		buttonRefls.transform.position = this.transform.position;
		
		if (healthBar.value <= 20) {
			Fill.color = Color.red;
		} else if (healthBar.value <= 50)
			Fill.color = Color.yellow;
		else 
			Fill.color = Color.green;

		if (!robotParts [LeftArm].active)
        {
			LeftArmUI.gameObject.SetActive (false);
			buttonRefls.transform.FindChild ("Left").GetComponent<Renderer> ().material.color = Color.gray;
		}
        else
        {
			buttonRefls.transform.FindChild ("Left").GetComponent<Renderer> ().material.color = Color.blue;
			LeftArmUI.gameObject.SetActive (true);
		}
		if (!robotParts [RightArm].active) {
			buttonRefls.transform.FindChild("Up").GetComponent<Renderer>().material.color = Color.gray;
			RightArmUI.gameObject.SetActive (false);
		}
		else {
			buttonRefls.transform.FindChild("Up").GetComponent<Renderer>().material.color = Color.yellow;
			RightArmUI.gameObject.SetActive (true);
		}

		if (!robotParts [LeftLeg].active) {
			LeftLegUI.gameObject.SetActive (false);
			buttonRefls.transform.FindChild("Down").GetComponent<Renderer>().material.color = Color.gray;
		}
		else {
			buttonRefls.transform.FindChild("Down").GetComponent<Renderer>().material.color = Color.green;
			LeftLegUI.gameObject.SetActive (true);
		}

		if (!robotParts [RightLeg].active) {
			RightLegUI.gameObject.SetActive (false);
			buttonRefls.transform.FindChild("Right").GetComponent<Renderer>().material.color = Color.gray;
		}
		else {
			buttonRefls.transform.FindChild("Right").GetComponent<Renderer>().material.color = Color.red;
			RightLegUI.gameObject.SetActive (true);
		}

	    if(currentHealth <= 0)
        {
			BreakRandomPart();
			BreakRandomPart();
			BreakRandomPart();
            gm.thisPlayerDied(mytag);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
	}

    private void InitializeParts()
    {
        Debug.Log("InitializeParts" + this);
        robotParts = new Part[PartCount];
		partsHolder = this.transform.FindChild("Parts");
		robotParts[LeftArm] = partsHolder.GetChild(LeftArm).GetComponent<Part>();
		robotParts[RightArm] = partsHolder.GetChild(RightArm).GetComponent<Part>();
		robotParts[LeftLeg] = partsHolder.GetChild(LeftLeg).GetComponent<Part>();
		robotParts[RightLeg] = partsHolder.GetChild(RightLeg).GetComponent<Part>();
    }

    //Attack Moves
    public void LeftPunch()
    {
		CharacterState thisMove = CharacterState.LeftPunch;

		if ( robotParts[LeftArm].active && ( !IsBusy() || CanComboMove(thisMove) ) )
        {
            robotParts[LeftArm].Attack();
			comboState = false;
			anim.SetTrigger(robotParts[LeftArm].GetTrigger());

            if(isGrounded)
            {
                rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
            }
           

			currentState = thisMove;
        }
    }

	public void RightPunch()
	{
		CharacterState thisMove = CharacterState.RightPunch;
		
		if ( robotParts[RightArm].active && ( !IsBusy() || CanComboMove(thisMove) ) )
		{
			robotParts[RightArm].Attack();
			comboState = false;
			anim.SetTrigger(robotParts[RightArm].GetTrigger());
			
			if(isGrounded)
			{
				rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
			}
			
			
			currentState = thisMove;
		}
	}

	public void RightPunchExplosion()
	{
		GameObject explosion = (GameObject)Resources.Load("Particles/RightPunchEx");
		GameObject punchExplosion = (GameObject)Instantiate(explosion, this.transform.Find("Pelvis").Find("Chest").Find("RightShoulder").Find("RightHand").position, Quaternion.identity);
        RightPunchExplosion exScript = punchExplosion.GetComponent<RightPunchExplosion>();
        exScript.SetOwner(this);
        if (isFacingLeft)
        {
            punchExplosion.transform.position = new Vector3((punchExplosion.transform.position.x - 8),
                                                        punchExplosion.transform.position.y - 3,
                                                        punchExplosion.transform.position.z);
        }
        else
        {
            punchExplosion.transform.position = new Vector3((punchExplosion.transform.position.x + 8),
                                                        punchExplosion.transform.position.y - 3,
                                                        punchExplosion.transform.position.z);
        }

        Destroy(punchExplosion, 5);
	}


    public void LeftKick()
    {
		CharacterState thisMove = CharacterState.LeftKick;

		if ( robotParts[LeftLeg].active && ( !IsBusy() || CanComboMove(thisMove) ) )
        {
            robotParts[LeftLeg].Attack();
			comboState = false;
			anim.SetTrigger(robotParts[LeftLeg].GetTrigger());
            rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);

			currentState = thisMove;
        }
    }

    public void RightKick()
    {
		CharacterState thisMove = CharacterState.RightKick;

		if ( robotParts[RightLeg].active && ( !IsBusy() || CanComboMove(thisMove) ) )
        {
            robotParts[RightLeg].Attack();
			comboState = false;
			anim.SetTrigger(robotParts[RightLeg].GetTrigger());
            rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);

			currentState = thisMove;
        }
    }

    public void Block()
    {
        if(( currentState == CharacterState.Blocking || !IsBusy() ) && isGrounded)
        {
            if(!IsBusy() && currentState != CharacterState.Blocking)
            {
                anim.SetTrigger("Block");

                currentState = CharacterState.Blocking;
            }

            rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
        }
    }

    public void UnBlock()
    {
        if(currentState == CharacterState.Blocking)
        {
            anim.SetTrigger("UnBlock");

            currentState = CharacterState.Idle;
        }
    }

	public void Pickup()
	{
		CharacterState thisMove = CharacterState.LeftKick;
		
		if (!IsBusy()) {
			anim.SetTrigger("Pickup");

			currentState = thisMove;
		}
	}

    public void OnPickup()
    {
		CharacterState thisMove = CharacterState.Pickup;

		PartPickup partToPickup = pickupBox.GetClosestPart();

		if(partToPickup != null)
		{
			int partIndex = partToPickup.GetIndex();
			Debug.Log(this + " partIndex: " + partIndex);
			if(!robotParts[partIndex].active)
			{	
				GameObject toAttach = Instantiate(partToPickup.GetAttachablePart()) as GameObject;
				Destroy(robotParts[partIndex].gameObject);
				toAttach.transform.SetParent(partsHolder);
				toAttach.transform.rotation = robotParts[partIndex].transform.rotation;
				toAttach.transform.position = robotParts[partIndex].transform.position;
				robotParts[partIndex] = toAttach.GetComponent<Part>();
				robotParts[partIndex].SetOwner(this);
				robotParts[partIndex].active = false;
				robotParts[partIndex].Attach();
				pickupBox.RemovePart(partToPickup);
                currnumparts++;
			}
		}
    }
    
	//UI rocket move reflection
	public void rocketPrepare()
	{
		buttonRefls.transform.FindChild ("Left").GetComponent<Renderer> ().transform.localScale = new Vector3 (1.2f, 1.2f, 1);
		buttonRefls.transform.FindChild ("Right").GetComponent<Renderer> ().transform.localScale = new Vector3 (1.2f, 1.2f, 1);
		buttonRefls.transform.FindChild ("Up").GetComponent<Renderer> ().transform.localScale = new Vector3 (1.2f, 1.2f, 1);
		buttonRefls.transform.FindChild ("Down").GetComponent<Renderer> ().transform.localScale = new Vector3 (1.2f, 1.2f, 1);
	}
	
	public void rocketUnPre()
	{
		buttonRefls.transform.FindChild ("Left").GetComponent<Renderer> ().transform.localScale = new Vector3 (0.75f, 0.75f, 1);
		buttonRefls.transform.FindChild ("Right").GetComponent<Renderer> ().transform.localScale = new Vector3 (0.75f, 0.75f, 1);
		buttonRefls.transform.FindChild ("Up").GetComponent<Renderer> ().transform.localScale = new Vector3 (0.75f, 0.75f, 1);
		buttonRefls.transform.FindChild ("Down").GetComponent<Renderer> ().transform.localScale = new Vector3 (0.75f, 0.75f, 1);
	}

    //Rocket Moves
    public void RocketLeftArm()
    {
        anim.SetTrigger("RocketLeftArm");
    }

    private void ShootPart(int index)
    {
        if(robotParts[index].active)
        {
            Debug.Log(robotParts[index] + "  |  " + robotParts[index].GetRocketPath());
			GameObject rocketPrefab = Instantiate(Resources.Load(robotParts[index].GetRocketPath())) as GameObject;

            RobotRocket rocket = rocketPrefab.GetComponent<RobotRocket>();
            rocket.SetOwner(this);
            Physics2D.IgnoreCollision(rocket.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            rocket.transform.position = this.transform.FindChild("Pelvis").FindChild("Chest").FindChild("LeftShoulder").transform.position;
            Rigidbody2D rocketBody = rocket.GetComponent<Rigidbody2D>();
            float direction = IsFacingLeft() ? -1 : 1;

            if (IsFacingLeft())
            {
                Debug.Log("rotate");
                rocket.transform.Rotate(new Vector3(0, 180, 0));
            }

            rocketBody.velocity = new Vector2(direction * 200, 0);

			Transform partToBreak = this.transform.GetChild(0).GetChild(2).GetChild(2);
			partToBreak.gameObject.SetActive(false);
			robotParts[index].active = false;
        }
    }

    public void ShootLeftArm()
    {
		if ( robotParts[LeftArm].active && !IsBusy() )
		{
			ShootPart(0);
		}

    }

	public void OnHitConnected()
	{
		comboState = true;
	}

    //Movement
    public void Jump()
    {
		if ((currentState == CharacterState.Idle || currentState == CharacterState.Run) && isGrounded)
        {
            //rigidbodyTwoD.velocity = new Vector2(rigidbodyTwoD.velocity.x, 100);

            if(delayedJump != null)
            {
                StopCoroutine(delayedJump);
            }
            
            anim.SetTrigger("JumpStart");
            delayedJump = DelayedJump(0.05f);
            StartCoroutine(delayedJump);
        }
		
		if ((currentState == CharacterState.Hang))
		{
			
			if(delayedJump != null)
			{
				StopCoroutine(delayedJump);
			}

			delayedJump = DelayedJump(0.0f);
			StartCoroutine(delayedJump);
		}
    }

	public void Drop()
	{
		drop = true;
	}
	
	public void UnDrop(){
		drop = false;
	}

    public void FaceLeft()
    {
		if ((currentState == CharacterState.Idle || currentState == CharacterState.Run) && !isFacingLeft)
        {
            this.transform.Rotate(new Vector3(0, 180, 0));
            isFacingLeft = true;
			DustEffect();
        }
    }

    public void FaceRight()
    {
		if ((currentState == CharacterState.Idle || currentState == CharacterState.Run) && isFacingLeft)
        {
            this.transform.Rotate(new Vector3(0, -180, 0));
            isFacingLeft = false;
			DustEffect();
        }
    }

    public void MoveLeft()
    {
		if (currentState == CharacterState.Idle || currentState == CharacterState.Run)
        {
            FaceLeft();
			if (isGrounded)
			{
                Debug.Log("RunningLeft");
                //anim.SetTrigger("Run");
                anim.SetBool("Running", true);
                currentState = CharacterState.Run;
			}
			else
			{
                //anim.SetTrigger("UnRun");
                anim.SetBool("Running", false);
                currentState = CharacterState.Idle;
			}
            rigidbodyTwoD.velocity = new Vector2(-50, rigidbodyTwoD.velocity.y);
        }
    }

    public void MoveRight()
    {
		if (currentState == CharacterState.Idle || currentState == CharacterState.Run)
        {
            FaceRight();
			if (isGrounded)
			{
                Debug.Log("Running");
                //anim.SetTrigger("Run");
                anim.SetBool("Running", true);
				currentState = CharacterState.Run;
			}
			else
			{
				//anim.SetTrigger("UnRun");
                anim.SetBool("Running", false);
                currentState = CharacterState.Idle;
			}
            rigidbodyTwoD.velocity = new Vector2(50, rigidbodyTwoD.velocity.y);
        }
    }

    public void StayStill()
    {
		if (currentState == CharacterState.Run)
        {
            Debug.Log("Unrunning");
            //anim.SetTrigger("UnRun");
            anim.SetBool("Running", false);
            currentState = CharacterState.Idle;
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
            GameObject dust = (GameObject)Resources.Load("Particles/Dust");
            var dustCloneLeft = Instantiate(dust, this.transform.Find("Pelvis").Find("LeftUpperLeg").Find("LeftLowerLeg").Find("LeftFoot").position, Quaternion.identity);
            var dustCloneRight = Instantiate(dust, this.transform.Find("Pelvis").Find("RightUpperLeg").Find("RightLowerLeg").Find("RightFoot").position, Quaternion.identity);

            Destroy(dustCloneLeft, dust.GetComponent<ParticleSystem>().startLifetime);
            Destroy(dustCloneRight, dust.GetComponent<ParticleSystem>().startLifetime); 
            
        }
    }

    public void HeavyHitStun(float damage, Vector2 pushVelocity, float duration)
    {
		comboState = false;
        currentHealth = currentHealth - damage;
        //healthBar.value = currentHealth / maxHealth * 100;
        anim.SetTrigger("HeavyHit");
        CancelAttacks();

        currentState = CharacterState.HeavyFlinch;

        if (moveTimeRoutine != null)
        {
            StopCoroutine(moveTimeRoutine);
        }

		if(currentHealth / maxHealth < 0.5)
		{
			System.Random rnd = new System.Random();
			int randNum = rnd.Next();
			Debug.Log("randNum " + randNum);
			if(randNum % 3 == 0)
			{
				BreakRandomPart();
			}
		}

        moveTimeRoutine = MoveOverTime(pushVelocity, duration);
        StartCoroutine(moveTimeRoutine);
    }

    public void BlockStun(Vector2 pushVelocity, float duration)
    {
        anim.SetTrigger("BlockStun");

        currentState = CharacterState.BlockStun;

        Debug.Log("BlockStun");
        if (moveTimeRoutine != null)
        {
            StopCoroutine(moveTimeRoutine);
        }

        moveTimeRoutine = MoveOverTime(pushVelocity, duration);
        StartCoroutine(moveTimeRoutine);
    }

    public void EnableHitBox(int limbIndex)
    {
        robotParts[limbIndex].EnableHitBox();
    }

    public void DisableHitBox(int limbIndex)
    {
        robotParts[limbIndex].DisableHitBox();
    }

	public void EnableHurtBox()
	{
		hurtBox.EnableHurtBox();
	}

	public void DisableHurtBox()
	{
		hurtBox.DisableHurtBox();
	}

    public void OnMoveOrFlinchEnd()
    {
		comboState = false;
        currentState = CharacterState.Idle;

        //CancelAttacks();
        anim.SetBool("Running", false);
        Debug.Log("finished " + this + " "+ currentState);

        if(delayedJump != null)
        {
            StopCoroutine(delayedJump);
        }
    }

    public void OnBlockStunEnd()
    {
        currentState = CharacterState.Blocking;
        CancelAttacks();
    }

    public bool IsBusy()
    {
		return currentState != CharacterState.Idle && currentState != CharacterState.Run && currentState != CharacterState.Blocking;
    }

	public bool CanComboMove(CharacterState nextMove)
	{
		bool canComboMove = false;

		if(   (currentState == CharacterState.LeftPunch)
		   || (currentState == CharacterState.LeftKick  && nextMove != CharacterState.LeftKick)
		   || (currentState == CharacterState.RightKick && nextMove == CharacterState.LeftKick))
		{
			canComboMove = true;
		}

		return canComboMove && comboState;
	}

    public void CancelAttacks()
    {
        robotParts[LeftArm].CancelAttack();
        robotParts[LeftLeg].CancelAttack();
        robotParts[RightLeg].CancelAttack();
    }

    public CharacterState GetCurrentState()
    {
        return currentState;
    }

	public void BreakRandomPart()
	{
        if(currnumparts <= 0)
        {
            return;
        }
        currnumparts--;
		List<int> breakIndexList = new List<int>();

		for(int i = 0; i < 4; i++)
		{
			if(robotParts[i] != null && robotParts[i].active)
			{
				breakIndexList.Add(i);
			}
		}

		if(breakIndexList.Count > 0)
		{
			System.Random rnd = new System.Random();
			int j = rnd.Next() % breakIndexList.Count;

			if(robotParts[breakIndexList[j]] != null)
			{
				robotParts[breakIndexList[j]].active = false;
			}

			GameObject pickObj = null;
			Transform pelvis = this.transform.GetChild(0);
			Transform partToBreak = null;
			
			if(breakIndexList[j] == LeftArm)
			{
				if(playerNum == 1)
				{
					pickObj = Instantiate(Resources.Load("ItemParts/TigerLeftHandPickup")) as GameObject;
				}
				else
				{
					pickObj = Instantiate(Resources.Load("ItemParts/BunnyLeftHandPickup")) as GameObject;
				}
				partToBreak = pelvis.GetChild(2).GetChild(2);
			}
            else if (breakIndexList[j] == RightArm)
            {
                if (playerNum == 1)
                {
                    pickObj = Instantiate(Resources.Load("ItemParts/TigerRightHandPickup")) as GameObject;
                }
                else
                {
                    pickObj = Instantiate(Resources.Load("ItemParts/BunnyRightHandPickup")) as GameObject;
                }

                partToBreak = pelvis.GetChild(2).GetChild(1);
            }
            else if(breakIndexList[j] == LeftLeg)
			{
				if(playerNum == 1)
				{
					pickObj = Instantiate(Resources.Load("ItemParts/TigerLeftLegPickup")) as GameObject;
				}
				else
				{
					pickObj = Instantiate(Resources.Load("ItemParts/BunnyLeftLegPickup")) as GameObject;
				}

				partToBreak = pelvis.GetChild(1);
			}
			else if(breakIndexList[j] == RightLeg)
			{
				if(playerNum == 1)
				{
					pickObj = Instantiate(Resources.Load("ItemParts/TigerRightLegPickup")) as GameObject;
				}
				else
				{
					pickObj = Instantiate(Resources.Load("ItemParts/BunnyRightLegPickup")) as GameObject;
				}

				partToBreak = pelvis.GetChild(0);
			}
		
			if(pickObj != null)
			{
				pickObj.transform.position = this.transform.position;
				PartPickup pickup = pickObj.GetComponent<PartPickup>();
				pickup.SpinBounce(1);
				partToBreak.gameObject.SetActive(false);
			}

		}



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

	void OnCollisionStay2D(Collision2D col){
//		Debug.Log (drop);
		if (previousPlatform != null) {
			Physics2D.IgnoreCollision(previousPlatform, GetComponent<Collider2D>(), drop);
		}

		if (col.gameObject.tag == "Platform")
		{
			Collider2D collider = col.collider;
			previousPlatform = collider;
			Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>(), drop);
		}
	}

    public string getTag()
    {
        return mytag;
    }

    public void setTag(string tag)
    {
        mytag = tag;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.gameObject.name == "DeathArea")
        {
            triggered = true;
            gm.thisPlayerDied(mytag);
        }

		if ((other.gameObject.name == "HangAreaLeft" && isFacingLeft == false) || (other.gameObject.name == "HangAreaRight" && isFacingLeft == true)) 
		{
            if(!IsBusy() && !isGrounded)
            {
                //anim.SetTrigger("Hang");
                anim.SetBool("Hanging", true);
                currentState = CharacterState.Hang;
                this.transform.position = other.transform.position;
                rigidbodyTwoD.gravityScale = 0;
                rigidbodyTwoD.velocity = Vector2.zero;
            }
		}

        

    }

    void OnTriggerExit2D(Collider2D other)
    {
		if (other.gameObject.name == "HangAreaLeft" || other.gameObject.name == "HangAreaRight") 
		{
			rigidbodyTwoD.gravityScale = 20;
            //anim.SetTrigger ("UnHang");
            anim.SetBool("Hanging", false);
			currentState = CharacterState.Idle;
		}

        if (!triggered)
        {
            return;
        }
        triggered = false;
    }

    //Coroutines
    IEnumerator MoveOverTime(Vector2 velocity, float duration)
    {
        yield return new WaitForFixedUpdate();

        float currentTime = 0;
        while (currentTime < duration)
        {
            float xDisplacement = velocity.x * Time.fixedDeltaTime;
			float yDisplacement = velocity.y * Time.fixedDeltaTime;
            float zPosition = this.transform.position.z;

            Vector3 displacement = new Vector3(xDisplacement, yDisplacement, zPosition);
            rigidbodyTwoD.MovePosition(this.transform.position + displacement);

			currentTime = currentTime + Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator DelayedJump(float duration)
    {
        yield return new WaitForSeconds(duration);

        rigidbodyTwoD.AddForce(new Vector2(0, 100), ForceMode2D.Impulse);
        isGrounded = false;
    }
}
