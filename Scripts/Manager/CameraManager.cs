using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	// Use this for initialization
	public Character playerOne;
	public Character playerTwo;
	
	void Start () {
		
	}
	void SetCameraPos() {
		Vector3 middle = (playerOne.transform.position + playerTwo.transform.position) * 0.5f;
		GetComponent<Camera>().transform.position = new Vector3(
			middle.x,
			middle.y,
			GetComponent<Camera>().transform.position.z
			);
	}
	
	void SetCameraSize() {
		float minSizeY = 40;
		//horizontal size is based on actual screen ratio
		float minSizeX = minSizeY * Screen.width / Screen.height;
		//multiplying by 0.5, because the ortographicSize is actually half the height
		float width = Mathf.Abs(playerOne.transform.position.x - playerTwo.transform.position.x) * 0.5f + 30;
		float height = Mathf.Abs(playerOne.transform.position.y - playerTwo.transform.position.y) * 0.5f;
		//computing the size
		float camSizeX = Mathf.Max(width, minSizeX);
		GetComponent<Camera>().orthographicSize = Mathf.Max(height,
		                                                    camSizeX * Screen.height / Screen.width, minSizeY);
		
	}
	
	// Update is called once per frame
	void Update () {
		SetCameraPos();
		SetCameraSize();
	}
}
