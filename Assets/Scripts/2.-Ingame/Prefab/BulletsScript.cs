using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BulletsScript : MonoBehaviour
    {
        private Transform m_Target;
        private Rigidbody2D Rigidbody2D;
        private float m_Damage;
        private float m_StateDeltaTime;
        public float Damage
        {
            get { return m_Damage; }
            set { m_Damage = value; }
        }
        public Transform Target
        {
            get { return m_Target; }
            set { m_Target = value; }
        }
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            m_StateDeltaTime = 0;
            
        }

        // Update is called once per frame
        void Update()
        {
            m_StateDeltaTime += Time.deltaTime;
            if (m_StateDeltaTime > 2.0f)
                Destroy(gameObject);
            if (m_Target != null)
            {
                Rigidbody2D.velocity = new Vector2(Target.position.x - transform.position.x, Target.position.y - transform.position.y).normalized * 15;
                transform.right = new Vector3(Target.position.x - transform.position.x, Target.position.y - transform.position.y, 0).normalized;

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerMask.NameToLayer("Enemy") == collision.gameObject.layer)
            {
                if (collision.gameObject.GetComponent<EnemyWaveBoss>())
                    collision.gameObject.GetComponent<EnemyWaveBoss>().setVida(m_Damage);
                else
                    collision.gameObject.GetComponent<Enemy>().setVida(m_Damage);

                Destroy(this.gameObject);
            }

        }
    }
}