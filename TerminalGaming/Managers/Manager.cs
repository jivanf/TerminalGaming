using UnityEngine;

namespace TerminalGaming.Managers;

/**
 * Singleton class.
 */
public abstract class Manager<TManager> : MonoBehaviour where TManager : Manager<TManager>
{
    private static string Name { get; } = "TerminalGaming_" + typeof(TManager).Name;

    public static TManager Instance => Instantiate<TManager>(Name);

    // public void Awake()
    // {
    //     if (Instance != null)
    //     {
    //         Destroy(this);
    //     }
    // }

    private static TInstantiatedManager Instantiate<TInstantiatedManager>(string name)
        where TInstantiatedManager : MonoBehaviour
    {
        GameObject? gameObject = GameObject.Find(name);

        if (gameObject is null)
        {
            return new GameObject(name, [typeof(TManager)]).GetComponent<TInstantiatedManager>();
        }

        // ReSharper disable once InvertIf
        if (gameObject.GetComponent<TInstantiatedManager>() is null)
        {
            gameObject.AddComponent<TInstantiatedManager>();
        }

        return gameObject.GetComponent<TInstantiatedManager>();
    }
}
