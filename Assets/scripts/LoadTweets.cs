using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadTweets : MonoBehaviour {
    Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = LoadNewTweet();
	}
	
	// Update is called once per frame
	string LoadNewTweet () {
        int r = Random.Range(0 , 7);
        string tweet;
        switch (r) {
            case 0:
                tweet = " President of India @RashtrapatiBhvn. #PresidentMukherjee inaugurated the ‘World Conference on Environment’ organized by the National Green Tribunal in New Delhi today";
                break;
            case 1:
                tweet = "Delhi govt's environment dept to start working on developing a butterfly park on International Forest Day, March 21. @ImranHussaain";
                break;
            case 2:
                tweet = "HMO India‏ @HMOIndia. Today climate change has been recognised as a major global challenge. : HM at World Conference on Environment";
                break;
            case 3:
                tweet = "NS Environment‏ @ns_environment. Don’t forget #EarthHour at 8:30 tonight turn off your lights";
                break;
            case 4:
                tweet = "NS Environment‏ @ns_environment. On Sat. Mar 25 8:30 - 9:30 pm switch off your lights and show your support for #EarthHour  2017.";
                break;
            case 5:
                tweet = "UN Environment‏Verified account @UNEP. Unexpected mass coral bleaching event shows reefs can heat up faster making them more vulnerable to #climatechange > http://bit.ly/2myuVI3 ";
                break;
            default:
                tweet = " UN Environment‏Verified account @UNEP. #EarthHour in Kenya starts in 5 minutes - lights out to show your support for the fight against #climatechange! @wwf";
                break;

        }
        return tweet;
	}
}
