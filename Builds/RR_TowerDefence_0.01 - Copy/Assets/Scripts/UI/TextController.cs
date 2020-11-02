using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextController : MonoBehaviour
{
    private static Transform _T;
    public GameObject Background;
    public GameObject TextDisplay;
    public GameObject TextInput; //only TextMeshPro objects supported

    private int LineCount = 0;
    public int MaxLines = 20;

    public String[] Lines;

    private int _pointer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Lines = new string[MaxLines];
        _T = this.gameObject.transform;
        if (!Background)
        {
            Transform _bkg = _T.Find("InputText");
            if (_bkg)
            {
                Background = _bkg.gameObject;
            }
            else
            {
                throw new Exception("No Text Background for " + this.gameObject.name + "!");
            }
        }

        if (!TextDisplay && !TextInput)
        {
            Transform _display = _T.Find("TextDisplay");
            Transform _input = _T.Find("TextInput");

            if(_display) 
            {
                TextDisplay = _display.gameObject;
            }
            else if (_input)
            {
                TextInput = _input.gameObject;
            }
            else
            {
                throw new Exception("No TextDisplay or TextInput Object for " + this.gameObject.name + " found!");
            }
        }


    }

    public void UpdateLine(int _key, string _i)
    {
        Lines[_key] = _i;

        if(_key > _pointer)
        {
            LineCount += (_key - _pointer);
            _pointer = _key;
        }
    }

    public int GetLinePosition(string _i)
    {
        int _p = 0;
        foreach(string s in Lines)
        {
            if(s == _i)
            {
                return _p;
            } else
            {
                _p += 1;
            }
        }
        throw new Exception("provided line does not exist. Try adding it?");
    }
    public int AddLine_GetPosition(string _i)
    {
        if (Lines.Length < 20)
        {
            //save position for return and add line to collection
            int _r = _pointer;
            Lines[_r] = _i;

            _pointer += 1;
            LineCount += 1;
            return _r;
        } else
        {
            throw new Exception("Max Lines Reached for " + this.gameObject.name + "!");
        }
    }
    
    public void ClearAll()
    {
        _pointer = 0;
        foreach(string i in Lines)
        {
            Lines[_pointer] = null;
            _pointer += 1;
        }
        _pointer = 0;
    }
    public void RemoveLine(int _k)
    {
        //adjust all lines after up 1 line
        while (_k < MaxLines) 
        {
            Lines[_k] = Lines[_k + 1];
            _k += 1;
        }
        _pointer -= 1;
        LineCount -= 1;
    }

}
