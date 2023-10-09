using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heroes", menuName = "ScriptableObjects/Heroes")]
public class Heroes : ScriptableObject
{
    public string Name;
    public float Range;
    public float Damage;
    public float AttackSpeed;
    public bool Detection;
    public int Level;
}
