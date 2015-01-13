using UnityEngine;
using System.Collections;

public class GUIStart : MonoBehaviour {

    public bool Started { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            Instantiate(Resources.Load("Prefab/Tali"));
            Started = true;
            Application.LoadLevelAdditive(1);
            Started = true;
        }
	}

    void OnGUI()
    {
        if(!Started)
        if(GUILayout.Button("Iniciar"))
        {
            Started = true;
            Application.LoadLevel(1);
        }
    }
}
