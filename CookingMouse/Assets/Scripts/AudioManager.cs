using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // 1. 拿起音效（6种完全不同）
    [Header("拿起音效 - 6种不同")]
    public AudioClip pick_bread;
    public AudioClip pick_meat;
    public AudioClip pick_vegetable;
    public AudioClip pick_potato;
    public AudioClip pick_drink;
    public AudioClip pick_sauce;

    // 2. 通用音效（放回、丢垃圾、制作完成）
    [Header("通用音效")]
    public AudioClip putBack;
    public AudioClip throwTrash;
    public AudioClip lockSuccess;

    // 3. 投入制作区分两类
    [Header("投入制作区 - 两类不同")]
    public AudioClip enter_normal;  // 普通食材：面包、肉饼、蔬菜、土豆
    public AudioClip enter_sauce;   // 酱料

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null) instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // 播放拿起音效
    public void PlayPickUp(string tag)
    {
        AudioClip clip = tag switch
        {
            "bread" => pick_bread,
            "meat" => pick_meat,
            "vegetables" => pick_vegetable,
            "potato" => pick_potato,
            "juice" => pick_drink,
            "sauce" => pick_sauce,
            _ => null
        };
        if (clip != null) audioSource.PlayOneShot(clip);
    }

    // 通用
    public void PlayPutBack() => audioSource.PlayOneShot(putBack);
    public void PlayThrowTrash() => audioSource.PlayOneShot(throwTrash);
    public void PlayLockSuccess() => audioSource.PlayOneShot(lockSuccess);

    // 投入区域
    public void PlayEnterNormal() => audioSource.PlayOneShot(enter_normal);
    public void PlayEnterSauce() => audioSource.PlayOneShot(enter_sauce);
}