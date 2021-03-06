﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private static Player instance;
    public static Player Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        inventory = Item.Items.items;
        inventoryID = Item.Items.orbIDs;
        orbsInPossession = 0;
    }

    public List<bool> inventory;
    public bool[] inventoryID;
    public int orbsInPossession;
    public PlayerMovement movement;
    public string currentLevel;
    public int currentLevelInt;
    public AudioClip gettingHit;
    bool jump;

    public void Inactive(bool active)
    {
        this.enabled = active;
    }

    void LateUpdate()
    {
        float x;
        float z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            x = Input.GetAxis("Horizontal") * sprintSpeed;
            z = Input.GetAxis("Vertical") * sprintSpeed;
        }
        else 
        {
            x = Input.GetAxis("Horizontal") * walkSpeed;
            z = Input.GetAxis("Vertical") * walkSpeed;
        }

        if (Input.GetButtonDown("Jump"))
            jump = true;

        movement.Move(x, z, jump);

        jump = false;
    }

    public void NewGame() 
    {
        hp = 100;
        currentLevelInt = 0;
        orbsInPossession = 0;
        inventory[0] = false;
        Collider collider = GetComponent<Collider>();
        Door door = gameObject.AddComponent<Door>() as Door;
        door.goingToIndex = currentLevelInt;
        door.Open(collider, door.scenes[door.goingToIndex], Door.Coordinates.cooridnates[door.goingToIndex]);
    }

    public void TakeDamage(int damage) 
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = gettingHit;
        audio.Play();
        hp -= damage;
        if (hp <= 0) 
        {
            Inactive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
