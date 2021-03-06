using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour{

	public static AudioManager instance;

	public Sound[] sounds;

	private void Awake(){

		if (instance != null && instance!=this){
			Destroy(gameObject);
		}
		else{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds){
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}

	private void Start() {
		this.Play("BackgroundMusic");
	}

	public void Play(string sound){
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null){
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		if(!s.loop)
			s.source.PlayOneShot(s.clip);
		else
			s.source.Play();
	}

}
