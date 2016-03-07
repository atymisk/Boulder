using UnityEngine;
using System.Collections;
using System;

public class TigerRightPunch : Part
{
	private IEnumerator delayedPush;

	protected override void ChangeSprite(GameObject part)
	{
		SpriteRenderer shoulderRender = part.GetComponent<SpriteRenderer>();
		SpriteRenderer handRender = part.transform.GetChild(0).GetComponent<SpriteRenderer>();
		
		shoulderRender.sprite = SpriteManager.instance.spriteMap["Tiger_01_1"];
		handRender.sprite = SpriteManager.instance.spriteMap["Tiger_01_7"];
	}
	
	void Start()
	{
		base.Initialize();
	}
	
	public override string GetRocketPath()
	{
		return "RocketParts/TigerRightHandRocket";
	}
	
	public override void Attack()
	{
		//Implement additional behaviour such as position changes or special hitbox cases here
		Rigidbody2D ownerBody = owner.GetComponent<Rigidbody2D> ();
		float direction = owner.IsFacingLeft() ? -1 : 1;
		//ownerBody.AddForce(new Vector2(direction*50, 50), ForceMode2D.Impulse);
		ownerBody.velocity = new Vector2(0, 50);
		delayedPush = DelayedPush(new Vector2(direction*50, 20), 0.05f);
		StartCoroutine(delayedPush);
	}
	
	public override void CancelAttack()
	{
		DisableHitBox();
		
		if(delayedPush != null)
		{
			StopCoroutine(delayedPush);
		}
	}
	
	public override int GetPartIndex()
	{
		return RightArm;
	}
	
	override public void OnHitConnected(Robot enemy)
	{
		float damage = 10;
		float speed = 50;
		float duration = 0.2f;
		float direction = owner.IsFacingLeft()? -1 : 1;
		Vector2 pushVelocity = new Vector2(direction * speed, 0);
		enemy.HeavyHitStun(damage, pushVelocity, duration);
		collider.enabled = false;
	}
	
	public override string GetTrigger()
	{
		return "TigerRightPunch";
	}

	IEnumerator DelayedPush(Vector2 speed, float duration)
	{
		yield return new WaitForFixedUpdate();
		
		float currentTime = 0;
		while (currentTime < duration)
		{
			currentTime = currentTime + Time.fixedDeltaTime;


			yield return new WaitForFixedUpdate();
		}

		//ownerBody.AddForce(new Vector2(direction*50, 50), ForceMode2D.Impulse);
		Rigidbody2D ownerBody = owner.GetComponent<Rigidbody2D> ();
		ownerBody.velocity = speed;

	}
}
