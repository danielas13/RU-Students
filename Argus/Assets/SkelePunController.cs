using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkelePunController : MonoBehaviour {

    Transform Canvas;

    private List<string> PunList = new List<string>();
    private int index = 0;
    // Use this for initialization

    private static readonly System.Random random = new System.Random();     //Create a read only random variable.

    void Awake()
    {
        Canvas = transform.FindChild("Canvas").transform;
    }

    void Start()
    {
        PunList.Add("How do you make a skeleton laugh? You tickle his funny bone!");
        PunList.Add("You know, when you're cold you can always stand in a corner. It's 90 degrees.");
        PunList.Add("I would help you out, but i don't have the guts.");
        PunList.Add("Why are skeletons always so calm? Nothing gets under their skin.");
        PunList.Add("RIP boiling water, you will be mist.");
        PunList.Add("What do you call a stupid skeleton? A bonehead!");
        PunList.Add("Why didn't the skeleton dance at the party? He had nobody to dance with!");
        PunList.Add("How do skeletons call their friends? On the telebone!");
        PunList.Add("Why did the skeleton go to the party? To pick up somebody!");
        PunList.Add("I wondered why that fireball was geting bigger. Then it hit me.");
        PunList.Add("Did you hear about the guy whose whole left side was cut off? He's all right now!");
        PunList.Add("I'd tell you a chemistry joke but I know I wouldn't get a reaction.");
        PunList.Add("I once heard a joke about amnesia, but I forgot how it goes");
        PunList.Add("I'm glad i know sign language. It's pretty handy.");
        PunList.Add("I used to be a banker but I lost interest");
        PunList.Add("It's hard to explain puns to kleptomaniacs cause they always take things literally.");
        PunList.Add("I'm not fat. I'm just big boned.");
        PunList.Add("I could have fought back harder. But my heart just wasn´t in it.");
        PunList.Add("I owe a lot to the sidewalks. They've been keeping me off the streets for years.");
        PunList.Add("A man sued an airline company after it lost his luggage. Sadly, he lost his case.");
        PunList.Add("When i was young, i used to think facial hair looked stupid, but then it grew on me.");
        PunList.Add("What would you call a fish with a missing eye? A fsh probably.");
        PunList.Add("Care to seduce a large woman? Piece of cake.");
        PunList.Add("Can February march? No, but April May.");
        PunList.Add("Oxygen is proven to be a toxic gas. Anyone who inhales it normaly dies within 80 years.");
        PunList.Add("I can't believe I got fired from the calendar factory. All I did was take a day off.");
        PunList.Add("What did the sea say to the sand? Nothing, it simply waved.");
        PunList.Add("Whenever i undress in the bathroom, my shower gets turned on.");
        PunList.Add("Never trust atoms, they make up everything.");
        PunList.Add("5/4 of people admit that they're bad with fractions.");
        PunList.Add("To the guy who invented zero. Thanks for nothing.");
        PunList.Add("I'm terrified of elevators. I'll have to take steps to avoid them.");
        PunList.Add("This crypt is geting too crowded. Adventurers are just dying to get in here.");
        PunList.Add("When life gives you melons, you're probably dyslexic.");
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            index = random.Next(0, PunList.Count);
            Canvas.GetComponent<Text>().text = PunList[index];
            Canvas.Translate(new Vector3(0, 0, -5));
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Canvas.Translate(new Vector3(0, 0, 5));
        }
    }
}
