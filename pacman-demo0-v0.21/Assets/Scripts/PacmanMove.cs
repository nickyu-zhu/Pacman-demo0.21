
//using System.Numerics;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PacmanMove : MonoBehaviour
{
    //吃豆人的移动速度
    public float speed = 0.35f;
    //吃豆人下一次移动将要去的目的地
    public Vector2 dest = Vector2.zero;

    private LineRenderer line;
    private int i;
    Vector3 RunStart;
    Vector3 RunNext;

    public  PATH path;

    private void Start()
    {
        //保证吃豆人在游戏刚开始的时候不会动
        dest = transform.position;
        //line = gameObject.GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
        //                                               //        line.SetColors(Color.white, Color.white);//设置颜色  
        //line.SetWidth(0.2f, 0.2f);//设置宽度  
        //i = 0;
    }

    private void FixedUpdate()
    {
        //插值得到要移动到dest位置的下一次移动坐标
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        //通过刚体来设置物体的位置
        GetComponent<Rigidbody2D>().MovePosition(temp);
        //必须先达到上一个dest的位置才可以发出新的目的地设置指令
        if ((Vector2)transform.position == dest)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Valid(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;

            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;

            }
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;

            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;

            }
            Vector2 dir = dest - (Vector2)transform.position;
            if (GameManager.Instance.colorFlag == true && dest != (Vector2)transform.position)
            {
                if (GameManager.Instance.colorPath.Exists(delegate (Vector2 p) { return p == dest; }))
                {
                    //结束 游戏
                    GameObject.Find("Pacman").gameObject.SetActive(false);
                    GameManager.Instance.gamePanel.SetActive(false);
                    Instantiate(GameManager.Instance.gameoverPrefab);
                    Invoke("ReStart", 3f);
                }
                else
                {
                    GameManager.Instance.colorPath.Add(dest);
                    if ((Vector2)transform.position != dest)
                    {
                        if (dir.x < 0)
                        {
                            //生成移动点
                            path.CreatePoint(Vector3.up * 180);

                        }
                        else if (dir.x > 0)
                        {

                            //生成移动点
                            path.CreatePoint(Vector3.zero);
                        }
                        else if (dir.y > 0)
                        {

                            //生成移动点
                            path.CreatePoint(Vector3.forward * 90);
                        }
                        else if (dir.y < 0)
                        {

                            //生成移动点
                            path.CreatePoint(Vector3.forward * -90);
                        }
                    }
                    //RunNext = transform.position;
                    //if (RunStart != RunNext)
                    //{
                    //    i++;
                    //    line.SetVertexCount(i);//设置顶点数 
                    //    Color tmpColor = GameObject.Find("Pacman").gameObject.GetComponent<SpriteRenderer>().color;
                    //    line.SetColors(tmpColor, tmpColor);
                    //    line.SetPosition(i - 1, transform.position);
                    //}
                    //RunStart = RunNext;
                }
            }
            //获取移动方向

            //把获取到的移动方向设置给动画状态机
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);


        }else{
            Debug.Log("移动中");
        }
    }


    //检测将要去的位置是否可以到达
    private bool Valid(Vector2 dir)
    {
        //记录下当前位置
        Vector2 pos = transform.position;
        //从将要到达的位置向当前位置发射一条射线，并储存下射线信息
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        //返回此射线是否打到了吃豆人自身上的碰撞器
        return (hit.collider == GetComponent<Collider2D > ());
    }
}
