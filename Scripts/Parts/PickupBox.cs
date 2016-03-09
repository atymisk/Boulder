using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupBox : MonoBehaviour {

	public GameObject pickableNote;
    public Robot owner;

    bool noteIsActive = false;
    List<PartPickup> nearbyParts;
	// Use this for initialization
	void Start ()
    {
        nearbyParts = new List<PartPickup>();
        owner = this.GetComponentInParent<Robot>();
        
		//the prefab cannot destroy if the character dies. better dont make it prefab
        if(owner.prefabName == "Tiger") {
            pickableNote = Instantiate(Resources.Load("UI/pickableTiger")) as GameObject;
        }
        else {
            pickableNote = Instantiate(Resources.Load("UI/pickableBunny")) as GameObject;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (GetClosestPart() != null) {
            PartPickup closestPart = GetClosestPart();
            pickableNote.SetActive(true);
            //Debug.Log(owner + " gettting closest");
            pickableNote.transform.position = new Vector2 (GetClosestPart().transform.position.x, GetClosestPart().transform.position.y + 10);
        }
        else {
            pickableNote.SetActive(false); //Dont know if calling this in update constantly is inefficient
            //Debug.Log(owner + " not getting");
        }
            
	}

	public void RemovePart(PartPickup part)
	{
		if(nearbyParts.Remove(part))
		{
			part.taken = true;
			Destroy(part.gameObject);
			pickableNote.SetActive(false);
		}
	}

    public PartPickup GetClosestPart() {
        PartPickup toTake = null;

		CleanPartList();

		if (nearbyParts.Count > 0) {
            bool found = false;
            for(int i = 0; i < nearbyParts.Count && !found; i++) {
                int j = nearbyParts[i].GetIndex();
                bool isActive = owner.robotParts[j].active;
                if (!isActive) {
                    found = true;
                    toTake = nearbyParts[i];
                }
            }
          
		}
        
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
			pickableNote.SetActive(false);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        PartPickup part = other.GetComponent<PartPickup>();

        if(part != null)
        {
            nearbyParts.Add(part);
            //Debug.Log("Parts: " + nearbyParts.Count);
			//pickableNote.SetActive(true);
			//pickableNote.transform.position.y += 10.0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PartPickup part = other.GetComponent<PartPickup>();

        if (part != null)
        {
			pickableNote.SetActive(false);
            nearbyParts.Remove(part);
            //Debug.Log("Parts: " + nearbyParts.Count);
        }
    }

	void OnDestroy()
	{
		Destroy (pickableNote);
	}
}
