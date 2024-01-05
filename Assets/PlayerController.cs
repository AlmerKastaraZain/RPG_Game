using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    public Transform movePoint;
    private Vector3 origPos, targetPos;
    public float timeToMove = 0.05f;

    public LayerMask StopMovement;
    private bool checkForCollision(Vector3 direction)
    {
        origPos = transform.position;
        targetPos = origPos + direction;
        targetPos = grid.GetCellCenterWorld(grid.WorldToCell(targetPos));

        movePoint.position = targetPos;

        if (Physics2D.OverlapCircle(movePoint.position, 0.45f, StopMovement))
        {
            movePoint.position = origPos;
            return false;
        }
        movePoint.position = origPos;
        return true;
    }
    void Start()
    {
        movePoint.parent = null;
    }

    public Vector3 m;
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !isMoving)
        {
            if (checkForCollision(Vector3.up))
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !isMoving)
        {
            if (checkForCollision(Vector3.left))
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !isMoving)
        {
            if (checkForCollision(Vector3.down))
            {
                StartCoroutine(MovePlayer(Vector3.down));
            }
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !isMoving)
        {
            if (checkForCollision(Vector3.right))
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }
        }

        //Control Diag
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !isMoving)
        {
            m[0] = 1;
            m[1] = 1;
            if (checkForCollision(m))
            {

                StartCoroutine(MovePlayer(m));
            }
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !isMoving)
        {
            m[0] = -1;
            m[1] = 1;
            if (checkForCollision(m))
            {

                StartCoroutine(MovePlayer(m));
            }
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !isMoving)
        {
            m[0] = -1;
            m[1] = -1;
            if (checkForCollision(m))
            {


                StartCoroutine(MovePlayer(m));
            }
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !isMoving)
        {
            m[0] = 1;
            m[1] = -1;
            if (checkForCollision(m))
            {
                StartCoroutine(MovePlayer(m));
            }
        }



    }



    public Grid grid;
    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        float elaspedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;
        targetPos = grid.GetCellCenterWorld(grid.WorldToCell(targetPos)) + new Vector3(0, 0.03f);

        while (elaspedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elaspedTime / timeToMove));
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
