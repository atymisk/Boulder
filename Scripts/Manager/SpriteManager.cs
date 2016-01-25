using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {

	public static SpriteManager instance;
	public Dictionary<string,Sprite> spriteMap;

	void Start () {
		if (instance != null) {
			Destroy (this);
		} 
		else {
			instance = this;
		}

		spriteMap = new Dictionary<string,Sprite>();
		Sprite[] spriteArray = Resources.LoadAll<Sprite>("SpriteSheets");

		foreach (Sprite sprite in spriteArray) {
			spriteMap.Add(sprite.name,sprite);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
