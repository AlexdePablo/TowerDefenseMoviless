using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense
{
    public class HeroeScript : MonoBehaviour
    {
        [SerializeField]
        Niveles m_StatsHeroe;
        private float m_Damage;
        private Transform Target;
        [SerializeField]
        private BulletsScript Bullet;
        private CircleCollider2D m_Collider;
        private List<Transform> m_EnemiesInRange = new List<Transform>();
        private Animator m_Animator;
        private int m_AnimationSpeed;

        private struct StatsPlayer
        {
            public string Name;
            public float Range;
            public float Damage;
            public float AttackSpeed;
            public bool Detection;
            public int Level;
            public int UpgradeMana;
        }
        private StatsPlayer statsPlayer;

        public Dictionary<string, string> HeroeStats()
        {
            Dictionary<string, string> stats = new Dictionary<string, string>
        {
            { "Name", statsPlayer.Name},
            { "Range", statsPlayer.Range.ToString()},
            { "Damage", statsPlayer.Damage.ToString()},
            { "AttackSpeed", statsPlayer.AttackSpeed.ToString()},
            { "Detection", statsPlayer.Detection.ToString()},
            { "Level", statsPlayer.Level.ToString()},
            { "Upgrade", statsPlayer.UpgradeMana.ToString()},
        };

            return stats;
        }
        public int getLevel()
        {
            return statsPlayer.Level;
        }
        private void Awake()
        {

            m_Animator = GetComponent<Animator>();
            m_Animator.SetBool("Idleing", true);
            m_Animator.SetBool("Arching", false);
            m_Collider = GetComponent<CircleCollider2D>();
            setStatsPlayer(m_StatsHeroe, 0);
            m_Damage = statsPlayer.Damage;
        }
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                m_EnemiesInRange.Add(collision.transform);
                Target = collision.transform;
                if (m_EnemiesInRange.Count == 1)
                {
                    m_Animator.speed = m_AnimationSpeed;
                    m_Animator.SetBool("Idleing", false);
                    m_Animator.SetBool("Arching", true);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Target = null;
                m_EnemiesInRange.RemoveAt(0);
                if (m_EnemiesInRange.Count == 0)
                {
                    m_Animator.speed = 1;
                    m_Animator.SetBool("Idleing", true);
                    m_Animator.SetBool("Arching", false);
                }
                else
                    Target = m_EnemiesInRange.First();
            }
        }
        private IEnumerator Disparar()
        {

            if (Target != null)
            {
                BulletsScript bullet = Instantiate(Bullet);
                bullet.transform.position = transform.position;
                bullet.Target = Target;
                bullet.Damage = m_Damage;
                yield return new WaitForSeconds(statsPlayer.AttackSpeed);
            }
        }
        public void setStatsPlayer(Niveles playerStats, int position)
        {
            Niveles.LevelStats stats = playerStats.m_levelStatsList[position];
            statsPlayer.Name = "Archer";
            statsPlayer.Detection = stats.DetectionLevel;
            statsPlayer.Damage = stats.DamageLevel;
            statsPlayer.Range = stats.RangeLevel;
            statsPlayer.AttackSpeed = stats.AttackSpeedLevel;
            m_Collider.radius = statsPlayer.Range;
            statsPlayer.Level = position + 1;
            statsPlayer.UpgradeMana = stats.upgradeManaLevel;
            m_AnimationSpeed = statsPlayer.Level;
        }
    }
}