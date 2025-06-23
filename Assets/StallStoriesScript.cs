using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;


public class StallStoriesScript : MonoBehaviour
{
    public GameObject stallStory;
    public Image ss;
    public GameObject background;
    public Image bg;
    public Sprite hauntedbg;
    public Sprite studiousbg;
    public Sprite springbg;
    public Sprite lovebg;
    public Sprite dilemmabg;
    public GameObject ApprovalPanel;
    public GameObject GameOverScreen;
    public GameObject NextLevelButton;
    public TMP_Text approvalMessage;
    private static int jokeCount = 0;
    private static int eventCount = 0;
    public TMP_Text m_TextComponent;
    public TMP_Text joke1;
    public TMP_Text joke2;
    public TMP_Text joke3;
    public TMP_Text joke4;
    public TMP_Text event1;
    public TMP_Text event2;
    public TMP_Text event3;
    public TMP_Text event4;
    public TMP_Text fact;
    public TMP_Text ChosenJokes;
    public TMP_Text ChosenEvents;
    public TMP_Text ChosenFacts;
    public TMP_Text score;
    public TMP_Text highScore;
    private Theme spring;
    private Theme dilemma;
    private Theme studious;
    private Theme haunted;
    private Theme love;
    private string currentThemeName;
    private Theme currentTheme;
    private Theme randTheme = new Theme();
    public GameObject jokePanel;
    public GameObject eventPanel;
    public GameObject factPanel;
    private bool badJoke = false;
    private bool badEvent = false;
    private bool tempBadFact = false;
    private bool badFact = false;
    private List<string> tFacts = new List<string>();
    private List<string> fFacts = new List<string>();
    private static int ffCount = 0;
    private static int tfCount = 0;
    private static int fCount = 0;
    private bool jokesDone = false;
    private bool factsDone = false;
    private bool eventsDone = false;

    // Start is called before the first frame update
    void Start()
    {
        ss = stallStory.GetComponent<Image>();
        bg = background.GetComponent<Image>();
        highScore.text = "High Score: " + StateNameController.highScore;
        score.text = "Score: " + StateNameController.ghostCount;
        ffCount = 0;
        tfCount = 0;
        fCount = 0;
        jokeCount = 0;
        eventCount = 0;
        ChosenJokes.text = "";
        createSpring();
        createDilemma();
        createStudious();
        createHaunted();
        createLove();
        currentThemeName = generateTheme();
        changeBg(currentThemeName);
        m_TextComponent.text = "Theme: " + currentThemeName;
        AssignCurrentTheme();
        initializeTFacts();
        initializeFFacts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeBg(string name)
    {
        switch (name)
        {
            case "Spring":
                bg.sprite = springbg;
                ss.color = new Color(0.40f,0.66f,0.47f,1);
                break;
            case "Dilemma":
                bg.sprite = dilemmabg;
                ss.color = new Color(0.61f,0.44f,0.34f,1);
                break;
            case "Studious":
                bg.sprite = studiousbg;
                ss.color = new Color(0.93f,0.57f,0.74f,1);
                break;
            case "Haunted":
                bg.sprite = hauntedbg;
                ss.color = new Color(0.66f,0.32f,0.25f,1);
                break;
            case "Love":
                bg.sprite = lovebg;
                ss.color = new Color(1,0.38f,0.51f,1);
                break;
        }
    }

    public void initializeTFacts()
    {
        for (int i = 0; i < currentTheme.trueFacts.Count; i++)
        {
            tFacts.Add(currentTheme.trueFacts[i]);
        }
    }

    public void initializeFFacts()
    {
        for (int i = 0; i < currentTheme.falseFacts.Count; i++)
        {
            fFacts.Add(currentTheme.falseFacts[i]);
        }
    }

    public void checkApproval()
    {
        if (!jokesDone || !factsDone || !eventsDone)
        {
            ApprovalPanel.SetActive(false);
            return;
        }
        if (badJoke)
        {
            approvalMessage.text = "Your joke did not match the theme.";
            GameOverScreen.SetActive(true);
        }
        else if (badEvent)
        {
            approvalMessage.text = "Your event did not match the theme.";
            GameOverScreen.SetActive(true);
        }
        else if (badFact)
        {
            approvalMessage.text = "You chose a false fact.";
            GameOverScreen.SetActive(true);
        }
        else
        {
            approvalMessage.text = "Approved!";
            NextLevelButton.SetActive(true);
        }
}

    public string generateTheme()
    {
        int Rand = Random.Range(1, 6);
        switch (Rand)
        {
            case 1:
                return ("Spring");
            case 2:
                return ("Dilemma");
            case 3:
                return ("Haunted");
            case 4:
                return ("Studious");
            case 5:
                return ("Love");
        }
        return ("something aint right");
    }

    public void generateEvents()
    {
        if (eventsDone)
        {
            eventPanel.SetActive(false);
            return;
        }
        int Rand = Random.Range(1, 5);
        Debug.Log(Rand);
        switch (Rand)
        {
           case (1):
                event1.text = currentTheme.randEvent();
                event2.text = randomNonCTheme().randEvent();
                event3.text = currentTheme.randEvent();
                event4.text = randomNonCTheme().randEvent();
                break;
           case (2):
                event2.text = currentTheme.randEvent();
                event1.text = randomNonCTheme().randEvent();
                event4.text = currentTheme.randEvent();
                event3.text = randomNonCTheme().randEvent();
                break;
           case (3):
                event4.text = currentTheme.randEvent();
                event3.text = randomNonCTheme().randEvent();
                event2.text = currentTheme.randEvent();
                event1.text = randomNonCTheme().randEvent();
                break;
           case (4):
                event3.text = currentTheme.randEvent();
                event4.text = randomNonCTheme().randEvent();
                event1.text = currentTheme.randEvent();
                event2.text = randomNonCTheme().randEvent();
                break;
        }
    }

    public void generateJokes()
    {
        if (jokesDone)
        {
            jokePanel.SetActive(false);
            return;
        }
        int Rand = Random.Range(1, 5);
        Debug.Log(Rand);
        switch (Rand)
        {
            case (1):
                joke1.text = currentTheme.randJoke();
                joke2.text = randomNonCTheme().randJoke();
                joke3.text = currentTheme.randJoke();
                joke4.text = randomNonCTheme().randJoke();
                break;
            case (2):
                joke2.text = currentTheme.randJoke();
                joke1.text = randomNonCTheme().randJoke();
                joke4.text = currentTheme.randJoke();
                joke3.text = randomNonCTheme().randJoke();
                break;
            case (3):
                joke4.text = currentTheme.randJoke();
                joke3.text = randomNonCTheme().randJoke();
                joke2.text = currentTheme.randJoke();
                joke1.text = randomNonCTheme().randJoke();
                break;
            case (4):
                joke3.text = currentTheme.randJoke();
                joke4.text = randomNonCTheme().randJoke();
                joke1.text = currentTheme.randJoke();
                joke2.text = randomNonCTheme().randJoke();
                break;
        }
    }

    public void generateFact()
    {
        if (factsDone)
        {
            factPanel.SetActive(false);
            return;
        }
        int Rand = Random.Range(1, 3);
        if (Rand == 1)
        {
            if (ffCount >= currentTheme.falseFacts.Count)
            {
                initializeFFacts();
            }
            fact.text = fFacts[0];
            fFacts.Remove(fFacts[0]);
            tempBadFact = true;
            ffCount++;
        }
        else if (Rand == 2)
        {
            if (tfCount >= currentTheme.trueFacts.Count)
            {
                initializeTFacts();
            }
            fact.text = tFacts[0];
            tFacts.Remove(tFacts[0]);
            tempBadFact = false;
            tfCount++;
        }
    }

    public void chooseFact(TMP_Text fact)
    {
        if (tempBadFact == true)
        {
            badFact = true;
            Debug.Log("badFact = " + badFact);
        }
        ChosenFacts.text += fact.text + "\n";
        fCount++;
        if (fCount >= 2)
        {
            factPanel.SetActive(false);
            factsDone = true;
        }
        else
        {
            generateFact();
        }
    }

    public void chooseJoke(TMP_Text joke)
    {
        bool found = false;
        ChosenJokes.text += joke.text + "\n";
        for (int i = 0; i < currentTheme.jokes.Count; i++)
        {
            if (joke.text.Equals(currentTheme.jokes[i]))
            {
                found = true;
            }
        }
        if(!found)
        {
            badJoke = true;
        }
        jokeCount++;
        if (jokeCount >= 2)
        {
            jokePanel.SetActive(false);
            jokesDone = true;
        }
        Debug.Log("badJoke " + badJoke);
    }

    public void chooseEvent(TMP_Text events)
    {
        bool found = false;
        ChosenEvents.text += events.text + "\n";
        eventCount++;
        for (int i = 0; i < currentTheme.events.Count; i++)
        {
            if (events.text.Equals(currentTheme.events[i]))
            {
                found = true;
            }
        }
        if (!found)
        {
            badEvent = true;
        }
        if (eventCount >= 2)
        {
            eventPanel.SetActive(false);
            eventsDone = true;
        }
        Debug.Log("badEvent " + badEvent);
    }

    public Theme randomNonCTheme()
    {
        int i = 0;
        while (randTheme.themeName.Equals(currentThemeName) || i == 0)
        {
        int Rand = Random.Range(1, 6);
        if (Rand == 1)
            {
                randTheme = spring;
            }
            else if (Rand == 2)
            {
                randTheme = dilemma;
            }
            else if (Rand == 3)
            {
                randTheme = haunted;
            }
            else if (Rand == 4)
            {
                randTheme = studious;
            }
            else if (Rand == 5)
            {
                randTheme = love;
            }
            i++;
        }
        return randTheme;
    }

    public void AssignCurrentTheme()
    {
        switch (this.currentThemeName)
        {
            case ("Spring"):
                currentTheme = this.spring;
                break;
            case ("Dilemma"):
                currentTheme = this.dilemma;
                break;
            case ("Studious"):
                currentTheme = this.studious;
                break;
            case ("Haunted"):
                currentTheme = this.haunted;
                break;
            case ("Love"):
                currentTheme = this.love;
                break;
        }
    }
    

    public void createSpring()
    {
        List<string> jokes = new List<string>();
        jokes.Add("Why do bees have sticky hair? Because they use honeycombs.");
        jokes.Add("What do you call a girl with a frog on her head? Lily.");
        jokes.Add("What do you get when you plant kisses? Tu-lips.");
        jokes.Add("Why was the Easter egg hiding? Because it was a little chicken.");
        jokes.Add("What do you call it when it rains chickens and ducks? Fowl weather.");
        jokes.Add("Why do ghosts hate when it rains? It dampens their spirits.");
        List<string> events = new List<string>();
        events.Add("AP Exams");
        events.Add("Juju's Birthday");
        events.Add("National Get Stung by a Bee Day");
        events.Add("Field Day");
        events.Add("Bring an Umbrella to School Day");
        List<string> trueFacts = new List<string>();
        trueFacts.Add("Ants weigh more than humans.");
        trueFacts.Add("In Latin, the word vernal means “spring” and equinox means “equal night.”");
        trueFacts.Add("Early spring is when couples are most likely to break up.");
        List<string> falseFacts = new List<string>();
        falseFacts.Add("Studies show that babies born in the spring are more likely to be early risers and pessimisticc.");
        falseFacts.Add("According to a Facebook study, stepping on a worm in April can lead to a 5% follower increase rate per month.");
        falseFacts.Add("June is the wettest month of the year on average in New York.");
        spring = new Theme("Spring", jokes, trueFacts, falseFacts, events);
    }

    public void createDilemma()
    {
        List<string> jokes = new List<string>();
        jokes.Add("What did the pencil sketch artist ask himself? 2B or not 2B?");
        jokes.Add("Why can’t dinosaurs clap? Because they’re all dead.");
        jokes.Add("Doctor: \"Do you want to hear the good news or the bad news first ? \"Patient: “Good new please!” " +
                                                                    "Doctor: “Well, we’re naming a disease after you...”");
        jokes.Add("As I watched my dog chasing his tail, I thought how easily dogs are amused... Then, I thought how " +
            "easily amused I am watching my dog chase his tail.");
        jokes.Add("My friend bought a bus pass to a nude beach. It turned out to be a ticket to no wear.");
        jokes.Add("I told my boss that three companies were after me and I need a raise. My boss asked, " +
            "\"What companies?\" I replied, \"Gas, water, and electricity.\"");
        jokes.Add("I was so broke in college that I sometimes had to choose between laundry detergent " +
            "and breakfast. It was All or muffin.");
        List<string> events = new List<string>();
        events.Add("D-Day");
        events.Add("The End of World War II");
        events.Add("National Stub Your Toe Day");
        events.Add("National Forget Your Lunch at Home Day");
        events.Add("Ivy Day");
        List<string> trueFacts = new List<string>();
        trueFacts.Add("A glass bottle can take up to 1 million years to decompose.");
        trueFacts.Add("Some countries remained ‘neutral’ in World War 2.");
        trueFacts.Add("Approximately 50 percent of patients affected by rare diseases are children and 30 percent " +
            "of these children will not live to see their fifth birthday.");
        trueFacts.Add("You cannot eat fish caught from Cayuga Lake.");
        List<string> falseFacts = new List<string>();
        falseFacts.Add("Paper from trees can be recycled 11 times.");
        falseFacts.Add("HIV stands for Human Implanted Virus.");
        falseFacts.Add("Plants vs. Zombies was released for free-to-play on iOS devices on May 5, 2009.");
        falseFacts.Add("Chickpeas come from chickens.");
        dilemma = new Theme("Dilemma", jokes, trueFacts, falseFacts, events);
    }

    public void createStudious()
    {
        List<string> jokes = new List<string>();
        jokes.Add("What did the buffalo say to his son when he left for college? Bi-son.");
        jokes.Add("If pilgrims traveled on the Mayflower, what do college students travel on? Scholar - ships.");
        jokes.Add("My dad told me that colleges are cracking down on ghost-written essays?I asked, “What about mummy - written essays ?”");
        jokes.Add("Why do math books always look so sad? They are full of problems.");
        jokes.Add("Why did the student bring scissors to school? Because he wanted to cut class.");
        jokes.Add("Why is beer never served at a math party? Because you should never drink and derive.");
        List<string> events = new List<string>();
        events.Add("Midterms");
        events.Add("Bring Your Child to Work Day");
        events.Add("Teacher Appreciation Day");
        events.Add("Math Appreciation Week");
        events.Add("Pi Day");
        List<string> trueFacts = new List<string>();
        trueFacts.Add("The citric acid cycle is a series of biochemical reactions to release the energy stored in nutrients through the oxidation of acetyl-CoA derived from carbohydrates, fats, and proteins.");
        trueFacts.Add("The pencil has a lifespan of approximately 45,000 words.");
        trueFacts.Add("School buses are yellow for safety reasons.");
        List<string> falseFacts = new List<string>();
        falseFacts.Add("The first computer programmer was a man.");
        falseFacts.Add("The longest school bus ride on record is 907 miles.");
        falseFacts.Add("A parabola is a curve in which every point on the curve is equidistant from a point called a focus and a straight line called a directional.");
        falseFacts.Add("An entomologist is someone who studies scholarlyness.");
        studious = new Theme("Studious", jokes, trueFacts, falseFacts, events);
    }

    public void createHaunted()
    {
        List<string> jokes = new List<string>();
        jokes.Add("The skeleton couldn't help being afraid of the storm—he just didn't have any guts.");
        jokes.Add("What did the fisherman say on Halloween? Trick or trout.");
        jokes.Add("Why are graveyards so noisy? Because of all the coffin.");
        jokes.Add("Why is a cemetery a great place to write a story? Because there are so many plots there!");
        jokes.Add("Who won the skeleton beauty contest? No body.");
        jokes.Add("What was the egg trapped alone in a dark room? Terror-fried.");
        jokes.Add("Don't break anybody's heart; they only have 1. Break their bones; they have 206.");
        List<string> events = new List<string>();
        events.Add("Halloween");
        events.Add("Lil Jhonny's Spooky Maze Party");
        events.Add("BooVille Middle School's Halloween Dance");
        events.Add("National Pumpkin Spice Day");
        events.Add("Dia de los Muertos");
        List<string> trueFacts = new List<string>();
        trueFacts.Add("Dead bodies can get goosebumps.");
        trueFacts.Add("Female spiders often eat male spiders.");
        trueFacts.Add("Bees enjoy addictive substances like coffee and nicotine.");
        List<string> falseFacts = new List<string>();
        falseFacts.Add("The largest pumpkin recorded in US history weighed 2,258 pounds and was grown by Bernie Sanders in New Hampshire.");
        falseFacts.Add("A 1970 study found that kids are less likely to steal when they are wearing costumes.");
        falseFacts.Add("Bendy rulers are statistically more effective than non-bendy rulers.");
        haunted = new Theme("Haunted", jokes, trueFacts, falseFacts, events);
    }

    public void createLove()
    {
        List<string> jokes = new List<string>();
        jokes.Add("When I was in college, my roommate used to clean my room and I used to clean hers. We were maid for each other.");
        jokes.Add("Why should you never break up with a goalie? Because he is a keeper.");
        jokes.Add("What happened when the two vampires went on a blind date? It was love at first bite.");
        jokes.Add("What did one boat say to the other? Are you up for a little row-mance?");
        jokes.Add("Why should you not marry a tennis player? Because love means nothing to them.");
        jokes.Add("What does the ghost call his true love? My ghoul-friend.");
        jokes.Add("What do you call two birds in love? Tweet-hearts!");
        List<string> events = new List<string>();
        events.Add("Valentines Day");
        events.Add("Abraham Lincoln's Birthday");
        events.Add("Prom");
        events.Add("National Golf Lover's Day");
        events.Add("National Do Something Nice Day");
        List<string> trueFacts = new List<string>();
        trueFacts.Add("According to doctors, spending time with people who love you reduces the risk of an early death by 50%.");
        trueFacts.Add("The heart symbol was first used in 1250.");
        trueFacts.Add("A Rabbi invented speed dating in 1999.");
        trueFacts.Add("Dogs can smell your feelings.");
        List<string> falseFacts = new List<string>();
        falseFacts.Add("A study conducted by researchers at the University of Syracuse found that falling in love has similar effects on the body as drinking caffeine.");
        falseFacts.Add("People fall in love 6 times before getting married.");
        falseFacts.Add("Bee's prefer roses over sunflowers because they find them more romantic.");
        love = new Theme("Love", jokes, trueFacts, falseFacts, events);
    }

    public void restartGame()
    {
        ffCount = 0;
        tfCount = 0;
        fCount = 0;
        jokeCount = 0;
        eventCount = 0;
        if (StateNameController.hard)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        if (StateNameController.ghostCount > StateNameController.highScore)
        {
            StateNameController.highScore = StateNameController.ghostCount;
        }
        StateNameController.ghostCount = 0;
        StateNameController.level = 1;
    }

    public void goHome()
    {
        ffCount = 0;
        tfCount = 0;
        fCount = 0;
        jokeCount = 0;
        eventCount = 0;
        SceneManager.LoadScene(0);
        if (StateNameController.ghostCount > StateNameController.highScore)
        {
            StateNameController.highScore = StateNameController.ghostCount;
        }
        StateNameController.ghostCount = 0;
        StateNameController.level = 1;
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(3);
        StateNameController.level++;
    }
}
