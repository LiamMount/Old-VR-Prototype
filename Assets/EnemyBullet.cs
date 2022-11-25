using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpan = 3.0f;
    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bulletSpan -= Time.deltaTime;

        if (bulletSpan <= 0)
        {
            Destroy(self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(self);
    }
}
