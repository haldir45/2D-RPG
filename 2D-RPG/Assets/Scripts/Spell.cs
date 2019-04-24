﻿using System;
using UnityEngine;


[Serializable]
public class Spell
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int damage;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float castTime;
    [SerializeField]
    private GameObject spellPrefab;
    [SerializeField]
    private Color barColor;

    public string Name { get => name; }
    public int Damage { get => damage; }
    public Sprite Icon { get => icon; }
    public float Speed { get => speed;  }
    public float CastTime { get => castTime; }
    public GameObject SpellPrefab { get => SpellPrefab1; }
    public GameObject SpellPrefab1 { get => spellPrefab; }
    public Color BarColor { get => barColor; set => barColor = value; }
}
