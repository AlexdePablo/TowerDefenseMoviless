using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    private Pool m_Owner;
    public void SetPool(Pool owner)
    {
        m_Owner = owner;
    }

    private void OnDisable()
    {
        if (!m_Owner.ReturnElement(gameObject))
            Debug.LogError(gameObject + "Pool Return error");
    }

}
