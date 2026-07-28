using UnityEngine;

public class playerCombat : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
    }

    void Attack()
    {
        //play atack animation
        //detect enemies in range of attack
        //damage them
    }
}
