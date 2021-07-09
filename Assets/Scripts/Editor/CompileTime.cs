using UnityEditor;
using UnityEngine;

namespace Editor
{
    class CompileTime : EditorWindow {
    
        bool isTrackingTime;
        double startTime, finishTime, compileTime;
 
        [MenuItem("Window/Compile Times")]
 
        public static void Init() {
            GetWindow(typeof(CompileTime));
        }
 
        void Update() {
            if (EditorApplication.isCompiling && !isTrackingTime) {
                startTime = EditorApplication.timeSinceStartup;
                isTrackingTime = true;
            }
            else if (!EditorApplication.isCompiling && isTrackingTime) {
                finishTime = EditorApplication.timeSinceStartup;
                isTrackingTime = false;
 
                compileTime = finishTime - startTime;
 
                Debug.Log("Script compilation time:" + compileTime.ToString("0.000") + "s");
            }
        }
    }
}