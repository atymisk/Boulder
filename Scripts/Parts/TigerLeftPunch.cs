using UnityEngine;
using System.Collections;
using System;

public class TigerLeftPunch : Part
{


    void Start()
    {
        base.Initialize();
    }

    public override void Attack()
    {
        //Implement additional behaviour such as position changes or special hitbox cases here
    }

    public override void CancelAttack()
    {
        DisableHitBox();
    }

    override public void OnHitConnected(Robot enemy)
    {
        float damage = 10;
        float speed = 75;
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
