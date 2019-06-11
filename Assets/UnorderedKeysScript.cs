using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class UnorderedKeysScript : MonoBehaviour {

    public KMAudio Audio;
    public KMBombInfo bomb;

    public List<KMSelectable> keys;
    public Renderer[] keyID;
    public Material[] keyColours;

    private static int[][][][] table = new int[6][][][] {
        new int[6][][] { new int[6][] { new int[6] { 1, 3, 4, 6, 2, 5},
                                        new int[6] { 4, 5, 1, 2, 6, 3},
                                        new int[6] { 6, 2, 5, 3, 1, 4},
                                        new int[6] { 2, 6, 3, 4, 5, 1},
                                        new int[6] { 3, 1, 2, 5, 4, 6},
                                        new int[6] { 5, 4, 6, 1, 3, 2} },

                        new int[6][]  { new int[6] { 4, 2, 5, 1, 3, 6},
                                        new int[6] { 3, 6, 1, 5, 4, 2},
                                        new int[6] { 2, 1, 3, 6, 5, 4},
                                        new int[6] { 5, 4, 2, 3, 6, 1},
                                        new int[6] { 1, 5, 6, 4, 2, 3},
                                        new int[6] { 6, 3, 4, 2, 1, 5} },

                        new int[6][]  { new int[6] { 3, 4, 2, 1, 5, 6},
                                        new int[6] { 5, 1, 6, 2, 3, 4},
                                        new int[6] { 6, 3, 5, 4, 1, 2},
                                        new int[6] { 4, 6, 3, 5, 2, 1},
                                        new int[6] { 2, 5, 1, 6, 4, 3},
                                        new int[6] { 1, 2, 4, 3, 6, 5} },

                        new int[6][]  { new int[6] { 2, 4, 5, 3, 6, 1},
                                        new int[6] { 4, 3, 1, 6, 5, 2},
                                        new int[6] { 1, 5, 4, 2, 3, 6},
                                        new int[6] { 6, 2, 3, 4, 1, 5},
                                        new int[6] { 3, 1, 6, 5, 2, 4},
                                        new int[6] { 5, 6, 2, 1, 4, 3} },

                        new int[6][]  { new int[6] { 6, 4, 5, 1, 2, 3},
                                        new int[6] { 1, 3, 6, 2, 5, 4},
                                        new int[6] { 5, 2, 1, 3, 4, 6},
                                        new int[6] { 3, 6, 4, 5, 1, 2},
                                        new int[6] { 4, 5, 2, 6, 3, 1},
                                        new int[6] { 2, 1, 3, 4, 6, 5} },

                        new int[6][]  { new int[6] { 5, 2, 4, 1, 3, 6},
                                        new int[6] { 3, 1, 2, 5, 6, 4},
                                        new int[6] { 1, 4, 3, 6, 2, 5},
                                        new int[6] { 2, 5, 6, 3, 4, 1},
                                        new int[6] { 6, 3, 5, 4, 1, 2},
                                        new int[6] { 4, 6, 1, 2, 5, 3} } },

        new int[6][][] {new int[6][]  { new int[6] { 4, 5, 3, 2, 6, 1},
                                        new int[6] { 3, 2, 4, 1, 5, 6},
                                        new int[6] { 6, 1, 2, 4, 3, 5},
                                        new int[6] { 5, 3, 1, 6, 4, 2},
                                        new int[6] { 2, 4, 6, 5, 1, 3},
                                        new int[6] { 1, 6, 5, 3, 2, 4} },

                        new int[6][]  { new int[6] { 5, 1, 3, 6, 4, 2},
                                        new int[6] { 6, 5, 2, 1, 3, 4},
                                        new int[6] { 3, 4, 1, 2, 5, 6},
                                        new int[6] { 2, 3, 4, 5, 6, 1},
                                        new int[6] { 1, 6, 5, 4, 2, 3},
                                        new int[6] { 4, 2, 6, 3, 1, 5} },

                        new int[6][]  { new int[6] { 1, 2, 5, 6, 4, 3},
                                        new int[6] { 3, 4, 6, 1, 5, 2},
                                        new int[6] { 6, 1, 4, 2, 3, 5},
                                        new int[6] { 4, 6, 3, 5, 2, 1},
                                        new int[6] { 5, 3, 2, 4, 1, 6},
                                        new int[6] { 2, 5, 1, 3, 6, 4} },

                        new int[6][]  { new int[6] { 3, 1, 4, 5, 2, 6},
                                        new int[6] { 6, 2, 5, 1, 4, 3},
                                        new int[6] { 1, 3, 2, 6, 5, 4},
                                        new int[6] { 4, 5, 1, 3, 6, 2},
                                        new int[6] { 2, 6, 3, 4, 1, 5},
                                        new int[6] { 5, 4, 6, 2, 3, 1} },

                        new int[6][]  { new int[6] { 6, 5, 4, 3, 2, 1},
                                        new int[6] { 3, 2, 1, 5, 6, 4},
                                        new int[6] { 1, 6, 2, 4, 3, 5},
                                        new int[6] { 5, 3, 6, 1, 4, 2},
                                        new int[6] { 2, 4, 5, 6, 1, 3},
                                        new int[6] { 4, 1, 3, 2, 5, 6} },

                        new int[6][]  { new int[6] { 2, 3, 6, 5, 4, 1},
                                        new int[6] { 3, 4, 2, 1, 5, 6},
                                        new int[6] { 4, 2, 5, 6, 1, 3},
                                        new int[6] { 6, 5, 1, 2, 3, 4},
                                        new int[6] { 5, 1, 3, 4, 6, 2},
                                        new int[6] { 1, 6, 4, 3, 2, 5} } },

        new int[6][][] {new int[6][]  { new int[6] { 4, 3, 6, 2, 5, 1},
                                        new int[6] { 5, 1, 4, 6, 3, 2},
                                        new int[6] { 6, 2, 5, 3, 1, 4},
                                        new int[6] { 3, 5, 2, 1, 4, 6},
                                        new int[6] { 2, 4, 1, 5, 6, 3},
                                        new int[6] { 1, 6, 3, 4, 2, 5} },

                        new int[6][]  { new int[6] { 2, 6, 1, 5, 3, 4},
                                        new int[6] { 5, 3, 4, 1, 2, 6},
                                        new int[6] { 6, 4, 3, 2, 1, 5},
                                        new int[6] { 3, 1, 5, 6, 4, 2},
                                        new int[6] { 1, 2, 6, 4, 5, 3},
                                        new int[6] { 4, 5, 2, 3, 6, 1} },

                        new int[6][]  { new int[6] { 3, 6, 1, 2, 5, 4},
                                        new int[6] { 1, 4, 6, 3, 2, 5},
                                        new int[6] { 5, 1, 3, 4, 6, 2},
                                        new int[6] { 2, 5, 4, 6, 1, 3},
                                        new int[6] { 4, 2, 5, 1, 3, 6},
                                        new int[6] { 6, 3, 2, 5, 4, 1} },

                        new int[6][]  { new int[6] { 5, 2, 3, 4, 1, 6},
                                        new int[6] { 2, 4, 1, 3, 6, 5},
                                        new int[6] { 3, 5, 6, 2, 4, 1},
                                        new int[6] { 6, 1, 4, 5, 3, 2},
                                        new int[6] { 4, 6, 5, 1, 2, 3},
                                        new int[6] { 1, 3, 2, 6, 5, 4} },

                        new int[6][]  { new int[6] { 1, 5, 2, 6, 3, 4},
                                        new int[6] { 2, 3, 6, 5, 4, 1},
                                        new int[6] { 4, 1, 3, 2, 6, 5},
                                        new int[6] { 3, 6, 4, 1, 5, 2},
                                        new int[6] { 5, 4, 1, 3, 2, 6},
                                        new int[6] { 6, 2, 5, 4, 1, 3} },

                        new int[6][]  { new int[6] { 6, 5, 3, 4, 2, 1},
                                        new int[6] { 3, 6, 2, 1, 5, 4},
                                        new int[6] { 2, 4, 6, 5, 1, 3},
                                        new int[6] { 4, 1, 5, 3, 6, 2},
                                        new int[6] { 1, 2, 4, 6, 3, 5},
                                        new int[6] { 5, 3, 1, 2, 4, 6} } },

        new int[6][][]  {new int[6][] { new int[6] { 5, 1, 2, 4, 6, 3},
                                        new int[6] { 3, 2, 6, 1, 5, 4},
                                        new int[6] { 6, 3, 1, 5, 4, 2},
                                        new int[6] { 2, 4, 5, 3, 1, 6},
                                        new int[6] { 4, 5, 3, 6, 2, 1},
                                        new int[6] { 1, 6, 4, 2, 3, 5} },

                        new int[6][]  { new int[6] { 1, 3, 2, 4, 5, 6},
                                        new int[6] { 6, 2, 3, 5, 1, 4},
                                        new int[6] { 2, 4, 1, 3, 6, 5},
                                        new int[6] { 5, 6, 4, 2, 3, 1},
                                        new int[6] { 4, 1, 5, 6, 2, 3},
                                        new int[6] { 3, 5, 6, 1, 4, 2} },

                        new int[6][]  { new int[6] { 2, 3, 4, 5, 6, 1},
                                        new int[6] { 5, 1, 6, 3, 4, 2},
                                        new int[6] { 3, 4, 2, 6, 1, 5},
                                        new int[6] { 4, 2, 3, 1, 5, 6},
                                        new int[6] { 6, 5, 1, 4, 2, 3},
                                        new int[6] { 1, 6, 5, 2, 3, 4} },

                        new int[6][]  { new int[6] { 4, 3, 2, 1, 5, 6},
                                        new int[6] { 6, 4, 3, 5, 2, 1},
                                        new int[6] { 3, 1, 6, 2, 4, 5},
                                        new int[6] { 5, 6, 1, 4, 3, 2},
                                        new int[6] { 2, 5, 4, 6, 1, 3},
                                        new int[6] { 1, 2, 5, 3, 6, 4} },

                        new int[6][]  { new int[6] { 6, 3, 4, 1, 2, 5},
                                        new int[6] { 5, 4, 3, 2, 6, 1},
                                        new int[6] { 1, 2, 6, 4, 5, 3},
                                        new int[6] { 3, 6, 1, 5, 4, 2},
                                        new int[6] { 4, 5, 2, 3, 1, 6},
                                        new int[6] { 2, 1, 5, 6, 3, 4} },

                        new int[6][]  { new int[6] { 3, 4, 1, 2, 6, 5},
                                        new int[6] { 6, 2, 3, 1, 5, 4},
                                        new int[6] { 2, 5, 4, 3, 1, 6},
                                        new int[6] { 4, 1, 6, 5, 2, 3},
                                        new int[6] { 1, 6, 5, 4, 3, 2},
                                        new int[6] { 5, 3, 2, 6, 4, 1} } },

        new int[6][][] {new int[6][]  { new int[6] { 2, 4, 6, 5, 3, 1},
                                        new int[6] { 4, 3, 1, 2, 6, 5},
                                        new int[6] { 1, 5, 3, 6, 4, 2},
                                        new int[6] { 6, 1, 2, 4, 5, 3},
                                        new int[6] { 5, 2, 4, 3, 1, 6},
                                        new int[6] { 3, 6, 5, 1, 2, 4} },

                        new int[6][]  { new int[6] { 3, 5, 6, 1, 2, 4},
                                        new int[6] { 2, 3, 1, 6, 4, 5},
                                        new int[6] { 4, 1, 3, 2, 5, 6},
                                        new int[6] { 6, 2, 4, 5, 1, 3},
                                        new int[6] { 1, 4, 5, 3, 6, 2},
                                        new int[6] { 5, 6, 2, 4, 3, 1} },

                        new int[6][]  { new int[6] { 4, 3, 2, 6, 5, 1},
                                        new int[6] { 5, 1, 6, 3, 4, 2},
                                        new int[6] { 2, 6, 5, 1, 3, 4},
                                        new int[6] { 1, 4, 3, 2, 6, 5},
                                        new int[6] { 6, 2, 4, 5, 1, 3},
                                        new int[6] { 3, 5, 1, 4, 2, 6} },

                        new int[6][]  { new int[6] { 6, 4, 5, 3, 1, 2},
                                        new int[6] { 3, 2, 6, 1, 4, 5},
                                        new int[6] { 4, 5, 1, 2, 6, 3},
                                        new int[6] { 1, 3, 4, 5, 2, 6},
                                        new int[6] { 5, 6, 2, 4, 3, 1},
                                        new int[6] { 2, 1, 3, 6, 5, 4} },

                        new int[6][]  { new int[6] { 5, 4, 1, 6, 2, 3},
                                        new int[6] { 1, 3, 6, 5, 4, 2},
                                        new int[6] { 4, 1, 2, 3, 6, 5},
                                        new int[6] { 3, 2, 5, 4, 1, 6},
                                        new int[6] { 2, 6, 3, 1, 5, 4},
                                        new int[6] { 6, 5, 4, 2, 3, 1} },

                        new int[6][]  { new int[6] { 1, 5, 6, 2, 3, 4},
                                        new int[6] { 2, 4, 1, 6, 5, 3},
                                        new int[6] { 5, 6, 3, 1, 4, 2},
                                        new int[6] { 3, 2, 4, 5, 1, 6},
                                        new int[6] { 6, 3, 5, 4, 2, 1},
                                        new int[6] { 4, 1, 2, 3, 6, 5} } },

        new int[6][][] {new int[6][]  { new int[6] { 3, 5, 6, 2, 1, 4},
                                        new int[6] { 2, 4, 1, 3, 6, 5},
                                        new int[6] { 1, 2, 3, 4, 5, 6},
                                        new int[6] { 5, 6, 4, 1, 2, 3},
                                        new int[6] { 4, 1, 5, 6, 3, 2},
                                        new int[6] { 6, 3, 2, 5, 4, 1} },

                        new int[6][]  { new int[6] { 6, 1, 3, 5, 4, 2},
                                        new int[6] { 3, 5, 1, 2, 6, 4},
                                        new int[6] { 5, 2, 4, 6, 1, 3},
                                        new int[6] { 1, 4, 6, 3, 2, 5},
                                        new int[6] { 4, 3, 2, 1, 5, 6},
                                        new int[6] { 2, 6, 5, 4, 3, 1} },

                        new int[6][]  { new int[6] { 2, 3, 5, 1, 4, 6},
                                        new int[6] { 4, 1, 2, 6, 5, 3},
                                        new int[6] { 3, 6, 4, 2, 1, 5},
                                        new int[6] { 6, 5, 3, 4, 2, 1},
                                        new int[6] { 1, 4, 6, 5, 3, 2},
                                        new int[6] { 5, 2, 1, 3, 6, 4} },

                        new int[6][]  { new int[6] { 1, 4, 3, 5, 6, 2},
                                        new int[6] { 5, 2, 1, 4, 3, 6},
                                        new int[6] { 2, 6, 4, 3, 1, 5},
                                        new int[6] { 3, 1, 5, 6, 2, 4},
                                        new int[6] { 6, 5, 2, 1, 4, 3},
                                        new int[6] { 4, 3, 6, 2, 5, 1} },

                        new int[6][]  { new int[6] { 5, 3, 2, 4, 6, 1},
                                        new int[6] { 4, 2, 6, 1, 5, 3},
                                        new int[6] { 1, 4, 5, 6, 3, 2},
                                        new int[6] { 6, 1, 3, 2, 4, 5},
                                        new int[6] { 2, 5, 4, 3, 1, 6},
                                        new int[6] { 3, 6, 1, 5, 2, 4} },

                        new int[6][]  { new int[6] { 4, 2, 3, 6, 5, 1},
                                        new int[6] { 5, 1, 4, 3, 6, 2},
                                        new int[6] { 6, 3, 5, 1, 2, 4},
                                        new int[6] { 1, 6, 2, 5, 4, 3},
                                        new int[6] { 3, 4, 6, 2, 1, 5},
                                        new int[6] { 2, 5, 1, 4, 3, 6} } } };

    private static string[] colourList = new string[6] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
    private int[][] info = new int[6][] { new int[3], new int[3], new int[3], new int[3], new int[3], new int[3] };
    private int pressCountdown = 6;
    private int resetCount;
    private IEnumerator sequence;
    private bool starting = true;
    private bool pressable;
    private bool[] alreadypressed = new bool[7];
    private List<string> answer = new List<string> { };

    //Logging
    static int moduleCounter = 1;
    int moduleID;
    private bool moduleSolved;

    private void Awake()
    {
        moduleID = moduleCounter++;
        sequence = Shuff();
        foreach (KMSelectable key in keys)
        {
            if (keys.IndexOf(key) != 6)
            {
                key.transform.localPosition = new Vector3(0, 0, -1f);
            }
            key.OnInteract += delegate () { KeyPress(key); return false; };
        }
    }

    void Start()
    {
        Reset();
    }

    private void KeyPress(KMSelectable key)
    {
        if (keys.IndexOf(key) == 6 && moduleSolved == false && pressable == true)
        {
            key.AddInteractionPunch();
            for(int i = 0; i < 7; i++)
            {
                if(i != 6)
                {
                    if(answer[i] == pressCountdown.ToString())
                    {
                        GetComponent<KMBombModule>().HandleStrike();
                        Debug.LogFormat("[Unordered Keys #{0}] Invalid reset", moduleID);
                        break;
                    }
                }
                else
                {
                    if (resetCount < 5)
                    {
                        resetCount++;
                    }
                    else
                    {
                        moduleSolved = true;
                    }
                    answer.Clear();
                    Reset();
                }
            }
        }
        else if (alreadypressed[keys.IndexOf(key)] == false && moduleSolved == false && pressable == true)
        {
            key.AddInteractionPunch();
            if (answer[keys.IndexOf(key)] == pressCountdown.ToString())
            {
                key.transform.localPosition = new Vector3(0, 0, -1f);
                GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
                alreadypressed[keys.IndexOf(key)] = true;
                if (pressCountdown > 1)
                {
                    pressCountdown--;
                    List<string> c = new List<string> { };
                    for (int i = 0; i < 6; i++)
                    {
                        if (answer[i] == pressCountdown.ToString())
                        {
                            c.Add((i + 1).ToString());
                        }
                    }
                    string[] carray = c.ToArray();
                    if (carray.Count() != 0)
                    {
                        string C = String.Join(", ", carray);
                        Debug.LogFormat("[Unordered Keys #{0}] Valid key(s) after {1} reset(s) and {3} pressed key(s): {2}", moduleID, resetCount, C, 6 - pressCountdown);
                    }
                    else
                    {
                        Debug.LogFormat("[Unordered Keys #{0}] No valid key(s) after {1} reset(s) and {2} pressed key(s); reset required.", moduleID, resetCount, 6 - pressCountdown);
                    }
                }
                else
                {
                    moduleSolved = true;
                    StartCoroutine(sequence);
                }
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Unordered Keys #{0}] Invalid key pressed: {1}", moduleID, keys.IndexOf(key));
            }
        }      
    }

    private void Reset()
    {
        if (moduleSolved == false)
        {
            for (int i = 0; i < 6; i++)
            {
                if (alreadypressed[i] == false)
                {
                    info[i][0] = UnityEngine.Random.Range(0, 6);
                    info[i][1] = UnityEngine.Random.Range(0, 6);
                    info[i][2] = UnityEngine.Random.Range(0, 6);
                    answer.Add(table[info[i][0]][info[i][1]][i][info[i][2]].ToString());
                }
                else
                {
                    answer.Add("/");
                }
            }
            string[] a = new string[6];
            string[] b = new string[6];
            for (int i = 0; i < 6; i++)
            {
                a[i] = colourList[info[i][0]];
                b[i] = colourList[info[i][1]];
                if (i == 5)
                {
                    string A = String.Join(", ", a);
                    string B = String.Join(", ", b);
                    Debug.LogFormat("[Unordered Keys #{0}] After {1} reset(s), the keys had the colours: {2}", moduleID, resetCount, A);
                    Debug.LogFormat("[Unordered Keys #{0}] After {1} reset(s), the labels had the colours: {2}", moduleID, resetCount, B);
                }
            }
            string[] answ = answer.ToArray();
            string ans = String.Join("", answ);
            Debug.LogFormat("[Unordered Keys #{0}] After {1} reset(s), the keys have the values {2}", moduleID, resetCount, ans);
            List<string> c = new List<string> { };
            for (int i = 0; i < 6; i++)
            {
                if (answer[i] == pressCountdown.ToString())
                {
                    c.Add((i + 1).ToString());
                }
            }
            string[] carray = c.ToArray();
            if (carray.Count() != 0)
            {
                string C = String.Join(", ", carray);
                Debug.LogFormat("[Unordered Keys #{0}] Valid key(s) after {1} reset(s) and {3} pressed key(s): {2}", moduleID, resetCount, C, 6 - pressCountdown);
            }
            else
            {
                Debug.LogFormat("[Unordered Keys #{0}] No valid keys after {1} reset(s) and {2} pressed key(s); reset required.", moduleID, resetCount, 6 - pressCountdown);
            }
        }
        pressable = false;
        StartCoroutine(sequence);
    }

    private IEnumerator Shuff()
    {
        for (int i = 0; i < 30; i++)
        {
            if (moduleSolved == false && starting == false)
            {
                switch (i % 4)
                {
                    case 0:
                        keys[6].GetComponentInChildren<TextMesh>().text = "RESETTING";
                        break;
                    case 1:
                        keys[6].GetComponentInChildren<TextMesh>().text = "RESETTING.";
                        break;
                    case 2:
                        keys[6].GetComponentInChildren<TextMesh>().text = "RESETTING..";
                        break;
                    case 3:
                        keys[6].GetComponentInChildren<TextMesh>().text = "RESETTING...";
                        break;
                }
            }
            if (i % 5 == 4)
            {
                if (moduleSolved == true)
                {
                    if(alreadypressed[(i - 4) / 5] == false)
                    {
                        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
                    }
                    keyID[(i - 4) / 5].material = keyColours[6];
                    keys[(i - 4) / 5].transform.localPosition = new Vector3(0, 0, -1f);
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(0, 0, 0, 255);
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().text = "0";
                    if (i == 29)
                    {
                        GetComponent<KMBombModule>().HandlePass();
                        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                    }
                }
                else
                {
                    if (alreadypressed[(i - 4) / 5] == false)
                    {
                        keys[(i - 4) / 5].transform.localPosition = new Vector3(0, 0, 0);
                        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
                        keyID[(i - 4) / 5].material = keyColours[info[(i - 4) / 5][0]];
                        switch (info[(i - 4) / 5][1])
                        {
                            case 0:
                                keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(255, 25, 25, 255);
                                break;
                            case 1:
                                keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(25, 255, 25, 255);
                                break;
                            case 2:
                                keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(25, 25, 255, 255);
                                break;
                            case 3:
                                keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(25, 255, 255, 255);
                                break;
                            case 4:
                                keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(255, 75, 255, 255);
                                break;
                            case 5:
                                keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().color = new Color32(255, 255, 75, 255);
                                break;
                        }
                    }
                    keys[(i - 4) / 5].GetComponentInChildren<TextMesh>().text = (info[(i - 4) / 5][2] + 1).ToString();
                }
                if (i == 29)
                {
                    i = -1;
                    pressable = true;
                    starting = false;
                    keys[6].GetComponentInChildren<TextMesh>().text = String.Empty;
                    StopCoroutine(sequence);
                }
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    if (alreadypressed[j] == false && j > (i - 4) / 5)
                    {
                        int[] rand = new int[3];
                        for (int k = 0; k < 3; k++)
                        {
                            rand[k] = UnityEngine.Random.Range(0, 6);
                        }
                        keyID[j].material = keyColours[rand[0]];
                        switch (rand[1])
                        {
                            case 0:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(255, 0, 0, 255);
                                break;
                            case 1:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(0, 255, 0, 255);
                                break;
                            case 2:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(75, 75, 225, 255);
                                break;
                            case 3:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(0, 255, 255, 255);
                                break;
                            case 4:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(255, 0, 255, 255);
                                break;
                            case 5:
                                keys[j].GetComponentInChildren<TextMesh>().color = new Color32(255, 255, 0, 255);
                                break;
                        }
                        keys[j].GetComponentInChildren<TextMesh>().text = (rand[2] + 1).ToString();              
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
