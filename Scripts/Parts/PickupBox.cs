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
        if (nearbyParts.Count == 0)
        {
            return null;
        }
        else
        {
            PartPickup taken = nearbyParts[0];
            nearbyParts.Remove(taken);

            return taken;
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
