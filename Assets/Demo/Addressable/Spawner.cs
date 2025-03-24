using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VA.Addressable;
using VA.DesignPattern;
public class Spawner : MonoBehaviour
{
    [SerializeField] private AssetReference assetReference;
    [SerializeField] private AssetReference uiReference;
    [SerializeField] private AssetReference sceneReference;
    [SerializeField] private List<AssetReferenceT<InfoSO>> infoSORef;
    [SerializeField] private InfoSO loadedInfo;
    [SerializeField] private Transform uiParent;





    public Vector3 startPosition = new Vector3(0, 0, 0);
    public Vector3 currentPos = new Vector3(0, 0, 0);
    private float nextX = 0;
    public List<GameObject> list = new List<GameObject>();
    void Start()
    {
        Application.targetFrameRate = 120;
        SpawnUI();
        LoadInfo();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                PoolingObject.ReturnObject(list[i]);
            }
            list.Clear();
            nextX = 0;
        } 
        else if(Input.GetKeyDown(KeyCode.S))
        {
            SpawnACube();         
        }    
    }
    
    private async void SpawnACube()
    {
        GameObject cubeFromPO = await PoolingObject.GetObjectWithAddressable(assetReference);
        currentPos.x = nextX;
        cubeFromPO.transform.position = currentPos;
        nextX += 2;
        list.Add(cubeFromPO);
    }    
    private async void SpawnUI()
    {
        await CustomAddressables.Spawn<GameObject>(uiReference, uiParent);
    } 
    private async void LoadInfo()
    {
        for(int i =0; i < infoSORef.Count; i++)
        {
            loadedInfo = await infoSORef[i].LoadAssetAsync<InfoSO>().Task;
            Debug.Log($"Loaded Info Num: {i}");
            Debug.Log($"Loaded Info Name: {loadedInfo.nameInfo}");
            Debug.Log($"Loaded Info Gender Male: {loadedInfo.isMale}");
            Debug.Log($"Loaded Info Gender Female: {loadedInfo.isFemale}");
            Debug.Log($"Loaded Info Vale: {loadedInfo.val}");
        }         
    }    
}
