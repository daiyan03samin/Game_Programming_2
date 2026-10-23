using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameFunction : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private GameObject crystalsPrefab;
    [SerializeField] private GameObject robotsPrefab;
    [SerializeField] TMP_Text scoreText;

    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveInput;
    private int score = 0;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        InvokeRepeating(nameof(SpawnCrystals), 3f, 2f);
        InvokeRepeating(nameof(SpawnRobots), 3f, 3f);
    }

    void MovePlayer(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(horizontalInput * movementSpeed, rb.linearVelocity.y, verticalInput * movementSpeed);
        // rb.AddForce(transform.up, ForceMode.Force);
    }

    private void SpawnCrystals()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-7f, 7f), 3f, Random.Range(-7f, 7f));

        GameObject spawnedCrystal = Instantiate(crystalsPrefab, spawnPosition, Quaternion.identity);

        Destroy(spawnedCrystal, 4f);
    }

    private void SpawnRobots()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-7f, 7f), 5f, Random.Range(-7f, 7f));

        GameObject spawnedRobot = Instantiate(robotsPrefab, spawnPosition, Quaternion.identity);

        Destroy(spawnedRobot, 4f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collectible")
        { // if (other.gameObject.CompareTag("Collectible"))
            score += 50;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            score -= 100;
            Destroy(other.gameObject);
        }
        else
        {
            return;
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        if (score < 0)
        {
            ResetScene();
        }
    }


    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
