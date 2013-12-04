using UnityEngine;
using System.Collections;

public class MapPlayer : MonoBehaviour {
	Vector2 LastPosition;
	Vector3 endWalkPosition;
	float walkDistance;
	bool walkEnable;
	Vector2 direction;

	// Use this for initialization
	void Start () 
	{
		walkEnable = false; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (walkEnable) 
		{
			rigidbody2D.velocity = direction;
			if (Vector2.Distance (LastPosition, new Vector2 (transform.position.x, transform.position.y)) > walkDistance)
			{
				walkEnable = false;
				transform.position = new Vector3(endWalkPosition.x, endWalkPosition.y, transform.position.z);
			}
		} 
		else
			rigidbody2D.velocity = Vector2.zero;

#if UNITY_EDITOR 
		if (Input.GetMouseButton (0))
		{
			walkEnable = true;
			endWalkPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			walkDistance = Vector2.Distance (new Vector2 (endWalkPosition.x, endWalkPosition.y), new Vector2 (transform.position.x, transform.position.y));
			LastPosition = new Vector2 (transform.position.x, transform.position.y);
			direction = new Vector2(endWalkPosition.x-LastPosition.x, endWalkPosition.y - LastPosition.y);
			direction.Normalize();
		}
		#endif 
	}
}
