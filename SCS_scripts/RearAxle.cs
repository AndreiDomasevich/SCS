using UnityEngine;
using System.Collections;

public class RearAxle : MonoBehaviour
{
	public WheelCollider rearLeftWheel;
	public WheelCollider rearRightWheel;
	private Vector3 centerOfLeftWheel;
	private Vector3 centerOfRightWheel;

	public GameObject rearLeftMesh;
	public GameObject rearRightMesh;

	public Transform axleJoint_L_000;
	public Transform axleJoint_L_001;
	public Transform axleJoint_L_002;

	public Transform axleJoint_R_000;
	public Transform axleJoint_R_001;
	public Transform axleJoint_R_002;

	public Transform shockLower_L_000;
	public Transform shockLower_L_001;
	public Transform shockLower_R_000;
	public Transform shockLower_R_001;

	public Transform shockUpper_L_000;
	public Transform shockUpper_L_001;
	public Transform shockUpper_R_000;
	public Transform shockUpper_R_001;

	private Vector3 transform_L;
	private Vector3 transform_R;

	private RaycastHit hit_0;
	private RaycastHit hit_1;

	private float suspensionCompressionLeft = 0.0f;
	private float suspensionCompressionRight = 0.0f;
	private float suspensionAngle_L_000 = 0.0f;
	private float suspensionAngle_L_001 = 0.0f;
	private float suspensionAngle_R_000 = 0.0f;
	private float suspensionAngle_R_001 = 0.0f;

	private float axlePivotAngle_L = 0.0f;
	private float axlePivotAngle_R = 0.0f;
	private float rotationValue_L = 0.0f;
	private float rotationValue_R = 0.0f;

	private float suspensionAngle = 0.6f;
	private float compareSuspensionCompression = 0.000001f;

	private Vector3 localRotation_L_001;
	private Vector3 localRotation_R_001;

	public Transform engineBelt;
	private float averageWheelRPM = 0.0f;
	private float rpm = 1.0f;
	private float rotationValue = 0.0f;

	

	private void Update ()
	{
		centerOfLeftWheel = rearLeftWheel.transform.TransformPoint (rearLeftWheel.center);
		
		if (Physics.Raycast (centerOfLeftWheel, -rearLeftWheel.transform.up, out hit_0, rearLeftWheel.suspensionDistance + rearLeftWheel.radius)) {
			transform_L = hit_0.point + (rearLeftWheel.transform.up * rearLeftWheel.radius);
		} else {
			transform_L = centerOfLeftWheel - (rearLeftWheel.transform.up * rearLeftWheel.suspensionDistance);
		}
		
		centerOfRightWheel = rearRightWheel.transform.TransformPoint (rearRightWheel.center);
		
		if (Physics.Raycast (centerOfRightWheel, -rearRightWheel.transform.up, out hit_1, rearRightWheel.suspensionDistance + rearRightWheel.radius)) {
			transform_R = hit_1.point + (rearRightWheel.transform.up * rearRightWheel.radius);
		} else {
			transform_R = centerOfRightWheel - (rearRightWheel.transform.up * rearRightWheel.suspensionDistance);
		}
		
		suspensionCompressionLeft = transform_L.y - centerOfLeftWheel.y;
		suspensionCompressionRight = transform_R.y - centerOfRightWheel.y;
		
		if (suspensionCompressionLeft >= -compareSuspensionCompression) {
			suspensionCompressionLeft = -compareSuspensionCompression;
		}
		if (suspensionCompressionRight >= -compareSuspensionCompression) {
			suspensionCompressionRight = -compareSuspensionCompression;
		}
		
		axlePivotAngle_L = (Mathf.Atan2 
			(suspensionCompressionLeft * -1.0f, suspensionAngle)) * Mathf.Rad2Deg;
		axlePivotAngle_R = (Mathf.Atan2 (
			suspensionCompressionRight * 1.0f, suspensionAngle)) * Mathf.Rad2Deg;
		
		setAxlesesLocalRotations();
		
		rearLeftMesh.transform.localRotation = Quaternion.Euler (rotationValue_L, 180.0f, 0.0f);
		rotationValue_L += (rearLeftWheel.rpm * (60) * Time.deltaTime) * -1.0f;
		
		rearRightMesh.transform.localRotation = Quaternion.Euler (rotationValue_L, 180.0f, 0.0f);
		rotationValue_R += (rearRightWheel.rpm * (60) * Time.deltaTime) * -1.0f;
		setSuspensionAngle();
		setShocksRotations ();
		averageWheelRPM = rearLeftWheel.rpm + rearRightWheel.rpm;
		
		if (averageWheelRPM < 0.0f) {
			averageWheelRPM *= -1.0f;
		}
		averageWheelRPM = (averageWheelRPM / 250.0f) + 1.0f;
		
		if (averageWheelRPM >= 4.0f) {
			averageWheelRPM = 4.0f;
		}
		
		engineBelt.localRotation = Quaternion.Euler (0.0f, rotationValue, 0.0f);
		rotationValue += averageWheelRPM * (360 / rpm) * Time.deltaTime;
	}

	private void setAxlesesLocalRotations()
	{
		axleJoint_L_000.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_L * -1.0f);
		axleJoint_L_001.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_L);
		axleJoint_L_002.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_L * 1.25f);

		axleJoint_R_000.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_R * -1.0f);
		axleJoint_R_001.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_R);
		axleJoint_R_002.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_R * 1.25f);
	}

	private void setShocksRotations()
	{
		shockLower_L_000.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_L * -0.875f) + suspensionAngle_L_000);
		shockLower_R_000.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_R * -0.875f) + suspensionAngle_R_000);
		shockLower_L_001.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_L * -0.875f) + suspensionAngle_L_001);
		shockLower_R_001.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_R * -0.875f) + suspensionAngle_R_001);

		shockUpper_L_000.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_L * 0.1f) + suspensionAngle_L_000);
		shockUpper_R_000.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_R * 0.1f) + suspensionAngle_R_000);
		shockUpper_L_001.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_L * 0.1f) + suspensionAngle_L_001);
		shockUpper_R_001.localRotation = Quaternion.Euler 
			(0.0f, 0.0f, (axlePivotAngle_R * 0.1f) + suspensionAngle_R_001);

	}

	private void setSuspensionAngle()
	{
		suspensionAngle_L_000 = (Mathf.Atan2 
			((suspensionCompressionLeft + -0.94f) * -1.0f, 0.14f)) * Mathf.Rad2Deg;
		suspensionAngle_R_000 = (Mathf.Atan2 
			((suspensionCompressionRight + -0.94f) * 1.0f, 0.14f)) * Mathf.Rad2Deg;
		suspensionAngle_L_001 = (Mathf.Atan2 
			((suspensionCompressionLeft + -0.86f) * -1.0f, 0.14f)) * Mathf.Rad2Deg;
		suspensionAngle_R_001 = (Mathf.Atan2 
			((suspensionCompressionRight + -0.86f) * 1.0f, 0.14f)) * Mathf.Rad2Deg;
	}
}
