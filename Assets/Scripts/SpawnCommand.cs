using UnityEngine;

public class SpawnCommand : ICommand
{
    private readonly CommandData _data;
    private GameObject _spawnedObject;

    public SpawnCommand(CommandData data)
    {
        _data = data;
    }

    public void Execute()
    {
        if (_data.Prefab != null)
        {
            _spawnedObject = Object.Instantiate(_data.Prefab, _data.Position, Quaternion.identity);
            Debug.Log($"Spawned at {_data.Position}");
        }
    }

    public void Undo()
    {
        if (_spawnedObject != null)
        {
            Object.Destroy(_spawnedObject);
            Debug.Log("Undo spawn");
        }
    }
}