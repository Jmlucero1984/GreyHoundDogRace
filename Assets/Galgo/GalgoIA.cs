using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalgoIA : MonoBehaviour
{

    public Transform target;        //the enemy
    float seeRange = 200.0f; //maximum attack distance ? 
                             //will attack if closer than 
                             //this to the enemy
    //float shootRange = 20.0f;
    float keepDistance = 15.0f; //closest distance to get to enemy
     
    float rotationSpeed = 5.0f;
    float sightAngle = 90f;
    float speed = 0.25f;
    string state = "PATROL";
    [Header ("Variables de Animación")]
    public Text mitexto;
    public Animator animator;
    public string variableMovimiento;
     

    void Start()
    {
      
  
    }
    void  Update() {
         if (CanSeeTarget())
        {
         
                speed = 0.25f;
                Pursue();
            }
        }
    bool CanSeeTarget()
    {
        var directionToTarget = target.position - transform.position;
        if (Vector3.Distance(transform.position, target.position) > seeRange)
            return false;

        return true;
    }
    void Pursue()
    {
        var position = target.position;
        var direction = position - transform.position;
        direction.y = 0;

        // Rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // Move the character
        mitexto.text = "Dist: " + Math.Round(direction.magnitude, 2);
        if (direction.magnitude > keepDistance)
        {
            direction = direction.normalized * speed;
            transform.position += direction;
            animator.SetFloat(variableMovimiento,76f);
        }
        else
        {
            animator.SetFloat(variableMovimiento,0f);
            speed = 0.00f;
        }
    }
}


/*
    void Start()
    {
        Patrol();
        this.GetComponent<Animator>()["Walk"].wrapMode = WrapMode.Loop;
        this.GetComponent<Animator>()["Run"].wrapMode = WrapMode.Loop;
        this.GetComponent<Animator>()["Sprint"].wrapMode = WrapMode.Loop;
    }

    void Update()
    {
        this.GetComponent<FuzzyBrain>().UpdateStatus();

        if (this.GetComponent<FuzzyBrain>().moodValue < 20)
        {
            state = "RUNAWAY";
            GetComponent<Animator>().CrossFade("Run");
            speed = 0.20f;
            RunAway();
            return;
        }

        if (CanSeeTarget())
        {
            if (this.GetComponent<FuzzyBrain>().moodValue < 50)
            {
                state = "SHOOTING";
                GetComponent<Animator>().CrossFade("Run");
                speed = 0.00f;
                Shoot();
            }
            else if (this.GetComponent<FuzzyBrain>().moodValue < 80)
            {
                state = "PURSUE";
                GetComponent<Animator>().CrossFade("Run");
                speed = 0.08f;
                Pursue();
            }
        }
        else
        {
            state = "PATROL";
            if (!GetComponent<Animator>().IsPlaying(" Idle"))
            {
                GetComponent<Animator>().Play("Idle");
                speed = 0.00f;
            }
            Patrol();
        }

    }

    void Patrol()
    {
        //stand around
    }


    bool CanSeeTarget()
    {
        var directionToTarget = target.position - transform.position;
        if (Vector3.Distance(transform.position, target.position) > seeRange)
            return false;

        return true;
    }

    bool CanShoot()
    {
        if (Vector3.Distance(transform.position, target.position) > shootRange)
            return false;

        return true;
    }


    void Pursue()
    {
        var position = target.position;
        var direction = position - transform.position;
        direction.y = 0;

        // Rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // Move the character
        if (direction.magnitude > keepDistance)
        {
            direction = direction.normalized * speed;
            transform.position += direction;
        }
        else
        {
            GetComponent<Animation>().Play("Galgo_Idle");
            speed = 0.00f;
        }
    }

    void RunAway()
    {
        var position = target.position;
        var direction = transform.position - position;
        direction.y = 0;

        // Rotate away from the target
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // Move away the character
        if (direction.magnitude < safeDistance)
        {
            direction = direction.normalized * speed;
            transform.position += direction;
        }
        else
        {
            GetComponent<Animation>().Play("Galgo_Idle");
            speed = 0.00f;
        }
    }

    void Shoot()
    {
        var position = target.position;
        var direction = position - transform.position;
        direction.y = 0;

        // Rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction), rotationSpeed *
            Time.deltaTime);
        transform.eulerAngles = new Vector3(0,
            transform.eulerAngles.y, 0);
    }*/
