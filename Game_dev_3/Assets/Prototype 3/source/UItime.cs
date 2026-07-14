using TMPro;
using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float cooldownOrFreezeTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownOrFreezeTime == 1)
        {
            textComponent.text = Mathf.CeilToInt(PlayerMovement.timeCooldown).ToString();
        }
        else if (cooldownOrFreezeTime == 2)
        {
            textComponent.text = Mathf.CeilToInt(PlayerMovement.timeTimer).ToString();
        }
    }

}
