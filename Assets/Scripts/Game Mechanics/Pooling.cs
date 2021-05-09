using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.GameMechanics
{
    //Isso é necessário para fazer a pool de múltiplos itens
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool; //Objeto da pool
        public int amountToPool; //Quantia inicial de pool

        public bool shouldExpand = true; //Para a pool expandir
    }

    //Este script é para controllar o pooling dos objetos necessários do jogo
    public class Pooling : MonoBehaviour
    {
        public static Pooling SharedInstance;
        public List<GameObject> pooledObjects;

        public List<ObjectPoolItem> itemsToPool;//Lista de itens, como inimigos, balas, powerups

        void Awake()
        {
            SharedInstance = this;
        }

        void Start()
        {
            pooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
            
        }

        public GameObject GetPooledObject(string tag)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
                {
                    return pooledObjects[i];
                }
            }
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item.objectToPool.tag == tag)
                {
                    if (item.shouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectToPool);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}