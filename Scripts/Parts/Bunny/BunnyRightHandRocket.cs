﻿using UnityEngine;
using System.Collections;

public class BunnyRightHandRocket : RobotRocket {

	// Use this for initialization
	void Start () {
        Initialize();
	}

    protected override string GetPickupPath()
    {
        return "ItemParts/BunnyRightHandPickup";
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
		{
			Rigidbody2D thisBody = this.GetComponent<Rigidbody2D>();
			if(thisBody.velocity.x == 0 && thisBody.velocity.y == 0)
			{
				DestroyRocket(this.transform.position);
			}

		}
	}
}
