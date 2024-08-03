using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{
    [Header("Gun settings")]
    [Tooltip("Time between gun fire")]
    public int gunDamage = 0;                   // �� ������
    public float gunFireInterval = 0.0f;        // �� �߻� ����
    public float gunRange = 0.0f;               // �� �߻� �Ÿ�

    [SerializeField]
    [Tooltip("Time between gun firing effects\nDefault value = 0.2f")]
    private float gunEffectTimeInterval = 0.0f; // �� �߻� ����Ʈ ����

    private float timer = 0.0f;                 
    private float hitDistance = 0.0f;

    private Ray gunRay;
    private RaycastHit gunRayHit;

    private Light gunLight;                     // �ѱ� �Һ�
    private LineRenderer gunLine;               // �Ѿ� ����
    private ParticleSystem gunParticle;         // �ѱ� ȭ��
    private AudioSource gunAudio;               // �� �߻� �Ҹ�

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
    /// �� �߻� ����Ʈ, ����, ��ƼŬ ��Ʈ��
    /// </summary>
    /// <param name="isFire">�� �߻� ����</param>
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
