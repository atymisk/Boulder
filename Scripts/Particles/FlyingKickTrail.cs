using UnityEngine;
using System.Collections;

public class FlyingKickTrail : MonoBehaviour {

	public bool inAction = true;

	private Transform rightFoot;
	private GameObject trail;

	// Use this for initialization
	void Start () {
		Transform pelvis = this.transform.parent.parent.FindChild ("Pelvis");
		Transform upper = pelvis.FindChild ("RightUpperLeg");
		Transform lower = upper.FindChild ("RightLowerLeg");
		rightFoot = lower.FindChild ("RightFoot");
		OnFlyingKickStart ();
		//Debug.Log (rightFoot);
	}
	
	// Update is called once per frame
	void Update () {
		if (inAction) 
		{
			GameObject trailPrefab = (GameObject) Resources.Load ("Particles/FlyingKickTrail");
			trail = Instantiate (trailPrefab, rightFoot.transform.position, Quaternion.identity) as GameObject;
			trail.transform.position = rightFoot.transform.position;
			Destroy(trail, trail.GetComponent<ParticleSystem>().startLifetime);

		}
	}

	public void OnFlyingKickStart() {
		//trail = Instantiate (trailPrefab, rightFoot.transform.position, Quaternion.identity) as GameObject;
		//Destroy(trail, trailPrefab.GetComponent<ParticleSystem>().startLifetime);

	}
}
