using UnityEngine;
using System.Collections;

public class PickupBunnyLK : PartPickup {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override int GetIndex()
    {
        return LeftLeg;
    }
}
