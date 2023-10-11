using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button hardButton;

    private void OnEnable()
    {
 
        if (hardButton != null)
        {
            // Check if "Hard" difficulty is unlocked.
            bool isHardUnlocked = DifficultyManager.Instance.IsHardUnlocked();
            // Set the button's interactable state based on the result.
            hardButton.interactable = isHardUnlocked;
        } 
    }

    public void SetAdvancedDifficulty()
    {
        DifficultyManager.Instance.SetDifficulty(DifficultyLevel.Normal);
    }

    public void SetHardDifficulty()
    {
        DifficultyManager.Instance.SetDifficulty(DifficultyLevel.Hard);
    }

    public void SetNormalDifficulty()
    {
        DifficultyManager.Instance.SetDifficulty(DifficultyLevel.Easy);
    }

}