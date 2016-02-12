using UnityEngine;
using System.Collections;

public class PickupTigerLP : PartPickup {

    private float lifetime = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= 15)
        {
            Destroy(this.gameObject);
        }
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
