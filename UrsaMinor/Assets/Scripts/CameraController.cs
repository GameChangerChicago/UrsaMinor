using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	private Camera _mainCamera;
	private Rigidbody2D _myRigidbody;
    public GameObject Ursa;

	private float _lastUrsaPos = 0;
	private bool _changingFocus,
		         _watchingUrsa = true;

	private float minPosition = -10f;
	private float maxPosition = 89f;
	private Transform cameraTransform;
    private GameObject _target;

	void Start ()
	{
		_mainCamera = Camera.main;
		_myRigidbody = this.GetComponent<Rigidbody2D> ();
        _target = Ursa;
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
            Debug.Log("eh?");
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
    }

	public void ChangeFocus (GameObject target)
	{
        iTween.MoveTo(this.gameObject, new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z), 1);
        _changingFocus = true;
        _target = target;
        Invoke("ChangeBack", 2);
	}

    private void ChangeBack()
    {
        iTween.MoveTo(this.gameObject, Ursa.transform.position, 1);
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