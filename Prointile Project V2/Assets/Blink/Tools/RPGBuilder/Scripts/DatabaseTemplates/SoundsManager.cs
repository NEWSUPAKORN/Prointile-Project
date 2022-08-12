using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Combat;
using BLINK.RPGBuilder.Templates;
using Blink.RPGBuilder.Visual;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.TriggerSoundsList += TriggerSoundsList;
        GameEvents.TriggerSound += InitSound;
        GameEvents.TriggerSoundTemplate += InitSound;
        GameEvents.TriggerSoundEntryOnGameObject += InitSound;
    }

    private void OnDisable()
    {
        GameEvents.TriggerSoundsList -= TriggerSoundsList;
        GameEvents.TriggerSound -= InitSound;
        GameEvents.TriggerSoundTemplate -= InitSound;
        GameEvents.TriggerSoundEntryOnGameObject -= InitSound;
    }

    private void TriggerSoundsList(CombatEntity entity, List<SoundEntry> sounds, ActivationType activationType, Transform targetTransform)
    {
        foreach (var visualEffect in sounds.Where(visualEffect => visualEffect.ActivationType == activationType))
        {
            StartCoroutine(TriggerSoundEntry(entity.gameObject, visualEffect, targetTransform));
        }
    }
    private void InitSound(CombatEntity entity, SoundEntry sound, Transform targetTransform)
    {
        StartCoroutine(TriggerSoundEntry(entity.gameObject, sound, targetTransform));
    }

    private void InitSound(CombatEntity entity, SoundTemplate sound, Transform targetTransform)
    {
        StartCoroutine(TriggerSoundTemplate(sound, targetTransform));
    }

    private void InitSound(GameObject go, SoundEntry sound, Transform targetTransform)
    {
        StartCoroutine(TriggerSoundEntry(go, sound, targetTransform));
    }
    
    private IEnumerator TriggerSoundEntry(GameObject go, SoundEntry sound, Transform targetTransform)
    {
        yield return new WaitForSeconds(sound.Delay);
        int randomSound = Random.Range(0, sound.Template.Sounds.Count);
        if (sound.Template.Sounds[randomSound] != null)
        {
            var audioSource = targetTransform.GetComponent<AudioSource>();
            if (audioSource == null) audioSource = targetTransform.gameObject.AddComponent<AudioSource>();

            if (sound.Parented)
            {
                audioSource.transform.SetParent(targetTransform);
            }

            audioSource.outputAudioMixerGroup = sound.Template.MixerGroup;
            audioSource.bypassEffects = sound.Template.BypassEffects;
            audioSource.bypassListenerEffects = sound.Template.BypassListenerEffects;
            audioSource.bypassReverbZones = sound.Template.BypassReverbZones;
            audioSource.loop = sound.Template.Loop;
            audioSource.priority = (int)Random.Range(sound.Template.Priority.x, sound.Template.Priority.y);
            audioSource.volume = Random.Range(sound.Template.Volume.x, sound.Template.Volume.y);
            audioSource.pitch = Random.Range(sound.Template.Pitch.x, sound.Template.Pitch.y);
            audioSource.panStereo = Random.Range(sound.Template.StereoPan.x, sound.Template.StereoPan.y);
            audioSource.spatialBlend = Random.Range(sound.Template.SpatialBlend.x, sound.Template.SpatialBlend.y);
            audioSource.rolloffMode = sound.Template.rolloffMode;
            audioSource.reverbZoneMix = Random.Range(sound.Template.ReverbZoneMix.x, sound.Template.ReverbZoneMix.y);
            audioSource.dopplerLevel = Random.Range(sound.Template.DopplerLevel.x, sound.Template.DopplerLevel.y);
            audioSource.spread = Random.Range(sound.Template.Spread.x, sound.Template.Spread.y);
            audioSource.minDistance = sound.Template.Distance.x;
            audioSource.maxDistance = sound.Template.Distance.y;
            audioSource.clip = sound.Template.Sounds[randomSound];

            audioSource.Play();

            if (audioSource.loop)
            {
                yield return new WaitForSeconds(sound.Template.LoopDuration);
                audioSource.Stop();
            }
        }
        else
        {
            Debug.LogError(go.name + " tried to play a sound with a missing Template. Name=" + sound.Template.entryName);
        }
    }

    private IEnumerator TriggerSoundTemplate(SoundTemplate sound, Transform targetTransform)
    {
        int randomSound = Random.Range(0, sound.Sounds.Count);
        if (sound.Sounds[randomSound] != null)
        {
            var audioSource = targetTransform.GetComponent<AudioSource>();
            if (audioSource == null) audioSource = targetTransform.gameObject.AddComponent<AudioSource>();

            audioSource.transform.SetParent(targetTransform);

            audioSource.outputAudioMixerGroup = sound.MixerGroup;
            audioSource.bypassEffects = sound.BypassEffects;
            audioSource.bypassListenerEffects = sound.BypassListenerEffects;
            audioSource.bypassReverbZones = sound.BypassReverbZones;
            audioSource.loop = sound.Loop;
            audioSource.priority = (int) Random.Range(sound.Priority.x, sound.Priority.y);
            audioSource.volume = Random.Range(sound.Volume.x, sound.Volume.y);
            audioSource.pitch = Random.Range(sound.Pitch.x, sound.Pitch.y);
            audioSource.panStereo = Random.Range(sound.StereoPan.x, sound.StereoPan.y);
            audioSource.spatialBlend = Random.Range(sound.SpatialBlend.x, sound.SpatialBlend.y);
            audioSource.rolloffMode = sound.rolloffMode;
            audioSource.reverbZoneMix = Random.Range(sound.ReverbZoneMix.x, sound.ReverbZoneMix.y);
            audioSource.dopplerLevel = Random.Range(sound.DopplerLevel.x, sound.DopplerLevel.y);
            audioSource.spread = Random.Range(sound.Spread.x, sound.Spread.y);
            audioSource.minDistance = sound.Distance.x;
            audioSource.maxDistance = sound.Distance.y;
            audioSource.clip = sound.Sounds[randomSound];

            audioSource.Play();

            if (audioSource.loop)
            {
                yield return new WaitForSeconds(sound.LoopDuration);
                audioSource.Stop();
            }
        }
    }
}
