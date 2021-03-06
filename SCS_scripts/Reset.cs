﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
	public Transform forcePoint_00;
	public Transform forcePoint_01;
	public Transform forcePoint_02;
	public Transform forcePoint_03;

	private Quaternion startRotation;
	private Vector3 startPosition;
	private Vector3 forceDirection;

	private void Start ()
	{
		startRotation = transform.rotation;
		startPosition = transform.position;
		forceDirection = new Vector3 (0.0f, 1000.0f, 0.0f);
	}

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			GetComponent<Rigidbody>().isKinematic = true;
			transform.rotation = startRotation;
			transform.position = startPosition;
			GetComponent<Rigidbody>().isKinematic = false;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("Main");
		}
	}
}
