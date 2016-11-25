using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	public Transform cameraTransform;
	
	void FixedUpdate (){ 
		cameraTransform.localRotation = Quaternion.Euler ( 0.0f, Input.GetAxis ("Horizontal")* Time.deltaTime * 1000 , 0.0f);
	}
}
