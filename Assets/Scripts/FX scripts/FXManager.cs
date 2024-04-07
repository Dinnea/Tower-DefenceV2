using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    [SerializeField] GameObject _floatingText;
    AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventBus<EnemyKilledEvent>.OnEvent += onEnemyKilled;
    }
    private void OnDisable()
    {
        EventBus<EnemyKilledEvent>.OnEvent -= onEnemyKilled;
    }
    private void onEnemyKilled(EnemyKilledEvent e)
    {
        Enemy enemy = e.enemy;
        _floatingText.GetComponentInChildren<TextMesh>().text = "+" + enemy.GetMoney().ToString();
        Instantiate(_floatingText, enemy.GetWorldLocation(), _floatingText.transform.rotation);
        //if(!_audioSource.isPlaying) _audioSource.Play();
    }
}
