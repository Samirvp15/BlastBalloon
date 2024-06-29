using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SpawnBalloonExtraLevel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        Instantiate(GameManager.Instance.balloonExtraLevel, gameObject.transform.position, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
