using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace OctopusController
{
    public enum TentacleMode { LEG, TAIL, TENTACLE };

    public class MyOctopusController 
    {
        
        MyTentacleController[] _tentacles =new  MyTentacleController[4];

        Transform _currentRegion;
        Transform _target;

        Transform[] _randomTargets;// = new Transform[4];


        float _twistMin, _twistMax;
        float _swingMin, _swingMax;

        #region public methods
        //DO NOT CHANGE THE PUBLIC METHODS!!

        public float TwistMin { set => _twistMin = value; }
        public float TwistMax { set => _twistMax = value; }
        public float SwingMin {  set => _swingMin = value; }
        public float SwingMax { set => _swingMax = value; }
        

        public void TestLogging(string objectName)
        {

           
            Debug.Log("hello, I am initializing my Octopus Controller in object "+objectName);

            
        }

        public void Init(Transform[] tentacleRoots, Transform[] randomTargets)
        {
            _tentacles = new MyTentacleController[tentacleRoots.Length];

            // foreach (Transform t in tentacleRoots)
            for(int i = 0;  i  < tentacleRoots.Length; i++)
            {

                _tentacles[i] = new MyTentacleController();
                _tentacles[i].LoadTentacleJoints(tentacleRoots[i],TentacleMode.TENTACLE);
                i++;
                //TODO: initialize any variables needed in ccd
            }

            _randomTargets = randomTargets;
            //TODO: use the regions however you need to make sure each tentacle stays in its region

        }

              
        public void NotifyTarget(Transform target, Transform region)
        {
            _currentRegion = region;
            _target = target;
        }

        public void NotifyShoot() {
            //TODO. what happens here?
            Debug.Log("Shoot");
        }


        public void UpdateTentacles()
        {
            //TODO: implement logic for the correct tentacle arm to stop the ball and implement CCD method
            float[] distance = new float[4];
            int moveArm = 0;
            for(int i=0;i<4;i++)
            {
               distance[i] = Vector3.Distance(_target.position, _tentacles[i].Bones[49].position);
            
             }
            float targetD = distance.Min();
            for (int i = 0; i < 4; i++)
            {
                if (distance[i] == targetD)
                {
                    moveArm = i;
                    
                }
                

            }
            float margin = 12;
            do
            {
                for (int i = 49; i > 0; i--)
                {
                    Transform temp = _tentacles[moveArm].Bones[i-1];
                    temp.LookAt(_target);
                   Quaternion retro = temp.rotation;
                    Vector3 distor = _tentacles[moveArm].Bones[49].position - _tentacles[moveArm].Bones[i-1].position;
                    Vector3 result = retro * distor;
                }
            } while (Vector3.Distance(_target.position, _tentacles[moveArm].Bones[49].position) > margin);





            update_ccd();
        }




        #endregion


        #region private and internal methods
        //todo: add here anything that you need

        void update_ccd() {
           

        }


        

        #endregion






    }
}
