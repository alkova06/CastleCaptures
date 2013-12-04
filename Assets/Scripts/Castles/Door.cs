using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public float MaxLife;
    [HideInInspector]
    public float Life;
    bool Shake;
    float timer;
    Vector3 position;

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    public GameObject lifebarGO;
    LifeBar lifebar;

    void Start()
    {
        Life = MaxLife;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject lifebarGOClone = (GameObject)Instantiate(lifebarGO);
        Vector3 scale = lifebarGOClone.transform.localScale;
        scale.x *= 1.25f;
        lifebarGOClone.transform.localScale = scale;
        lifebar = lifebarGOClone.GetComponent<LifeBar>();
        lifebar.Alpha = 0;
        lifebar.Value = Life;
        lifebar.MaxValue = MaxLife;
        lifebar.drawOrder = spriteRenderer.sortingOrder+1;
    }

    void Update()
    {
        if (Shake)
        {
            timer += Time.deltaTime * 3f;
            if (timer < 1)
            {
                transform.position = position + new Vector3(0, Mathf.Sin(timer * 6.28f) * 0.03f, 0);
            }
            else
            {
                transform.position = position;
                Shake = false;
                timer = 0;
            }
        }

        lifebar.position = position + new Vector3(-0.1f, 1.2f, 0);
        if (lifebar.Alpha > 0)
            lifebar.Alpha -= Time.deltaTime * 1.5f;
        else
            lifebar.Alpha = 0;
        
        if (Input.GetKeyDown(KeyCode.Space))
            SetDamage(20);
    }

    public void SetDamage(float damage)
    {
        Life -= damage;

        if (Life < 0)
        {
            gameObject.SetActive(false);
            return;
        }

        lifebar.Alpha = 1;
        lifebar.Value = Life;

        if (Life < MaxLife / 3.0f)
            spriteRenderer.sprite = sprites[2];
        else if (Life < 2 * MaxLife / 3.0f)
            spriteRenderer.sprite = sprites[1];
        else
            spriteRenderer.sprite = sprites[0];
        Shake = true;
        position = transform.position;
        timer = 0;
    }
}
