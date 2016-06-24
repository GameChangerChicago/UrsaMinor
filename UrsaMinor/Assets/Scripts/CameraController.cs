using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	private Camera _mainCamera;
	private Rigidbody2D _myRigidbody,
                        _targetRigidbody;
    public GameObject Ursa;

	private float _lastUrsaPos = 0;
	private bool _changingFocus,
		         _watchingUrsa = true,
                 _fadingIn,
                 _fadingOut;

    public float MinPosition;
    public float MaxPosition;
    private SpriteRenderer _fadeMask;
	private Transform cameraTransform;
    private GameObject _target;

	void Start ()
	{
		_mainCamera = Camera.main;
		_myRigidbody = this.GetComponent<Rigidbody2D> ();
        _target = Ursa;
        _targetRigidbody = _target.GetComponent<Rigidbody2D>();
        _fadeMask = GetComponentInChildren<SpriteRenderer>();
		//cameraTransform = _mainCamera.transform;
	}

    void Update()
    {
        if (!_changingFocus)
        {
            Follow();
        }
        else if(Vector2.Distance(_target.transform.position, this.transform.position) < 0.25f)
        {
            _changingFocus = false;
            Ursa.GetComponent<UrsaController>().InputActive = true;
        }

        if (_mainCamera.transform.position.x > MaxPosition)
        {
            _mainCamera.transform.position = new Vector3(MaxPosition, _mainCamera.transform.position.y, _mainCamera.transform.position.z);
        }

        if (_mainCamera.transform.position.x < MinPosition)
        {
            _mainCamera.transform.position = new Vector3(MinPosition, _mainCamera.transform.position.y, _mainCamera.transform.position.z);
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
        Ursa.GetComponent<UrsaController>().InputActive = false;
        _targetRigidbody = _target.GetComponent<Rigidbody2D>();
        Invoke("ChangeBack", 4);
	}

    private void ChangeBack()
    {
        iTween.MoveTo(this.gameObject, new Vector3(Ursa.transform.position.x, Ursa.transform.position.y, this.transform.position.z), 1);
        _changingFocus = true;
        _target = Ursa;
        Ursa.GetComponent<UrsaController>().InputActive = false;
        _targetRigidbody = _target.GetComponent<Rigidbody2D>();
    }

    private void Follow()
    {
        if (_target.transform.position.x > this.transform.position.x + 1 || _target.transform.position.x < this.transform.position.x - 1)
        {
            _myRigidbody.velocity = new Vector2(_targetRigidbody.velocity.x, _myRigidbody.velocity.y);
        }
        else
        {
            _myRigidbody.velocity = new Vector2(0, _myRigidbody.velocity.y);
            this.transform.Translate(new Vector3((_target.transform.position.x - this.transform.position.x) * 1f * Time.deltaTime, 0, 0));
        }

        if (_target.transform.position.y > this.transform.position.y + 1 || _target.transform.position.y < this.transform.position.y - 1)
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, _targetRigidbody.velocity.y);
        }
        else
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, 0);
            this.transform.Translate(new Vector3(0, (_target.transform.position.y - this.transform.position.y) * 0.75f * Time.deltaTime, 0));
        }
    }
}