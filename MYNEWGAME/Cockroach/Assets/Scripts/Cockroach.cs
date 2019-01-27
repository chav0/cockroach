using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using DG.Tweening;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Cockroach : MonoBehaviour
{
    public string Name;
    public Age Age; 
    public Animator Animator;
    public List<GameObject> Stages; 
    public bool IsPregnant;
    public float TimePregnant;
    public float TimeBirth;
    public float lerp = 0.9f;
    public float speedScaler = 1f;
    public List<string> names; 

    private string json; 
    private Sequence _sequence; 

    public void CockroachCreate(float timeBirth, Age newAge = Age.Young)
    {
        Name = names[(int) Random.Range(0, names.Count - 1)]; 
        
        TimeBirth = timeBirth;
        Age = newAge;

        for (var i = 0; i < Stages.Count; i++)
        {
            Stages[i].SetActive(i == (int)newAge);
        }
        
        _sequence = DOTween.Sequence(); 
            
        transform.localScale = Vector3.zero;

        _sequence.Append(transform.DOScale(1f, .25f).SetEase(Ease.OutBack));

        _sequence.Play();
    }

    /*public void SetPosition(Vector2 angle, Vector2 average, Vector3 cameraPos)
    {
        
        
        transform.position = Vector3.Lerp(transform.position, average, .01f) +
                             Vector3.Lerp(angle, transform.position.normalized / 20f, .1f);

        var posAngle = transform.position - cameraPos; 
        Vector3 perpVector = Vector3.Cross (Vector3.up, angle);
        float out_angle = Mathf.Atan2(Vector3.Dot(Vector3.one, perpVector), 
                              Vector3.Dot(Vector3.up, angle)) * Mathf.Rad2Deg;
        perpVector = Vector3.Cross (Vector3.up, posAngle);
        float out_angle2 = Mathf.Atan2(Vector3.Dot(Vector3.one, perpVector), 
                               Vector3.Dot(Vector3.up, posAngle)) * Mathf.Rad2Deg;

        var delta = transform.eulerAngles.z - out_angle2; 
        Debug.Log(delta);
        out_angle = Mathf.LerpAngle(out_angle, transform.eulerAngles.z, 0.9f);
        transform.eulerAngles = new Vector3(0f,0f, out_angle); 
        
        
    }*/

    public void SetPosition(Vector2 angle, Vector3 cameraPos, Vector2 average, float speed)
    {
        Vector2 vector = cameraPos - transform.position + 100 * new Vector3(angle.x, angle.y);
        vector = vector.normalized; 
        Vector3 perpVector = Vector3.Cross (Vector3.up, vector);
        float out_angle = Mathf.Atan2(Vector3.Dot(Vector3.one, perpVector), 
                              Vector3.Dot(Vector3.up, vector)) * Mathf.Rad2Deg;
        out_angle = Mathf.LerpAngle(out_angle, transform.eulerAngles.z, lerp);
        transform.eulerAngles = new Vector3(0f,0f, out_angle + Mathf.Cos(Time.time) * 1.3f);

        var scaler = (new Vector3(average.x, average.y) - transform.position).sqrMagnitude > 15 ? 1f : speedScaler; 
        //transform.position += Vector3.Lerp(Vector3.zero,
        //new Vector3((float)Math.Cos(out_angle), (float)Math.Sin(out_angle)).normalized * speed, .1f); 
        transform.position += Vector3.Lerp(Vector3.zero, new Vector3(vector.x, vector.y) * speed * scaler, lerp); 

    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        var food = col.GetComponent<Food>();
        if (food != null)
        {
            food.isCollisionEnter = true; 
        }
    }

    public void SetDeath()
    {
        if (_sequence != null)
            _sequence.Kill();
        
        Animator.SetBool("isDeath", true);
        
        _sequence = DOTween.Sequence(); 
        _sequence.AppendInterval(10f);
        _sequence.AppendCallback(() =>
        {
            Destroy(gameObject);
            _sequence.Kill();
        });

        _sequence.Play(); 
    }

    public void SetPregnant(bool isPregnant, float timePregnant)
    {
        IsPregnant = isPregnant;
        TimePregnant = timePregnant; 
    }

    public void ChangeStage(Age newAge)
    {
        if (newAge != Age)
        {
            
            if (_sequence != null)
                _sequence.Kill();
            _sequence = DOTween.Sequence(); 
            
            transform.localScale = Vector3.zero;

            Stages[(int)Age].gameObject.SetActive(false);
            Stages[(int)newAge].gameObject.SetActive(true);

            _sequence.Append(transform.DOScale(1f, .25f).SetEase(Ease.OutBack));

            _sequence.Play();

            Age = newAge; 
        }
            
    }    
}

public enum Age
{
    Young = 0,
    Adult = 1,
    Old = 2,
    Death = 3, 
}
