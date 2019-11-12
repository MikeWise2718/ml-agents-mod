using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;

public class CmvAgent : Agent
{
    // Note:
    // We don't actually move the agent around, just its body
    // The body does everything around physics, i.e. collisiton detection, ray perception, etc.
    // This is so we can attach non-convex mesh-based objects (like TextMeshPro) without error messages
    // It is a bit confusing

    public GameObject ground;
    public GameObject area;
    public GameObject redGoal;
    public List<CmvAgent> otherAgents;
    public bool useVectorObs;
 
    Material groundMaterial;
    Renderer groundRenderer;
    CmvAcademy academy;
    CmvAgMan cmvAgMan;
    public CmvAgentBody cmvagbod;

    int selection;
    int nmoves;
    public TextMeshPro bannertmp = null;
    public GameObject bannergo = null;
    public GameObject cube = null;
    public bool showStartMarker = false;
    public SpaceType sType = SpaceType.Continuous;

    public void SetupAgent(CmvAgMan cmvAgMan)
    {
        this.cmvAgMan = cmvAgMan;

        // Initialize agent parameters
        //brain = cmvAgMan.brain;
        area = cmvAgMan.area;
        ground = cmvAgMan.ground;
        redGoal = cmvAgMan.redGoal;
        agentParameters = cmvAgMan.agentParameters;

        useVectorObs = true;

        // Create body
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "body";
        cube.transform.parent = transform;
        cmvagbod = cube.AddComponent<CmvAgentBody>();
        cmvagbod.Init(this);
        var bhp = GetComponent<BehaviorParameters>();
        //bhp.m_UseHeuristic = true;
        var bp = bhp.brainParameters;
        bp.vectorActionSpaceType = SpaceType.Continuous;
        bp.vectorObservationSize = 36;
        this.sType = bp.vectorActionSpaceType;


        // Create banner textmeshpro
        //var text = name + "\n" + name;
        ////bannertmp = GraphAlgos.GraphUtil.MakeTextGo(gameObject, text, 1.5f, fvek: Vector3.back);
        //bannergo = bannertmp.transform.parent.gameObject;
        //bannergo.name = "banner";
    }
    public override void InitializeAgent()
    {
        base.InitializeAgent();
        academy = FindObjectOfType<CmvAcademy>();


        if (showStartMarker)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = transform;
            Destroy(sphere.GetComponent<SphereCollider>());
        }

        groundRenderer = ground.GetComponent<Renderer>();
        groundMaterial = groundRenderer.material;
        nmoves = 0;
        cmvagbod.InitializeAgentBody();
        Debug.Log("Initialized agent " + area.name + ":" + name + " maxStep:" + agentParameters.maxStep);
    }
    public override float[] Heuristic()
    {
        switch (sType)
        {
            case SpaceType.Continuous:
                {
                    var r1 = Random.Range(-1, 1);
                    var r2 = Random.Range(-1, 1);
                    return new float[] { r1, r2 };
                }
            case SpaceType.Discrete:
            {
                    if (Input.GetKey(KeyCode.D))
                    {
                        return new float[] { 3 };
                    }
                    if (Input.GetKey(KeyCode.W))
                    {
                        return new float[] { 1 };
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        return new float[] { 4 };
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        return new float[] { 2 };
                    }
                    return new float[] { 0 };
                }
        }
        return new float[] { 0,0 };
    }
    public void CrowdManInit()
    {
        FindOtherAgents();
    }
    public void FindOtherAgents()
    {
        otherAgents = new List<CmvAgent>(FindObjectsOfType<CmvAgent>());
        otherAgents.Remove(this);
        var removeList = new List<CmvAgent>();
        foreach ( var agent in otherAgents)
        {
            if (agent.area.name!=area.name)
            {
                removeList.Add(agent);
            }
        }
        foreach( var agent in removeList)
        {
            otherAgents.Remove(agent);
        }
        Debug.Log("FindOtherAgents - Agent " + name + " has " + otherAgents.Count + " neighbors");
        var aglist = "";
        var ln = otherAgents.Count;
        foreach(var ag in otherAgents)
        {
            aglist += ag.name;
            if (ag!=otherAgents[ln-1])
            {
                aglist += ",";
            }
        }
        Debug.Log("otherAgents:"+aglist);
    }
    public override void CollectObservations()
    {
        Debug.Log("Collecting observations for " + name + " useVectorobs:" + useVectorObs);
        if (useVectorObs)
        {
            AddVectorObs(GetStepCount() / (float)agentParameters.maxStep);
            var rayobs = cmvagbod.Perceive();
            AddVectorObs(rayobs);
        }
    }
    public Vector3 GetPos()
    {
        return cube.transform.position;
    }

    IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
        //Debug.Log("Swapping ground mat to " + mat.name);
        groundRenderer.material = mat;
        yield return new WaitForSeconds(time);
        groundRenderer.material = groundMaterial;
        //Debug.Log("Swapping ground back ");
    }
    string closestString = "";
    public float FindClosestAgentDist()
    {
        closestString = "";
        var mindist = 9e9f;
        var ln = otherAgents.Count;
        var i = 0;
        var pos = GetPos();
        foreach(var agent in otherAgents)
        {
            var dst = Vector3.Distance(agent.GetPos(), pos);
            if (dst<mindist)
            {
                mindist = dst;
            }
            closestString += agent.name + ":" + dst.ToString("f1");
            if (i<=ln-1)
            {
                closestString += ",";
            }
        }
        closestString += " mindist:"+mindist.ToString("f1");
        return mindist;
    }
    float hitFlashTimeMark = -10;
    float flashTime = 0.1f;

    //Color c0 = GraphAlgos.GraphUtil.getcolorbyname("blue", alpha: 1);
    //Color c1 = GraphAlgos.GraphUtil.getcolorbyname("green", alpha: 1);
    //Color c2 = GraphAlgos.GraphUtil.getcolorbyname("red", alpha: 1);
    //Color c3 = GraphAlgos.GraphUtil.getcolorbyname("black", alpha: 1);
    //Color c4 = GraphAlgos.GraphUtil.getcolorbyname("purple", alpha: 1);
    Color c0 = Color.blue;
    Color c1 = Color.green;
    Color c2 = Color.red;
    Color c3 = Color.black;
    Color c4 = Color.magenta;


    Vector3 lastpos = Vector3.zero;
    public void MoveAgent(float[] act)
    {

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;
        Vector3 fwdDir = cube.transform.forward;
        Vector3 upDir = cube.transform.up;
        if (sType== SpaceType.Continuous)
        {
            dirToGo = fwdDir * Mathf.Clamp(act[0], -1f, 1f);
            rotateDir = upDir * Mathf.Clamp(act[1], -1f, 1f);
        }
        else
        {
            int action = Mathf.FloorToInt(act[0]);
            switch (action)
            {
                case 1:
                    dirToGo = fwdDir * 1f;
                    break;
                case 2:
                    dirToGo = fwdDir * -1f;
                    break;
                case 3:
                    rotateDir = upDir * 1f;
                    break;
                case 4:
                    rotateDir = upDir * -1f;
                    break;
            }
        }
        var forceVek = dirToGo * academy.agentRunSpeed;
        var hitstring = cmvagbod.rpi.GetHitObs();
        var dist = Vector3.Magnitude(cube.transform.position - lastpos);
        var force = Vector3.Magnitude(forceVek);
        //Debug.Log("Move "+name+" "+sType+" rot:"+rotateDir.ToString("F1")+" force:"+forceVek.ToString("F3")+" hit:"+hits+" dst:"+dist.ToString("f1"));
        if (bannertmp!=null)
        {
            bannertmp.text = name + "\n" + hitstring + "\n" +  hitFlashTimeMark.ToString("f1");
        }
        if (cube!=null)
        {
            Color c = c0;
            var stepCount = GetStepCount();
            if (Time.time-hitFlashTimeMark<flashTime)
            {
                c = c4;
            }
            else if (stepCount<0.05f*agentParameters.maxStep)
            {
                var lamb = stepCount / (0.05f * this.agentParameters.maxStep);
                c = Color.Lerp(c0, c1, lamb);
            }
            else
            {
                var lamb = stepCount / (1.0f * this.agentParameters.maxStep);
                c = Color.Lerp(c1, c2, lamb);
            }
            //GraphAlgos.GraphUtil.SetColorOfGo(cube,c);
        }
        //cmvagbod.rpi.DumpRays();
        cmvagbod.AddMovement(rotateDir,forceVek, ForceMode.VelocityChange);
        cmvagbod.SyncToBody(bannergo);
        lastpos = cube.transform.position;
        nmoves++;
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        AddReward(-1f / agentParameters.maxStep);
        var mindist = FindClosestAgentDist();
        if (mindist<3)
        {
            AddReward(-0.5f);
            hitFlashTimeMark = Time.time;
            //StartCoroutine(GoalScoredSwapGroundMaterial(academy.failMaterial, 0.5f)); // happens too often to work
        }
        MoveAgent(vectorAction);
    }
    public void ReachedGoal()
    {
        SetReward(5.0f);
        StartCoroutine(GoalScoredSwapGroundMaterial(academy.goalScoredMaterial, 1.0f));
        //Debug.Log("Found goal - calling done in " + area.name + "   agent:" + name+" moves:"+nmoves);
        Done();
    }

    public override void AgentReset()
    {
        //Debug.Log("AgentReset in " + area.name + " for:" + name+"  nmoves:"+nmoves);
        float agentOffset = -15f;

        selection = Random.Range(0, 2);

        var xpos = 0f + Random.Range(-3f, 3f);
        var ypos = 1f;
        var zpos = agentOffset + Random.Range(-5f, 5f);
        transform.position = new Vector3(xpos, ypos, zpos) + ground.transform.position;
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        cmvagbod.AgentReset();
        redGoal.transform.position = new Vector3(0f, 0.5f, 9f) + area.transform.position;
        nmoves = 0;
    }
}
