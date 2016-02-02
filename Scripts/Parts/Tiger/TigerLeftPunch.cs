using UnityEngine;
using System.Collections;
using System;

public class TigerLeftPunch : Part
{
	protected override void ChangeSprite(GameObject part)
	{
		SpriteRenderer shoulderRender = part.GetComponent<SpriteRenderer>();
		SpriteRenderer handRender = part.transform.GetChild(0).GetComponent<SpriteRenderer>();
		
		shoulderRender.sprite = SpriteManager.instance.spriteMap["Tiger_01_0"];
		handRender.sprite = SpriteManager.instance.spriteMap["Tiger_01_6"];
		
		//shoulderRender.sprite = Resources.Load ("SpriteSheets/Bunny(1)/Bunny(1)_0", typeof(Sprite)) as Sprite;
		//handRender.sprite = Resources.Load ("SpriteSheets/Bunny(1)/Bunny(1)_6", typeof(Sprite)) as Sprite;

	}

    void Start()
    {
        base.Initialize();
    }

    public override string GetRocketPath()
    {
        return "RocketParts/TigerLeftHandRocket";
    }

    public override void Attack()
    {
        //Implement additional behaviour such as position changes or special hitbox cases here
    }

    public override void CancelAttack()
    {
        DisableHitBox();
    }

    public override int GetPartIndex()
    {
        return LeftArm;
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
        return "TigerLeftPunch";
    }
}
