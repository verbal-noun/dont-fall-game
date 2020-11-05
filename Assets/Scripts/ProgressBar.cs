using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject tower;
    [SerializeField]
    private GameObject player;
    private Powerbar powerbar;
    private float towerTop; 
    private float towerBottom;

    private float position = 0;
    void Start()
    {
        towerTop = GameObject.Find("Tower Top").transform.position.y;
        towerBottom = 0f;

        powerbar = GetComponent<Powerbar>();

        powerbar.SetMaxValue(towerTop);
    }

    // Update is called once per frame
    void Update()
    {
        position = player.transform.position.y;
        powerbar.SetPower(position);
    }
}
