using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public struct Item
{
    //public Sprite spr;
    // this is a holder, will replace with useful stuff later.
    // image?
    // stats?
    // etc.
}


public class Inventory : MonoBehaviour {

    public GameObject scrollView;
    public GameObject scrollContent;
    public GameObject itemPrefab;
    private static Random random = new Random();
    //TODO: make a map and pair these sobs instead, too lazy atm.
    // list to hold actual items that we can use
    List<Item> items = new List<Item>();
    // list to hold ui elements which we can delete or something.
    List<GameObject> uiItems = new List<GameObject>();
    float offset = -200;
	// Use this for initialization
	void Start () {
        SetView(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.P))
        {
            AddInventoryItem();
        }	
	}
    // hides or shows the view object.
    public void SetView(bool val)
    {
        scrollView.SetActive(val);
    }

    void AddInventoryItem()
    {
        // usually this will take in an item reference
        // create an item from that, but as there is still no actual items lets make a placeholder.

        // when adding items we need to add a visual component too
        GameObject newUIItem = Instantiate(itemPrefab);
        newUIItem.GetComponent<ItemUIObject>().objectName.text = Get8CharacterRandomString();
        // set its parent
        newUIItem.transform.parent = scrollContent.transform;
        newUIItem.transform.localPosition = new Vector3(100,100 + offset,newUIItem.transform.localPosition.z);
        offset -= 100f;
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, scrollContent.GetComponent<RectTransform>().sizeDelta.y + 100);
    }
    // random string
    public string Get8CharacterRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path.Substring(0, 8);  // Return 8 character string
    }
}
