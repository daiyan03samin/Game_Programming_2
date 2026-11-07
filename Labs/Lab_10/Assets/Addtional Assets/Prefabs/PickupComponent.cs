using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KartGame.KartSystems
{
    public class PickupComponent : MonoBehaviour
    {
        public ArcadeKart ak;
        public ArcadeKart.StatPowerup powerup;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 30.0f, 0) * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {

                // UNCOMMENT THIS
                gameObject.SetActive(false);
                Invoke("Reset", 3.0f);



                ArcadeKart ak = other.transform.parent.GetComponent<ArcadeKart>();
                if (ak != null)
                {
                    ak.AddPowerup(powerup);
                }
                // UNCOMMENT THIS 
            }

        }
        void Reset()
        {
            gameObject.SetActive(true);
        }
    }
}