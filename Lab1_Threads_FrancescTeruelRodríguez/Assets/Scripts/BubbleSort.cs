using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Unity.VisualScripting;

public class BubbleSort : MonoBehaviour
{
    float[] array;
    List<GameObject> mainObjects;
    public GameObject prefab;

    void Start()
    {
        mainObjects = new List<GameObject>();
        array = new float[30000];
        for (int i = 0; i < 30000; i++)
        {
            array[i] = (float)Random.Range(0, 1000)/100;
        }

        // TO DO 4
        // Call the three previous functions in order to set up the exercise
        logArray();
        spawnObjs();

        // TO DO 5
        // Create a new thread using the function "bubbleSort" and start it.
        Thread myThread = new Thread(bubbleSort);
        myThread.Start();

    }

    void Update()
    {
        //TO DO 6
        //Call ChangeHeights() in order to update our object list.
        //Since we'll be calling UnityEngine functions to retrieve and change some data,
        //we can't call this function inside a Thread
        updateHeights();
    }

    //TO DO 5
    //Create a new thread using the function "bubbleSort" and start it.
    void bubbleSort()
    {
        int i, j;
        int n = array.Length;
        bool swapped;
        for (i = 0; i < n- 1; i++)
        {
            swapped = false;
            for (j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j+1]) = (array[j+1], array[j]);
                    swapped = true;
                }
            }
            if (swapped == false)
                break;
        }

        // You may debug log your Array here in case you want to. It will only be called one the bubble algorithm has finished sorting the array
        logArray();
    }

    void logArray()
    {
        string text = "";

        // TO DO 1
        // Simply show in the console what's inside our array.

        foreach (float item in array)
        {
            text += item.ToString() + ", "; // Convert each item to a string and add a space
        }

        Debug.Log(text);
    }
    
    void spawnObjs()
    {
        //TO DO 2
        //We should be storing our objects in a list so we can access them later on.
        
        for (int i = 0; i < array.Length; i++)
        {
            //We have to separate the objs accordingly to their width, in which case we divide their position by 1000.
            //If you decide to make your objs wider, don't forget to up this value

            mainObjects.Add(Instantiate(prefab, new Vector3((float)i / 1000, 
                this.gameObject.GetComponent<Transform>().position.y, 0), Quaternion.identity));
        }

    }

    //TO DO 3
    //We'll just change the height of every obj in our list to match the values of the array.
    //To avoid calling this function once everything is sorted, keep track of new changes to the list.
    //If there weren't, you might as well stop calling this function

    bool updateHeights()
    {
        bool changed = false;
        for (int i = 0; i < array.Length; i++)
        {
            mainObjects[i].transform.localScale = new Vector3(mainObjects[i].transform.localScale.x, array[i], mainObjects[i].transform.localScale.z);
            changed = true;
        }

        return changed;
    }
}
