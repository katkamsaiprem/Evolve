using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [SerializeField] private Vector3 startPos, endPos, startScale, endScale;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float startSpeed, timeForScale;
    [SerializeField]private AudioClip movementSoundClip,pointClip,loseSoundClip;

    private float speed;

    private void Awake()
    {
        speed = startSpeed;
    }

    private void OnEnable()
    {
        GameManager_.gameStarted+=GameStarted;//event subscription
        GameManager_.gameEnded+=GameEnded;
    }

    private void OnDisable()
    {
        GameManager_.gameStarted-=GameStarted;//event unsubscription
        GameManager_.gameEnded-=GameEnded;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySound(movementSoundClip);
            speed*=-1;//change the direction
        }
        
    }

    private void FixedUpdate()
    {
        transform.Translate(speed*Time.fixedDeltaTime*Vector3.right);//move the character
        if (transform.position.x > endPos.x || transform.position.x < startPos.x)//if the character is out of bounds
        {
           // AudioManager.Instance.PlaySound(movementSoundClip);
            speed*=-1;//change the direction
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.OBSTACLE))
        { 
            AudioManager.Instance.PlaySound(loseSoundClip);
          GameManager_.Instance.EndGame();
          GetComponent<Collider2D>().enabled = false;
          Instantiate(explosionPrefab, transform.position, Quaternion.identity);
          return;
        }

        if (other.CompareTag(Constants.Tags.SCORE))
        {
            AudioManager.Instance.PlaySound(pointClip);
            GameManager_.Instance.UpdateScore();
            Destroy(other.gameObject);
            return;
        }
    }


    void GameStarted()
    {
        
        StartCoroutine(Scale_Char(transform,startScale,endScale,timeForScale));
    }
    void GameEnded()
    {
      
        StartCoroutine(Scale_Char(transform,endScale,Vector3.zero, timeForScale));
        Destroy(gameObject,timeForScale);

    }
    // public  IEnumerator Scale_Char(Transform target, Vector3 startScale, Vector3 endScale, float timeforFinish)
    // {
    //     // Set the initial scale of the target
    //     target.localScale = startScale;
    //
    //     float timeElapsed = 0f; // Time elapsed since the start of the scaling
    //     float speed = 1 / timeforFinish; // Speed of scaling
    //     Vector3 offset = endScale - startScale; // Difference between the start and end scales
    //
    //     // Scale the target over time
    //     while (timeElapsed < 1)
    //     {
    //         timeElapsed += speed * Time.deltaTime; // Increment the elapsed time
    //         target.localScale = startScale + timeElapsed * offset; // Update the scale of the target
    //         yield return null; // Wait for the next frame
    //     }
    //
    //     // Ensure the target reaches the final scale
    //     target.localScale = endScale;
    // }
    public IEnumerator Scale_Char(Transform target, Vector3 startScale, Vector3 endScale, float timeforFinish)
    {
        if (timeforFinish <= 0)
        {
            target.localScale = endScale;
            yield break;
        }

        target.localScale = startScale;
        float timeElapsed = 0f;
        float speed = 1 / timeforFinish;
        Vector3 offset = endScale - startScale;

        while (timeElapsed < 1)
        {
            timeElapsed += speed * Time.deltaTime;
            target.localScale = startScale + timeElapsed * offset;
            yield return null;
        }

        target.localScale = endScale;
    }
}
