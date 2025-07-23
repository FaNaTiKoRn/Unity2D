using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPooler[] m_objectPoolers;

    public enum PoolNames
    {
        Projectiles,
        Stars,
        OrangesShips,
        GreenShips,
        AsteroidePequeño,
        AsteroideMediano,
        AsteroideGrande,
        Cometa
    }

    public static ObjectPoolManager m_instance;

    private void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        foreach (ObjectPooler op in m_objectPoolers)
        {
            op.Init(this);
        }
    }

    public GameObject GetPooledObject(PoolNames poolName)
    {
        foreach (ObjectPooler op in m_objectPoolers)
        {
            if (op.m_poolNames == poolName)
            {
                return op.GetPooledObject();
            }
        }

        return null;
    }
}
