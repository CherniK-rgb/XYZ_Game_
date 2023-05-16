using System;
using System.Collections.Generic;
using UnityEngine;

/*
    - const Speed = 5.0
    - swamp - debuff - -50%
    - enemy - debuff - -2m/s

    когда игрок заходит в болото и в него кастуют, у него одна скорость 
    а когда в него кастуют а потом заходит в болото другая
*/
class Player : MonoBehaviour
    {
        float speed = 5.0f;
        float baseSpeed = 5.0f;

        private List<DebuffEffect> debuffs;

        public void AddDebuff(DebuffEffect debuff)
        {
            if (debuffs.Contains(debuff)) return;

            debuffs.Add(debuff);

            RecalculateSpeed();
        }
        public void RemoveDebuff(DebuffEffect debuff)
        {
            if (!debuffs.Contains(debuff)) return;

            debuffs.Remove(debuff);

            RecalculateSpeed();
        }

        private void RecalculateSpeed()
        {
            speed = baseSpeed;

            foreach (DebuffEffect debuff in debuffs)
            {
                speed -= debuff.AbsoluteValue; 
            }

            foreach (DebuffEffect debuff in debuffs)
            {
                speed *= debuff.RelativeValue; 
            }

            if (speed < 0) speed = 0;
        }
        private void Move(float speed)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void Update()
        {
            Move(speed);
        }
    }

class Swamp
    {
        DebuffEffect debuff = new DebuffEffect();
        void OnEnter(Player Player)
        {
            Player.AddDebuff(debuff);
        }

        void OnExit(Player Player)
        {
            Player.RemoveDebuff(debuff);
        }
    }

class Enemy
    {
        DebuffEffect debuff = new DebuffEffect();
        void OnStartCast(Player Player)
        {
            Player.AddDebuff(debuff);
        }
        void OnStopCast(Player Player)
        {
            Player.RemoveDebuff(debuff);
        }
    }

class DebuffEffect
    {
        [Range(0, 1)] public float RelativeValue;
        public float AbsoluteValue;
    }
