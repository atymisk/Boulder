using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupBox : MonoBehaviour {

    List<PartPickup> nearbyParts;
	// Use this for initialization
	void Start ()
    {
        nearbyParts = new List<PartPickup>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	public void RemovePart(PartPickup part)
	{
		Debug.Log (nearbyParts.Contains(part));
		if(nearbyParts.Remove(part))
		{
			Debug.Log ("removePart 2");
			part.taken = true;
			Destroy(part.gameObject);
		}
	}

    public PartPickup GetClosestPart()
    {
        PartPickup toTake = null;

		CleanPartList();

		if (nearbyParts.Count > 0) {
			toTake = nearbyParts[0];
		}

		Debug.Log("getclosest" + nearbyParts.Contains(toTake));
        return toTake;
    }

	public void CleanPartList()
	{
		List<PartPickup> removalList = new List<PartPickup> ();

		foreach (PartPickup part in nearbyParts) {
			if(part.taken){
				removalList.Add(part);
			}
		}

		foreach (PartPickup part in removalList) {
			nearbyParts.Remove(part);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        PartPickup part = other.GetComponent<PartPickup>();

        if(part != null)
        {
            nearbyParts.Add(part);
            Debug.Log("Parts: " + nearbyParts.Count);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PartPickup part = other.GetComponent<PartPickup>();

        if (part != null)
        {
            nearbyParts.Remove(part);
            Debug.Log("Parts: " + nearbyParts.Count);
        }
    }
    
}
