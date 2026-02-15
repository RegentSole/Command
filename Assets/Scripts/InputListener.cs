using UnityEngine;

public class InputListener : MonoBehaviour
{
    private GameObject _player;
    private GameObject _spawnPrefab;
    private CommandProcessor _invoker;

    public void Initialize(CommandProcessor invoker, GameObject player, GameObject spawnPrefab)
    {
        _invoker = invoker;
        _player = player;
        _spawnPrefab = spawnPrefab;
    }

    private void Update()
    {
        if (_invoker == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            var pos = GetMouseWorldPosition();
            var data = new CommandData(pos, target: _player);
            var command = new MoveCommand(data);
            _invoker.ExecuteCommand(command);
        }

        if (Input.GetMouseButtonDown(1))
        {
            var pos = GetMouseWorldPosition();
            var data = new CommandData(pos, prefab: _spawnPrefab);
            var command = new SpawnCommand(data);
            _invoker.AddToRightClickQueue(command);
        }

        if (Input.GetMouseButtonDown(2))
        {
            _invoker.Undo();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _invoker.ExecuteRightClickQueue();
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}