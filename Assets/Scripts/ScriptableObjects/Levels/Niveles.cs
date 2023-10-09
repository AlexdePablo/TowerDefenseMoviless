using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelStats", menuName = "ScriptableObjects/LevelStats")]
public class Niveles : ScriptableObject
{
    public List<LevelStats> m_levelStatsList;

    [Serializable]
    public struct LevelStats{
        public float RangeLevel;
        public float DamageLevel;
        public float AttackSpeedLevel;
        public bool DetectionLevel;
        public int upgradeManaLevel;
    }
}
