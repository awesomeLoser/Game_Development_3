using UnityEngine;

public class humanScript : MonoBehaviour
{
    private GameManager gameManager;
    public float speed = 40.0f;
    private float topBound = 30;
    private float lowerBound = -10;
    private float sideBound = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // If an object goes past the players view in the game, remove that object 
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }

        else if (transform.position.z < lowerBound)
        {
           
            Destroy(gameObject);
        }
        else if (transform.position.x > sideBound)
        {
           
            Destroy(gameObject);
        }
        else if (transform.position.x < -sideBound)
        {
           
            Destroy(gameObject);
        }
    }


     private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Pizza"))
        {

            gameManager.AddLives(-1);
            Destroy(gameObject);
        }

    }
}