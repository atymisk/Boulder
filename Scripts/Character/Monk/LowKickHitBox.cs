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
		//enemy.LightHitStun();
		enemy.HeavyHitStun();
	}
}
