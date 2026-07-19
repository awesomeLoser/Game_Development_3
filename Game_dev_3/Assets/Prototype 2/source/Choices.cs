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
        Debug.Log($"Clicked. Scene: {currentScene.name}, Choice: {myChoice}");


        if (currentScene.name == "Dialouge_Begins" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("1_1");
        }
        if (currentScene.name == "Dialouge_Begins" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("1_2");
        }

        if (currentScene.name == "1_1" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("1_1_1");
        }
        if (currentScene.name == "1_1" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("1_1_2");
        }

        if (currentScene.name == "1_1_1" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("1_com");
        }
        if (currentScene.name == "1_1_1" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("bad_end");
        }

        if (currentScene.name == "1_1_2" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("bad_end");
        }
        if (currentScene.name == "1_1_2" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("1_com");
        }

        if (currentScene.name == "1_2" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("1_2_1");
        }
        if (currentScene.name == "1_2" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("1_2_2");
        }

        if (currentScene.name == "1_2_1" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("1_com");
        }
        if (currentScene.name == "1_2_1" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("bad_end");
        }

        if (currentScene.name == "1_2_2" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("bad_end");
        }
        if (currentScene.name == "1_2_2" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("1_com");
        }

        if (currentScene.name == "1_com" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("1_com_1");
        }
        if (currentScene.name == "1_com" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("1_com_2");
        }

        if (currentScene.name == "1_com_1" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("bad_end");
        }
        if (currentScene.name == "1_com_1" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("1_com_com");
        }

        if (currentScene.name == "1_com_2" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("1_com_com");
        }
        if (currentScene.name == "1_com_2" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("bad_end");
        }

        if (currentScene.name == "1_com_com" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("good_end");
        }
        if (currentScene.name == "1_com_com" && myChoice == CHOICE.CHOICE2)
        {
            SceneManager.LoadScene("bad_end");
        }

        if (currentScene.name == "bad_end" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("Dialouge_Begins");
        }

        if (currentScene.name == "good_end" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("Dialouge_Begins");
        }
              if (currentScene.name == "timeWin" && myChoice == CHOICE.CHOICE1)
        {
            SceneManager.LoadScene("MainLevelTime");
        }
    }

    public void Consequences()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Check by scene name
        if (currentScene.name == "Dialouge_Begins" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("1_1");
        }
        if (currentScene.name == "Dialouge_Begins" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("1_2");
        }

        if (currentScene.name == "1_1" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("1_1_1");
        }
        if (currentScene.name == "1_1" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("1_1_2");
        }

        if (currentScene.name == "1_1_1" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("1_com");
        }
        if (currentScene.name == "1_1_1" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("bad_end");
        }

        if (currentScene.name == "1_1_2" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("bad_end");
        }
        if (currentScene.name == "1_1_2" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("1_com");
        }

        if (currentScene.name == "1_2" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("1_2_1");
        }
        if (currentScene.name == "1_2" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("1_2_2");
        }

        if (currentScene.name == "1_2_1" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("1_com");
        }
        if (currentScene.name == "1_2_1" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("bad_end");
        }

        if (currentScene.name == "1_2_2" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("bad_end");
        }
        if (currentScene.name == "1_2_2" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("1_com");
        }

        if (currentScene.name == "1_com" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("1_com_1");
        }
        if (currentScene.name == "1_com" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("1_com_2");
        }

        if (currentScene.name == "1_com_1" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("bad_end");
        }
        if (currentScene.name == "1_com_1" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("1_com_com");
        }

        if (currentScene.name == "1_com_2" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("1_com_com");
        }
        if (currentScene.name == "1_com_2" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("bad_end");
        }

        if (currentScene.name == "1_com_com" && gameObject.name == "Choice 1")
        {
            SceneManager.LoadScene("good_end");
        }
        if (currentScene.name == "1_com_com" && gameObject.name == "Choice 2")
        {
            SceneManager.LoadScene("bad_end");
        }


    }

}
