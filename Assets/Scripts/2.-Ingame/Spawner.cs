using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefense
{
    public class Spawner : MonoBehaviour
    {
        [Header("Lists of enemies")]
        [SerializeField]
        private List<GameObject> enemies = new List<GameObject>();
        [SerializeField]
        private List<GameObject> enemiesFinalWave = new List<GameObject>();

        [Header("SpawnRate")]
        [SerializeField]
        private float m_SpawnRate = 0.2f;

        [Header("Points of the spawns")]
        [SerializeField]
        private Transform[] m_SpawnPoints;

        [Header("Tilemap of the pathfinding")]
        [SerializeField]
        private Tilemap m_Tilemap;

        [Header("Number of enemies of each wave")]
        [SerializeField]
        private int enemiesAtWave = 3;
        private int enemiesToSpawn;

        void Start()
        {
           NewWave();   
        }
        public void NewWave()
        {
            enemiesAtWave += 2;
            enemiesToSpawn = enemiesAtWave;
            StartCoroutine(SpawnCoroutine());
        }
        IEnumerator SpawnCoroutine()
        {
            while (enemiesToSpawn > 0)
            {
                if(enemiesToSpawn == 1)
                {
                    GameObject spawned = Instantiate(enemiesFinalWave[Random.Range(0, enemiesFinalWave.Count)]);
                    spawned.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
                    spawned.GetComponent<Enemy>().SetTileMap(m_Tilemap);
                }
                else
                {
                    GameObject spawned = Instantiate(enemies[Random.Range(0, enemies.Count)]);
                    spawned.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
                    spawned.GetComponent<Enemy>().SetTileMap(m_Tilemap);
                }
                enemiesToSpawn--;
                yield return new WaitForSeconds(m_SpawnRate);
            }
        }
    }
}