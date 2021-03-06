﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text orbCount;
    public Text hudText;
    public Text hpCount;

    // Update is called once per frame
    void Update()
    {
        hpCount.text = Player.Instance.hp.ToString() + " / 100";
        orbCount.text = Player.Instance.orbsInPossession.ToString() + " / 13  (Need: 10)";
    }
}
