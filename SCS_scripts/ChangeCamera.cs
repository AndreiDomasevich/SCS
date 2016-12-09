using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour
{
	public GameObject[] camera_;
	public GameObject[] helpText;

	private bool help = true;
	private int currentCamera = 0;

	private void Start ()
	{
		for (int num_camera = 0; num_camera < camera_.Length; num_camera++) {
			if (num_camera == 0) {
				camera_[num_camera].SetActive (true);
			} else {
				camera_[num_camera].SetActive (false);
			}
		}
	}

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.C)) {
			camera_ [currentCamera].SetActive (false);
			currentCamera += 1;
			if (currentCamera >= camera_.Length) {
				currentCamera = 0;
			}
			camera_ [currentCamera].SetActive (true);
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			if (help == false) {
				for (int b = 0; b < helpText.Length; b++) {
					helpText [b].SetActive (true);
				}
				help = true;
			} else {
				for (int b = 0; b < helpText.Length; b++) {
					helpText [b].SetActive (false);
				}
				help = false;
			}
		}
	}
}
