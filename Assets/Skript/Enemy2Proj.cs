using System;
using UnityEngine;

public class Enemy2Proj : MonoBehaviour
{
    
    private void Update()
    {
        if (transform.parent == GameManager.singleton.projFolder.transform)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 맞음!");
            Destroy(gameObject);
        }
    }

   // private void DestroyGameObject()
   // {
   //     Destroy(gameObject);
   //  }
   // Invoke("DestroyGameObject",  5f); // 5초뒤에 투사체 삭제
}
