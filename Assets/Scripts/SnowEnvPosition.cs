using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEnvPosition : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]
    private GameObject player;

    void Update()
    {
        Vector3 ptp = player.transform.position;
        transform.position = new Vector3(transform.position.x, ptp.y + 20, transform.position.z);
    }
}
