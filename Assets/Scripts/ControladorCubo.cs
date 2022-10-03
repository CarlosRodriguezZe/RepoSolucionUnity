using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCubo : MonoBehaviour
{
    public Material colorCubo;
    public GameObject cubo;
    private Animator animacion;
    private Transform escala;
    private Vector3 nuevaEscala;
    private bool cambioEnElCubo = false;
    private bool inicioAnimacion = false;
    
    private Animation animacionCorrutina;
    private AnimationCurve curve;
    private AnimationClip clip;

    void Start()
    {
        escala = cubo.GetComponent<Transform>();
        nuevaEscala = new Vector3(2.5f, 2.5f, 2.5f);
        animacion = cubo.GetComponent<Animator>();
        animacion.speed = 0f;
        colorCubo.color = Color.white;

        animacionCorrutina = cubo.GetComponent<Animation>();
        clip = new AnimationClip();
        clip.legacy = true;
    }


    public void ChangeCubeColorAndScale()
    {
        if (cambioEnElCubo == false)
        {
            colorCubo.color = Color.blue;
            escala.localScale = nuevaEscala;
            cambioEnElCubo = true;
        } 
        else
        {
            colorCubo.color = Color.white;
            escala.localScale = new Vector3(1, 1, 1);
            cambioEnElCubo = false;
        }  
    }


    public void StartAnimation()
    {
        if (inicioAnimacion == false)
        {
            animacion.speed = 0.15f;
            inicioAnimacion = true;
        }
        else
        {
            animacion.speed = 0f;
            inicioAnimacion = false;
        } 
    }


    public void StartCoroutineAnimation()
    {
        
        StartCoroutine(Rotation());
                 
    }

    IEnumerator Rotation()
    {
        Keyframe[] keys;

        keys = new Keyframe[3];

        keys[0] = new Keyframe(0.0f, 0.0f);
        keys[1] = new Keyframe(1.0f, -90.0f);
        keys[2] = new Keyframe(2.0f, 0.0f);

        curve = new AnimationCurve(keys);


        clip.SetCurve("", typeof(Transform), "localEulerAngles.y", curve);

        animacionCorrutina.AddClip(clip, "RotarDerecha");
        animacionCorrutina.Play("RotarDerecha");

        yield return new WaitForSeconds(2.0f);

        Keyframe[] keys2;

        keys2 = new Keyframe[3];

        keys2[0] = new Keyframe(0.0f, 0.0f);
        keys2[1] = new Keyframe(1.0f, 90.0f);
        keys2[2] = new Keyframe(2.0f, 0.0f);


        curve = new AnimationCurve(keys2);

        clip.SetCurve("", typeof(Transform), "localEulerAngles.y", curve);
        animacionCorrutina.AddClip(clip, "RotarIzquierda");
        animacionCorrutina.Play("RotarIzquierda");
    }
}
