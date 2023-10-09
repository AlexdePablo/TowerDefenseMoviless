using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject PoolableObject;
    public int Capacity = 10;

    //es pot fer amb una Poolable
    //que gestioni que l'element pot estar en
    //pool i facilita el tornar
    private List<GameObject> m_Pool;

    private void Awake()
    {
        //Comprovar que al PoolableObject hi hagi
        //component de Poolable

        //inicialitzem la llista de valors (m_Pool)
        //element = Instantiate(PoolableObject, gameObject);
        //element.GetComponent<Poolable>().SetPool(this);
    }

    public GameObject GetElement()
    {
        //busquem el primer element disponible
        //gameObject.activeInHierarchy == false
        //l'activem i el retornem
        //NO EL TRAIEM DE LA LLISTA/ARRAY
        return null;
    }

    public bool ReturnElement(GameObject element)
    {
        //Comprovar que originalment teniem l'element
        //true si s'ha tornat
        return true;
    }

}
