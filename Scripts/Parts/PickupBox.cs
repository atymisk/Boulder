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


    private PartPickup GetClosestPart()
    {
        if(nearbyParts.Count == 0)
        {
            return null;
        }
        else
        {
            Debug.Log("nearbyPart[0]" + nearbyParts[0]);
            return nearbyParts[0];
        }
    }

    public PartPickup TakeClosestPart()
    {
        PartPickup toTake = null;

        for(bool found = false; !found && nearbyParts.Count > 0; nearbyParts.Remove(nearbyParts[0]))
        {
            if (nearbyParts[0] != null && !nearbyParts[0].taken)
            {
                found = true;
                nearbyParts[0].taken = true;
                toTake = nearbyParts[0];
            }
        }
       
        return toTake;
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
