using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right; // direction(y�n) isminde bir 2 boyutlu bir vector de�i�keni tan�ml�yoruz ve
                                               // ilk ba�ta her zaman sa�a do�ru gitmesi i�in vector2.right metodunu kullan�yoruz

    private List<Transform> segments = new List<Transform>(); // y�lan�n eklencek b�l�mlerini tutmak i�in segments(par�alar) isimli bir liste olu�turduk.
                                                              // liste Transform bile�enlerinden olu�uyor
        
    public Transform segmentPrefab; // segmentPrefab isminde bir Transform olu�turduk daha sonradan prefab y�lan b�l�mlerini kullanabilmek i�in

    public int initialSize = 3; //y�lan�n ba�taki boyutunu belirlemek i�in yazd���m�z de�er
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
    private void Update() // her karede oynat�lan unity metodu
    {

        if (this.direction.x != 0f) // direction de�i�keninin de�erlerini de�i�tirip y�lan� kontrol etmek i�in yaz�lan kod
                                    // y�lan�n �st�ne kafas� par�alar�n�n �st�ne ge�mesin diye x ekseninde hareket ediyorsa yaln�zca
                                    // y eksenine d�nebilece�ini sa�layan kod
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) //y�n tu�lar� veya wasd'yi kullanarak y�lan�n kontrol edildi�i k�s�m
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
    private void FixedUpdate() // fixed update sistemin fps de�erine g�re de�i�meyen belirli bir kare de�erine g�re �al�� update metodu
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
