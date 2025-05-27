using UnityEngine;

public class CoinFloat : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float lifetime = 1f;

    private float timer = 0f;

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
