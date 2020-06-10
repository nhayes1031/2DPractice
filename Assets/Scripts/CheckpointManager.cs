using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint[] _checkpoints;

    private void Start()
    {
        _checkpoints = GetComponentsInChildren<Checkpoint>();
    }

    public Checkpoint GetLastCheckpointThatWasPassed()
    {
        return _checkpoints.Last(t => t.Passed);
    }
}
