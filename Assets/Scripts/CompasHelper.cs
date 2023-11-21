using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompasHelper : MonoBehaviour
{

    [SerializeField] private ScanTarget [] _scanTargets;
    [SerializeField] private ScanTarget _scanTarget;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _scanTargets = FindObjectsOfType<ScanTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        float nearObjectDistance=100000;
        ScanTarget nearObjectTarget = null;
        foreach (ScanTarget scanTarget in _scanTargets)
        {
            float distance = Vector3.Distance(gameObject.transform.position, scanTarget.gameObject.transform.position);
            if (distance < nearObjectDistance)
            {
                nearObjectDistance = distance;
                nearObjectTarget = scanTarget;
            }
        }

        if (nearObjectTarget != null)
        {

            _scanTarget = nearObjectTarget;
            Vector3 directionToTarget = nearObjectTarget.transform.position - transform.position;
            Debug.Log(directionToTarget);

            
            
            // Create a rotation to face the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Rotate only on the Z-axis
           // targetRotation.x = 0;
           // targetRotation.y = 0;

            // Rotate towards the target smoothly
            transform.rotation =
                targetRotation; //;Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
            /*Quaternion r = transform.rotation;
            r.eulerAngles = new Vector3(0, 0, r.eulerAngles.z);
            transform.rotation = r;*/
        }
    }
}
