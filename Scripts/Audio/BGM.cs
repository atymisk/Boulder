using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour
{
    private static BGM instance = null;
    public static BGM Instance { get { return instance; } }
    private static AudioSource bgm;

    void OnLevelWasLoaded(int level)
    {
        if(level > 2)
        {
            bgm.Stop();
        }
        else
        {
            bgm.Play();
        }
    }

	// Use this for initialization
	void Awake ()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        bgm = (AudioSource)gameObject.AddComponent<AudioSource>();
        AudioClip ac = (AudioClip)Resources.Load("Audio/Music/VG Music Demo 1");
        bgm.clip = ac;
        bgm.loop = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
