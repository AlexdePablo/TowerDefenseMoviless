using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotnesGameOver : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        textMeshProUGUI.text = "Has mort a la Wave: " + gameManager.Oleada + " i el record es: " + gameManager.OleadaRecord;
    }
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene("EscenaJuego");
    }

}
