﻿using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	
	public void ChangetoScene (string sceneName) {
		Application.LoadLevel (sceneName);
	}
}
