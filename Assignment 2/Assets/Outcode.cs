using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcode : MonoBehaviour {

   public bool up;
   public bool down;
   public bool left;
   public bool right;

    public Outcode() //Return 0000
    {
        up = false;
        down = false;
        left = false;
        right = false;

    }

    public Outcode(Vector3 v)
    {
        checkUp(v.y);
        checkDown(v.y);
        checkLeft(v.x);
        checkRight(v.x);
    }

    public Outcode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public string toString()
    {
        byte upValue = Convert.ToByte(up);
        byte downValue = Convert.ToByte(down);
        byte lValue = Convert.ToByte(left);
        byte rValue = Convert.ToByte(right);


        return "Result : /n " + upValue + "-" + downValue + "-" + lValue + "-" + rValue;
    }

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    bool checkUp(float y)
    {
        if (y > 1)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        return up;
    }

    bool checkDown(float y)
    {
        if (y < -1)
        {
            down = true;
        }
        else
        {
            down = false;
        }


        return down;
    }
    bool checkLeft(float x)
    {
        if (x < -1)
        {
            left = true;
        }
        else
        {
            left = false;
        }

        return left;
    }
    bool checkRight(float x)
    {
        if (x > 1)
        {
            right = true;
        }
        else
        {
            right = false;
        }

        return right;
    }


    public static Outcode orOp(Outcode x, Outcode y)
    {
        Outcode outcode = new Outcode();

        outcode.up = (x.up || y.up);
        outcode.down = (x.down || y.down);
        outcode.left = (x.left || y.left);
        outcode.right = (x.right || y.right);

        return outcode;

    }

    public static Outcode operator &(Outcode x, Outcode y)
    {
        Outcode outcode = new Outcode();

        outcode.up = (x.up && y.up);
        outcode.down = (x.down && y.down);
        outcode.left = (x.left && y.left);
        outcode.right = (x.right && y.right);

        return outcode;

    }

    public static bool operator == (Outcode x, Outcode y)
    {
        return (x.up == y.up) && (x.down == y.down) && (x.left == y.left) && (x.right == y.right);
    }

    public static bool operator != (Outcode x, Outcode y)
    {
        return !(x == y);
    }


    bool trivialAccept(Outcode x, Outcode y)
    {
        Outcode z = new Outcode();
        print("Trivially Accept");
        return ((x == y) && (y == z));
    }

    bool trivialReject(Outcode x, Outcode y)
    {
        Outcode z = new Outcode();
        print("Trivially Reject");
        return((x & y) == z);
    }
    

}
