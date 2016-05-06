using UnityEngine;
using System.Collections;

public class SpriteContainer : MonoBehaviour {
    public Sprite[] pLegs, pUnarmedWalk;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Sprite[] getPlayerLegs()
    {
        return pLegs;
    }

    public Sprite[] getPlayerUnarmedWalk()
    {
        return pUnarmedWalk;
    }
}
