using UnityEngine;

public class MoveCommand : ICommand
{
    private readonly CommandData _data;
    private Vector2 _previousPosition;

    public MoveCommand(CommandData data)
    {
        _data = data;
    }

    public void Execute()
    {
        if (_data.Target != null)
        {
            _previousPosition = _data.Target.transform.position;
            _data.Target.transform.position = _data.Position;
            Debug.Log($"Moved to {_data.Position}");
        }
    }

    public void Undo()
    {
        if (_data.Target != null)
        {
            _data.Target.transform.position = _previousPosition;
            Debug.Log("Undo move");
        }
    }
}