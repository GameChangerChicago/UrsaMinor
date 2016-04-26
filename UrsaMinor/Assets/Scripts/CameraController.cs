using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	private Camera _mainCamera;
	private Rigidbody2D _myRigidbody;
    public GameObject Ursa;

	private float _lastUrsaPos = 0;
	private bool _changingFocus,
		         _watchingUrsa = true,
                 _fadingIn,
                 _fadingOut;

	private float minPosition = -10f;
	private float maxPosition = 89f;
    private SpriteRenderer _fadeMask;
	private Transform cameraTransform;
    private GameObject _target;

	void Start ()
	{
		_mainCamera = Camera.main;
		_myRigidbody = this.GetComponent<Rigidbody2D> ();
        _target = Ursa;
        _fadeMask = GetComponentInChildren<SpriteRenderer>();
		//cameraTransform = _mainCamera.transform;
	}

    void Update()
    {
        if (!_changingFocus)
        {
            Follow();
        }
        else if(Vector2.Distance(_target.transform.position, this.transform.position) < 2)
        {
            _changingFocus = false;
        }

        if (_mainCamera.transform.position.x > maxPosition)
        {
            _mainCamera.transform.position = new Vector3(maxPosition, _mainCamera.transform.position.y, _mainCamera.transform.position.z);
        }

        if (_mainCamera.transform.position.x < minPosition)
        {
            _mainCamera.transform.position = new Vector3(minPosition, _mainCamera.transform.position.y, _mainCamera.transform.position.z);
        }

        if(_fadingIn || _fadingOut)
        {
            Fade();
        }
    }

    public void FadeOut()
    {
        _fadingOut = true;
    }
    public void FadeIn()
    {
        _fadingIn = true;
    }
    private void Fade()
    {
        if(_fadingIn)
        {
            _fadeMask.color = new Color(1, 1, 1, _fadeMask.color.a - (Time.deltaTime));

            if (_fadeMask.color.a < 0.05f)
            {
                _fadeMask.color = new Color(1, 1, 1, 0);
            }
        }
        if(_fadingOut)
        {
            _fadeMask.color = new Color(1, 1, 1, _fadeMask.color.a + (Time.deltaTime));

            if(_fadeMask.color.a > 0.95f)
            {
                _fadeMask.color = new Color(1, 1, 1, 1);
            }
        }
    }

	public void ChangeFocus (GameObject target)
	{
        iTween.MoveTo(this.gameObject, new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z), 0.5f);
        _changingFocus = true;
        _target = target;
        Invoke("ChangeBack", 4);
	}

    private void ChangeBack()
    {
        iTween.MoveTo(this.gameObject, new Vector3(Ursa.transform.position.x, Ursa.transform.position.y, this.transform.position.z), 1);
        _changingFocus = true;
        _target = Ursa;
    }

    //private void MoveToNewTarget()
    //{
    //    iTween.MoveTo(this.gameObject, )
    //}

    private void Follow()
    {
        if (_target.transform.position.x > this.transform.position.x + 1 || _target.transform.position.x < this.transform.position.x - 1)
        {
            _myRigidbody.velocity = new Vector2(_target.GetComponent<Rigidbody2D>().velocity.x, _myRigidbody.velocity.y);
        }
        else
        {
            _myRigidbody.velocity = new Vector2(0, _myRigidbody.velocity.y);
            this.transform.Translate(new Vector3((_target.transform.position.x - this.transform.position.x) * 1f * Time.deltaTime, 0, 0));
        }

        if (_target.transform.position.y > this.transform.position.y + 2 || _target.transform.position.y < this.transform.position.y - 2)
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, _target.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, 0);
            this.transform.Translate(new Vector3(0, (_target.transform.position.y - this.transform.position.y) * 1f * Time.deltaTime, 0));
        }
    }
}