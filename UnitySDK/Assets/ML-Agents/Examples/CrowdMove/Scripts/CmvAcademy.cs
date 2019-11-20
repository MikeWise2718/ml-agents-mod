using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class CmvAcademy : Academy {

    public float agentRunSpeed;
    public float agentRotationSpeed;
    public Material goalScoredMaterial; // when a goal is scored the ground will use this material for a few seconds.
    public Material failMaterial; // when fail, the ground will use this material for a few seconds. 
    public float gravityMultiplier; // use ~3 to make things less floaty

    public int successes;
    public int failures;
    public int attempts;
    public int totsteps;
    public float avgsteps;

    public void RegisterSuccess(int nsteps)
    {
        attempts++;
        successes++;
        totsteps += nsteps;
        avgsteps = totsteps*1.0f / attempts;
    }
    public void RegisterFailure(int nsteps)
    {
        attempts++;
        failures++;
        totsteps += nsteps;
        avgsteps = totsteps * 1.0f / attempts;
    }

    public override void AcademyStep()
    {
        envStatMan.AddFloatStat("CrowdMove/Attempts", attempts);
        envStatMan.AddFloatStat("CrowdMove/Successes", successes);
        envStatMan.AddFloatStat("CrowdMove/Failures", failures);
        envStatMan.AddFloatStat("CrowdMove/AvgSteps", avgsteps);
    }


    public override void InitializeAcademy()
    {
        Debug.Log("Initializing Academy");
        Debug.Log(Environment.CommandLine);
        Debug.Log(Environment.GetCommandLineArgs());
        Physics.gravity *= gravityMultiplier;
        RpcComLogger.doMessLogging = true;
        RpcComLogger.maxLogStep = 300;
        RpcComLogger.freqModulo = 3;
    }

    public override void AcademyReset()
    {
        successes = 0;
        failures = 0;
        attempts = 0;
        totsteps = 0;
        avgsteps = 0;
    }
}
