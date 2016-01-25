using UnityEngine;
using System.Collections;

public class PickupTigerLP : PartPickup {

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

	public override GameObject GetAttachablePart()
	{
		return Resources.Load ("Moves/TigerLeftPunch") as GameObject;
	}
}
