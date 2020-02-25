using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour {

	public RectTransform playerCursor;
	public Transform target;
	public Vector3 PlayerOffset;

	Vector3 offset;


	// Use this for initialization
	void Awake () {
		transform.position = new Vector3(target.position.x,transform.position.y,target.position.z);
		offset = transform.position - target.position - PlayerOffset;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position + offset;
		playerCursor.rotation = Quaternion.Euler(0f,0f,-target.rotation.eulerAngles.y);
	}
}
