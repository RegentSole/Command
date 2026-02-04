using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    [SerializeField] private Color objectColor = Color.red;
    [SerializeField] private float objectSize = 0.3f;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        
        CreateObjectSprite();
    }
    
    private void CreateObjectSprite()
    {
        Texture2D texture = new Texture2D(32, 32);
        
        // Создаем круглый спрайт
        Vector2 center = new Vector2(texture.width / 2, texture.height / 2);
        float radius = texture.width / 2;
        
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                Color color = distance <= radius ? objectColor : Color.clear;
                texture.SetPixel(x, y, color);
            }
        }
        
        texture.Apply();
        
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
        
        _spriteRenderer.sprite = sprite;
        transform.localScale = Vector3.one * objectSize;
    }
}