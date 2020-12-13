using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace OctopusController
{
  
    public class MyScorpionController
    {
        //TAIL
        Transform tailTarget;
        Transform tailEndEffector;
        MyTentacleController _tail;
        float animationRange;

        //LEGS
        Transform[] legTargets;
        Transform[] legFutureBases;
        MyTentacleController[] _legs = new MyTentacleController[6];

        
        #region public
        public void InitLegs(Transform[] LegRoots,Transform[] LegFutureBases, Transform[] LegTargets)
        {
            _legs = new MyTentacleController[LegRoots.Length];
            //Legs init
            for(int i = 0; i < LegRoots.Length; i++)
            {
                _legs[i] = new MyTentacleController();
                _legs[i].LoadTentacleJoints(LegRoots[i], TentacleMode.LEG);
                //TODO: initialize anything needed for the FABRIK implementation
            }

        }

        public void InitTail(Transform TailBase)
        {
            _tail = new MyTentacleController();
            _tail.LoadTentacleJoints(TailBase, TentacleMode.TAIL);
            //TODO: Initialize anything needed for the Gradient Descent implementation
        }

        //TODO: Check when to start the animation towards target and implement Gradient Descent method to move the joints.
        public void NotifyTailTarget(Transform target)
        {

        }

        //TODO: Notifies the start of the walking animation
        public void NotifyStartWalk()
        {

        }

        //TODO: create the apropiate animations and update the IK from the legs and tail

        public void UpdateIK()
        {
 
        }
        #endregion


        #region private
        //TODO: Implement the leg base animations and logic
        private void updateLegPos()
        {
            //check for the distance to the futureBase, then if it's too far away start moving the leg towards the future base position
            //
        }
        //TODO: implement Gradient Descent method to move tail if necessary
        private void updateTail()
        {

        }
        //TODO: implement fabrik method to move legs 
        private void updateLegs()
        {
            for (int i=0;i<6;i++)
            {
                float margin = 0.6F;
                Transform currenTarget = legTargets[i];
                Transform currentBase = legFutureBases[i];
                do
                {
                    float[] distanceVects = { 0, 0, 0 };
                    for(int j = 0; j < 2; j++)
                    {
                        distanceVects[j] = Vector3.Distance(_legs[i].Bones[j].position, _legs[i].Bones[j + 1].position);
                    }
                    _legs[i].Bones[2].position = currenTarget.position;
                    for (int j = 2; j > 0; j--)
                    {
                        Transform inverter = _legs[i].Bones[j-1];
                        float distanceReal = Vector3.Distance(_legs[i].Bones[j-1].position, _legs[i].Bones[j].position);
                        Vector3 reposition = _legs[i].Bones[j].position - _legs[i].Bones[j-1].position;
                        reposition.Normalize();
                        reposition = reposition*distanceVects[j-1];

                    }

                    _legs[i].Bones[0].position = currentBase.position;
                    for (int j = 1; i < 3; j++)
                    {
                        Transform inverter = _legs[i].Bones[j];
                        float distanceReal = Vector3.Distance(_legs[i].Bones[j - 1].position, _legs[i].Bones[j].position);
                        Vector3 reposition = - _legs[i].Bones[j].position + _legs[i].Bones[j - 1].position;
                        reposition.Normalize();
                        reposition = reposition * distanceVects[j - 1];
                    }
                } while (Vector3.Distance(currenTarget.position, _legs[i].Bones[2].position) > margin);

                
            }
        }
        #endregion
    }
}
