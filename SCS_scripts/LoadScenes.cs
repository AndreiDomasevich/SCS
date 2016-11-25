using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour {

	public void StartLevel() {
		SceneManager.LoadScene ("SandDunesDemo");
	}
}
