using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]//Will auto add this component
public class ScoreText : MonoBehaviour
{
    public Text text;
    [SerializeField] private int score;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        this.text.text = ScoreManager.Instance.GetScore().ToString();
    }
}
