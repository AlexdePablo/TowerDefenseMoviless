using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camara : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool clicking;
    private Coroutine m_Rutina;
    [SerializeField]
    InputActionAsset m_InputAsset;
    InputActionAsset m_Input;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //m_Camera = Camera.main;
        m_Input = Instantiate(m_InputAsset);
       // m_Input.FindActionMap("Default").FindAction("LeftMouse").canceled += CancelDrag;
        //m_Input.FindActionMap("Default").FindAction("LeftMouse").canceled += StartDrag;
        m_Input.Enable();
    }
   /* private void CancelDrag(InputAction.CallbackContext obj)
    {
        clicking = false;
        StopCoroutine(m_Rutina);
    }
    private void StartDrag(InputAction.CallbackContext obj)
    {
        clicking = true;
        m_Rutina = StartCoroutine(OnDrag());
    }
   

    private IEnumerator OnDrag()
    {
        yield return new WaitForSeconds(0.2f);
        while (clicking)
        {
            Vector2 drag = m_Input.FindActionMap("Default").FindAction("DragMouse").ReadValue<Vector2>();
            Vector3 dragReal = new Vector3(drag.x, drag.y, transform.position.z);
            transform.Translate(dragReal);
            yield return new WaitForFixedUpdate();
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        

        Vector3 mousePos = Input.mousePosition; 

        mousePos.x = mousePos.x / Screen.width;
        mousePos.y = mousePos.y / Screen.height;
        /*
        if (mousePos.x < 0.1f)
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime, Space.World);
        }
        if (mousePos.x > 0.9f)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (mousePos.y > 0.9f)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        if (mousePos.y < 0.1f)
        {
            transform.Translate(Vector3.up * -speed * Time.deltaTime, Space.World);
        }*/
        
    }

  
    private void OnDestroy()
    {
        //m_Input.FindActionMap("Default").FindAction("LeftMouse").canceled -= CancelDrag;
        //m_Input.FindActionMap("Default").FindAction("LeftMouse").canceled -= StartDrag;
        m_Input.Disable();
    }
}
