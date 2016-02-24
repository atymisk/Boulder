using UnityEngine;
using System.Collections;

public class PickupBunnyRP : PartPickup {
    private float lifetime = 0;
    private bool flip = false;
    Color[] mine;
    Renderer[] rend;
    // Use this for initialization
    void Start()
    {
        rend = gameObject.GetComponentsInChildren<Renderer>();
        mine = new Color[rend.Length];
        for (int i = 0; i < rend.Length; i++)
        {
            mine[i] = rend[i].material.color;
        }
    }

    void toggletrans()
    {
        flip = !flip;
        if (flip)
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material.color = new Color(mine[i].r, mine[i].b, mine[i].g, 0);
            }
        }
        else
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material.color = mine[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= 10)
        {
            InvokeRepeating("toggletrans", 1, 1);
        }
        if (lifetime >= 15)
        {
            Destroy(this.gameObject);
        }
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
