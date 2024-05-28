//#region Tools
//using System.Collections.Generic;
//using UnityEngine.Events;
//using UnityEngine;

//[Tooltip("The maximum amount of health. THIS IS NOT THE CURRENT HEALTH.")]
//public float maxHealth = 100f;
//[Tooltip("Drag and drop the image game object which contains the bloody frame.")]
//public Image bloodyFrame;
//[Tooltip("The amount of critical health which when reached will make a pulsating effect.")]
//public float criticalHealth = 25f;

//[Tooltip("Will show a blur effect when hit.")]
//public bool useBlurEffect = true;
//[Tooltip("The radial blur image.")]
//public Image blurImage;
//[Tooltip("The duration of the blur effect."), Range(0f, 5f)]
//public float blurDuration = 0.1f;
//[Tooltip("The speed of the blur fading in and out.")]
//public float blurFadeSpeed = 5f;

//[Tooltip("Audio source of pulse sound. Will play when health is critical, if empty will skip.")]
//public AudioSource pulseSound;
//[Tooltip("Fade out certain audio sources when health is critical.")]
//public bool fadeAudios = true;
//[Tooltip("The audio sources you want to fade.")]
//public List<AudioSource> audiosToFade = new List<AudioSource>();
//[Tooltip("The volume to fade the audios to when in critical health.")]
//public float audiosFadeVolume = 0.3f;
//[Tooltip("Turn on/off auto healing.")]
//public bool isDamaged = false;
//[Tooltip("The amount of uninterrupted time required before auto healing kicks in.")]
//public float autoHealTime = 1f;

//[Tooltip("Set an event to trigger on death.")]
//public UnityEvent deathEvent;
//#endregion

//#region SYSTEM VARIABLES
//float opacity = 1f;
//float showDamageOpacity;

//Animator animator;
//Dictionary<AudioSource, float> fadedAudiosCopy;
//PlayerController playerController;
//bool pulseSoundOn = false;
//bool pulseSoundOff = false;
//bool showingBlur = false;
//bool triggerBlur = false;
//bool hideBlur = false;
//bool hideDamage = true;

//int currentVolumeIndex = 0;

//private void CurrentHealth(float health)
//{
//    hideDamage = false;

//    // show radial blur effect
//    if (useBlurEffect && blurImage != null && health > 0f)
//    {
//        blurImage.enabled = true;
//        hideBlur = false;
//        triggerBlur = true;
//    }


//    bloodyFrame.enabled = true;


//    // if health set to something less than 0 - DEAD
//    //if (playerController.PlayerHealth <= 0f)
//    //{
//    //    animator.enabled = false;
//    //    if (pulseSound != null) pulseSoundOff = true;

//    //    //deathEvent.Invoke();

//    //    // change blood frame color to black
//    //    var temp = bloodyFrame.color;
//    //    temp = new Color(0, 0, 0, 255);
//    //    bloodyFrame.color = temp;

//    //    return;
//    //}


//    // turn on/off pulsating effect
//    if (health <= criticalHealth && (health > 0f))
//    {
//        animator.enabled = true;

//        // play pulse sound if not playing
//        if (pulseSound != null)
//        {
//            if (!pulseSound.isPlaying)
//            {
//                pulseSoundOff = false;
//                pulseSound.Play();
//                pulseSoundOn = true;
//            }
//        }
//    }
//    else
//    {
//        if (pulseSound != null)
//        {
//            pulseSoundOn = false;
//            pulseSoundOff = true;
//        }

//        animator.enabled = false;
//    }


//    // calculate the opacity
//    opacity = 1f - (health / maxHealth);


//    // change image opacity value
//    var tempColor = bloodyFrame.color;
//    tempColor = new Color(255, 255, 255, opacity);
//    bloodyFrame.color = tempColor;
//}

//#endregion

//#region UNITY METHODS

//void Start()
//{
//    // Check if bloodyFrame is not null before trying to access its Animator component
//    if (bloodyFrame != null)
//    {
//        animator = bloodyFrame.transform.GetComponent<Animator>();
//    }
//    else
//    {
//        Debug.LogError("Bloody frame is not assigned to ObserveHealth script!");
//    }

//    //CurrentHealth = maxHealth;

//    //set the volume to 0, for better fading experience
//    if (pulseSound != null) { pulseSound.volume = 0f; }
//    if (fadeAudios && audiosToFade.Count > 0) { fadedAudiosCopy = new Dictionary<AudioSource, float>(); }
//}


//void Update()
//{
//    Debug.Log(opacity);

//    // fade the pulse sound in
//    if (pulseSoundOn)
//    {

//        pulseSound.volume += Time.deltaTime * 1f;
//        if (pulseSound.volume >= 1f)
//        {
//            pulseSoundOn = false;
//        }

//        //if fading is checked then fade out audios
//        if (fadeAudios) DecreaseVolumes();
//    }

//    // fade the pulse sound out
//    if (pulseSoundOff)
//    {

//        pulseSound.volume -= Time.deltaTime * 1f;
//        if (pulseSound.volume <= 0f)
//        {
//            pulseSoundOff = false;
//            pulseSound.Stop();
//        }

//        //if fading is checked then fade in audios
//        if (fadeAudios && playerController.PlayerHealth > 0f)
//        {
//            IncreaseVolumes();
//        }
//    }

//    // fade in blur
//    if (triggerBlur)
//    {

//        var tempColor = blurImage.color;
//        tempColor.a += Time.deltaTime * blurFadeSpeed;
//        blurImage.color = tempColor;

//        if (tempColor.a >= 1.9f)
//        {
//            triggerBlur = false;
//            showRadialBlurCoroutine = StartCoroutine(ShowRadialBlur());
//        }
//    }

//    // fade out blur
//    if (hideBlur)
//    {
//        var tempColor = blurImage.color;
//        tempColor.a -= Time.deltaTime * blurFadeSpeed;
//        blurImage.color = tempColor;

//        if (tempColor.a <= 0f)
//        {
//            hideBlur = false;
//            blurImage.enabled = false;
//        }
//    }

//    // hide the damage called from ShowDamage()
//    if (hideDamage)
//    {
//        showDamageOpacity -= Time.deltaTime * showDamageOpacity;
//        bloodyFrame.color = new Color(bloodyFrame.color.r, bloodyFrame.color.g, bloodyFrame.color.b, showDamageOpacity);

//        if (showDamageOpacity <= 0.1)
//        {
//            hideDamage = false;
//        }
//    }
//}

//#endregion

//#region SYSTEM METHODS


//// decrease volume of world audio sources
//void DecreaseVolumes()
//{
//    foreach (AudioSource audio in audiosToFade)
//    {
//        //only decrease volume of sounds that aren't the main pulse sound
//        //and audios that are actually playing
//        if ((audio != pulseSound) && audio != null)
//        {
//            if (audio.isPlaying)
//            {

//                //save the original audio reference
//                if (!fadedAudiosCopy.ContainsKey(audio)) fadedAudiosCopy.Add(audio, audio.volume);

//                if (audio.volume > audiosFadeVolume)
//                {
//                    audio.volume -= Time.deltaTime * 1f;
//                    if (audio.volume <= audiosFadeVolume)
//                    {
//                        audio.volume = audiosFadeVolume;
//                    }
//                }
//                else
//                {
//                    audio.volume += Time.deltaTime * 1f;
//                    if (audio.volume >= audiosFadeVolume)
//                    {
//                        audio.volume = audiosFadeVolume;
//                    }
//                }
//            }
//        }
//    }
//}


//// increase volume of world audio sources
//void IncreaseVolumes()
//{
//    foreach (AudioSource audio in audiosToFade)
//    {
//        currentVolumeIndex++;
//        //only decrease volume of sounds that aren't the main pulse sound
//        if (audio != pulseSound && audio != null)
//        {
//            if (fadedAudiosCopy.ContainsKey(audio))
//            {
//                if (audio.volume < fadedAudiosCopy[audio])
//                {
//                    audio.volume += Time.deltaTime * 1f;
//                    if (audio.volume >= fadedAudiosCopy[audio])
//                    {
//                        audio.volume = fadedAudiosCopy[audio];
//                    }
//                }
//                else
//                {
//                    audio.volume -= Time.deltaTime * 1f;
//                    if (audio.volume <= fadedAudiosCopy[audio])
//                    {
//                        audio.volume = fadedAudiosCopy[audio];
//                    }
//                }
//            }
//        }

//        if (currentVolumeIndex == fadedAudiosCopy.Count)
//        {
//            fadedAudiosCopy.Clear();
//            currentVolumeIndex = 0;
//        }
//    }
//}


//// show the radial blur coroutine
//Coroutine showRadialBlurCoroutine;
//IEnumerator ShowRadialBlur()
//{
//    if (showingBlur) yield break;

//    showingBlur = true;
//    blurImage.enabled = true;

//    yield return new WaitForSeconds(blurDuration);

//    showingBlur = false;
//    hideBlur = true;
//}

//IEnumerator HideShownDamage()
//{
//    yield return new WaitForSeconds(autoHealTime);
//    hideDamage = true;
//}

//#endregion

//#region APIs

//public void ShowDamage(float shownValue)
//{
//    hideDamage = false;

//    bloodyFrame.enabled = true;
//    opacity = 1f - (shownValue / maxHealth);

//    var tempColor = bloodyFrame.color;
//    tempColor = new Color(255, 255, 255, opacity);
//    bloodyFrame.color = tempColor;

//    ShowBlur();

//    if (isDamaged) // if(Damaged)
//    {
//        StopCoroutine("HideShownDamage");
//        StartCoroutine("HideShownDamage");
//    }

//    showDamageOpacity = opacity;
//}

//public void ShowBlur()
//{
//    if (useBlurEffect)
//    {
//        blurImage.enabled = true;
//        hideBlur = false;
//        triggerBlur = true;
//    }
//}

//#endregion