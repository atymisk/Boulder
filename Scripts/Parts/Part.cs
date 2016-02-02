using UnityEngine;
using System.Collections;

public abstract class Part : MonoBehaviour
{
    protected const int LeftArm = 0;
    protected const int RightArm = 1;
    protected const int LeftLeg = 2;
    protected const int RightLeg = 3;

    public bool active = true;

    protected Robot owner;
    protected Collider2D collider;

    public abstract void Attack();
    public abstract void CancelAttack();
    public abstract string GetTrigger();
    public abstract int GetPartIndex();

	protected abstract void ChangeSprite(GameObject part);

    protected void Initialize()
    {
		Debug.Log("initialize");
        owner = this.transform.parent.parent.gameObject.GetComponent<Robot>();
        collider = gameObject.GetComponent<Collider2D>();
    }

    public virtual string GetRocketPath()
    {
        return "RocketParts/TigerLeftHandRocket";
    }

	public void SetOwner(Robot robot)
	{
		owner = robot;
	}

    public void Attach()
    {
		Debug.Log ("attach");
        if(!active)
        {
            Transform pelvis = owner.transform.GetChild(0);
            int partIndex = GetPartIndex();
            Transform partToActivate = null;
			Debug.Log ("attach partIndex " + partIndex);
            if (partIndex == LeftArm)
            {
                partToActivate = pelvis.GetChild(2).GetChild(2);
                Debug.Log("im here");
            }
            else if(partIndex == RightArm)
            {
                partToActivate = pelvis.GetChild(2).GetChild(1);
            }
            else if(partIndex == LeftLeg)
            {
                partToActivate = pelvis.GetChild(1);
            }
            else if(partIndex == RightLeg)
            {
                partToActivate = pelvis.GetChild(0);
            }

            partToActivate.gameObject.SetActive(true);
			ChangeSprite(partToActivate.gameObject);

            owner.robotParts[partIndex].active = true;
        }
    }

    public virtual void OnHitConnected(Robot enemy)
    {
        float damage = 10;
        float speed = 30;
        float direction = owner.IsFacingLeft() ? -1 : 1;
        Vector2 pushVelocity = new Vector2(direction * speed, 0);
        enemy.HeavyHitStun(damage, pushVelocity, 0.2f);
        collider.enabled = false;
    }

    public virtual void OnHitBlocked(Robot enemy)
    {
        float speed = 30;
        float duration = 0.2f;
        float direction = owner.IsFacingLeft() ? -1 : 1;
        Vector2 pushVelocity = new Vector2(direction * speed, 0);
        enemy.BlockStun(pushVelocity, duration);
        collider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RobotHurtBox hurtBox = other.gameObject.GetComponent<RobotHurtBox>();
        if (hurtBox)
        {
            Robot enemy = hurtBox.GetRobot();

            if (enemy != owner)
            {
                if(enemy.GetCurrentState() != Robot.CharacterState.Blocking)
                {
                    Debug.Log("hitconnected");
                    OnHitConnected(enemy);
					owner.OnHitConnected();
                    GameObject sparks = (GameObject)Resources.Load("Particles/HitEffect");
                    var clone = Instantiate(sparks, enemy.transform.position, Quaternion.identity);
                    Destroy(clone, sparks.GetComponent<ParticleSystem>().startLifetime);
                }
                else
                {
                    OnHitBlocked(enemy);
                }

                SpecialEffects.instance.SlowMo(0.1f, 0.1f);
            }
            
        }

    }

    public virtual void EnableHitBox()
    {
        collider.enabled = true;
    }

    public virtual void DisableHitBox()
    {
        collider.enabled = false;
    }
}
