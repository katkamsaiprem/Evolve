using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
 [SerializeField] private float movementTime,timeForScale;
 [SerializeField] private Vector3 startPosTop, endPosTop, startPosBottom, endPosBottom, startScale, endScale;

 private void Start()
 {
  StartCoroutine(Move());
 }

 private void OnEnable()
 {
  GameManager_.gameEnded += GameEnded;
 }

 private void OnDisable()
 {
  GameManager_.gameEnded-=GameEnded;

 }
 private IEnumerator Move()
 {
  Vector3 posStart = startPosTop + Random.Range(0f, 1f) * (endPosTop - startPosTop);
  transform.position = posStart;
  Vector3 posEnd = startPosBottom + Random.Range(0f, 1f) * (endPosBottom - startPosBottom);
  transform.rotation=Quaternion.identity;

  yield return Scale_Enemy(transform, startScale, endScale, timeForScale);

  float timeElapsed = 0f;
  float Speed = 1 / movementTime;
  Vector3 offset = posEnd - posStart;
  Quaternion startRotation=Quaternion.Euler(0,0,0);
  
  Quaternion endRotation = Quaternion.Euler(0, 0, (Random.Range(0, 2) == 0 ? 1 : -1) * 180);
   

  while (timeElapsed < 1f)
  {
   timeElapsed += Speed * Time.fixedDeltaTime;
   transform.SetPositionAndRotation( posStart+timeElapsed*offset,Quaternion.Lerp(startRotation,endRotation,timeElapsed));
   yield return new WaitForFixedUpdate();//if we dont pass this ,it will wait for next frame(inside pc, update is 0.5 sec,so that is not much physics movement,but problem happens in low-end mobiles
  }
  yield return Scale_Enemy(transform, endScale, Vector3.zero, timeForScale);
  Destroy(gameObject);
  
 }

 void GameEnded()
 {
  StartCoroutine(Scale_Enemy(transform,endScale,Vector3.zero, timeForScale));
  Destroy(gameObject,timeForScale);
 }
 public  IEnumerator Scale_Enemy(Transform target, Vector3 startScale, Vector3 endScale, float timeforFinish)
 {
  // Set the initial scale of the target
  target.localScale = startScale;
    
  float timeElapsed = 0f; // Time elapsed since the start of the scaling
  float speed = 1 / timeforFinish; // Speed of scaling
  Vector3 offset = endScale - startScale; // Difference between the start and end scales

  // Scale the target over time
  while (timeElapsed < 1)
  {
   timeElapsed += speed * Time.deltaTime; // Increment the elapsed time
   target.localScale = startScale + timeElapsed * offset; // Update the scale of the target
   yield return null; // Wait for the next frame
  }

  // Ensure the target reaches the final scale
  target.localScale = endScale;
 }
}
