using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeBridge : MonoBehaviour
{
    [Range(2, 1000)]
    [SerializeField] private int Curiosity;
    [SerializeField] private float Width;

    private LineRenderer RopeRenderer;
    private List<Transform> Cells = new List<Transform>();

    private void Awake()
    {
        RopeRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < Cells.Count; i++)
        {
            RopeRenderer.SetPosition(i, Cells[i].position);
        }
    }

    public void Generate(Vector2 first_point, Vector2 second_point, HingeJoint2D last_joint=null)
    {
        Clear();

        float progress = 0;
        Rigidbody2D last_cell = null;
        RopeRenderer.positionCount = Curiosity;
        float Lenght = Vector2.Distance(first_point, second_point);


        for (int i = 0; i < Curiosity; i++)
        {
            Vector2 cell_position = Vector2.Lerp(first_point, second_point, progress);
            Vector2 cell_size = new Vector2(Lenght / Curiosity, Width);

            GameObject new_cell = new GameObject();
            new_cell.layer = LayerMask.NameToLayer("Rope");
            new_cell.transform.position = cell_position;
            new_cell.transform.localScale = cell_size;

            progress += 1f / Curiosity;

            Rigidbody2D rigidbody = new_cell.AddComponent<Rigidbody2D>();
            BoxCollider2D collider = new_cell.AddComponent<BoxCollider2D>();
            HingeJoint2D hingle_joint = new_cell.AddComponent<HingeJoint2D>();

            if( i == Curiosity - 1)
            {
                if(last_joint != null)
                {
                    last_joint.connectedBody = rigidbody;
                }
            }

            if (last_cell != null)
            {
                hingle_joint.connectedBody = last_cell;
                hingle_joint.enableCollision = true;
            }

            Cells.Add(new_cell.transform);

            RopeRenderer.startWidth = Width;
            RopeRenderer.endWidth = Width;

            last_cell = rigidbody;
        }
    }
    
    public void Clear()
    {
        foreach (var cell in Cells)
        {
            Destroy(cell.gameObject);
        }

        RopeRenderer.positionCount = 0;
        Cells = new List<Transform>();
    }
}
