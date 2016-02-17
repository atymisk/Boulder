using UnityEngine;
using System.Collections;

public class BodyDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
		{
			Rigidbody2D partBody = other.GetComponent<Rigidbody2D>();
			Transform partCenter = other.transform.GetChild(1);

			float magnitude = 0;
			if(this.transform.position.x >= partCenter.position.x) {
				magnitude = -25f;
			}
			else {
				magnitude = 25f;
			}
            
            partBody.AddForce(new Vector2(magnitude, 0), ForceMode2D.Force);
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Rigidbody2D playerBody = other.GetComponent<Rigidbody2D>();
			Transform partCenter = other.transform.GetChild(1);
			
			float magnitude = 0;
			if(this.transform.position.x >= partCenter.position.x) {
				magnitude = -100f;
			}
			else {
				magnitude = 100f;
			}
			playerBody.AddForce(new Vector2(magnitude, 0), ForceMode2D.Force);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
            Rigidbody2D partBody = other.GetComponent<Rigidbody2D>();
            Transform partCenter = other.transform.GetChild(1);
            
            float speed = 0;
            if (this.transform.position.x >= partCenter.position.x)
            {
                speed = -50f;
            }
            else {
                speed = 50f;
            }

            speed = partBody.velocity.x + speed;
            partBody.velocity = new Vector2(speed, partBody.velocity.y);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Rigidbody2D playerBody = other.GetComponent<Rigidbody2D>();
            Transform partCenter = other.transform.GetChild(1);
            
            float speed = 0;
            if (this.transform.position.x >= partCenter.position.x)
            {
                speed = -20f;
            }
            else {
                speed = 20f;
            }
            speed = playerBody.velocity.x + speed;
            playerBody.velocity = new Vector2(speed, playerBody.velocity.y);
        }
    }
}
