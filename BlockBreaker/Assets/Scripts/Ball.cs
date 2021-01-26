using UnityEngine;

public class Ball : MonoBehaviour
{
    //config param
    [SerializeField] Paddle paddle1;
    [SerializeField] float veloX = 2f;
    [SerializeField] float veloY = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.8f;
    //state
    Vector2 paddleToBallVector;

    // Cached component reference
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;

    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPedal();
            LaunchOnClick();
        }
    }

    private void LaunchOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(veloX, veloY);
        }
    }

    private void LockBallToPedal()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 veloTweak = new Vector2
            (Random.Range(0.4f, randomFactor),
            Random.Range(0.4f, randomFactor));

        if(hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += veloTweak;
        }
    }
}
