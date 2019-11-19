using System;
using UnityEngine;

namespace MLAgents
{
    public class RpcComLogger
    {
        public static bool doMessLogging = false;
        public static string messRootDirName = "RpcMessages";
        public static int maxLogStep = 100;
        public static int freqModulo = 1;
        string messDirName = "";
        string messSubDirName = "";
        int stepNum = 0;

        string fmt = "D3";
        private void DoSaveMessageText(string filename, string sdata)
        {
            if (messSubDirName == "")
            {
                messSubDirName = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                messDirName = messRootDirName + "/" + messSubDirName;
                stepNum = 0;
                fmt = "D" + maxLogStep.ToString().Length;
            }
            if (!System.IO.Directory.Exists(messDirName))
            {
                System.IO.Directory.CreateDirectory(messDirName);
            }
            var sstep = stepNum.ToString(fmt);
            var fname = messDirName + "/" + filename + sstep + ".json";
            Debug.Log("Writing " + fname + " len:" + sdata.Length);
            System.IO.File.WriteAllText(fname, sdata);
        }

        public void SaveMessageText(string filename, string sdata, bool incstep = false)
        {
            if (doMessLogging)
            {
                if (stepNum <= maxLogStep)
                {
                    if (freqModulo <= 1 || (stepNum % freqModulo == 0))
                    {
                        DoSaveMessageText(filename, sdata);
                    }
                }
            }
            if (incstep)
            {
                stepNum++;
            }
        }
    }
}