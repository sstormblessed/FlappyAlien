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

    //Método que se ejecuta al iniciar el juego
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    //Este método determina la posición del Jugador al inicio de cada partida
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    //Este método sirve para hacer que el Jugador salte al pulsar el espacio, click izquierdo o dar un toque en una pantalla táctil
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    //Este método sirve para ciclar entre los 3 sprites que componen la nave que representa al Jugador
    private void AnimateSprite()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    //Método para registrar las colisiones tanto con tuberías como con la zona de puntuación
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            //Reproducir Explosión y Game Over al chocar con una tubería
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            FindAnyObjectByType<GameManager>().GameOver();

            //Sonidos de Explosión
            SFXSource.clip = death;
            SFXSource.Play();

            //Desactiva al Jugador
            gameObject.SetActive(false);

        } else if (collision.gameObject.tag == "Scoring")
        {
            //Aumenta la puntuación al pasar entre las tuberías
            FindAnyObjectByType<GameManager>().IncreaseScore();

            //Reproduce sonido al puntuar
            SFXSource.clip = point;
            SFXSource.Play();
        }
    }

    public void ResetPlayer()
    {
        // Restaura la posición inicial
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;

        // Restaura la dirección inicial
        direction = Vector3.zero;

        // Reactiva el GameObject del jugador
        gameObject.SetActive(true);
    }
}