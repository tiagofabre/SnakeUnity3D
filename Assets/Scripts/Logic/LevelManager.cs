using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject Item;

	// Use this for initialization
	void Start () {
        NewItem();
	}
    
    public void NewItem()
    {
        GameObject x = Instantiate( Item, new Vector3(Random.Range(17,-18)*1.2f,1, Random.Range(8,-9)*1.2f), Quaternion.identity ) as GameObject;
        ItemScript item = x.GetComponent<ItemScript>();
        item.levelManager = this;
    }
}
