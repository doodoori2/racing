using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {

	public float PlayerZPoint = 1.0f;
	public float RockSpawnerZPoint = 10000f;
	public GameObject SlidingArea;
	public GameObject imagePrefab;
	private GameObject left = null;
	private GameObject right = null;
	private GameObject starObject = null;

	// private Dictionary<GameObject, GameObject[]> leftObject = new Dictionary<GameObject, GameObject[]>();
	public void Awake()
	{
		left = GameObject.Instantiate(imagePrefab);
		left.transform.parent = SlidingArea.transform;
		left.transform.localPosition = new Vector3(-100.0f, 0.0f, 0.0f);
		left.gameObject.SetActive(true);

		right = GameObject.Instantiate(imagePrefab);
		right.transform.parent = SlidingArea.transform;
		right.transform.localPosition = new Vector3(100.0f, 0.0f, 0.0f);
		right.gameObject.SetActive(true);
	}
	public void AddObject(GameObject newStarObject)
	{
		starObject = newStarObject;
		left.transform.localPosition = new Vector3(-100.0f, 0.0f, 0.0f);
		left.gameObject.SetActive(true);
		right.transform.localPosition = new Vector3(100.0f, 0.0f, 0.0f);
		right.gameObject.SetActive(true);
	}
	
	void Update () {
		if(starObject == null)
		{
			return;
		}

		var localPosition = starObject.transform.localPosition;
		// var ratio = Mathf.Clamp((localPosition.z - PlayerZPoint)/ (RockSpawnerZPoint - PlayerZPoint), 0.0f, 1.0f);
		var ratio = (localPosition.z - PlayerZPoint)/ (RockSpawnerZPoint - PlayerZPoint);
		if(left != null)
		{
			var p  = new Vector3(-100 * ratio, left.transform.localPosition.y, left.transform.localPosition.z);
			left.transform.localPosition = p;
		}
		if(right != null)
		{
			var p  = new Vector3( 100 * ratio, left.transform.localPosition.y, left.transform.localPosition.z);
			right.transform.localPosition = p;
		}
		if(ratio < 0.01f)
		{
			// left.gameObject.SetActive(false);
			// right.gameObject.SetActive(false);
		}
	}
}
