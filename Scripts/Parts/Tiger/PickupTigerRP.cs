using UnityEngine;
using System.Collections;

public class PickupTigerRP : PartPickup {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override int GetIndex()
    {
        return RightArm;
    }
}
