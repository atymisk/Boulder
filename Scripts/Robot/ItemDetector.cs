using UnityEngine;
using System.Collections;

public class ItemDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
		{
			Debug.Log("hello");
			Rigidbody2D partBody = other.GetComponent<Rigidbody2D>();
			Transform partCenter = other.transform.GetChild(1);

			float magnitude = 0;
			if(this.transform.position.x >= partCenter.position.x) {
				magnitude = -150f;
			}
			else {
				magnitude = 150f;
			}

			partBody.AddForce(new Vector2(magnitude, 0), ForceMode2D.Force);
		}
	}
}
