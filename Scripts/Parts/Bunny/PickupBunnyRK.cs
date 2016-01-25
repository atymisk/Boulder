using UnityEngine;
using System.Collections;

public class PickupBunnyRK : PartPickup {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override int GetIndex()
    {
        return RightLeg;
    }

	public override GameObject GetAttachablePart()
	{
		return Resources.Load ("Moves/BunnyRightKick") as GameObject;
	}
}
