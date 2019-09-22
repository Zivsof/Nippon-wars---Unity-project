using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class EnemyState : MonoBehaviour
{
    public String ememyType;
    public int id;
    public float expWorth;
    public float hp;
    public float attackPower;
}
