using UnityEditor;
using UnityEngine;

namespace DistantLands {
    public class Abridge : EditorWindow {
        public static Abridge window;
        private static SceneView.OnSceneFunc onSceneGUIFunc;
        private Vector3 buildPos1;
        private Vector3 buildPos2;
        private GameObject currentGameObject;
        private bool enabled;

        private bool enableHelp;
        private int indexname;
        private bool instantiatePrefab;
        private GameObject newSelectedGameObject;
        private Vector3 offSet;
        private bool placing;
        private LineRenderer previewLine;
        private Vector2 scrollPos;
        private bool showPreview;
        private float strecth = 1;
        private bool zScale = true;

        private void OnEnable() {
            onSceneGUIFunc = OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDestroy() {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnGUI() {
            EditorGUILayout.BeginVertical();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);


            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (!Selection.activeObject) {
                EditorGUILayout.HelpBox("Select a prefab in the project folder to get started.", MessageType.Info);
            }
            else {
                if (!enableHelp)
                    EditorGUILayout.HelpBox("Click the Enable Help to get detailed information on each control.",
                        MessageType.Info);

                if (!enableHelp)
                    enableHelp = EditorGUILayout.Toggle("Enable Help", enableHelp);
                else if (enableHelp) enableHelp = EditorGUILayout.Toggle("Disable Help", enableHelp);

                if (!enabled) {
                    if (enableHelp) {
                        EditorGUILayout.HelpBox("Click the enable button below to enable Abridge.", MessageType.Info);
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.HelpBox("Make sure there is at least 1 existing collider in your scene.",
                            MessageType.Info);
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.HelpBox(
                            "Select a prefab (or prefabs) in the project and left click on the existing collider in the scene to place the start position and then click somewhere else to stretch your prefab.",
                            MessageType.Info);
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.HelpBox(
                            "If you select multiple prefabs, one will be chosen at random everytime you spawn a prefab into the scene.",
                            MessageType.Info);
                    }

                    if (GUILayout.Button("Enable")) enabled = true;
                }
                else if (enabled) {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    zScale = EditorGUILayout.Toggle("Z Scale", zScale);


                    if (enableHelp)
                        EditorGUILayout.HelpBox(
                            "Enable Z Scale if you only want your object to be scaled on the Z axis.",
                            MessageType.Info);

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (enableHelp)
                        EditorGUILayout.HelpBox("Click the disable button below to disable Abridge", MessageType.Info);

                    if (GUILayout.Button("Disable")) enabled = false;

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (enableHelp)
                        EditorGUILayout.HelpBox("Click the reset button to clear the last position you clicked",
                            MessageType.Info);
                    if (GUILayout.Button("Reset")) {
                        buildPos1 = Vector3.zero;
                        placing = false;
                    }


                    EditorGUILayout.Space();
                    EditorGUILayout.Space();


                    if (enableHelp) {
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.HelpBox(
                            "Enable preview to see a debug line from the start position to the end position.",
                            MessageType.Info);
                    }

                    if (!showPreview)
                        showPreview = EditorGUILayout.Toggle("Show Preview", showPreview);
                    else if (showPreview) showPreview = EditorGUILayout.Toggle("Show Preview", showPreview);
                }
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void OnInspectorUpdate() {
            Repaint();
        }

        [MenuItem("Distant Lands/Abridge")]
        public static void ShowWindow() {
            window = GetWindow<Abridge>(false, "Abridge");
        }

        public void OnSceneGUI(SceneView sceneView) {
            if (enabled) {
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

                if (Selection.activeGameObject != null) {
                    if (Selection.gameObjects != null && Selection.activeTransform == null)
                        newSelectedGameObject =
                            Selection.gameObjects[Random.Range(0, Selection.gameObjects.Length - 1)];

                    if (Event.current.type == EventType.MouseDown && Event.current.button == 0) {
                        if (placing) {
                            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit)) {
                                buildPos2 = hit.point;
                                placing = false;

                                if (Selection.activeGameObject != null && Selection.activeTransform == null)
                                    instantiatePrefab = true;
                                if (Selection.activeTransform != null) instantiatePrefab = false;

                                if (instantiatePrefab) AddSingle(buildPos1, buildPos2, hit);

                                if (!instantiatePrefab) WarnUser();
                            }
                        }
                        else {
                            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit)) {
                                buildPos1 = hit.point;

                                placing = true;
                            }
                        }
                    }
                    else {
                        if (showPreview) {
                            if (!previewLine)
                                previewLine = new GameObject().AddComponent<LineRenderer>();

                            previewLine.name = "ABRIDGE DEBUG";
                            var debugMat = (Material)Resources.Load("Debug Material", typeof(Material));
                            previewLine.material = debugMat;
                            previewLine.widthMultiplier = 0.1f;


                            if (placing) {
                                var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                                RaycastHit hit;

                                if (Physics.Raycast(ray, out hit)) {
                                    previewLine.SetPosition(0, buildPos1);
                                    previewLine.SetPosition(1, hit.point);
                                }
                            }
                            else {
                                previewLine.SetPosition(0, Vector3.zero);
                                previewLine.SetPosition(1, Vector3.zero);
                            }
                        }
                        else if (previewLine) {
                            DestroyImmediate(previewLine.gameObject);
                        }
                    }
                }
            }
            else {
                if (previewLine)
                    DestroyImmediate(previewLine.gameObject);
            }
        }

        private void AddSingle(Vector3 buildPos, Vector3 buildPos2, RaycastHit clickedObject) {
            var prefab = Selection.gameObjects[Random.Range(0, Selection.gameObjects.Length - 1)];
            prefab = PrefabUtility.InstantiatePrefab(prefab.gameObject) as GameObject;
            strecth = prefab.GetComponentInChildren<MeshRenderer>().bounds.size.z;
            var newPos = new Vector3(buildPos.x, buildPos.y, buildPos.z);
            var diff = buildPos2 - buildPos;
            var invDiff = buildPos - buildPos2;

            prefab.transform.position = newPos;

            prefab.transform.LookAt(prefab.transform.position + diff.normalized);


            if (zScale)
                prefab.transform.localScale = new Vector3(1, 1, invDiff.magnitude / strecth);
            else
                prefab.transform.localScale = Vector3.one * (invDiff.magnitude / strecth);


            indexname++;

            prefab.name = string.Format("{0}_{1}", prefab.name, indexname);

            Undo.RegisterCreatedObjectUndo(prefab, "Added " + prefab.name + " to Scene");
        }

        private void WarnUser() {
            Debug.Log(
                "You have selected an object in the scene! Please use prefabs or gameobjects from your project window or disable the Point Click Placement Tool");
        }
    }
}