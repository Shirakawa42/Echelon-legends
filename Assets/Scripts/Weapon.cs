using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract float baseDamages { get; }
    public abstract void damageTarget(BasicBehavior target, float damage);
}
