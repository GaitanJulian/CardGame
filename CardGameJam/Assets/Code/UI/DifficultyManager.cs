using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private const string DifficultyKey = "CurrentDifficulty";
    private const string HardUnlockedKey = "HardUnlocked";

    private static DifficultyManager instance;

    public static DifficultyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DifficultyManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(DifficultyManager).Name;
                    instance = obj.AddComponent<DifficultyManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance of DifficultyManager.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        PlayerPrefs.SetInt(DifficultyKey, (int)difficulty);
        PlayerPrefs.Save();
    }

    public bool IsHardUnlocked()
    {
        return PlayerPrefs.GetInt(HardUnlockedKey, 0) == 1;
    }

    public void UnlockHardDifficulty()
    {
        PlayerPrefs.SetInt(HardUnlockedKey, 1);
        PlayerPrefs.Save();
    }

    public DifficultyLevel LoadDifficulty()
    {
        int difficultyValue = PlayerPrefs.GetInt(DifficultyKey, (int)DifficultyLevel.Easy);
        return (DifficultyLevel)difficultyValue;
    }
}
