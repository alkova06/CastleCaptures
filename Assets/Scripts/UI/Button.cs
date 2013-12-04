using UnityEngine;
using System.Collections;

public delegate void ButtonAction();

public class Button : MonoBehaviour
{
	public Sprite ButtonLeave, ButtonHover, ButtonClick;
	public event ButtonAction PressButton;
	public event ButtonAction HoverButton;

	BoxCollider2D boxCollider2D;
	SpriteRenderer spriteRenderer;

	void Start()
	{
		boxCollider2D = transform.GetComponent<BoxCollider2D> ();
		spriteRenderer = transform.GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		spriteRenderer.sprite = ButtonLeave;
		Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit2D = Physics2D.Raycast(new Vector2(touchPosition.x, touchPosition.y), Vector2.zero);
		if (hit2D != null)
			if (hit2D.collider is BoxCollider2D)
				if ((BoxCollider2D)hit2D.collider == boxCollider2D) 
				{
					if(HoverButton!=null)
						HoverButton();
					spriteRenderer.sprite = ButtonHover;
					if (Input.GetMouseButton (0)) 
					{
						if(PressButton!=null)
							PressButton();
						spriteRenderer.sprite = ButtonClick;
					}
				}
	}
}
