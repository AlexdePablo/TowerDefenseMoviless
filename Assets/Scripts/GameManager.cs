using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEditor.Searcher;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace TowerDefense
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_Instance;
        [Header("Inputs")]
        [SerializeField]
        InputActionAsset m_InputAsset;
        public InputActionAsset m_Input;

        [Header("Variables of the camera")]
        private Camera cam;
        [SerializeField]
        private int maximPosition;
        Vector3 dragOrigin;
        private bool dragCamera;

        [Header("Variables of the GUI")]
        [SerializeField]
        public float radioDeteccion = .0f;
        private int m_ManaPlayer;
        private int m_Oleada = 0;
        private int m_OleadaRecord;

        //Delegats
        public delegate void onCLicKHeroe(Dictionary<string, string> stats, HeroeScript heroe);
        public event onCLicKHeroe OnClickHeroe;
        public delegate void nextWave(int numWave, int waveRecord);
        public event nextWave OnNextWave;
        public delegate void manaChange(int numMana);
        public event manaChange OnManaChange;


        //Setters/Getters
        public static GameManager Instance
        {
            get { return m_Instance; }
        }
        public int Oleada
        {
            get { return m_Oleada; }
            set { m_Oleada = value; }
        }
        public int ManaPLayer
        {
            get { return m_ManaPlayer; }
            set { m_ManaPlayer = value; }
        }
        public int OleadaRecord
        {
            get { return m_OleadaRecord; }
           
        }

        private void Awake()
        {
            if (m_Instance == null)
                m_Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            m_Input = Instantiate(m_InputAsset);
        }
        // Start is called before the first frame update
        void Start()
        {
            m_OleadaRecord = 1;
        }
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            /*InputSystem.EnableDevice(ProximitySensor.current);
            InputSystem.EnableDevice(LightSensor.current);*/
        }
        // called second
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            m_Input.Enable();
            if (scene.name == "EscenaJuego")
            {
                m_Input.FindActionMap("Default").FindAction("LeftMouse").performed += Clicky;
                m_Input.FindActionMap("Default").FindAction("LeftMouse").canceled += noClicky;
                InitValues();
            }
            else
            {
                m_Input.FindActionMap("Default").FindAction("LeftMouse").performed -= Clicky;
                m_Input.FindActionMap("Default").FindAction("LeftMouse").canceled -= noClicky;
                m_Input.Disable();
               if(corrutina != null)
                    StopCoroutine(corrutina);
            }
        }
        private void InitValues()
        {
            m_ManaPlayer = 400;
            cam = Camera.main;
            m_Oleada = 1;
            OnNextWave?.Invoke(m_Oleada, m_OleadaRecord);
            OnManaChange?.Invoke(m_ManaPlayer);
        }
        // Update is called once per frame
        void Update()
        {
           /* var _ProximitySensor = ProximitySensor.current;
            var _dist = _ProximitySensor.distance;
            print(_dist);*/
        }
        private void LateUpdate()
        {
            
        }
        private bool clicking;
        private Coroutine corrutina;
        private void Clicky(InputAction.CallbackContext context)
        {
            clicking = true;
         

            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            if (HayPersonajeEnPunto(new Vector2(dragOrigin.x, dragOrigin.y), "Heroe"))
            {
                Collider2D[] colisiones = Physics2D.OverlapCircleAll(new Vector2(dragOrigin.x, dragOrigin.y), radioDeteccion, LayerMask.GetMask("Heroe"));
                HeroeScript manolo = colisiones[0].GetComponentInParent<HeroeScript>();
                Dictionary<string, string> StatsDictionary = manolo.HeroeStats();
                OnClickHeroe?.Invoke(StatsDictionary, manolo);
                dragCamera = false;
            }
            if (HayPersonajeEnPunto(new Vector2(dragOrigin.x, dragOrigin.y), "PlayerGui"))
            {
                dragCamera = false;
            }
            if (!HayPersonajeEnPunto(new Vector2(dragOrigin.x, dragOrigin.y), "PlayerGui") && !HayPersonajeEnPunto(new Vector2(dragOrigin.x, dragOrigin.y), "Heroe"))
                dragCamera = true;
               corrutina = StartCoroutine("pepe");
        }
        private void noClicky(InputAction.CallbackContext context)
        {
            clicking = false;
            StopCoroutine(corrutina);
            dragCamera = false;
        }
        private IEnumerator pepe()
        {
            while (clicking)
            {
                Cursor.lockState = CursorLockMode.Confined;

                if (m_Input.FindActionMap("Default").FindAction("LeftMouse").IsPressed())
                {
                    if (dragCamera)
                    {
                        Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(m_Input.FindActionMap("Default").FindAction("DragPhone").ReadValue<Vector2>());
                        cam.transform.position += difference;
                        if (cam.transform.position.x < -maximPosition)
                            cam.transform.position = new Vector3(-maximPosition, cam.transform.position.y, cam.transform.position.z);
                        if (cam.transform.position.x > maximPosition)
                            cam.transform.position = new Vector3(maximPosition, cam.transform.position.y, cam.transform.position.z);
                        if (cam.transform.position.y < -maximPosition)
                            cam.transform.position = new Vector3(cam.transform.position.x, -maximPosition, cam.transform.position.z);
                        if (cam.transform.position.y > maximPosition)
                            cam.transform.position = new Vector3(cam.transform.position.x, maximPosition, cam.transform.position.z);
                    }

                }
                yield return new WaitForEndOfFrame();
            }
        }
        public bool HayPersonajeEnPunto(Vector2 punto, String Layer)
        {
            Collider2D[] colisiones = Physics2D.OverlapCircleAll(punto, radioDeteccion, LayerMask.GetMask(Layer));
            return colisiones.Length > 0;
        }

        public void ActualizarWave()
        {
            m_Oleada++;
            if (m_Oleada > m_OleadaRecord)
                m_OleadaRecord = m_Oleada;
            m_ManaPlayer += 1000;
            OnManaChange?.Invoke(m_ManaPlayer);
            OnNextWave?.Invoke(m_Oleada, m_OleadaRecord);
        }
        private void OnDestroy()
        {
           
        }
        public void CastilloAsediado()
        {
            SceneManager.LoadScene("GameOver");
        }
        public void ChangeMana(int num)
        {
            m_ManaPlayer += num;
            if (m_ManaPlayer < 0)
                m_ManaPlayer = 0;
            OnManaChange?.Invoke(m_ManaPlayer);
        }
    }
}