using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        protected Transform m_Target;
        protected Rigidbody2D m_Rb;
        [SerializeField]
        protected Enemies m_enemyStats;
        protected float HP;
        protected GameManager m_manager;
        protected List<Vector3> m_Cells;
        protected Tilemap m_Tilemap;
        protected float HPBueno;
        protected TextMesh m_Text;
        private void Awake()
        {
            m_manager = GameManager.Instance;
            m_Target = FindAnyObjectByType<CastleManager>().transform;
            m_Rb = GetComponent<Rigidbody2D>();
            m_Text = GetComponentInChildren<TextMesh>();
            m_manager.OnNextWave += BuffVida;
        }

        private void BuffVida(int wave, int waveMaxima)
        {
            HPBueno = m_enemyStats.Hp * (1 + wave * 0.1f);
        }

        // Start is called before the first frame update
        void Start()
        {
            HPBueno = m_enemyStats.Hp;
            HP = HPBueno;
            m_Text.text = HP + "/" + HPBueno;
            //print(HP);
            //m_Rb.velocity = ((m_Target.position - transform.position).normalized) * m_enemyStats.Velocity;

        }
        public void SetTileMap(Tilemap tilemap)
        {
            m_Tilemap = tilemap;
            SetPath();
        }

        private void SetPath()
        {
            Pathfinding.Instance.FindPathWorldSpace(m_Tilemap.WorldToCell(transform.position), m_Tilemap.WorldToCell(m_Target.position), out m_Cells);
            StartCoroutine(MoveEnemy());
        }
        private IEnumerator MoveEnemy()
        {
            foreach (Vector3 tile in m_Cells)
            {
                m_Rb.velocity = (tile - transform.position).normalized * m_enemyStats.Velocity;
                while (Vector3.Distance(transform.position, tile) >= m_enemyStats.Velocity * Time.fixedDeltaTime)
                    yield return new WaitForFixedUpdate();
            }
            m_Rb.velocity = Vector2.zero;
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void setVida(float numVida)
        {
            if(this.gameObject != null)
            {
                HP -= numVida;
                int manaSum = (int)(numVida / 2);
                m_manager.ChangeMana(manaSum);
                m_Text.text = HP + "/" + HPBueno;
                if (HP <= 0 && this.gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        private void OnDisable()
        {
            m_manager.OnNextWave -= BuffVida;
        }
    }
}