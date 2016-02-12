using UnityEngine;
using System.Collections;

public class PickupTigerLK : PartPickup {

    private float lifetime = 0;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frC:\Users\Public\Documents\Unity Projects\Project Boulder\Assets\GitHub\Scripts\Parts\Tiger\PickupTigerLK.csame
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
        return LeftLeg;
    }

	public override GameObject GetAttachablePart()
	{
		return Resources.Load ("Moves/TigerLeftKick") as GameObject;
	}
}
