using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleManager : MonoBehaviour
{
    private AudioSource m_Audio;

    private void Awake()
    {
        m_Audio = GetComponent<AudioSource>();  
        m_Audio.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            m_Audio.Play();
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
