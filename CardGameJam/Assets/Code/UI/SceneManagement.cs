using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private static SceneManagement instance;

    public static SceneManagement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneManagement>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(SceneManagement).Name;
                    instance = obj.AddComponent<SceneManagement>();
                }
            }

            return instance;
        }
    }

    public void LoadCardGame()
    {
        SceneManager.LoadScene("CardGameScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
