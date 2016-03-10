using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Robot : MonoBehaviour {
    public enum CharacterState { Idle, Run, Hang, Blocking, BlockStun, LightFlinch, HeavyFlinch, LeftPunch, RightPunch, LeftKick, RightKick, 
								 Pickup, Airborne, Jumping, Rocketing, HeadButt };

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
    public float maxHealth = 120f;
    public float currentHealth = 120f;
    public int currnumparts = 4;
	//public Text healthNum;
	public Slider healthBar;
	public Image Fill;
    public string prefabName;

	public GameObject buttonRefls;
	public GameObject charUI;
	public GameObject hangSpot;
	public int toturial;


    //Private members
    private GameManager gm;
    private CharacterState currentState;
    private Animator anim;
    private bool isFacingLeft = false;
    private bool isGrounded = true;
    private bool triggered = false;
	private bool comboState = false;
	private bool drop = false;
	private List<Collider2D> previousPlatform = new List<Collider2D>();
	private Transform partsHolder;
	private RobotHurtBox hurtBox;
    private PickupBox pickupBox;
	private BoxCollider2D headHitBox;
    private IEnumerator moveTimeRoutine;
    private IEnumerator delayedJump;
	private IEnumerator hangBlock;
	private GameObject shieldParticle;

	//private Text healthNum;
	//private Slider healthBar;
	//private Image Fill;
	private Image LeftArmUI;
	private Image RightArmUI;
	private Image LeftLegUI;
	private Image RightLegUI;

    public void setCharUI()
    {
        if(charUI == null)
            return;
        LeftArmUI = charUI.transform.Find("leftArm").GetComponent<Image>();
        RightArmUI = charUI.transform.Find("RightArm").GetComponent<Image>();
        LeftLegUI = charUI.transform.Find("leftLeg").GetComponent<Image>();
        RightLegUI = charUI.transform.Find("RightLeg").GetComponent<Image>();
    }

    void Start ()
    {
        currentState = CharacterState.Idle;
        anim = gameObject.GetComponent<Animator>();
        rigidbodyTwoD = this.gameObject.GetComponent<Rigidbody2D>();
		hurtBox = this.transform.FindChild ("HurtBox").GetComponent<RobotHurtBox>();
        pickupBox = this.transform.FindChild("PickupBox").GetComponent<PickupBox>();
		headHitBox = this.transform.FindChild("HeadHitBox").GetComponent<BoxCollider2D>();
		shieldParticle = this.transform.FindChild ("Pelvis").FindChild ("Shield").gameObject;
		if(toturial == 0)
			gm = (GameManager)GameObject.Find("GameManager").GetComponent<GameManager>();

        //healthNum = charUI.transform.Find ("healthText").GetComponent<Text> ();
        setCharUI();

        InitializeParts();
		this.transform.FindChild ("Pelvis").FindChild ("ThrusterEffects").gameObject.SetActive(false);
		this.transform.FindChild ("Pelvis").FindChild ("Shield").gameObject.SetActive(false);

        if (this.transform.right.x < 0)
        {
            isFacingLeft = true;
        }
        else
        {
            isFacingLeft = false;
        }
		InvokeRepeating ("updateNoParts", 0, 1f);
    }

    public void updateHP()
    {
        healthBar.value = currentHealth / maxHealth * 100;
        buttonRefls.transform.position = this.transform.position;

        if (healthBar.value <= 20)
            Fill.color = Color.red;
        else if (healthBar.value <= 50)
            Fill.color = Color.yellow;
        else
            Fill.color = Color.green;
    }

    public void updatePartsUI()
    {
        if (!robotParts[LeftArm].active)
        {
            LeftArmUI.gameObject.SetActive(false);
            charUI.transform.Find("leftArmBack").GetComponent<Image>().gameObject.SetActive(false);
            buttonRefls.transform.FindChild("Left").GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            buttonRefls.transform.FindChild("Left").GetComponent<Renderer>().material.color = Color.blue;
            LeftArmUI.gameObject.SetActive(true);
            charUI.transform.Find("leftArmBack").GetComponent<Image>().gameObject.SetActive(true);
        }

        if (!robotParts[RightArm].active)
        {
            buttonRefls.transform.FindChild("Up").GetComponent<Renderer>().material.color = Color.black;
            RightArmUI.gameObject.SetActive(false);
            charUI.transform.Find("rightArmBack").GetComponent<Image>().gameObject.SetActive(false);
        }
        else
        {
            buttonRefls.transform.FindChild("Up").GetComponent<Renderer>().material.color = Color.yellow;
            RightArmUI.gameObject.SetActive(true);
            charUI.transform.Find("rightArmBack").GetComponent<Image>().gameObject.SetActive(true);
        }

        if (!robotParts[LeftLeg].active)
        {
            LeftLegUI.gameObject.SetActive(false);
            charUI.transform.Find("leftLegBack").GetComponent<Image>().gameObject.SetActive(false);
            buttonRefls.transform.FindChild("Down").GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            buttonRefls.transform.FindChild("Down").GetComponent<Renderer>().material.color = Color.green;
            LeftLegUI.gameObject.SetActive(true);
            charUI.transform.Find("leftLegBack").GetComponent<Image>().gameObject.SetActive(true);
        }

        if (!robotParts[RightLeg].active)
        {
            RightLegUI.gameObject.SetActive(false);
            charUI.transform.Find("rightLegBack").GetComponent<Image>().gameObject.SetActive(false);
            buttonRefls.transform.FindChild("Right").GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            buttonRefls.transform.FindChild("Right").GetComponent<Renderer>().material.color = Color.red;
            RightLegUI.gameObject.SetActive(true);
            charUI.transform.Find("rightLegBack").GetComponent<Image>().gameObject.SetActive(true);
        }
    }

	void updateNoParts(){
		if (!(robotParts [LeftLeg].active || robotParts [RightLeg].active || robotParts [LeftArm].active || robotParts [RightArm].active)) {
			if (this.currentHealth > 5f){
				this.currentHealth -= this.maxHealth * .05f;
				this.currentHealth = this.currentHealth <= 5f ? 5f : this.currentHealth;
			}
			else{
				this.currentHealth -= this.maxHealth * .01f;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateHP();

        updatePartsUI();

	    if(currentHealth <= 0)
        {
			BreakRandomPart();
			BreakRandomPart();
			BreakRandomPart();
			if(toturial == 0)
				gm.thisPlayerDied(mytag);
        }

 /*       if (Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
*/
        if(!isGrounded && rigidbodyTwoD.velocity.y < 0)
        {
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }

		if (!robotParts [LeftLeg].active && !robotParts [RightLeg].active)
        {
			this.transform.FindChild ("Pelvis").FindChild ("ThrusterEffects").gameObject.SetActive(true);
            SFXManager.ThrusterOn(mytag);
		} 
		else 
		{
			this.transform.FindChild ("Pelvis").FindChild ("ThrusterEffects").gameObject.SetActive(false);
		}

        if (currentState != CharacterState.Blocking)
        {
            shieldParticle.SetActive(false);
        }
        else if (currentState == CharacterState.Blocking)
        {
            shieldParticle.SetActive(true);
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
	public void HeadButt()
	{
		CharacterState thisMove = CharacterState.HeadButt;

		if(!IsBusy())
		{
			anim.SetTrigger("HeadButt");

			if(isGrounded)
			{
				rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
			}

			currentState = thisMove;
		}
	}


    public void LeftPunch()
    {
		CharacterState thisMove = CharacterState.LeftPunch;
		if ( robotParts[LeftArm].active && ( !IsBusy() || CanComboMove(thisMove) ) )
        {
            robotParts[LeftArm].Attack();
			comboState = false;
			anim.SetTrigger(robotParts[LeftArm].GetTrigger());
            SFXManager.cueFX(mytag, "Whiff");
            if (isGrounded)
            {
                rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
            }
           
			currentState = thisMove;
        }
		else if(NoActiveParts())
		{
			HeadButt();
		}

        else if(!robotParts[LeftArm].active)
        {
            SFXManager.cueFX(mytag, "NoLimb");
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
            SFXManager.cueFX(mytag, "Whiff");
            if (isGrounded)
			{
				rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
			}
			
			currentState = thisMove;
		}
		else if(NoActiveParts())
		{
			HeadButt();
		}
	
        else if (!robotParts[RightArm].active)
        {
            SFXManager.cueFX(mytag, "NoLimb");
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
			hangBlock = HangBlock(0.5f);
			StartCoroutine(hangBlock);
            SFXManager.cueFX(mytag, "Whiff");
            robotParts[LeftLeg].Attack();
			comboState = false;
			anim.SetTrigger(robotParts[LeftLeg].GetTrigger());
            rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
            currentState = thisMove;
        }
		else if(NoActiveParts())
		{
			HeadButt();
		}
        else if (!robotParts[LeftLeg].active)
        {
            SFXManager.cueFX(mytag, "NoLimb");
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
            SFXManager.cueFX(mytag, "Whiff");
            currentState = thisMove;
        }
		else if(NoActiveParts())
		{
			HeadButt();
		}
        else if (!robotParts[RightLeg].active)
        {
            SFXManager.cueFX(mytag, "NoLimb");
        }
    }

    public void Block()
    {
        if(( currentState == CharacterState.Blocking || !IsBusy() ) && isGrounded)
        {
            if(!IsBusy() && currentState != CharacterState.Blocking)
            {
                anim.SetBool("Running", false);
                anim.SetBool("Hanging", false);
                //anim.SetTrigger("Block");
                anim.SetBool("Blocking", true);

                currentState = CharacterState.Blocking;
				shieldParticle.SetActive(true);
            }

            rigidbodyTwoD.velocity = new Vector2(0, rigidbodyTwoD.velocity.y);
        }
    }

    public void UnBlock()
    {
        if(currentState == CharacterState.Blocking)
        {
            //anim.SetTrigger("UnBlock");
            anim.SetBool("Blocking", false);
            currentState = CharacterState.Idle;
			//this.transform.FindChild ("Pelvis").FindChild ("Shield").gameObject.SetActive(false);
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

	public bool NoActiveParts()
	{
		if(!robotParts[LeftArm].active && !robotParts[RightArm].active && !robotParts[LeftLeg].active && !robotParts[RightLeg].active)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

    public void OnPickup()
    {
		CharacterState thisMove = CharacterState.Pickup;

		PartPickup partToPickup = pickupBox.GetClosestPart();
		if(partToPickup != null)
		{
            SFXManager.cueFX(mytag, "LimbGained");
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
		if (robotParts[LeftArm].active)
			buttonRefls.transform.FindChild ("Left").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.7f, 2.7f, 1);
		if (robotParts[RightLeg].active)
			buttonRefls.transform.FindChild ("Right").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.7f, 2.7f, 1);
		if (robotParts[RightArm].active)
			buttonRefls.transform.FindChild ("Up").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.7f, 2.7f, 1);
		if (robotParts[LeftLeg].active)
			buttonRefls.transform.FindChild ("Down").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.7f, 2.7f, 1);
	}
	
	public void rocketUnPre()
	{
		buttonRefls.transform.FindChild ("Left").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.0f, 2.0f, 1);
		buttonRefls.transform.FindChild ("Right").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.0f, 2.0f, 1);
		buttonRefls.transform.FindChild ("Up").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.0f, 2.0f, 1);
		buttonRefls.transform.FindChild ("Down").GetComponent<Renderer> ().transform.localScale = new Vector3 (2.0f, 2.0f, 1);
	}

    //Rocket Moves
    public void RocketLeftArm()
    {
		CharacterState thisMove = CharacterState.Rocketing;
		
		if ( robotParts[LeftArm].active && ( !IsBusy() || CanComboMove(thisMove) ) )
		{
			anim.SetTrigger("RocketLeftArm");
			currentState = thisMove;
		}

    }

	public void RocketRightArm()
	{
		CharacterState thisMove = CharacterState.Rocketing;
		
		if ( robotParts[RightArm].active && ( !IsBusy() || CanComboMove(thisMove) ) )
		{
			anim.SetTrigger("RocketRightArm");
			currentState = thisMove;
		}
	}

	public void RocketLeftLeg()
	{
		CharacterState thisMove = CharacterState.Rocketing;
		
		if ( robotParts[LeftLeg].active && ( !IsBusy() || CanComboMove(thisMove) ) )
		{
			anim.SetTrigger("RocketLeftLeg");
			currentState = thisMove;
		}
	}

	public void RocketRightLeg()
	{
		CharacterState thisMove = CharacterState.Rocketing;
		
		if ( robotParts[RightLeg].active && ( !IsBusy() || CanComboMove(thisMove) ) )
		{
			anim.SetTrigger("RocketRightLeg");
			currentState = thisMove;
		}
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

			Transform partToBreak = null;
			//change this to switch leg parts
			switch(index)
			{
			case LeftArm: partToBreak = this.transform.GetChild(0).GetChild(2).GetChild(2);
				rocketBody.velocity = new Vector2(direction * 200, 0);
				rigidbodyTwoD.AddForce(new Vector2(-direction*50, 0), ForceMode2D.Impulse);

				break;
			case RightArm: partToBreak = this.transform.GetChild(0).GetChild(2).GetChild(1);
				rocketBody.velocity = new Vector2(0, -200);
				rocket.transform.Rotate(new Vector3(0, 0, -90));
				rigidbodyTwoD.velocity = new Vector2(rigidbodyTwoD.velocity.x, 0);
				rigidbodyTwoD.AddForce(new Vector2(0, 125), ForceMode2D.Impulse);
				break;
			case LeftLeg: partToBreak = this.transform.GetChild(0).GetChild(1);
				rocketBody.velocity = new Vector2(0, 200);
				rocket.transform.Rotate(new Vector3(0, 0, 90));
				rigidbodyTwoD.AddForce(new Vector2(0, -75), ForceMode2D.Impulse);
				break;
			case RightLeg: partToBreak = this.transform.GetChild(0).GetChild(0);
				rocketBody.velocity = new Vector2(direction * 200, 0);
				rigidbodyTwoD.AddForce(new Vector2(-direction*50, 0), ForceMode2D.Impulse);
				break;
			}
            SFXManager.cueFX(mytag,"Launch");
			partToBreak.gameObject.SetActive(false);
			robotParts[index].active = false;
        }
    }

	public void OnHang()
	{
		if (currentState != CharacterState.Hang) 
		{
			robotParts[LeftArm].CancelAttack();
			robotParts[LeftLeg].CancelAttack();
			robotParts[RightLeg].CancelAttack();
			robotParts[LeftLeg].CancelAttack();
			
			this.transform.position = hangSpot.transform.position;
			rigidbodyTwoD.gravityScale = 0;
			rigidbodyTwoD.velocity = Vector2.zero;
			
			if (delayedJump != null)
			{
				StopCoroutine(delayedJump);
			}
			
			currentState = CharacterState.Hang;
		}
	}

	public void OnHitConnected()
	{
		comboState = true;
	}

	public bool IsGrounded()
	{
		return isGrounded;
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
            
			currentState = CharacterState.Jumping;
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

			currentState = CharacterState.Jumping;
			anim.SetTrigger("JumpStart");
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
                //anim.SetTrigger("Run");
                anim.SetBool("Running", true);
                currentState = CharacterState.Run;
                SFXManager.cueFX(mytag, "Step");
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
                //anim.SetTrigger("Run");
                anim.SetBool("Running", true);
				currentState = CharacterState.Run;
                SFXManager.cueFX(mytag, "Step");
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
        SFXManager.cueFX(mytag, "Hit");
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

	public void EnableHeadHitBox()
	{
		headHitBox.enabled = true;
	}

	public void DisableHeadHitBox()
	{
		headHitBox.enabled = false;
	}

    public void OnMoveOrFlinchEnd()
    {
		comboState = false;
        currentState = CharacterState.Idle;

        //CancelAttacks();
        anim.SetBool("Blocking", false);
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
		DisableHeadHitBox();
    }

    public bool IsBusy()
    {
		return currentState != CharacterState.Idle && currentState != CharacterState.Run 
            && currentState != CharacterState.Blocking && currentState != CharacterState.Airborne;
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
        SFXManager.cueFX(mytag, "LimbLost");
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
                SFXManager.cueFX(mytag, "Land");
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

	void OnCollisionStay2D(Collision2D col)
    {
		if (previousPlatform.Count != 0 && !drop) {
			foreach(Collider2D platform in previousPlatform){
				Physics2D.IgnoreCollision(platform, GetComponent<Collider2D>(), drop);
			}
			previousPlatform.Clear();
		}

		if (col.gameObject.tag == "Platform")
		{
			Collider2D collider = col.collider;
			previousPlatform.Add(collider);
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
			if(toturial == 0)
				gm.thisPlayerDied(mytag);
        }

		if ((other.gameObject.name == "HangAreaLeft" && isFacingLeft == false) || (other.gameObject.name == "HangAreaRight" && isFacingLeft == true)) 
		{
			hangSpot = other.gameObject;

			if( currentState != CharacterState.Hang &&
			   !anim.GetBool("HangBlock") && (!IsBusy() || currentState == CharacterState.LeftKick)
			   && !isGrounded)
            {
                anim.SetBool("Running", false);
                anim.SetBool("Blocking", false);
                //anim.SetTrigger("Hang");
                anim.SetBool("Hanging", true);
                currentState = CharacterState.Hang;
                SFXManager.cueFX(mytag, "Ledge");
				robotParts[LeftArm].CancelAttack();
				robotParts[LeftLeg].CancelAttack();
				robotParts[RightLeg].CancelAttack();
				robotParts[LeftLeg].CancelAttack();

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
		currentState = CharacterState.Idle;
    }

	IEnumerator HangBlock(float duration)
	{
		anim.SetBool("HangBlock", true);
		yield return new WaitForSeconds(duration);
		anim.SetBool("HangBlock", false);
	}

    void OnDestroy()
    {
        SFXManager.ThrusterOff(mytag);
    }
}
