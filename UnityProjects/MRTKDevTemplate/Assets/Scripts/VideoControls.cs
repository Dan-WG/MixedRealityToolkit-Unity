using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControls : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<UnityEngine.Video.VideoPlayer> videoList = new List<UnityEngine.Video.VideoPlayer>(); //Lista de videos
    int[] index;
    [SerializeField] private GameObject loadingIcon;
    
    private void Awake() 
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();//componente de video para funcion de reload
    }

    private void Start() {
        //Revisar en qué paso vamos y asignar el video correspondiente al videoPlayer
        index = JSONReader.diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum - 1].VideoIndex;
    }
    private void Update()
    {
        Debug.Log("Frame: " + videoPlayer.frame);
    }

    private void OnEnable() {
        loadingIcon.SetActive(true);
        index = JSONReader.diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum - 1].VideoIndex;
        videoPlayer.prepareCompleted += videoPreparation;
        videoPlayer.clip = videoList[index[0]].clip;
       
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack (0, true);
        if (videoPlayer.clip != null)
        {
            videoPlayer.Prepare();
            Debug.Log("Preparing...");
        }


        //reload();
    }

    void videoPreparation(VideoPlayer videoPlayer)
    {
        

        if (videoPlayer.isPrepared)
        {
            Debug.Log("Video is ready");
            videoPlayer.frame = 0;
            videoPlayer.Play();
            loadingIcon.SetActive(false);
        }
        
        
        
    }
   
    public void reload() //reiniciar video 
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}
