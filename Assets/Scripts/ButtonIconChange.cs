using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonIconChange : MonoBehaviour
{
    public Sprite muteSprite, unmuteSprite;
    public bool isMusic;
    private Sound[] musics,effects;
    private Image image;

    private void Start() {
        musics = Array.FindAll(AudioManager.instance.sounds,item => item.loop == true);
        effects = Array.FindAll(AudioManager.instance.sounds,item => item.loop == false);
        image = GetComponent<Image> ();
    }

    private void Update(){
        if(isMusic){
            foreach (Sound s in musics){
                if(s.source.mute){
                    image.sprite = muteSprite;
                }
                else{
                    image.sprite = unmuteSprite;
                }
            }
        }
        else{
            foreach (Sound s in effects){
                if(s.source.mute){
                    image.sprite = muteSprite;
                }
                else{
                    image.sprite = unmuteSprite;
                }
            }
        }
    }
}
