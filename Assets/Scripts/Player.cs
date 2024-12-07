using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    public GameObject explosionPrefab;

    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    [Header("----- Audio Management -----")]
    [SerializeField] AudioSource SFXSource;
    public AudioClip death;
    public AudioClip point;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //M�todo que se ejecuta al iniciar el juego
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    //Este m�todo determina la posici�n del Jugador al inicio de cada partida
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    //Este m�todo sirve para hacer que el Jugador salte al pulsar el espacio, click izquierdo o dar un toque en una pantalla t�ctil
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    //Este m�todo sirve para ciclar entre los 3 sprites que componen la nave que representa al Jugador
    private void AnimateSprite()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    //M�todo para registrar las colisiones tanto con tuber�as como con la zona de puntuaci�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            //Reproducir Explosi�n y Game Over al chocar con una tuber�a
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            FindAnyObjectByType<GameManager>().GameOver();

            //Sonidos de Explosi�n
            SFXSource.clip = death;
            SFXSource.Play();

            //Desactiva al Jugador
            gameObject.SetActive(false);

        } else if (collision.gameObject.tag == "Scoring")
        {
            //Aumenta la puntuaci�n al pasar entre las tuber�as
            FindAnyObjectByType<GameManager>().IncreaseScore();

            //Reproduce sonido al puntuar
            SFXSource.clip = point;
            SFXSource.Play();
        }
    }

    public void ResetPlayer()
    {
        // Restaura la posici�n inicial
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;

        // Restaura la direcci�n inicial
        direction = Vector3.zero;

        // Reactiva el GameObject del jugador
        gameObject.SetActive(true);
    }
}