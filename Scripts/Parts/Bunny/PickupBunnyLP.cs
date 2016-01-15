using UnityEngine;
using System.Collections;
using System;

public class PickupBunnyLP : PartPickup {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override int GetIndex()
    {
        return LeftArm;
    }
}
