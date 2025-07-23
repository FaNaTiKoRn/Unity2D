using UnityEngine;

[System.Serializable]
public class ObjectPooler
{
    public GameObject m_objectToPool;
    public int amount = 10;
    public ObjectPoolManager.PoolNames m_poolNames;

    private GameObject[] m_pooledObjects;

    public void Init(ObjectPoolManager opm)
    {
        GameObject container = new GameObject(m_poolNames.ToString());
        container.transform.parent = opm.transform;

        m_pooledObjects = new GameObject[amount];
        for (int i = 0; i < m_pooledObjects.Length; i++)
        {
            m_pooledObjects[i] = GameObject.Instantiate(m_objectToPool);
            m_pooledObjects[i].SetActive(false);
            m_pooledObjects[i].transform.parent = container.transform;
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < m_pooledObjects.Length; i++)
        {
            if (!m_pooledObjects[i].activeInHierarchy)
                return m_pooledObjects[i];
        }

        return null;
    }
}
