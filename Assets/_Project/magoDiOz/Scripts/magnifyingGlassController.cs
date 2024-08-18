using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnifyingGlassController : MonoBehaviour
{
    private enum STATE { ACTIVE, FOUND, COMPLETE };

    private STATE state ;

    private float timer;

    private float delay = 1.6f;

    [SerializeField]
    private string targetTag = "scarpette";

    [SerializeField]
    private GameObject lens; // Sprite trasparente che rappresenta la lente

    [SerializeField]
    private MDO_MuralesController manager;

    private Camera cam;

    private LayerMask layerMask = 1 << 6; // maschera layer "murales"

    private void OnEnable()
    {
        this.state = STATE.ACTIVE;
        this.transform.position = manager.transform.position;
    }

    private void Awake()
    {
        manager.enabled = true;
        this.cam = Camera.main;
        this.transform.position = manager.transform.position;

    }

    private void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 1.3f;//cam.nearClipPlane + 0.6f; //1.3f;

            //if (Physics.Raycast(this.cam.ScreenToWorldPoint(mousePosition), this.cam.transform.forward, out RaycastHit hitRay, layerMask)) //Physics.Raycast(this.cam.transform.position, (this.cam.ScreenToWorldPoint(mousePosition) - this.cam.transform.position)
            //{
            //    Vector3 collisionPoint = hitRay.point;
            //    Vector3 direction = hitRay.normal;
            //    collisionPoint.x -= 0.1f;
            //    //collisionPoint.y += 0.01f;
            //    this.transform.position = collisionPoint;// + direction * 0.2f;
            //}
            mousePosition.z = manager.transform.position.z - cam.nearClipPlane;
            this.transform.position = this.cam.ScreenToWorldPoint(mousePosition);


        }

    switch (this.state) {
            case STATE.ACTIVE:

                if (IsTargetFound())
                {
                        this.state = STATE.FOUND;
                        timer = 0;
                }

                break;

            case STATE.FOUND:
                this.timer += Time.deltaTime;

                if(this.timer >= this.delay)
                {
                    this.manager.ChangeStateBN();
                    this.transform.position = manager.transform.position; // resetta la posizione altrimenti appena ricompare sta sulle scarpette
                    this.state = STATE.COMPLETE;
                }
                break;
                

        }
    

    }

    private bool IsTargetFound()
    {
        if (Physics.Raycast(this.lens.transform.position, this.lens.transform.forward, out RaycastHit hit)) // se sto sulle scarpette
        {
            GameObject gameObject = hit.collider.gameObject;

            return (gameObject.CompareTag(targetTag));

        }

        return false;
        
    }



}
