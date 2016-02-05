using UnityEngine;
using System.Collections;

public class TigerRightHandRocket : RobotRocket {

	// Use this for initialization
	void Start () {
        Initialize();
	}

    protected override string GetPickupPath()
    {
        return "ItemParts/TigerRightHandPickup";
    }
}
