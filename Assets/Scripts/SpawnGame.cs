using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SpawnGame : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefabRed;
    [SerializeField] private GameObject _playerPrefabBlue;
    [SerializeField] private Transform[] _spawnPointsBlue;
    [SerializeField] private Transform[] _spawnPointsRed;
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private Text coinCounterText;
    
    void Start()
    { 
        SpawnPlayer();
        
    }
    
    public void SpawnPlayer()
    {
        int randomSpawn = Random.Range(0,2);
        
        //Instantiate(_playerPrefab, _spawnPointsBlue[0].position, Quaternion.identity);
        if(randomSpawn == 0)
            PhotonNetwork.Instantiate(_playerPrefabBlue.name, _spawnPointsBlue[Random.Range(0,6)].position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate(_playerPrefabRed.name, _spawnPointsRed[Random.Range(0,6)].position, Quaternion.identity);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void RespawnPlayer()
    {
        SpawnPlayer();
        _deathCanvas.SetActive(false);
        
    }

    public void DeathPlayer(int coinCount)
    {
        coinCounterText.text = coinCount.ToString();
        _deathCanvas.SetActive(true);
    }
    
}
