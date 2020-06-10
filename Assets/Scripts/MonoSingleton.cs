using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = FindObjectOfType(typeof(T)) as T;
        Instance.Init();
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void Init() { }

    private void OnApplicationQuit()
    {
        Instance = null;
    }
}