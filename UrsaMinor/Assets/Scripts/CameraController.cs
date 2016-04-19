using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;
    private Rigidbody2D _myRigidbody;
    public GameObject Ursa,
                      Owlbert;

    private float _lastUrsaPos = 0;
    private bool _changingFocus,
                 _watchingUrsa;

    void Start()
    {
        _mainCamera = Camera.main;
        _myRigidbody = this.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (!_changingFocus)
        {
            if (_watchingUrsa)
                Follow(Ursa);
            else
                Follow(Owlbert);
        }
    }

    public void ChangeFocus(GameObject target)
    {
        
    }

    //private void MoveToNewTarget()
    //{
    //    iTween.MoveTo(this.gameObject, )
    //}

    private void Follow(GameObject target)
    {
        if (target.transform.position.x > this.transform.position.x + 1 || target.transform.position.x < this.transform.position.x - 1)
        {
            _myRigidbody.velocity = new Vector2(target.GetComponent<Rigidbody2D>().velocity.x, _myRigidbody.velocity.y);
        }
        else
        {
            _myRigidbody.velocity = new Vector2(0, _myRigidbody.velocity.y);
            this.transform.Translate(new Vector3((target.transform.position.x - this.transform.position.x) * 1f * Time.deltaTime, 0, 0));
        }

        if (target.transform.position.y > this.transform.position.y + 2 || target.transform.position.y < this.transform.position.y - 2)
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, target.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, 0);
            this.transform.Translate(new Vector3(0, (target.transform.position.y - this.transform.position.y) * 1f * Time.deltaTime, 0));
        }
    }
}