using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name, typeof(T));
                    return instance = gameObject.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (transform.parent != null && transform.root != null) //�θ� ������Ʈ�� �ְų�, �ֻ����� ������Ʈ�� ���� ��
        {
            DontDestroyOnLoad(this.transform.root.gameObject); //�ֻ����� ������Ʈ�� �ı����� �ʴ´�.
        }
        else //�����ΰ� �ֻ��� ������Ʈ�� ��
        {
            DontDestroyOnLoad(this.gameObject); //���� ��ȯ�ǵ� ������Ʈ�� �ı����� �ʴ´�. (�ش� ������Ʈ�� ������ ���ԵǾ��ִٸ� ����� �������� �ʴ´�.)
        }
    }
}
