using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FrameWork.Manager
{
    public class AudioSourcePool:ObjectPool<AudioSource>
    {
        private GameObject soundObj;
        public AudioSourcePool(GameObject _soundObj)
        {
            soundObj = _soundObj;
        }
        public override void PushObj(AudioSource obj)
        {
            base.PushObj(obj);
            obj.enabled = false;

        }
        public override AudioSource GetObj()
        {
            AudioSource audioSource;
            if (doHava())
            {
                audioSource=objQueuePool.Dequeue();
                audioSource.enabled = true;
            }
            else
            {
                audioSource=soundObj.AddComponent<AudioSource>();
            }
            return audioSource;
        }
    }

    public class MusicMgr : BaseManager
    {
        private AudioSource bkMusic = null;

        private  float bkVaule = 1;

        private float soundVaule = 1;

        private GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();

        private readonly AudioSourcePool audioSourcePool;
        public MusicMgr() {
            AppFacade.MonoMgr.AddUpdateListener(Update);
            GameObject obj = new GameObject("Sounds");
            GameObject.DontDestroyOnLoad(obj);
            audioSourcePool = new AudioSourcePool(obj);
        }
        private void Update() {
            for (int i = soundList.Count-1; i >= 0; i--) {
                if (!soundList[i].isPlaying) {
                    audioSourcePool.PushObj(soundList[i]);
                    soundList.RemoveAt(i);
                }
            }
        }

        //播放背景音乐
        public void PlayBKMusic(string name) {
            if (bkMusic == null) {
                GameObject obj = new GameObject("BKMusic");
                bkMusic = obj.AddComponent<AudioSource>();
            }
            //异步加载背景音乐并且加载完成后播放
            AppFacade.ResMgr.LoadAsync<AudioClip>("Music/bk/"+name,(clip) => {
                bkMusic.clip = clip;
                bkMusic.loop = true;
                //调整大小 
                bkMusic.volume = bkVaule;
                bkMusic.Play();
            });
        }
        //改变音量大小
        public void ChangeBKValue(float v) {
            bkVaule = v;
            if (bkMusic == null) {
                return;
            }
            bkMusic.volume = bkVaule;
        }
        //暂停背景音乐
        public void PauseBKMusic() {
            if (bkMusic == null)
            {
                return;
            }
            bkMusic.Pause();  
        }

        //停止背景音乐
        public void StopBKMusic() {
            if (bkMusic == null) {
                return;
            }
            bkMusic.Stop();
        }


        //播放音效
        public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callback=null )
        {
            AudioSource source = audioSourcePool.GetObj();
            AppFacade.ResMgr.LoadAsync<AudioClip>("Music/Sounds/" + name, (clip) => {
                source.clip = clip;
                source.loop = isLoop;
                //调整大小 
                source.volume = soundVaule;
                source.Play();
                //音效资源异步加载结束后，将这个音效组件加入集合中
                soundList.Add(source);
                if (callback != null) {
                    callback(source);
                }
            });
        }
    
        //改变所有音效大小
        public void ChangeSoundValue(float value) {
            soundVaule = value;
            for (int i = 0; i < soundList.Count; ++i) {
                soundList[i].volume = value;
            }
        }
        //停止音效
        public void StopSound(AudioSource source) {
            if (soundList.Contains(source)) {
                soundList.Remove(source);
                source.Stop();
                audioSourcePool.PushObj(source);
            }
        }

        public override void Destroy()
        {
            foreach (var sound in soundList)
            {
                StopSound(sound);
            }

            soundList = null;
            audioSourcePool.Destory();
        }
    }
}