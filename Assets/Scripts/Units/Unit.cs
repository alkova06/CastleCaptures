using UnityEngine;
public class Unit
{
    public enum UnitType : int
    {
        Knight = 0,
        Bandit = 1,
        Archer = 2,
        Redneck = 3,
        Helper = 4
    }

    public UnitType Type;

    public int Life;
    public int Damage;
    public float AttackSpeed;
    public int Evasion;
    public int RandomDamage;
    public int Accuracy;

    public GameObject GameObject;

    public Unit()
    {
    }
    public Unit(UnitType Type, int Life, int Damage, float AttackSpeed, int Evasion, int RandomDamage, int Accuracy, GameObject GameObject)
    {
        this.Type = Type;
        this.Life = Life;
        this.Damage = Damage;
        this.AttackSpeed = AttackSpeed;
        this.Evasion = Evasion;
        this.RandomDamage = RandomDamage;
        this.Accuracy = Accuracy;
        this.GameObject = GameObject;
    }
}
