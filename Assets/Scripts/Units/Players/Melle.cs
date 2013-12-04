using UnityEngine;
using System.Collections;

public class Melle : MonoBehaviour
{
    public enum State
    {
        Idle,
        WalkToPoint,
        WalkToEnemie,
        Attack,
        Die
    }

    Animator anim;
    SpriteRenderer spriteRenderer;

    public Unit.UnitType type;
    Unit unitParams;
    State state;

    public GameObject LifebarGO;
    LifeBar lifebar;

    Vector2 startWalkPoint;
    Vector2 endWalkPoint;
    float walkDistance;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        unitParams = UnitContainer.Instance.GetUnit(type);
        state = State.Idle;

        GameObject lifebarGOClone = (GameObject)Instantiate(LifebarGO);
        lifebar = lifebarGOClone.GetComponent<LifeBar>();
        lifebar.Alpha = 0;
        lifebar.Value = unitParams.Life;
        lifebar.MaxValue = unitParams.Life;
        lifebar.drawOrder = spriteRenderer.sortingOrder + 1;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (transform.position.x < -9.0f)
                {
                    startWalkPoint = new Vector2(transform.position.x, transform.position.y);
                    endWalkPoint = EngineHelper.PointOnRectangle(Ground.Instance.GroundRect);
                    walkDistance = Vector2.Distance(startWalkPoint, endWalkPoint);
                    state = State.WalkToPoint;
                }
                break;
            case State.WalkToPoint:
                Vector2 translate = endWalkPoint - startWalkPoint;
                translate.Normalize();
                translate *= Time.deltaTime * 2;
                transform.position += new Vector3(translate.x, translate.y, 0);

                if (walkDistance > Vector2.Distance(startWalkPoint, new Vector2(transform.position.x, transform.position.y)))
                    anim.SetBool("Walk", true);
                else
                {
                    anim.SetBool("Walk", false);
                    transform.position = new Vector3(endWalkPoint.x, endWalkPoint.y, transform.position.z);
                    state = State.Idle;
                }
                break;
        }

        spriteRenderer.sortingOrder = -(int)(transform.position.y * 1000);

        lifebar.position = transform.position + new Vector3(0f, 2.25f, 0);
        if (lifebar.Alpha > 0)
            lifebar.Alpha -= Time.deltaTime * 1.5f;
        else
            lifebar.Alpha = 0;

        if (Input.GetKeyDown(KeyCode.Space))
            SetDamage(10);
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

    public void DisableEvasion()
    {
        anim.SetBool("Evasion", false);
    }
    public void Die()
    {
    }
}
