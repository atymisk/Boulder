using UnityEngine;
using System.Collections;

public class HurtBox : MonoBehaviour {

	private Character owner;
	// Use this for initialization
	void Start () 
	{
		owner = this.transform.parent.parent.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Character GetCharacter()
	{
		return owner;
	}
}
