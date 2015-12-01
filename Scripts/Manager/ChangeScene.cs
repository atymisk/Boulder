using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
    public static ChangeScene instance = null;

    void Start()
    {
        instance = this;

    }

	public void ChangetoScene (string sceneName) {
		Application.LoadLevel (sceneName);
	}

    public void ChangeAfterDelay(string sceneName, float delay)
    {

        StartCoroutine(DelayRoutine(sceneName, delay));
    }

    IEnumerator DelayRoutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangetoScene(sceneName);
    }
}
