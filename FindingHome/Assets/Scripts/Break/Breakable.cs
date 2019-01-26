using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter(Collision collision) {
        var shouldBreak = collision.collider.GetComponents<CubeController>()
            .Where(cube => cube.IsFox && cube.IsDashing)
            .Any();

        if (shouldBreak) {
            Destroy(gameObject);
        }
    }
}
