using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.GameCenter;
using VA;
using VA.Addressable;
using VA.DesignPattern;
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objReference;
    [SerializeField] private AssetReference assetReference;
    public Vector3 startPosition = new Vector3(0, 0, 0); 
    private float nextX;
    public List<GameObject> list = new List<GameObject>();
    void Start()
    {
        Application.targetFrameRate = 120;
        nextX = startPosition.x;
        StartCoroutine(SpawnObjects());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(GameObject go in list)
            {
                PoolingObject.ReturnObject(go);
            }
            list.Clear();
        }    
    }
    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Task<GameObject> loadTask = LoadAndReturnObject();
            yield return new WaitUntil(() => loadTask.IsCompleted);

            GameObject pooledObj = loadTask.Result;
            if (pooledObj != null)
            {
                list.Add(pooledObj);
            }
            nextX += 2;
            yield return new WaitForSeconds(2f);
        }
    }
    private async Task<GameObject> LoadAndReturnObject()
    {
        GameObject obj = await CustomAddressables.Spawn<GameObject>(assetReference);

        if (obj != null)
        {
            PoolingObject.ReturnObject(obj);
        }

        GameObject pooledObj = PoolingObject.GetObject(obj);
        pooledObj.transform.position =  new Vector3(nextX, startPosition.y, startPosition.z);
        return pooledObj;
    }
}
