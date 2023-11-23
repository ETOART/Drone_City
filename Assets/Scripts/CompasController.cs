using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompasController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform arrow;


    private void LateUpdate()
    {
        //// Получите направление от стрелки к цели
        //Vector3 direction = target.position - arrow.position;

        //Quaternion rotation = Quaternion.LookRotation
        //    (target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        //arrow.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        //Vector3 direction = target.position - arrow.position;

        // Вычислите угол между направлением и вектором (1, 0) в 2D пространстве
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.LookAt(target);
        arrow.transform.eulerAngles = new Vector3(0, arrow.transform.eulerAngles.z, 0);




    }
}
