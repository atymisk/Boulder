using UnityEngine;
using System.Collections;

public class RobotHurtBox : MonoBehaviour {

    private Robot owner;
    // Use this for initialization
    void Start()
    {
        owner = this.transform.GetComponentInParent<Robot>();
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
}
