using UnityEngine;
using UnityEngine.SceneManagement;

public enum CHOICE
{
    None,
    CHOICE1,
    CHOICE2
}

public class Choices : MonoBehaviour
{
    public CHOICE myChoice = CHOICE.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ClickOnMe()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Dialouge_Begins" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("branch_1_1");
        }

    }

    public void Consequences()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Check by scene name
        if (currentScene.name == "Dialouge_Begins" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("branch_1_1");
        }

        // Check by scene name
        if (currentScene.name == "Dialouge_Begins" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("branch_2_1");
        }
    }


}
