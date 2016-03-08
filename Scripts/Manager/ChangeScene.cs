using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeScene : MonoBehaviour {
    public static ChangeScene instance = null;

    void Start()
    {
        instance = this;
    }

	public void ChangetoScene (string sceneName)
    {
        if(sceneName != "BoxedIn")
            GameManager.ot = false;
		Application.LoadLevel (sceneName);
	}

    public void ChangeAfterDelay(string sceneName, float delay)
    {

        StartCoroutine(DelayRoutine(sceneName, delay));
    }

	public void Exit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		Application.OpenURL(webplayerQuitURL);
		#else
		Application.Quit();
		#endif
	}

    IEnumerator DelayRoutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangetoScene(sceneName);
    }

}
