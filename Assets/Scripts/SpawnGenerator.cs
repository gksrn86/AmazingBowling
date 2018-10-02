using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour {

    public GameObject[] propPrefabs;

    private BoxCollider area;

    public int count = 100;

    private List<GameObject> props = new List<GameObject>();



	// Use this for initialization
	//初期化のためにこれを使用
	void Start () {
        area = GetComponent<BoxCollider>();

        for(int i = 0; i < count; i++)
        {
            Spawn();
            //생성용 함수
	    //生成用関数
        }

        area.enabled = false;
	}
	
	private void Spawn()
    {
        int selection = Random.Range(0, propPrefabs.Length);

        GameObject selectedPrefab = propPrefabs[selection];

        Vector3 spawnPos = GetRandomPostion();

        GameObject instance =Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        props.Add(instance);
    }

    private Vector3 GetRandomPostion()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);

        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

    public void Reset()
    {
        for(int i = 0; i < props.Count; i++)
        {
            props[i].transform.position = GetRandomPostion();
            props[i].SetActive(true);
        }
    }
}
