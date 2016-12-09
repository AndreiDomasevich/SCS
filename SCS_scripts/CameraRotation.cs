using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	private int deltaTime = 1000;
	public Transform cameraTransform;
	
	private void FixedUpdate (){ 
		cameraTransform.localRotation = 
			Quaternion.Euler ( 0.0f, Input.GetAxis ("Horizontal")
				* Time.deltaTime * deltaTime , 0.0f);
	}
}
