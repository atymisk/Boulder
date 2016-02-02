using UnityEngine;
using System.Collections;

public class TigerRightHandRocket : RobotRocket {

	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override string GetPickupPath()
    {
        return "ItemParts/TigerRightHandPickup";
    }
}
