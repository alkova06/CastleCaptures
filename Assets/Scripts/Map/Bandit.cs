using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (spriteRenderer.color.a < 1)
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a + Time.deltaTime);
    }
}
