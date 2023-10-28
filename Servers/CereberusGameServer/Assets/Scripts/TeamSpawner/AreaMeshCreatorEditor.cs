using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.TeamSpawner.MeshCreator {
	[CustomEditor(typeof(AreaMeshCreator))]
	public class AreaMeshCreatorEditor : UnityEditor.Editor
	{
		protected AreaMeshCreator _areaMeshCreator;
		protected float m_SquareSideLength;

		/// <summary>
		/// Recreates the mesh when this script becomes active
		/// </summary>
		protected void OnEnable()
		{
			_areaMeshCreator = (AreaMeshCreator)target;
			CreateMesh();
		}

		/// <summary>
		/// Inspector GUI
		/// </summary>
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			GUILayout.BeginVertical();
			ForcePointsFlat();

			GUILayout.Space(10.0f);

			m_SquareSideLength = EditorGUILayout.FloatField("Side Length", m_SquareSideLength);
			if (GUILayout.Button("Generate"))
			{
				SetSquare(m_SquareSideLength);
			}

			GUILayout.EndVertical();
		}

		/// <summary>
		/// Creates the mesh from the points currently in the mesh creator
		/// </summary>
		protected void CreateMesh()
		{
			List<Vector3> vertices3D = _areaMeshCreator.GetPoints();
			Vector2[] vertices2D = new Vector2[vertices3D.Count];
			for (int i = 0; i < vertices3D.Count; i++)
			{
				Vector3 v = _areaMeshCreator.transform.InverseTransformPoint(vertices3D[i]);
				vertices2D[i] = new Vector2(v.x, v.z);
			}
			// Use the triangulator to get indices for creating triangles
			var tr = new Triangulator(vertices2D);
			int[] indices = tr.Triangulate();

			// Create the Vector3 vertices
			Vector3[] vertices = new Vector3[vertices2D.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = new Vector3(vertices2D[i].x, 0, vertices2D[i].y);
			}

			// Create the mesh
			var msh = new Mesh();
			msh.vertices = vertices;
			msh.triangles = indices;
			msh.RecalculateNormals();
			msh.RecalculateBounds();

			var filter = _areaMeshCreator.GetComponent<MeshFilter>();
			if (_areaMeshCreator.GetComponent<MeshFilter>() == null)
			{
				filter = _areaMeshCreator.gameObject.AddComponent<MeshFilter>();
			}
			filter.mesh = msh;

			Mesh mesh = msh;
			int numberTriangles = mesh.triangles.Length / 3;
			int[] triangles = mesh.triangles;
			List<Triangle> trianglesList = new List<Triangle>();

			for (int i = 0; i < numberTriangles; i++)
			{
				Vector3 v0 = mesh.vertices[triangles[i * 3]];
				Vector3 v1 = mesh.vertices[triangles[i * 3 + 1]];
				Vector3 v2 = mesh.vertices[triangles[i * 3 + 2]];
				trianglesList.Add(new Triangle(v0, v1, v2));
			}
			_areaMeshCreator.meshObject = new MeshObject(trianglesList);
		}

		/// <summary>
		/// Makes points coplanar
		/// </summary>
		protected void ForcePointsFlat()
		{
			_areaMeshCreator.ForcePointsFlat();
		}

		/// <summary>
		/// Adds a new point at the midpoint of 2 other points
		/// </summary>
		/// <param name="point1">First point</param>
		/// <param name="point2">Second point</param>
		protected void AddPoint(Transform point1, Transform point2)
		{
			Vector3 first = point1.position, last = point2.position, midpoint = Midpoint(first, last);

			GameObject p = Instantiate(_areaMeshCreator.pointsTransforms[0].gameObject, midpoint, Quaternion.identity);
			p.name = "point";

			p.transform.SetParent(_areaMeshCreator.transform.GetChild(0));
			int index = Mathf.Min(point1.GetSiblingIndex(), point2.GetSiblingIndex()) + 1;
			if (index == 1 && (point1.GetSiblingIndex() == _areaMeshCreator.pointsTransforms.Length - 2 ||
			                   point2.GetSiblingIndex() == _areaMeshCreator.pointsTransforms.Length - 2))
			{
				p.transform.SetAsLastSibling();
			}
			else
			{
				p.transform.SetSiblingIndex(index);
			}
			CreateMesh();

			Undo.RegisterCreatedObjectUndo(p, "Created point");
		}

		/// <summary>
		/// Draws and handles input for manipulating the mesh in scene
		/// </summary>
		protected void OnSceneGUI()
		{
			if (_areaMeshCreator.pointsTransforms == null || _areaMeshCreator.pointsTransforms.Length < 3)
			{
				SetSquare(1);
			}

			if (Event.current.shift && _areaMeshCreator.GetPoints().Count > 3) {
				List<Vector3> allPoints = _areaMeshCreator.GetPoints();
				var plane = new Plane(allPoints[0], allPoints[1], allPoints[2]);
				Vector2 mousePos = Event.current.mousePosition;

				Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
				float rayDistance;
				if (plane.Raycast(ray, out rayDistance)) {
					Transform closestPoint = GetClosetsPoint(ray.GetPoint(rayDistance));

					if (DeleteButton(closestPoint.position)) {
						DeletePoint(closestPoint);
					}
				}
			} else {
				Transform[] points = _areaMeshCreator.pointsTransforms;
				if (points == null)
				{
					return;
				}

				int length = points.Length;
				for (int i = 0; i < length; i++)
				{
					Transform t = points[i];

					float size = HandleUtility.GetHandleSize(t.position) * 0.125f;
					Vector3 snap = Vector3.one * 0.5f;
                    
                    Vector3 newPosition = Handles.FreeMoveHandle(t.position, size, snap, Handles.RectangleHandleCap);
					newPosition.y = t.position.y;

					if (newPosition != t.position)
					{
						t.position = newPosition;
						Undo.RecordObject(t, string.Format("Moved {0}", t.name));
						EditorUtility.SetDirty(t);
						CreateMesh();
					}
				}

				List<Vector3> allPoints = _areaMeshCreator.GetPoints();
				var plane = new Plane(allPoints[0], allPoints[1], allPoints[2]);
				Vector2 mousePos = Event.current.mousePosition;

				Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
				float rayDistance;
				if (plane.Raycast(ray, out rayDistance))
				{
					Transform[] closestPoints = GetClosestTwoPoints(ray.GetPoint(rayDistance));
					Vector3 position = Midpoint(closestPoints[0].position, closestPoints[1].position);

					if (AddButton(position))
					{
						AddPoint(closestPoints[0], closestPoints[1]);
					}
				}

				ForcePointsFlat();
			}

			// maintain selection of object
			Selection.activeGameObject = _areaMeshCreator.gameObject;
		}

		/// <summary>
		/// Gets the 2 closest points to the specified (cursor) position
		/// </summary>
		/// <param name="position">The position of the cursor</param>
		/// <returns>The 2 closest points to the cursor</returns>
		protected Transform[] GetClosestTwoPoints(Vector3 position)
		{
			Transform[] points = new Transform[2];
			points[0] = GetClosetsPoint(position);

			int index = points[0].GetSiblingIndex(), length = _areaMeshCreator.pointsTransforms.Length;
			int previousIndex = index > 0 ? index - 1 : length - 1;
			Transform previous = points[0].parent.GetChild(previousIndex);
			int nextIndex = index < length - 1 ? index + 1 : 0;
			Transform next = points[0].parent.GetChild(nextIndex);

			float previousDistance = Vector3.Distance(previous.position, position);
			float nextDistance = Vector3.Distance(next.position, position);

			points[1] = previousDistance < nextDistance ? previous : next;

			return points;
		}

		/// <summary>
		/// Finds the closest point to the specified (cursor) position
		/// </summary>
		/// <param name="position">The position of the cursor</param>
		/// <returns>The closest point to the cursor</returns>
		protected Transform GetClosetsPoint(Vector3 position)
		{
			Transform[] ordererPoints = _areaMeshCreator.pointsTransforms.OrderBy(x => Vector3.Distance(x.position, position)).ToArray();
			return ordererPoints[0];
		}

		/// <summary>
		/// Deletes the selected point Transform.
		/// </summary>
		protected void DeletePoint(Transform point)
		{
			Undo.DestroyObjectImmediate(point.gameObject);
			CreateMesh();
		}

		/// <summary>
		/// Gets the midpoint between 2 Vector3's
		/// </summary>
		/// <param name="first">First point</param>
		/// <param name="last">Last point</param>
		protected static Vector3 Midpoint(Vector3 first, Vector3 last)
		{
			return (first + last) * 0.5f;
		}

		/// <summary>
		/// Destroys the current points in the mesh
		/// </summary>
		protected void ClearCurrentPoints()
		{
			Transform[] points = _areaMeshCreator.pointsTransforms;
			int length = points.Length;
			for (int i = 0; i < length; i++)
			{
				Undo.DestroyObjectImmediate(points[i].gameObject);
			}
		}

		/// <summary>
		/// Creates a new point at the specified position
		/// </summary>
		/// <param name="position">Position to create the point at</param>
		protected void CreateNewPoint(Vector3 position)
		{
			var point = new GameObject("point");
			point.transform.position = position;
			point.transform.SetParent(_areaMeshCreator.pointsCenter);
			Undo.RegisterCreatedObjectUndo(point, "Created point");
		}

		/// <summary>
		/// Creates a basic square
		/// </summary>
		/// <param name="sideLength">Length of the sides of the square</param>
		protected void SetSquare(float sideLength)
		{
			ClearCurrentPoints();
			Vector3 center = _areaMeshCreator.pointsCenter.position;
			float halfSide = sideLength / 2f;
			CreateNewPoint(center + new Vector3(halfSide, 0, halfSide));
			CreateNewPoint(center + new Vector3(-halfSide, 0, halfSide));
			CreateNewPoint(center + new Vector3(-halfSide, 0, -halfSide));
			CreateNewPoint(center + new Vector3(halfSide, 0, -halfSide));
			CreateMesh();
		}

		bool AddButton(Vector3 position)
		{
			return HandleButton(position, "ADD", 60, 25);
		}

		bool DeleteButton(Vector3 position)
		{
			return HandleButton(position, "DELETE", 60, 25);
		}

		bool HandleButton(Vector3 position, string text, float width, float height)
		{
			Vector2 pos2D = HandleUtility.WorldToGUIPoint(position);

			Handles.BeginGUI();
			bool clicked = GUI.Button(new Rect(pos2D.x - width * 0.5f, pos2D.y - height * 0.5f, width, height), text);
			Handles.EndGUI();

			HandleUtility.Repaint();
			return clicked;
		}
	}
}