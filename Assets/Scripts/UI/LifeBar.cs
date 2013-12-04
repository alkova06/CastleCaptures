using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour
{
    public GameObject frame;
    public GameObject progress;
    SpriteRenderer progressSprite;
    SpriteRenderer frameSprite;

    public float MaxValue;
    public float Value;

    public Vector3 position;
    public int drawOrder;

    public float Alpha;

    void Start()
    {
        progressSprite = progress.GetComponent<SpriteRenderer>();
        frameSprite = frame.GetComponent<SpriteRenderer>();
        Value = 1;
    }

    void Update()
    {
        Alpha = Mathf.Clamp(Alpha, 0, 1);

        transform.position = new Vector3(position.x, position.y, 0);
        float r=Value/MaxValue;
        r = Mathf.Clamp(r, 0, 1);
        Vector3 scale = progress.transform.localScale;
        scale.x = 2.1f * r;
        progress.transform.localScale = scale;
        progressSprite.color = new Color(1.0f - r, r, 0, Alpha);
        frameSprite.color = new Color(1.0f, 1.0f, 1.0f, Alpha);

        progressSprite.sortingOrder = drawOrder;
        frameSprite.sortingOrder = drawOrder+1;
    }
}
