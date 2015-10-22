using UnityEngine;
using System.Collections;

public class FlyingKickHitBox : HitBox {

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
        float speed = 200;
        float direction = owner.IsFacingLeft() ? -1 : 1;
        Vector2 pushVelocity = new Vector2(direction * speed, 0);
        enemy.HeavyHitStun(pushVelocity);
    }
}
