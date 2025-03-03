using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackObject", menuName = "ScriptableObjects/Attack", order = 1)]
public class AttackObject : ScriptableObject
{
    public int damage;
    public float cooldown;
    public float launchForce;
}