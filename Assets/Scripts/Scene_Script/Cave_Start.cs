using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cave_Start : MonoBehaviour
{
    public GameObject caveSpawnPoint; // 플레이어를 이동시킬 동굴 내 오브젝트

    void Start()
    {
        MovePlayerToCave();
    }

    private void MovePlayerToCave()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && caveSpawnPoint != null)
        {
            player.transform.position = caveSpawnPoint.transform.position;
            player.transform.rotation = caveSpawnPoint.transform.rotation;
            Debug.Log("플레이어가 동굴 내 지정된 오브젝트 위치로 이동됨");
        }
        else if (caveSpawnPoint == null)
        {
            Debug.LogError("caveSpawnPoint 오브젝트가 설정되지 않음");
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없음");
        }
    }
}
