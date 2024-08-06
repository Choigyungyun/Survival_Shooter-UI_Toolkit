using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameSettingProperty;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyObjects;

    [Header("Enemy Spawn Transforms")]
    [SerializeField] private Transform[] enemySpawnTransforms;
    [Tooltip("Set the enemy's parent object.")]
    [SerializeField] private Transform enemyIndexTransform;

    private int maxEnemySpawn = 0;
    private float enemySpawnInterval = 0.0f;
    private float timer = 0.0f;

    private void Update()
    {
        if(GameManager.Instance.gameState != GameState.Play)
        {
            return;
        }
        timer += Time.deltaTime;
        SpawnEnemyToDifficult(GameManager.Instance.gameModeDifficult);
    }

    public void InitializeSpawnDifficult(GameModeDifficulty mode)
    {
        GameManager.Instance.gameModeDifficult = mode;

        switch (mode)
        {
            case GameModeDifficulty.None:
                break;
            case GameModeDifficulty.Easy:
                maxEnemySpawn = 15;
                enemySpawnInterval = 2f;
                break;
            case GameModeDifficulty.Nomal:
                maxEnemySpawn = 25;
                enemySpawnInterval = 1.25f;
                break;
            case GameModeDifficulty.Hard:
                maxEnemySpawn = 35;
                enemySpawnInterval = 1f;
                break;
        }

        Debug.Log($"Settings according to mode registration are now ready.\n" +
                  $"Game State : {GameManager.Instance.gameState}\n" +
                  $"Game mode : {GameManager.Instance.gameModeDifficult}\n" +
                  $"Max Enemy Spawn : {maxEnemySpawn}\n" +
                  $"Enemy Spawn Interval : {enemySpawnInterval}\n");
    }

    public void SpawnEnemyToDifficult(GameModeDifficulty mode)
    {
        switch (mode)
        {
            case GameModeDifficulty.None:
                break;
            case GameModeDifficulty.Easy:
                SpawnEnemy(EasyModeSpawnPercentage(UnityEngine.Random.Range(0, 100)));
                break;
            case GameModeDifficulty.Nomal:
                break;
            case GameModeDifficulty.Hard:
                break;
        }
    }

    private void SpawnEnemy(int enemyTypeNumber)
    {
        if(timer <= enemySpawnInterval || enemyIndexTransform.childCount == maxEnemySpawn)
        {
            return;
        }
        GameObject enemyObject = Instantiate
                                 (
                                    enemyObjects[enemyTypeNumber],
                                    enemySpawnTransforms[GetRandomSpawn(enemySpawnTransforms)].position,
                                    enemySpawnTransforms[GetRandomSpawn(enemySpawnTransforms)].rotation
                                 );

        enemyObject.transform.parent = enemyIndexTransform;

        timer = 0.0f;
    }

    private int EasyModeSpawnPercentage(int randomNumber)
    {
        if(randomNumber < 60)
        {
            return 0;
        }
        else if(randomNumber > 60 && randomNumber < 90)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    /// <summary>
    /// �������� ���׸� Ÿ������ �޾� ������ ������ ���� ��ȯ �մϴ�.
    /// </summary>
    /// <remarks>
    /// ���׸� Ÿ���� <typeparamref name="T"/>�� ���������θ� �����մϴ�.
    /// None, Nothing �� ���ʿ��� ��Ұ� �����ϱ� ������ ���� �ּ� ������
    /// 0�� �ƴ� 1���� �����մϴ�. �ִ� ������ �Է¹��� �������� �ִ� ���Դϴ�.
    /// </remarks>
    /// <typeparam name="T">Enum</typeparam>
    /// <returns>���� Enum ��</returns>
    private T RandomTypeEnum<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(1, values.Length));
    }

    private int GetRandomSpawn(Transform[] spawnerTransforms)
    {
        int spawner = spawnerTransforms.Length;
        return UnityEngine.Random.Range(0, spawner);
    }
}
