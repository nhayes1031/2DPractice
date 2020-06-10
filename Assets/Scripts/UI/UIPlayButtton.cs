using UnityEngine;

public class UIPlayButtton : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.MoveToNextLevel();
    }
}
