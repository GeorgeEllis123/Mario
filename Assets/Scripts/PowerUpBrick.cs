using UnityEngine;

public class PowerUpBrick : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color usedColor = new Color(0.4f, 0.2f, 0f);
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Hit Settings")]
    public bool hasBeenHit = false;
    // public bool isMushroom = false;

    [Header("Coin Settings")]
    public bool spawnMushroom = false;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject mushroomPrefab;
    private Transform spawnPoint;       

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spawnPoint == null)
        {
            spawnPoint = this.transform;
        }
    }

    public void OnBrickHit()
    {
        // just double checking!
        if (hasBeenHit)
            return;

        hasBeenHit = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = usedColor;
        }

        Debug.Log("Power-Up Brick hit!");

        if (spawnMushroom)
        {
            SpawnMushroom();
        }
        else
        {
            SpawnCoin();
        }


    }

    private void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, spawnPoint.position + Vector3.up * 0.5f, Quaternion.identity);
        }
    }

    private void SpawnMushroom()
    {
        if (mushroomPrefab != null)
        {
            Instantiate(mushroomPrefab, spawnPoint.position + Vector3.up * 0.5f, Quaternion.identity);
        }
    }
}
