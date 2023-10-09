using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "ScriptableObjects/Enemies")]
public class Enemies : ScriptableObject
{
    public string Name;
    public float Hp;
    public float Velocity;
}
