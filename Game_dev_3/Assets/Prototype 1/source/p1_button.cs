using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.MaterialProperty;

public class p1_button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void onClick()
    {
        SceneManager.LoadScene("Prototype 2");
    }
}
