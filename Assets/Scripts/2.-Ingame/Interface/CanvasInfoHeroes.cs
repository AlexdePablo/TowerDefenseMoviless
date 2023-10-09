using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class CanvasInfoHeroes : MonoBehaviour
    {
        [SerializeField]
        private GameObject panelStatsPlayer;
        [SerializeField]
        private TextMeshProUGUI NameText;
        [SerializeField]
        private TextMeshProUGUI DamageText;
        [SerializeField]
        private TextMeshProUGUI AttackSpeedText;
        [SerializeField]
        private TextMeshProUGUI RangeText;
        [SerializeField]
        private TextMeshProUGUI DetectionText;
        [SerializeField]
        private TextMeshProUGUI LevelText;
        GameManager gameManager;
        [SerializeField]
        Niveles archerLevels;
        HeroeScript m_Heroe;
        [SerializeField]
        private GameObject canvasBotonesJugadores;
        [SerializeField]
        private TextMeshProUGUI ButtonLevelUpgradeText;
        [SerializeField]
        private Button buttonUpgrade;
        private int manaRest;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.Instance;
            gameManager.OnClickHeroe += GetStatsHeroe;
        }

        public void HideStats()
        {
            canvasBotonesJugadores.SetActive(true);
            panelStatsPlayer.SetActive(false);
        }

        private void GetStatsHeroe(Dictionary<string, string> stats, HeroeScript heroe)
        {
            NameText.text = "Name: ";
            DamageText.text = "Damage: ";
            AttackSpeedText.text = "Attack Speed: ";
            RangeText.text = "Range: ";
            DetectionText.text = "Detection: ";
            LevelText.text = "Level: ";
            ButtonLevelUpgradeText.text = "";
            panelStatsPlayer.SetActive(true);
            canvasBotonesJugadores.SetActive(false);
            foreach (KeyValuePair<string, string> stat in stats)
            {
                switch (stat.Key)
                {
                    case "Name":
                        NameText.text += stat.Value;
                        break;
                    case "Range":
                        DamageText.text += stat.Value;
                        break;
                    case "Damage":
                        AttackSpeedText.text += stat.Value;
                        break;
                    case "AttackSpeed":
                        RangeText.text += stat.Value;
                        break;
                    case "Detection":
                        DetectionText.text += stat.Value;
                        break;
                    case "Level":
                        LevelText.text += stat.Value;
                        break;
                    case "Upgrade":
                        ButtonLevelUpgradeText.text = stat.Value;
                        manaRest = int.Parse(stat.Value);
                        if (int.Parse(stat.Value) > gameManager.ManaPLayer)
                            buttonUpgrade.interactable = false;
                        else
                            buttonUpgrade.interactable = true;
                        break;
                }
            }
            m_Heroe = heroe;
        }

        public void UpgradeHeroe()
        {
            m_Heroe.setStatsPlayer(archerLevels, m_Heroe.getLevel());
            m_Heroe.GetComponentInChildren<TextMesh>().text = "Level " + m_Heroe.getLevel();
            HideStats();
            gameManager.ChangeMana(-manaRest);
        }
        // Update is called once per frame
        void Update()
        {

        }
        private void OnDisable()
        {
            gameManager.OnClickHeroe -= GetStatsHeroe;
        }
    }
}