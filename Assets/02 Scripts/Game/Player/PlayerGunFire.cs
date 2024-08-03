using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{
    [Header("Gun settings")]
    [Tooltip("Time between gun fire")]
    public int gunDamage = 0;                   // ÃÑ µ¥¹ÌÁö
    public float gunFireInterval = 0.0f;        // ÃÑ ¹ß»ç °£°Ý
    public float gunRange = 0.0f;               // ÃÑ ¹ß»ç °Å¸®

    [SerializeField]
    [Tooltip("Time between gun firing effects\nDefault value = 0.2f")]
    private float gunEffectTimeInterval = 0.0f; // ÃÑ ¹ß»ç ÀÌÆåÆ® °£°Ý

    private float timer = 0.0f;                 
    private float hitDistance = 0.0f;

    private Ray gunRay;
    private RaycastHit gunRayHit;

    private Light gunLight;                     // ÃÑ±¸ ºÒºû
    private LineRenderer gunLine;               // ÃÑ¾Ë ±¥Àû
    private ParticleSystem gunParticle;         // ÃÑ±¸ È­¿°
    private AudioSource gunAudio;               // ÃÑ ¹ß»ç ¼Ò¸®

    private void Awake()
    {
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        gunLight.enabled = false;
        gunLine.enabled = false;

        gunRay = new Ray();
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.Play)
        {
            return;
        }

        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= gunFireInterval)
        {
            FireControl(true);
        }

        if(timer >= gunFireInterval * gunEffectTimeInterval)
        {
            FireControl(false);
        }
    }

    /// <summary>
    /// ÃÑ ¹ß»ç ÀÌÆåÆ®, »ç¿îµå, ÆÄÆ¼Å¬ ÄÁÆ®·Ñ
    /// </summary>
    /// <param name="isFire">ÃÑ ¹ß»ç ¿©ºÎ</param>
    private void FireControl(bool isFire)
    {
        gunLight.enabled = isFire;
        gunLine.enabled = isFire;

        if (!isFire)
        {
            return;
        }

        gunRay.origin = transform.position;
        gunRay.direction = gunLight.transform.forward;

        if (!Physics.Raycast(gunRay.origin, gunRay.direction, out gunRayHit, gunRange))
        {
            return;
        }

        hitDistance = Vector3.Distance(transform.position, gunRayHit.point);
        gunLine.SetPosition(1, new Vector3(0.0f, 0.0f, hitDistance));

        gunAudio.Play();
        gunParticle.Play();

        timer = 0;

        if (!gunRayHit.collider.gameObject.GetComponent<EnemyState>())
        {
            return;
        }

        gunRayHit.rigidbody.gameObject.GetComponent<EnemyState>().EnemyTakeDamage(gunDamage, gunRayHit.point);
    }
}
