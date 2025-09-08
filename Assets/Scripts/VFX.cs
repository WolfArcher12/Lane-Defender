using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
