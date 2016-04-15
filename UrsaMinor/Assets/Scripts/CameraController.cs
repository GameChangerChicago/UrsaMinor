using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;
    public GameObject Ursa;

    //protected bool ursaMoving
    //{
    //    get
    //    {
    //        if (Ursa.GetComponent<Rigidbody2D>().velocity.x > 0.01f)
    //            _ursaMoving = true;
    //        else
    //            _ursaMoving = false;

    //        return _ursaMoving;
    //    }
    //}
    //private bool _ursaMoving;
    //protected bool finishCamMovement
    //{
    //    get
    //    {
    //        return _finishCamMove;
    //    }
    //    set
    //    {
    //        if (_finishCamMove != value)
    //        {
    //            if (value)
    //            {
    //                Hashtable ht = new Hashtable();
    //                ht.Add("easetype", iTween.EaseType.easeOutSine);
    //                ht.Add("x", Ursa.transform.position.x);
    //                iTween.MoveTo(this.gameObject, ht);
    //            }
    //            _finishCamMove = value;
    //        }
    //    }
    //}
    //private bool _finishCamMove;

    private float _lastUrsaPos = 0;

    void Start()
    {
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        //Follow(Ursa);
    }

    public void Follow(GameObject target)
    {
        //if (Ursa.transform.position.x > this.transform.position.x + 2)
        //{
        //    this.transform.Translate(new Vector3(6 * Time.deltaTime, 0, 0));
        //}
        //else 
        if (target.transform.position.x > this.transform.position.x + 0.1f)
        {
            //finishCamMovement = false;
            float xVel = target.transform.position.x - this.transform.position.x;
            this.transform.position = new Vector3(Mathf.SmoothDamp(this.transform.position.x, target.transform.position.x, ref xVel, 0.07f, 5, Time.deltaTime), 0, -10);//new Vector3(Mathf.Lerp(this.transform.position.x, Ursa.transform.position.x, /*ref xVel,*/ 0.05f)* 5 * Time.deltaTime, 0, -10);
        }
        //else if(Ursa.transform.position.x < this.transform.position.x - 2)
        //{
        //    this.transform.Translate(new Vector3(-6 * Time.deltaTime, 0, 0));
        //}
        else if (target.transform.position.x < this.transform.position.x - 0.1f)
        {
            //finishCamMovement = false;
            this.transform.Translate(new Vector3((target.transform.position.x - this.transform.position.x) * 2.5f * Time.deltaTime, 0, 0));
        }
        //else if(Ursa.transform.position.x > this.transform.position.x + 0.01f || Ursa.transform.position.x < this.transform.position.x - 0.01f && !ursaMoving)
        //{
        //    finishCamMovement = true;
        //}
            

        //if (_finishCamMove)
        //{
        //    iTween.MoveTo(this.gameObject, ht);
        //    _finishCamMove = false;
        //}
    }
}