using UnityEngine;

public class MyPlayer : MonoBehaviour

{
    #region Private Fields

    [SerializeField]
    private int _ammo;

    [SerializeField]
    private int _ammoCapacity = 10;

    [SerializeField]
    private bool _isReLoading = false;

    [SerializeField]
    private bool _isEmpty = false;

    [SerializeField]
    private Vector3 _laserOffSet = new Vector3(0.0f, 0.8f, 0.0f);

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _reloadTime = 1.5f;

    [SerializeField]
    private float _reloadTimer = 0.0f;

    [SerializeField]
    private float _speed = 3.5f;

    #endregion Private Fields

    #region Private Methods

    private void Ammocheck()
    {
        _isEmpty = ( _ammo == 0 );

        if (_isEmpty)
        {
            _isReLoading = true;
            Reloading();
        }
        else
        {
            ResetReloadTimer();
        }
    }

    private void FireLaser()
    {
        Instantiate(_laserPrefab, (transform.position + _laserOffSet), Quaternion.identity);
        _ammo -= 1;
    }

    private void PlayerMovement()
    {
        // Setting player boundaries

        float rightBoundary = 11f;
        float leftBoundary = -11f;
        float upperBoundary = 0f;
        float lowerBoundary = -3.8f;

        // Moving

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // Boundary checking & Adjusting

        float xPosition = transform.position.x;
        float yPosition = transform.position.y;

        if (xPosition > rightBoundary)
        {
            transform.position = new Vector3(leftBoundary, yPosition, 0);
        }
        else if (xPosition < leftBoundary)
        {
            transform.position = new Vector3(rightBoundary, yPosition, 0);
        }

        if (yPosition > upperBoundary)
        {
            transform.position = new Vector3(xPosition, upperBoundary, 0);
        }
        else if (yPosition < lowerBoundary)
        {
            transform.position = new Vector3(xPosition, lowerBoundary, 0);
        }
    }

    private void RefillAmmo()
    {
        _ammo = _ammoCapacity;
        _isReLoading = false;
    }

    private void Reloading()
    {
        if (_reloadTimer < _reloadTime)
        {
            AugmentReloadTimer();
        }
        else
        {
            RefillAmmo();
        }
    }

    private void ResetReloadTimer()
    {
        _reloadTimer = 0.0f;
    }

    private void AugmentReloadTimer()
    {
        _reloadTimer += Time.deltaTime;
    }

    private void Start()
    {
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        _ammo = _ammoCapacity;
    }

    private void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && (!_isReLoading))
        {
            FireLaser();
        }

        Ammocheck();
    }

    #endregion Private Methods
}