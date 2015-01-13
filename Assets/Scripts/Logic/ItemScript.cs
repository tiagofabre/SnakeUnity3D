using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

    public LevelManager levelManager;

    void OnTriggerEnter(Collider other)
    {
        SnakeLogic.Instance.NewItem();
        
        levelManager.NewItem();
        Destroy(this.gameObject);
    }
}
