using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _blood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);               
            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1<<0))
            {
                IDamageable damagedObject = hitInfo.collider.GetComponent<IDamageable>();
                if(damagedObject != null)
                {
                    var createdBlood = Instantiate(_blood, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    createdBlood.transform.parent = hitInfo.transform;
                    damagedObject.Damage(10);
                    Destroy(createdBlood, 1.5f);
                }
            }
        }    
    }
}
