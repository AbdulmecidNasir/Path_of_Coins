using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] GameObject thePlayer;       // Oyuncu nesnesi
    [SerializeField] GameObject playerAnimator;  // Animatörün bulunduğu nesne
    [SerializeField] AudioSource collisionFX; // Çarpma ses efekti

    void OnCollisionEnter(Collision collision)
    {
        collisionFX.Play();
        // Çarpan nesnenin oyuncu olup olmadığını kontrol et
        if (collision.gameObject == thePlayer)
        {
            // PlayerMovement scriptini devre dışı bırak
            PlayerMovement movement = thePlayer.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.enabled = false;
                Debug.Log("Karakter durdu!");
            }
            else
            {
                Debug.LogError("PlayerMovement componenti bulunamadı!");
            }

            // Animasyonu oynat
            Animator animator = playerAnimator.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("Stumble Backwards");
                Debug.Log("Stumble Backwards animasyonu oynatıldı!");
            }
            else
            {
                Debug.LogError("Animator componenti bulunamadı!");
            }
        }
        StartCoroutine(CollisionEnd());
    }
    IEnumerator CollisionEnd()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}