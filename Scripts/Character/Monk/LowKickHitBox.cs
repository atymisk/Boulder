using UnityEngine;
using System.Collections;

public class LowKickHitBox : HitBox {

	
	// Use this for initialization
	void Start ()
	{
		base.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	override public void OnHitConnected(Character enemy)
	{
        float damage = 10;
        float speed = 75;
        float direction = owner.IsFacingLeft() ? -1 : 1;
        Vector2 pushVelocity = new Vector2(direction * speed, 0);
        enemy.HeavyHitStun(damage,pushVelocity, 0.2f);
        //collider.enabled = false;
    }
}
