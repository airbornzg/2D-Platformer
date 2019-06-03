using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_boss_sight : MonoBehaviour
{
    [SerializeField]
    private SemiBoss SemiBoss;
   
     void OnTriggerEnter2D(Collider2D other)
    {
        /*
         * the target of the semiboss
         * was start and will be set for null sometimes
         * however, passing a null variable will be a pain
         * on the ass so use try catch here
         * This case the null variable is just a normal warning
         * not make huge impact on the scene
         */
        try {
            if (other.tag == "Player")
            {
                SemiBoss.Target = other.gameObject;
             //   Debug.Log(SemiBoss.Target);
            }
        } catch (System.NullReferenceException e) {

        }
           
    }

     void OnTriggerExit2D(Collider2D other)
    {
        try {
            if (other.tag == "Player")
            {
                SemiBoss.Target = null;

            }
        } catch (System.NullReferenceException e) {

        }
       
    }
}
