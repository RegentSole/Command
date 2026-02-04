using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Color playerColor = Color.blue;
    [SerializeField] private float playerSize = 0.5f;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        
        // Создаем простой спрайт для игрока
        CreatePlayerSprite();
    }
    
    private void CreatePlayerSprite()
    {
        Texture2D texture = new Texture2D(64, 64);
        
        // Заполняем текстуру цветом игрока
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, playerColor);
            }
        }
        
        texture.Apply();
        
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
        
        _spriteRenderer.sprite = sprite;
        transform.localScale = Vector3.one * playerSize;
    }
}