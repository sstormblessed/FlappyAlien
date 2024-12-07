using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] explosionSprites;
    public float frameRate = 0.05f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayExplosion());
    }

    //M�todo que reproduce la explosi�n
    private IEnumerator PlayExplosion()
    {
        foreach (Sprite sprite in explosionSprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(frameRate);
        }

        Destroy(gameObject);
    }
}
