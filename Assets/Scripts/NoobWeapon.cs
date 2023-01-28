using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobWeapon : Weapon
{
    public override float baseDamages
    {
        get
        {
            if (Random.Range(0, 100) < 25)
                return Random.Range(16.0f, 24.0f);
            else
                return Random.Range(8.0f, 12.0f);
        }
    }

    public override void damageTarget(BasicBehavior target, float damage)
    {
        target.TakeDamage(damage);
    }
}
