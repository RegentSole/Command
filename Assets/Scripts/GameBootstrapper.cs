using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private InputListener inputListener;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnPrefab;

    private void Awake()
    {
        var invoker = new CommandProcessor();
        inputListener.Initialize(invoker, player, spawnPrefab);
    }
}