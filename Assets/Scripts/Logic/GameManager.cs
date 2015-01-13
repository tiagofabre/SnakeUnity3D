using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    
    public enum Difficult { Easy, Medium, Hard }
    public Difficult GameDiffcult = Difficult.Easy; 

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
