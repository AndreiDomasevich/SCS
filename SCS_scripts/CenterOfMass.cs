using UnityEngine;
using System.Collections;

public class CenterOfMass : MonoBehaviour
{
	public GameObject vehicleCenterOfMass;

	private void Start ()
	{
		GetComponent<Rigidbody>().centerOfMass = 
			vehicleCenterOfMass.transform.localPosition;
	}
}
