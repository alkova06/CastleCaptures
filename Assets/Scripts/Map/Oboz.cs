using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Oboz : MonoBehaviour 
{
	Vector2 startPosition;
	Vector2 lastPosition;
	Vector2 direction;
	bool walkEnable;
	float distance;

	public Vector2[] pointArrays;

	float timer;
	const float spawnTime = 1;

	SpriteRenderer sprite;

	// Use this for initialization
	void Start () 
	{
		timer = 0;
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(1,1,1,0);
	}

	void Update () 
	{
		if (walkEnable)
		{
			if (Vector2.Distance(startPosition, new Vector2(transform.position.x, transform.position.y)) < distance)
				rigidbody2D.velocity = direction * 0.75f;
			else
			{
				walkEnable = false;
				rigidbody2D.isKinematic = true;
				transform.position = new Vector3(lastPosition.x, lastPosition.y, transform.position.z);
				rigidbody2D.velocity = Vector2.zero;
			}
		}
		else
		{
			timer += Time.deltaTime;
			if (timer > spawnTime)
			{
				rigidbody2D.isKinematic = false;

				timer = 0;
				walkEnable = true;
				List<Vector2> tempPoints = new List<Vector2>();
				tempPoints.AddRange(pointArrays);

				int index = Random.Range(0,tempPoints.Count);
				startPosition = tempPoints[index];
				tempPoints.RemoveAt(index);
				index = Random.Range(0,tempPoints.Count);
				lastPosition = tempPoints[index];

				direction = lastPosition - startPosition;
				direction.Normalize();

				distance = Vector2.Distance(lastPosition, startPosition);

				transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z);
				SetFlip();
			}
		}

		SetColor ();
	}
	void SetColor()
	{
		if (walkEnable) 
		{
			if(sprite.color.a<1)
				sprite.color = new Color(1,1,1,sprite.color.a+Time.deltaTime);
			else
				sprite.color = Color.white;
		}
		else
			if(sprite.color.a>0)
				sprite.color = new Color(1,1,1,sprite.color.a-Time.deltaTime);
			else
				sprite.color = new Color(1,1,1,0);
	}

	void SetFlip()
	{
		Debug.Log (direction.ToString ());
		if ((direction.x > 0 && direction.y < 0) || (direction.x < 0 && direction.y > 0))
			transform.localScale = new Vector3(-2.1f, transform.localScale.y, transform.localScale.z);
		else
			transform.localScale = new Vector3(2.1f, transform.localScale.y, transform.localScale.z);
	}
}
