using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Car_Controller_2D : MonoBehaviour
{
    //Public vars
    [Header("Wheels")]
    public List<WheelJoint2D> Wheel_Joints; //All of the wheeljoints

    [Header("General Settings")]
    public float Motor_Speed; //The motor speed of the car
    public float Top_Speed; //The top speed of the car (in KPH)
    public float Torque; //The maximum torque of the car

    [Space(15)]

    public Rigidbody2D Car_RigidBody; //The car's rigidbody

    [Header("Key Settings")]
    public KeyCode[] Brake_Keys; //The key(s) that brake the car

    [Header("Scene Settings")]
    public bool Enable_Scene_Settings; //Scene setting boolean
    public KeyCode Restart_Scene_Key; //Restart scene key

    [Header("Audio Settings")]
    public bool Enable_Audio; //Use audio?
    public AudioSource Engine_Sound; //Audio source for engine sound
    public float Minimum_Pitch_Value; //Minimum pitch value for engine
    public float Maximum_Pitch_Value; //Maximum pitch value for engine

    [Header("Debug Values")]
    
    public int Car_Int_Speed_KPH; //Car speed in KPH (integer value)

    //Private Vars
    private float MX;
    private float Float_Speed_KPH;
    private float pitch; //Pitch

    public void Update(){
        //Speed
        Float_Speed_KPH = Car_RigidBody.velocity.magnitude * 3.6f; //Calculate car speed in KPH
        Car_Int_Speed_KPH = (int) Float_Speed_KPH;

        //Horizontal Input
        MX = Input.GetAxis("Horizontal");

        if(Float_Speed_KPH <= Top_Speed){
            //Reverse
            if(MX < 0){
                for(int i = 0; i < Wheel_Joints.Count; i++){
                    JointMotor2D motor;
                    motor = Wheel_Joints.ElementAt(i).motor;
                    motor.motorSpeed = -Motor_Speed;
                    motor.maxMotorTorque = Torque;
                    Wheel_Joints.ElementAt(i).motor = motor;
                }
            }

            //Move Forward
            else if(MX > 0){
                for(int i = 0; i < Wheel_Joints.Count; i++){
                    JointMotor2D motor;
                    motor = Wheel_Joints.ElementAt(i).motor;
                    motor.motorSpeed = Motor_Speed;
                    motor.maxMotorTorque = Torque;
                    Wheel_Joints.ElementAt(i).motor = motor;
                }
            }

            //Stop when not moving
            else{
                for(int i = 0; i < Wheel_Joints.Count; i++){
                    JointMotor2D motor;
                    motor = Wheel_Joints.ElementAt(i).motor;
                    motor.motorSpeed = 0;
                    Wheel_Joints.ElementAt(i).motor = motor;
                }
            }
        }

        //Do not let the car go above the top speed
        else{
            for(int i = 0; i < Wheel_Joints.Count; i++){
                JointMotor2D motor;
                motor = Wheel_Joints.ElementAt(i).motor;
                motor.motorSpeed = 0;
                Wheel_Joints.ElementAt(i).motor = motor;
            }
        }

        //Brake
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

        //Scene Settings
        if(Enable_Scene_Settings){
            //Restart Scene
            if(Input.GetKeyDown(Restart_Scene_Key)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        //Audio 
        if(Enable_Audio){
            //Setting the pitch according to the speed of the car.
            pitch = Float_Speed_KPH/Top_Speed + 1f;
            
            //Do this if the pitch variable exceeds the maximum pitch value
            if(pitch > Maximum_Pitch_Value){
                pitch = Maximum_Pitch_Value;
            }

            //Do this if the pitch variable is lower than the minimum pitch value
            else if(pitch < Minimum_Pitch_Value){
                pitch = Minimum_Pitch_Value;
            }

            //This actually sets the audio source pitch
            Engine_Sound.pitch = pitch;
        }
    }
}
