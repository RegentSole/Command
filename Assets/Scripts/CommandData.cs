using UnityEngine;

/// <summary>
/// Неизменяемые данные, необходимые для выполнения команд.
/// </summary>
public class CommandData
{
    public Vector2 Position { get; }
    public GameObject Target { get; }
    public GameObject Prefab { get; }

    public CommandData(Vector2 position, GameObject target = null, GameObject prefab = null)
    {
        Position = position;
        Target = target;
        Prefab = prefab;
    }
}