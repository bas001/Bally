﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject blueBall = (GameObject)Instantiate(Resources.Load("blueBall"));
        GameObject redBall = (GameObject)Instantiate(Resources.Load("redBall"));


    }

    // Update is called once per frame
    void Update () {
		
	}
}
