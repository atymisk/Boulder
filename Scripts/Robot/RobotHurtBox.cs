using UnityEngine;
using System.Collections;

public class RobotHurtBox : MonoBehaviour {

    private Robot owner;
	private BoxCollider2D collider;
    // Use this for initialization
    void Start()
    {
        owner = this.transform.GetComponentInParent<Robot>();
		collider = this.transform.GetComponent<BoxCollider2D>();
        Debug.Log(owner);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Robot GetRobot()
    {
        return owner;
    }

	public void EnableHurtBox()
	{
		collider.enabled = true;
	}

	public void DisableHurtBox()
	{
		collider.enabled = false;
	}
}
