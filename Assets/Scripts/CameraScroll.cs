using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScroll : MonoBehaviour {

	public float speed = 20;
	public float acceleration = 1f;
	public Vector3 defaultVelocity = new Vector3(0.1f, 0.1f, 1.0f);
	public Vector3 currentVelocity { get; private set; }
	float yaw = 0;
	float pitch = 0;
	public static CameraScroll main;

	void Awake() { 
		main = this; 
		currentVelocity = defaultVelocity;
		transform.rotation = Quaternion.Euler(pitch, yaw, 0);
	}

	public bool isSlowDown = false;
	void Update() {
		var targetVelocity = (isSlowDown) ? Vector3.zero : defaultVelocity;
		if((currentVelocity - targetVelocity).magnitude > 0.01f)
		{
			currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration); 
		}
		transform.position += currentVelocity * Time.deltaTime * speed;
	}
}
