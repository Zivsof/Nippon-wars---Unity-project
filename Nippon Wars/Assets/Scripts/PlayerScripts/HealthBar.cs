using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public PlayerState player;
    public Image currentHealthbar;
    public Text ratioText;

    private float hitpoint = 100;
    private float maxHitpoint = 100;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    private void Start()
    {
        hitpoint = player.localPlayer.HP;
        UpdateHealthbar();
    }

    private void Update()
    {
        hitpoint = player.localPlayer.HP;
        UpdateHealthbar();
    }
    private void UpdateHealthbar()
    {
        float ratio = hitpoint / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }
}
