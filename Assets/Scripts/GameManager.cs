using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int Lives { get; private set; }
    public int Coins { get; private set; }

    private int currentLevelIndex;

    public event Action<int> OnLivesChanged;
    public event Action<int> OnCoinsChanged;

    protected override void Init()
    {
        RestartGame();
    }

    public void KillPlayer()
    {
        Lives--;
        OnLivesChanged?.Invoke(Lives);

        if (Lives <= 0)
            RestartGame();
        else
            SendPlayerToCheckpoint();
    }

    private void SendPlayerToCheckpoint()
    {
        var checkpointManager = FindObjectOfType<CheckpointManager>();
        var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();
        var player = FindObjectOfType<PlayerMovementController>();

        player.transform.position = checkpoint.transform.position;
    }

    public void AddCoin()
    {
        Coins++;
        OnCoinsChanged?.Invoke(Coins);
    }

    public void MoveToNextLevel()
    {
        currentLevelIndex++;
        SceneManager.LoadScene(currentLevelIndex);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    private void RestartGame()
    {
        currentLevelIndex = 0;

        Lives = 3;
        Coins = 0;

        SceneManager.LoadScene(currentLevelIndex);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
}
