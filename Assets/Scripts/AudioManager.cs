using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    [Header("Components")]
    [SerializeField] AudioClip _jump;
    [SerializeField] AudioClip _coin;
    [SerializeField] AudioClip _gui;
    [SerializeField] AudioClip _damage;
    [SerializeField] AudioClip _win;
    [SerializeField] AudioClip _defeatSlime;
    private AudioSource _as;

    public static AudioManager Instance {  get { return _instance; } }

    private void Awake()
    {
        _as = gameObject.AddComponent<AudioSource>();

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _as.PlayOneShot(clip);
            _as.volume = 0.5f;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    public void PlayJump() { PlaySound(_jump); }
    public void PlayCoin() { PlaySound(_coin); }
    public void PlayGui() { PlaySound(_gui); }
    public void PlayDefeatSlime() { PlaySound(_defeatSlime); }
    public void PlayDamage() { PlaySound(_damage); }
    public void PlayWin() { PlaySound(_win); }
}
