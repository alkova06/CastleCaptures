using UnityEngine;
using System.Collections;

public class EnemyMelle : MonoBehaviour
{
    public enum State
    {
        Idle,
        WalkToEnemie,
        Attack,
        Die
    }

    Animator anim;
    SpriteRenderer spriteRenderer;

    State state;
    public GameObject LifeBarGO;
    LifeBar lifebar;

    Vector2 startWalkPoint;
    Vector2 endWalkPoint;
    float walkDistance;

    public Unit.UnitType type;
    Unit unitParams;

    public Color CastleColor;
    Color selectColor;

    float colorTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject lifeBarGOClone = (GameObject)Instantiate(LifeBarGO);
        lifebar = lifeBarGOClone.GetComponent<LifeBar>();
    }

    void Start()
    {
        unitParams = UnitContainer.Instance.GetUnit(type);
        state = State.Idle;

        lifebar.Alpha = 0;
        lifebar.Value = unitParams.Life;
        lifebar.MaxValue = unitParams.Life;
        lifebar.drawOrder = spriteRenderer.sortingOrder + 1;

        spriteRenderer.material.color = CastleColor;
        selectColor = Color.red;

        colorTimer = 0;
    }

    void Update()
    {
        if (spriteRenderer.material.color != CastleColor)
        {
            colorTimer += Time.deltaTime;
            if (colorTimer > 1)
                colorTimer = 1;
            spriteRenderer.material.color = Color.Lerp(selectColor, CastleColor, colorTimer);
        }

        spriteRenderer.sortingOrder = -(int)(transform.position.y * 1000);
        lifebar.position = transform.position + new Vector3(0f, 2.25f, 0);
        if (lifebar.Alpha > 0)
            lifebar.Alpha -= Time.deltaTime * 1.5f;
        else
            lifebar.Alpha = 0;
    }

    public void SetDamage(int damage)
    {
        if (unitParams.Life > 0)
        {
            if (Random.Range(0, 100) < unitParams.Evasion)
                anim.SetBool("Evasion", true);
            else
            {
                unitParams.Life -= damage;
                lifebar.Value = unitParams.Life;
                lifebar.Alpha = 1;
            }
        }
        if (unitParams.Life <= 0)
            anim.SetBool("Die", true);
    }

    public void Select()
    {
        spriteRenderer.material.color = selectColor;
        colorTimer = 0;
    }

    public void DisableEvasion()
    {
        anim.SetBool("Evasion", false);
    }
    public void Die()
    {
    }
}
