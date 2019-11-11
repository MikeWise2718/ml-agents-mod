﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;


public class ParmProcessor
{
    string preset = "";
    Dictionary<string, string> parmsdict;
    Dictionary<string, float> parmfdict;
    Dictionary<string, int> parmidict;
    Dictionary<string, bool> parmbdict;
    public ParmProcessor()
    {
        parmsdict = new Dictionary<string, string>();
        parmfdict = new Dictionary<string, float>();
        parmidict = new Dictionary<string, int>();
        parmbdict = new Dictionary<string, bool>();
    }
    public void SetPreset(string preset)
    {
        this.preset = preset;
    }
    public void Process()
    {
        if (preset == "")
        {
            preset = Environment.CommandLine;
        }
        var par = preset.Split(' ');
        foreach (var p in par)
        {
            var colonda = p.Contains(":");
            if (colonda)
            {
                var sar = p.Split(':');
                var key = sar[0];
                var vals = sar[1];
                parmsdict[key] = vals;
                var fda = float.TryParse(vals, out float valf);
                if (fda)
                {
                    parmfdict[key] = valf;
                }
                var ida = int.TryParse(vals, out int vali);
                if (ida)
                {
                    parmidict[key] = vali;
                }
                var bda = bool.TryParse(vals, out bool valb);
                if (bda)
                {
                    parmbdict[key] = valb;
                }
            }
            else
            {
                parmbdict[p] = true;
            }
        }
    }
    public bool GetBval(string key)
    {
        return parmbdict.ContainsKey(key);
    }
    public string GetSval(string key,string def)
    {
        if (!parmsdict.ContainsKey(key)) return def;
        return parmsdict[key];
    }
    public float GetFval(string key, float def)
    {
        if (!parmfdict.ContainsKey(key)) return def;
        return parmfdict[key];
    }
    public int GetIval(string key, int def)
    {
        if (!parmidict.ContainsKey(key)) return def;
        return parmidict[key];
    }
    public void Dump()
    {
        Debug.Log("parmsdict");
        foreach(var p in parmsdict)
        {
            Debug.Log("   parmsdict[" + p.Key + "]=" + p.Value);
        }
        Debug.Log("parmfdict");
        foreach (var p in parmfdict)
        {
            Debug.Log("   parmfdict[" + p.Key + "]=" + p.Value);
        }
        Debug.Log("parmidict");
        foreach (var p in parmidict)
        {
            Debug.Log("   parmidict[" + p.Key + "]=" + p.Value);
        }
        Debug.Log("parmbdict");
        foreach (var p in parmbdict)
        {
            Debug.Log("   parmbdict[" + p.Key + "]=" + p.Value);
        }
    }
}

public class CmvAgMan : MonoBehaviour
{
    // Start is called before the first frame update
    public int nagents = 1;
    public GameObject ground;
    public GameObject redGoal;
    public GameObject area;
    public BrainParameters brain;
    public AgentParameters agentParameters;


    void CreateAgents()
    {
        Debug.Log("CreateAgents called");
        var pp = new ParmProcessor();
#if UNITY_EDITOR
        pp.SetPreset("p1:par  p2:2.3  p3:3 p4:true p5:false p6");
#endif
        pp.Process();
        pp.Dump();
        area = transform.parent.gameObject;
        ground = area.transform.Find("Ground").gameObject;
        //Debug.Log("Found ground");
        redGoal = area.transform.Find("redGoal").gameObject;
        //Debug.Log("Found redGoal");
        agentParameters = new AgentParameters();
        agentParameters.maxStep = 3000;
        agentParameters.resetOnDone = true;
        agentParameters.onDemandDecision = false;
        agentParameters.numberOfActionsBetweenDecisions = 6;
        var ap = agentParameters;
 
        var bp = brain;

        Debug.Log("AreaInit " + nagents + " agents " + name + " maxStep:" + ap.maxStep + " " +
                  bp.vectorActionSpaceType +" vos:"+bp.vectorObservationSize+" vas:"+bp.vectorActionSize.Length);
        for (var i = 0; i < nagents; i++)
        {
            var aname = "Agent" + i.ToString("D2");
            Debug.Log("Creating agent " + aname);
            var ago = new GameObject(aname);

            //ago.SetActive(false); // if we don't do this it gets enable events too soon (before we can init AgentParameters)
            ago.transform.parent = this.transform;
            var cmvag = ago.AddComponent<CmvAgent>();
            cmvag.SetupAgent(this);
            //ago.SetActive(true);// causes agent's InitializeAgent() to be called
        }
        CrowdManInitAgents();
        //for (var i = 0; i < nagents; i++)
        //{
        //    var aname = "Agent" + i.ToString("D2");
        //    //Debug.Log("Creating agent " + aname);
        //    var ago = transform.Find(aname).gameObject;
        //    ago.SetActive(true);// causes agent's InitializeAgent() to be called
        //}
        Debug.Log("CreateAgents finished");
    }


    void CrowdManInitAgents()
    {
        Debug.Log("CrowdManInitAgents called");
    
        var agents = GameObject.FindObjectsOfType<CmvAgent>();
        foreach( var agent in agents)
        {
            if (agent.area.name==area.name)
            {
                agent.CrowdManInit();
            }
        }
        Debug.Log("CrowdManInitAgents finished");
    }

    void Awake()
    {
        CreateAgents();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
