using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обработчик команд. Хранит очередь и историю. Не MonoBehaviour.
/// </summary>
public class CommandProcessor
{
    private readonly Queue<ICommand> _rightClickQueue = new Queue<ICommand>();
    private readonly List<ICommand> _history = new List<ICommand>();
    private const int MaxHistorySize = 10;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        AddToHistory(command);
    }

    public void AddToRightClickQueue(ICommand command)
    {
        _rightClickQueue.Enqueue(command);
        Debug.Log($"Added to right-click queue. Queue size: {_rightClickQueue.Count}");
    }

    public void ExecuteRightClickQueue()
    {
        if (_rightClickQueue.Count == 0)
        {
            Debug.Log("Right-click queue is empty");
            return;
        }

        Debug.Log($"Executing {_rightClickQueue.Count} right-click commands");
        while (_rightClickQueue.Count > 0)
        {
            var command = _rightClickQueue.Dequeue();
            command.Execute();
            AddToHistory(command);
        }
    }

    public void Undo()
    {
        if (_history.Count == 0)
        {
            Debug.Log("Nothing to undo");
            return;
        }

        var lastCommand = _history[^1];
        lastCommand.Undo();
        _history.RemoveAt(_history.Count - 1);
        Debug.Log($"Undo performed. History size: {_history.Count}");
    }

    private void AddToHistory(ICommand command)
    {
        _history.Add(command);
        if (_history.Count > MaxHistorySize)
        {
            _history.RemoveAt(0);
            Debug.Log($"History limited to {MaxHistorySize} commands");
        }
    }

    public int RightClickQueueSize => _rightClickQueue.Count;
    public int HistorySize => _history.Count;
}