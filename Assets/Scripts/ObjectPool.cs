using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    //Esta clase se genera para crear el numero de elementos en escena necesarios 
    //Para continuar el juego
    //Aquí se pueden poner el numero de elementos que apareceran dependiendo del nivel
    public int amountToPool;
    public GameObject objectToPool;
}


public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;


    // Start is called before the first frame update
    void Start()
    {
        //Se crean todos los objetos que se cargaran para juagr en la escena
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            for(int i=0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject(string tag)
    {
        for(int i=0; i <pooledObjects.Count; i++)
        {
            if(pooledObjects[i].activeInHierarchy==false && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        return null;

    }
}
