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

	public override GameObject GetAttachablePart()
	{
		return Resources.Load ("Moves/TigerRightPunch") as GameObject;
	}
}
