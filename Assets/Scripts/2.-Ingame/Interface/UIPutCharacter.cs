using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;

public class UIPutCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject ArcherUI;
    [SerializeField]
    private GameObject Archer;
    private Camera cam;
    public float radioDeteccion = .0f;
    private bool canPutCharacter;
    [SerializeField]
    private Tilemap m_Tilemap;
    [SerializeField]
    private Tile[] m_ListaTilesBetadas;
    GameObject archer;

    [Header("Variables of GUI")]
    [SerializeField]
    private GameObject panelPutCharacters;
    [SerializeField]
    private GameObject panelAcceptCancel;
    private GameManager gameManager;
    private InputActionAsset m_Input;
    public bool HayPersonajeEnPunto(Vector2 punto, string layer)
    {
        Collider2D[] colisiones = Physics2D.OverlapCircleAll(punto, radioDeteccion, LayerMask.GetMask(layer));
        return colisiones.Length > 0;
    }
    public void putArcher()
    {
        StartCoroutine("PuttingArcher");
        panelPutCharacters.SetActive(false);
        panelAcceptCancel.SetActive(true);
    }
    private void Awake()
    {
        gameManager = GameManager.Instance;
        m_Input = gameManager.m_Input;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator PuttingArcher()
    {
        // yield return new WaitForSeconds(.3f);
        archer = Instantiate(ArcherUI);
        archer.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);
        while (true)
        {
            //Debug.Log( m_Tilemap.GetTile(new Vector3Int((int)point.x, (int)point.y, 0)));
            if (m_Input.FindActionMap("Default").FindAction("LeftMouse").IsPressed() && HayPersonajeEnPunto(new Vector2(cam.ScreenToWorldPoint(m_Input.FindActionMap("Default").FindAction("DragPhone").ReadValue<Vector2>()).x, cam.ScreenToWorldPoint(m_Input.FindActionMap("Default").FindAction("DragPhone").ReadValue<Vector2>()).y), "PlayerGui"))
            {
                archer.transform.position = new Vector3(cam.ScreenToWorldPoint(m_Input.FindActionMap("Default").FindAction("DragPhone").ReadValue<Vector2>()).x, cam.ScreenToWorldPoint(m_Input.FindActionMap("Default").FindAction("DragPhone").ReadValue<Vector2>()).y, 0);
            }
            if (HayPersonajeEnPunto(new Vector2(archer.transform.position.x, archer.transform.position.y), "Heroe"))
            {
                archer.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                archer.GetComponent<SpriteRenderer>().color = Color.green;
            }
            for (int i = 0; i < m_ListaTilesBetadas.Length; i++)
            {
                if (m_Tilemap.GetTile(new Vector3Int((int)archer.transform.position.x, (int)archer.transform.position.y, 0)) == m_ListaTilesBetadas[i])
                {
                    archer.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
                else if (m_Tilemap.GetTile(new Vector3Int((int)archer.transform.position.x, (int)archer.transform.position.y, 0)) == null)
                {
                    archer.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
            }
            yield return new WaitForFixedUpdate();
        }

    }

    public void PutCharacter()
    {
        if(archer.GetComponent<SpriteRenderer>().color == Color.green && gameManager.ManaPLayer >= 100)
        {
            GameObject trueArcher = Instantiate(Archer);
            trueArcher.transform.position = new Vector3(archer.transform.position.x, archer.transform.position.y, 0);
            StopAllCoroutines();
            Destroy(archer);
            gameManager.ChangeMana(-100);
        }
        else
        {
            StopAllCoroutines();
            Destroy(archer);
        }
        panelPutCharacters.SetActive(true);
        panelAcceptCancel.SetActive(false);
    }
    public void CancelCharacter()
    {
        StopAllCoroutines();
        Destroy(archer);
        panelPutCharacters.SetActive(true);
        panelAcceptCancel.SetActive(false);
    }
}
