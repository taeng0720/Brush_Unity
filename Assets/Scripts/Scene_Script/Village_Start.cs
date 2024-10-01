using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Village_Start : MonoBehaviour
{
    public GameObject villageSpawnPoint; // 플레이어를 이동시킬 마을 내 오브젝트

    void Start()
    {
        MovePlayerToVillage();
    }

    private void MovePlayerToVillage()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && villageSpawnPoint != null)
        {
            player.transform.position = villageSpawnPoint.transform.position;
            player.transform.rotation = villageSpawnPoint.transform.rotation;
            Debug.Log("플레이어가 마을 내 지정된 오브젝트 위치로 이동됨");
        }
        else if (villageSpawnPoint == null)
        {
            Debug.LogError("villageSpawnPoint 오브젝트가 설정되지 않음");
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없음");
        }
    }
}
