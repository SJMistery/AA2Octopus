﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;




namespace OctopusController
{

    
    internal class MyTentacleController

    //MAINTAIN THIS CLASS AS INTERNAL
    {

        TentacleMode tentacleMode;
        Transform[] _bones;
        Transform[] _endEffectorSphere;

        public Transform[] Bones { get => _bones; }

        //Exercise 1.
        public Transform[] LoadTentacleJoints(Transform root, TentacleMode mode)
        {
            //TODO: add here whatever is needed to find the bones forming the tentacle for all modes
            //you may want to use a list, and then convert it to an array and save it into _bones
            tentacleMode = mode;

            switch (tentacleMode){
                case TentacleMode.LEG:
                    //TODO: in _endEffectorsphere you keep a reference to the base of the leg
                    Transform[3] boneTr;
                    Transform prov = root;
                    for(int i=0;i<3;i++){
                        Transform[i] = prov->FindChild;
                        prov = prov->FindChild;
                    }
                    _endEffectorSphere = prov;
                    break;
                case TentacleMode.TAIL:
                    //TODO: in _endEffectorsphere you keep a reference to the red sphere 
                    Transform[5] boneTr;
                    Transform prov = root;
                    for(int i=0;i<5;i++){
                        Transform[i] = prov->FindChild;
                        prov = prov->FindChild;
                    }
                    _endEffectorSphere = prov;
                    break;
                case TentacleMode.TENTACLE:
                    //TODO: in _endEffectorphere you  keep a reference to the sphere with a collider attached to the endEffector
                    Transform[50] boneTr;
                    Transform prov = root;
                    for(int i=0;i<50;i++){
                        Transform[i] = prov->FindChild;
                        prov = prov->FindChild;
                    }
                    _endEffectorSphere = prov;
                    break;
            }
            return Bones;
        }
    }
}
