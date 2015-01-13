using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SnakeLogic : MonoBehaviour {

    //public
    public static SnakeLogic Instance;
    public GameObject SnakePiece;
    public int LenghtSnake { get; set; }
    public float Speed { get; set; }
    public List<GameObject> SnakeList { get; set; }
    
    //private
    private GameManager gameManager;
    public enum Direction { Up, Down, Left, Right};
    private Direction direction;
    private Direction directionFinal;
    private bool dead;

    void Awake()
    {
        Instance = this;
        LenghtSnake = 5;
        SnakeList = new List<GameObject>();
    }

	// Use this for initialization
	void Start () {

        gameManager = GameManager.Instance;
        if (gameManager.GameDiffcult == GameManager.Difficult.Easy)
            Speed = 3;
        else if (gameManager.GameDiffcult == GameManager.Difficult.Medium)
            Speed = 5;
        else if (gameManager.GameDiffcult == GameManager.Difficult.Hard)
            Speed = 8;
        
        for(int i=0; i < LenghtSnake; i++)
            SnakeList.Add( Instantiate(SnakePiece, new Vector3(1.2f * i, 1, 0), Quaternion.identity) as GameObject);
        
        InvokeRepeating("UpdateX", 0.1f,1/Speed);
	}
	
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            if (directionFinal != Direction.Down)
                direction = Direction.Up;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            if (directionFinal != Direction.Up)
                direction = Direction.Down;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            if (directionFinal != Direction.Right)
                direction = Direction.Left;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            if (directionFinal != Direction.Left)
                direction = Direction.Right;
        }
    }

	// Update is called once per frame
	void UpdateX () 
    {
        //move o corpo do snake
        for (int i = 0; i < SnakeList.Count - 1; i++)
        {
            SnakeList[i].transform.position = SnakeList[i + 1].transform.position;
            SnakeList[i].renderer.material.color = Color.green;
        }
        
        directionFinal = direction;
        
        //move a cabeça do snake
        if (directionFinal == Direction.Up)
            SnakeList[SnakeList.Count - 1].transform.localPosition += Vector3.forward * 1.2f;
        else if (directionFinal == Direction.Down)
            SnakeList[SnakeList.Count - 1].transform.localPosition -= Vector3.forward * 1.2f;
        else if (directionFinal == Direction.Left)
            SnakeList[SnakeList.Count - 1].transform.localPosition += Vector3.left * 1.2f;
        else if (directionFinal == Direction.Right)
            SnakeList[SnakeList.Count - 1].transform.localPosition -= Vector3.left * 1.2f;

        Vector3 pos = SnakeList[SnakeList.Count - 1].transform.localPosition;
        
        //verifica limites da tela
        if (SnakeList[SnakeList.Count - 1].transform.localPosition.z > 9.6f)
            SnakeList[SnakeList.Count - 1].transform.localPosition = new Vector3(pos.x, pos.y, -9.6f);

        if (SnakeList[SnakeList.Count - 1].transform.localPosition.z < - 9.6f)
            SnakeList[SnakeList.Count - 1].transform.localPosition = new Vector3(pos.x, pos.y, 9.6f);

        if (SnakeList[SnakeList.Count - 1].transform.localPosition.x > 24)
            SnakeList[SnakeList.Count - 1].transform.localPosition = new Vector3(-22.8f, pos.y, pos.z);

        if (SnakeList[SnakeList.Count - 1].transform.localPosition.x < -24)
            SnakeList[SnakeList.Count - 1].transform.localPosition = new Vector3(22.8f, pos.y, pos.z);

        //verifica morte
        for (int i = 0; i < SnakeList.Count-1; i++)
        {
            if (SnakeList[SnakeList.Count - 1].transform.localPosition == SnakeList[i].transform.localPosition)
            {
                Time.timeScale = 0;
                dead = true;
            }
        }
	}

    public void NewItem()
    {
        LenghtSnake += 1;
        Vector3 pos = SnakeList[SnakeList.Count - 1].transform.localPosition;
        SnakeList.Add(Instantiate(SnakePiece, new Vector3(pos.x, 1, pos.z), Quaternion.identity) as GameObject);
    }

    void OnGUI()
    {
        if (dead)
        {
            if (GUILayout.Button("Reiniciar"))
            {
                Time.timeScale = 1;
                Application.LoadLevel(1);
                dead = false;
            }
        }
    }

}
