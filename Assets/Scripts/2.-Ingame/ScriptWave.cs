using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class ScriptWave : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_textoWave;
        [SerializeField]
        private TextMeshProUGUI m_numeroMana;
        [SerializeField]
        private TextMeshProUGUI m_RecordWave;
        GameManager gameManager;
        // Start is called before the first frame update
        void Awake()
        {
            gameManager = GameManager.Instance;
            gameManager.OnNextWave += UpdateWaveText;
            gameManager.OnManaChange += UpdateMana;
        }

        private void UpdateWaveText(int numWave, int RecordWave)
        {
            m_textoWave.text = "Wave: " + numWave;
            m_RecordWave.text = "Record: " + RecordWave;
        }
        private void UpdateMana(int numMana)
        {
            m_numeroMana.text = numMana.ToString();
        }
        private void OnDestroy()
        {
            gameManager.OnNextWave -= UpdateWaveText;
            gameManager.OnManaChange -= UpdateMana;
        }
    }
}