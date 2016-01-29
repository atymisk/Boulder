using UnityEngine;
using System.Collections;
using System;

public class BunnyRightKick : Part {
	protected override void ChangeSprite(GameObject part)
	{
		SpriteRenderer upperLegRender = part.GetComponent<SpriteRenderer>();
		SpriteRenderer lowerLegRender = part.transform.GetChild(0).GetComponent<SpriteRenderer>();
		SpriteRenderer footRender = part.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
		upperLegRender.sprite = SpriteManager.instance.spriteMap["Bunny (1)_4"];
		lowerLegRender.sprite = SpriteManager.instance.spriteMap["Bunny (1)_11"];
		footRender.sprite = SpriteManager.instance.spriteMap["Bunny (1)_14"];
	}
	
	private IEnumerator moveTimeRoutine;
	
	void Start()
	{
		base.Initialize();
	}


    public override string GetRocketPath()
    {
        return "RocketParts/BunnyRightLegRocket";
    }

    public override void Attack()
	{
		float speed = -50;
		float duration = 0.30f;
		float direction = owner.IsFacingLeft() ? 1 : -1;
		Vector2 velocity = new Vector2(speed, 0);
		moveTimeRoutine = MoveOverTime(direction * velocity, duration);
		StartCoroutine(moveTimeRoutine);
	}
	
	public override void CancelAttack()
	{
		DisableHitBox();
		
		if(moveTimeRoutine != null)
		{
			StopCoroutine(moveTimeRoutine);
		}
	}
	
	public override int GetPartIndex()
	{
		return RightLeg;
	}
	
	override public void OnHitConnected(Robot enemy)
	{
		float damage = 10;
		float speed = 50;
		float direction = owner.IsFacingLeft() ? -1 : 1;
		Vector2 pushVelocity = new Vector2(direction * speed, 0);
		enemy.HeavyHitStun(damage, pushVelocity, 0.2f);
		collider.enabled = false;
	}
	
	public override string GetTrigger()
	{
		return "TigerRightKick";
	}
	
	//Coroutines
	IEnumerator MoveOverTime(Vector2 speed, float duration)
	{
		yield return new WaitForFixedUpdate();
		
		float currentTime = 0;
		while (currentTime < duration)
		{
			Debug.Log("looping");
			currentTime = currentTime + Time.fixedDeltaTime;
			float xDisplacement = speed.x * Time.fixedDeltaTime;
			float yDisplacement = speed.y * Time.fixedDeltaTime;
			float zPosition = this.transform.position.z;
			
			Vector3 displacement = new Vector3(xDisplacement, yDisplacement, zPosition);
			/*
            owner.transform.position = new Vector3(xDisplacement + owner.transform.position.x,
                                                  yDisplacement + owner.transform.position.y,
                                                  owner.transform.position.z);
            */
			owner.rigidbodyTwoD.MovePosition(owner.transform.position + displacement);
			yield return new WaitForFixedUpdate();
		}
	}
}
