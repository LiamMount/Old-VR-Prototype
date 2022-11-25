using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTurret : MonoBehaviour
{
    public float health = 10.0f;
    public float maxHealth = 10.0f;
    public float hitTimer = 0.1f;
    public GameObject enemySelf;
    public HealthBar healthBar;
    public BulletInfo bulletInfo;
    public MeshRenderer meshRenderer;
    public Material hitMaterial;
    private Material oldMaterial;

    public Light chargeBeam;
    public AudioSource audioSource;
    public AudioClip chargeSound;
    public AudioClip shot;

    public float bulletSpeed = 10.0f;
    private float bulletTimer = 5.0f;
    public Transform barrel;
    public GameObject bullet;

    public Transform barrelOrientation;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        oldMaterial = meshRenderer.material;

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        chargeBeam.enabled = false;

        bulletTimer = Random.Range(3.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Find main camera
        if (target == null)
        {
            target = GameObject.FindWithTag("Hit Point").transform;
        }

        barrelOrientation.LookAt(target);

        bulletTimer -= Time.deltaTime;
        hitTimer -= Time.deltaTime;

        //Charge
        if (bulletTimer <= 1 && !chargeBeam.enabled)
        {
            chargeBeam.enabled = true;
            audioSource.PlayOneShot(chargeSound);
        }

        //Shoot
        if (bulletTimer <= 0)
        {
            Fire();
            bulletTimer = 5.0f;
            chargeBeam.enabled = false;
        }

        if (hitTimer <= 0)
        {
            meshRenderer.material = oldMaterial;
        }

        if (health <= 0)
        {
            Destroy(enemySelf);
        }
    }

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;
        audioSource.PlayOneShot(shot);
    }

    private void OnCollisionEnter(Collision col)
    {
        BulletCollision(col);
    }

    public void BulletCollision(Collision col)
    {
        bulletInfo = col.gameObject.GetComponent<BulletInfo>();

        if (bulletInfo != null)
        {
            TakeDamage(bulletInfo.damage);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.SetHealth(health);
        hitTimer = 0.1f;
        meshRenderer.material = hitMaterial;
        bulletTimer = 5.0f;
    }
}
