using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

namespace TowerDefense
{
    public class EnemyWaveBoss : Enemy
    {
       
        [SerializeField]
        GameEvent m_gameEvent;
        private void Awake()
        {
            m_manager = GameManager.Instance;
            m_Target = FindAnyObjectByType<CastleManager>().transform;
            m_Rb = GetComponent<Rigidbody2D>();
            m_Text = GetComponentInChildren<TextMesh>();
        }
        // Start is called before the first frame update
        void Start()
        {
            HPBueno = m_enemyStats.Hp;
            HP = HPBueno;
            m_Text.text = HP + "/" + HPBueno;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public new void SetTileMap(Tilemap tilemap)
        {
            m_Tilemap = tilemap;
            SetPath();
        }
        public new void setVida(float numVida)
        {
            HP -= numVida;
            int manaSum = (int)(numVida / 2);
            m_manager.ChangeMana(manaSum);
            m_Text.text = HP + "/" + HPBueno;
            if (HP <= 0)
            {
                Destroy(this.gameObject);
                m_gameEvent.Raise();
            }
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
    }
}