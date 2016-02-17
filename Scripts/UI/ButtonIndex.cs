using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonIndex : MonoBehaviour, ISelectHandler
{
    public int index = 0;

    public void OnSelect(BaseEventData eventData)
    {
        UIInputManager.index = this.index;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
