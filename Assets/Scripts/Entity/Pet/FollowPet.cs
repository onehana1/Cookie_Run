using System.Collections;
using UnityEngine;

public class FollowPet : MonoBehaviour
{
    public Transform player;
    public float xOffset = 2.0f; 
    public float followSpeed = 5.0f; 
    public float followDelay = 0.2f;

    private float fixedX;  
    private float targetY; 

    private void Start()
    {
        if (player == null)
        {
            return;
        }

        fixedX = player.position.x + xOffset;
        targetY = player.position.y;

        StartCoroutine(FollowYPosition());
    }

    private IEnumerator FollowYPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(followDelay); 
            targetY = player.position.y;  
        }
    }

    private void Update()
    {
        transform.position = new Vector3(fixedX, Mathf.Lerp(transform.position.y, targetY, followSpeed * Time.deltaTime), transform.position.z);
    }
}
