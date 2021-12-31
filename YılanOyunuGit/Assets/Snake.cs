using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right; // direction(yön) isminde bir 2 boyutlu bir vector deðiþkeni tanýmlýyoruz ve
                                               // ilk baþta her zaman saða doðru gitmesi için vector2.right metodunu kullanýyoruz

    private List<Transform> segments = new List<Transform>(); // yýlanýn eklencek bölümlerini tutmak için segments(parçalar) isimli bir liste oluþturduk.
                                                              // liste Transform bileþenlerinden oluþuyor
        
    public Transform segmentPrefab; // segmentPrefab isminde bir Transform oluþturduk daha sonradan prefab yýlan bölümlerini kullanabilmek için

    public int initialSize = 3; //yýlanýn baþtaki boyutunu belirlemek için yazdýðýmýz deðer
    public Text ScoreText;
    public int score = 0;
    public float speed = 2.0f;
    private Rigidbody rb;
    private Vector2 movement;
    
    

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        ResetState();

    }
    private void Update() // her karede oynatýlan unity metodu
    {

        if (this.direction.x != 0f) // direction deðiþkeninin deðerlerini deðiþtirip yýlaný kontrol etmek için yazýlan kod
                                    // yýlanýn üstüne kafasý parçalarýnýn üstüne geçmesin diye x ekseninde hareket ediyorsa yalnýzca
                                    // y eksenine dönebileceðini saðlayan kod
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) //yön tuþlarý veya wasd'yi kullanarak yýlanýn kontrol edildiði kýsým
            {
                this.direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.direction = Vector2.down;
            }
        }
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.direction = Vector2.left;
            }
        }

    }
    private void FixedUpdate() // fixed update sistemin fps deðerine göre deðiþmeyen belirli bir kare deðerine göre çalýþ update metodu
                               // 
    {
        for(int i = segments.Count -1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        moveCharacter(movement);
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }
    void moveCharacter(Vector2 direction)
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
        
        
    }

    private void ResetState()
    {
        for(int i=1; i<segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        for(int i =1; i< this.initialSize; i++)
        {
            segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            AddScore();
        }else if(other.tag == "Obstacle")
        {
            ResetScore();
            ResetState();
        }
    }
    
    void AddScore()
    {
        score++;
        ScoreText.text = score.ToString();
    }
    void ResetScore()
    {
        score = 0;
        ScoreText.text = score.ToString();
    }
}
