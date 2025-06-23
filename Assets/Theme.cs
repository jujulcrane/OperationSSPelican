using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme
{
    public string themeName;
    public List<string> jokes;
    public List<string> trueFacts;
    public List<string> falseFacts;
    public List<string> events;
    private int usedRand = 0;
    private int Rand = -1;
    private int trueUsedRand = 0;
    private int trueRand = -1;
    private int falseUsedRand = 0;
    private int falseRand = -1;
    private int eventUsedRand = 0;
    private int eventRand = -1;


    public Theme(string themeName, List<string> jokes, List<string> trueFacts, List<string> falseFacts, List<string> events)
    {
        this.themeName = themeName;
        this.jokes = jokes;
        this.falseFacts = falseFacts;
        this.trueFacts = trueFacts;
        this.events = events;
    }

    public Theme()
    {
        themeName = "temp";
    }

    public string randJoke()
    {
        Rand = Random.Range(0, jokes.Count - 1);
        while (usedRand == Rand)
        {
            Rand = Random.Range(0, jokes.Count - 1);
        }
        usedRand = Rand;
        return jokes[Rand];
    }

    public string randTrueFact()
    {
        trueRand = Random.Range(0, trueFacts.Count - 1);
        while (trueUsedRand == trueRand)
        {
           falseRand = Random.Range(0, trueFacts.Count - 1);
        }
        trueUsedRand = trueRand;
        return trueFacts[trueRand];
    }

    public string randFalseFact()
    {
        falseRand = Random.Range(0, falseFacts.Count - 1);
        while (falseUsedRand == falseRand)
        {
            falseRand = Random.Range(0, falseFacts.Count - 1);
        }
        falseUsedRand = falseRand;
        return falseFacts[falseRand];
    }

    public string randEvent()
    {
        eventRand = Random.Range(0, events.Count - 1);
        while (eventUsedRand == eventRand)
        {
            eventRand = Random.Range(0, events.Count - 1);
        }
        eventUsedRand = eventRand;
        return events[eventRand];
    }

    public string getThemeName()
    {
        return themeName;
    }

    public List<string> getJokes()
    {
        return jokes;
    }

    public List<string> getEvents()
    {
        return events;
    }
}
