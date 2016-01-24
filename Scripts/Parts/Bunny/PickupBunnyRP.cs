using UnityEngine;
using System.Collections;

public class PickupBunnyRP : PartPickup {

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
		return Resources.Load ("Moves/BunnyRightPunch") as GameObject;
	}
}
