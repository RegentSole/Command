using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    // Очереди команд
    private Queue<ICommand> _leftClickCommands = new Queue<ICommand>();
    private Queue<ICommand> _rightClickCommands = new Queue<ICommand>();
    
    // История выполненных команд для отмены
    private Stack<ICommand> _executedCommands = new Stack<ICommand>();
    private const int MAX_HISTORY_SIZE = 10;
    
    // Ссылки на объекты
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnPrefab;
    
    // Флаги состояний
    private bool _isRightClickMode = false;
    private List<ICommand> _pendingRightClickCommands = new List<ICommand>();
    
    private void Update()
    {
        HandleInput();
    }
    
    private void HandleInput()
    {
        // ЛКМ - немедленное выполнение MoveCommand
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ExecuteLeftClickCommand(mousePos);
        }
        
        // ПКМ - добавление команды в очередь правого клика
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AddRightClickCommand(mousePos);
        }
        
        // Колесо мыши - отмена последней команды
        if (Input.GetMouseButtonDown(2))
        {
            Undo();
        }
        
        // Enter - выполнение всей очереди правого клика
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteRightClickQueue();
        }
    }
    
    private void ExecuteLeftClickCommand(Vector2 position)
    {
        var commandData = new CommandData
        {
            Position = position,
            Target = player,
            Prefab = spawnPrefab
        };
        
        var moveCommand = new MoveCommand(commandData);
        moveCommand.Execute();
        
        // Добавляем в историю
        AddToHistory(moveCommand);
    }
    
    private void AddRightClickCommand(Vector2 position)
    {
        var commandData = new CommandData
        {
            Position = position,
            Target = player,
            Prefab = spawnPrefab
        };
        
        var spawnCommand = new SpawnCommand(commandData);
        _rightClickCommands.Enqueue(spawnCommand);
        
        Debug.Log($"Added spawn command to queue. Queue size: {_rightClickCommands.Count}");
    }
    
    private void ExecuteRightClickQueue()
    {
        Debug.Log($"Executing right-click queue ({_rightClickCommands.Count} commands)");
        
        while (_rightClickCommands.Count > 0)
        {
            var command = _rightClickCommands.Dequeue();
            command.Execute();
            
            // Сохраняем команды для возможной отмены
            _pendingRightClickCommands.Add(command);
            AddToHistory(command);
        }
        
        _pendingRightClickCommands.Clear();
    }
    
    private void AddToHistory(ICommand command)
    {
        _executedCommands.Push(command);
        
        // Ограничиваем размер истории
        if (_executedCommands.Count > MAX_HISTORY_SIZE)
        {
            // Удаляем самую старую команду
            var tempStack = new Stack<ICommand>();
            while (_executedCommands.Count > 1) // Оставляем одну команду
            {
                tempStack.Push(_executedCommands.Pop());
            }
            
            _executedCommands.Clear();
            while (tempStack.Count > 0)
            {
                _executedCommands.Push(tempStack.Pop());
            }
        }
    }
    
    public void Undo()
    {
        if (_executedCommands.Count > 0)
        {
            var lastCommand = _executedCommands.Pop();
            lastCommand.Undo();
            Debug.Log($"Undo performed. History size: {_executedCommands.Count}");
        }
        else
        {
            Debug.Log("No commands to undo");
        }
    }
    
    // Публичные методы для тестирования
    public int GetRightClickQueueSize() => _rightClickCommands.Count;
    public int GetHistorySize() => _executedCommands.Count;
}