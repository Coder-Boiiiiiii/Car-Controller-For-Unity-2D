using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Car_Controller_2D : MonoBehaviour
{
    //Public vars
    [Header("Wheels")]
    public List<WheelJoint2D> Wheel_Joints;

    [Header("General Settings")]
    public float Speed;
    public float Torque;

    [Header("Key Settings")]
    public KeyCode[] Brake_Keys;

    //Private Vars
    private float MX;

    public void Update(){
        MX = Input.GetAxis("Horizontal");

        if(MX < 0){
            for(int i = 0; i < Wheel_Joints.Count; i++){
                JointMotor2D motor;
                motor = Wheel_Joints.ElementAt(i).motor;
                motor.motorSpeed = -Speed;
                motor.maxMotorTorque = Torque;
                Wheel_Joints.ElementAt(i).motor = motor;
            }
        }

        else if(MX > 0){
            for(int i = 0; i < Wheel_Joints.Count; i++){
                JointMotor2D motor;
                motor = Wheel_Joints.ElementAt(i).motor;
                motor.motorSpeed = Speed;
                motor.maxMotorTorque = Torque;
                Wheel_Joints.ElementAt(i).motor = motor;
            }
        }

        else{
            for(int i = 0; i < Wheel_Joints.Count; i++){
                JointMotor2D motor;
                motor = Wheel_Joints.ElementAt(i).motor;
                motor.motorSpeed = 0;
                Wheel_Joints.ElementAt(i).motor = motor;
            }
        }

        foreach(KeyCode K in Brake_Keys){
            if(Input.GetKey(K)){
                for(int i = 0; i < Wheel_Joints.Count; i++){
                    JointMotor2D motor;
                    motor = Wheel_Joints.ElementAt(i).motor;
                    motor.motorSpeed = 0;
                    Wheel_Joints.ElementAt(i).motor = motor;
                }
            }
        }
    }
}
