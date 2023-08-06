using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControll : MonoBehaviour
{
    
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _deathCanvas;
    
    [SerializeField] private float _speed;
    
    [SerializeField] private int _hp = 100;
    
    [SerializeField] private Animator _animator;
    
    [SerializeField] private Text _coinCountText;
    
    public Text textName;
    public Joystick joystick;
    
    [Header("Transforms")]
    [SerializeField] private Transform _up;
    [SerializeField] private Transform _down;
    [SerializeField] private Transform _right;
    [SerializeField] private Transform _left;
    private Transform _curretDirection;
    
    private float _dirX, _dirY;
    private float forceBullet;
    
    
    private int _coinCount { get; set; }
    private GameObject instBullet;
    private Vector2 _moveVector;
    private Rigidbody2D _rb;
    private PhotonView _phView;
    private SpawnGame _mainScript;
    
    SpawnGame _spawnGame;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _phView = GetComponent<PhotonView>();
        textName.text = _phView.Owner.NickName;
        
        joystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<Joystick>();
        _mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<SpawnGame>();

    }

    
    void Update()
    {
        Run();
        Shoot();
        
        
        
    }

    private void FixedUpdate()
    {
        if(_phView.IsMine)
            _rb.velocity = new Vector2(_dirX,_dirY);
    }

    private void Run()
    {
        _dirX = joystick.Horizontal * _speed;
        _dirY = joystick.Vertical * _speed;

        if (joystick.Vertical > 0)
        {
            _curretDirection = _up;
        }
        else if(joystick.Vertical < 0)
        {
            _curretDirection = _down;
        } else if (joystick.Horizontal > 0)
        {
            _curretDirection = _right;
        }
        else
        {
            _curretDirection = _left;
        }
        
        if (_phView.IsMine)
        {
            _animator.SetFloat("Hor", joystick.Horizontal);
            _animator.SetFloat("Vert", joystick.Vertical);
            Vector2 moveAmount = _moveVector.normalized * _speed * Time.deltaTime;
            transform.position += (Vector3) moveAmount;
            /*_rb.MovePosition(_rb.position + _moveVector * _speed * Time.deltaTime);*/
        }
    }

    public void EarnCoin()
    {
        _coinCount += 1;
        _coinCountText.text = _coinCount.ToString();
    }
    
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            if(_phView.IsMine)
                _mainScript.DeathPlayer(_coinCount);
            PhotonNetwork.Destroy(gameObject);
            PhotonNetwork.Instantiate(_coin.name, transform.position, Quaternion.identity);
            
        }
    }
    private void Shoot()
    {
        if (_phView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                PhotonNetwork.Instantiate(_bullet.name, _curretDirection.position, _curretDirection.rotation);

            }
        }
    }
    
    
}
