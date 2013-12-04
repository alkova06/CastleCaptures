using UnityEngine;
using System.Collections;

public class CameraTranslate : MonoBehaviour {

	const float MinPositionY = -3.4f;
	const float MaxPositionY = 3.4f;

	float MouseLastPositionY;

	void Start () 
	{
	
	}
	
	void Update () 
	{
#if UNITY_EDITOR
		if (Input.GetMouseButton(0)) 
		{
			float mousePositionY= Input.mousePosition.y;

			camera.transform.position +=new Vector3(0, (mousePositionY - MouseLastPositionY) * 0.05f,0);
			camera.transform.position = new Vector3(camera.transform.position.x, Mathf.Clamp(camera.transform.position.y, MinPositionY, MaxPositionY), camera.transform.position.z);
		}		
		MouseLastPositionY = Input.mousePosition.y;
#endif
	}
}
