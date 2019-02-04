using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class GameManager : MonoBehaviour {
	int[] guess = {0,0,0};
	int[] solution = {0,0,0};
	public static GameManager control;
	public float money;
    public float moneyAtStart;
	public int currentStreak;
	public int lastStreak;
	public int topStreak;
	public int beanScore;
	public float luck;
    private bool gambling;
    private int bet;
    private int prize;

	public float updateTime;
	public bool minimumUpdateTime;
	public float sixtySecondsCounter;
	public float currentTime;
	public float initialTime;
	public int currentSPM;

	public float moneySnapshot;
	public float tenSecondCounter;

	public GameObject black;
	public GameObject pinto;

	public Text luckText;
	public Text beanScoreText;
	public Text moneyText;
	public Text moneyGainText;
	public Text currentStreakText;
	public Text topStreakText;
	public Text lastStreakText;
	public Text oddsText;
	public Text spmText;
	public Text WinLose;
    //public Text animationStreakText;
    //public Animator an;

	public AudioSource beanSound;
	public AudioSource handSound;
	public AudioSource moneySoundWin;
	public AudioSource moneySoundLose;


	//public AudioSource handSound;


	// Use this for initialization
	void Start () {
		hideBeans ();
		rollBeans ();
		Load ();
		luck = 50;
		beanScore = 2;
        gambling = false;
        //animationStreakText.enabled = false;
        moneyAtStart = money;
        //an = animationStreakText.GetComponent<Animator>();
        print("Money at start = " + moneyAtStart);
    }
    // Update is called once per frame
    void Update () {
		updateAllStats ();
	}
	void FixedUpdate(){
        Save();
    }


    // On opening the game, saves data between scenes
    //	void Awake () {
    //		if (control == null) {
    //			DontDestroyOnLoad (gameObject);
    //			control = this;
    //		} else if (control != this) {
    //			Destroy (gameObject);
    //		}
    //	}

    //	void OnGUI(){
    //		//GUI.Label (new Rect(), "Money: " + money);
    //		if (GUI.Button (new Rect(50, 660, 300, 100), "Save")) {
    //			GameManager.control.Save ();
    //		}
    //
    //		if (GUI.Button  (new Rect(50, 800, 300, 100), "Load")) {
    //			GameManager.control.Load ();
    //		}
    //	}

    
    public void setGuessHands(int leftOrRight){
		handSound.Play();
		guess [1] = leftOrRight;
	}

    
	public void setGuessBeans(int blackOrPinto){

		guess [2] = blackOrPinto;
		//print ("guess hands:" + guess[1]);
		//print ("guess beans:" + guess[2]);

		/*
		ORIGINAL CODE FOR SETTING/CHECKING GUESS
		checkGuess ();
		rollBeans ();
		*/

        if (gambling)
        {
            bool win = luckGuess(getLuck());  //eventually make this a constant 50?
            if (win)
            {
                correct();
            }
            else
            {
                incorrect();
            }

        }
        else
        {
            bool win = luckGuess(getLuck());
            if (win)
            {
                correct();
            }
            else
            {
                incorrect();
            }

        }
    }

    public bool checkGuess(){
		if((guess[1] == solution[1] && guess[2] == solution[2]) || (guess[1] != solution[1] && guess[2] != solution[2])){
			//print ("true");
			correct();
			return true;
		}
		//print("false");
		incorrect();
		return false;
	}

	private void rollBeans(){
		int leftOrRight = UnityEngine.Random.Range(1,3);
		int blackOrPinto = UnityEngine.Random.Range(1,3);

		solution [1] = leftOrRight;
		solution [2] = blackOrPinto;

		//print ("solution hand: " + leftOrRight);
		//print ("solution bean: " + blackOrPinto);

	}

	private bool luckGuess(float luck){
		float solution = UnityEngine.Random.Range(0,100);
		print (solution);
		if (solution < luck) {
			return true;
		}
		return false;
	}

	public float getMoney(){
		return money;
	}

	public void setMoney(float setMoney){
		money = setMoney;
	}

	public void addMoney(float addMoney){
		money += addMoney;
	}

	public void subtractMoney(float subMoney){
		money -= subMoney;
	}

	public int getStreak(){
		return currentStreak;
	}

    public void setStreak(int streak)
    {
        currentStreak = streak;
    }

    public int getLastStreak(){
		return lastStreak;
	}

	public void setLastStreak(int last){
		lastStreak = last;
	}

	public int getTopStreak(){
		return topStreak;
	}

	public void setTopStreak(int top){
		topStreak = top;
	}

	public int getBeanScore(){
		return beanScore;
	}

	public void setBeanScore(int beanScore){
		this.beanScore = beanScore;
	}

	public void beanScorePlusPlus(){
		beanScore++; //my naming is godlike
	}

	public void beanScoreMinusMinus(){
		beanScore--;
	}
		
	public float getLuck(){
		return luck;
	}

	public void setLuck(float luck){
		this.luck = luck;
	}

	public void luckPlusPlus(){
		luck++; //my naming is godlike
	}

	public void luckMinusMinus(){
		luck--;
	}

	public float getOdds(){
		float odds;
		if (currentStreak <= 0) {
			odds = 0;//-(Mathf.Pow(0.5f,(float)currentStreak));
		} else {
			odds = (Mathf.Pow(0.5f,(float)currentStreak));
			//print (odds);
		}
		return odds;
	}
    /*
	public int getSPM(){
		
		float newScorePerMinute = 123;
		//print (moneyValue.getMoney());

		if (minimumUpdateTime) {
			updateTime = Time.time;
		}

		if (sixtySecondsCounter < updateTime) {
			sixtySecondsCounter += Time.time;
			//print ("counting up" + sixtySecondsCounter);
			//float testSPM = (moneyValue.getMoney() - )/();
			//print ();
		} else {
			currentTime = Time.time;
			//money = moneyValue.getMoney ();

			newScorePerMinute = (money) / (currentTime);  //  $/m
			newScorePerMinute = newScorePerMinute * 60;  //  scorePerminute = scorePerMinute * 60s

			//print (currentTime);
			//print (newScorePerMinute + " = new spm");
			//print (currentMoney - initialMoney + " = delta money");
			//print (currentTime - initialTime + " = delta time");

			initialTime = Time.time;
			//money = moneyValue.getMoney ();  //its getting this every .5 seconds, i need a better way to tell the difference...
		}
		
		if (newScorePerMinute > scorePerMinute) {
			scorePerMinute = newScorePerMinute;
		}

		if (counter < updateTime) {
			counter += Time.deltaTime;
		} else {

			initialMoney = moneyValue.getMoney ();
			initialTime = Time.deltaTime;
			counter = 0;
		}
        
		//float newScorePerMinte = 123;
		currentTime = Time.time%60;
		newScorePerMinute = (money) / (currentTime);  //  $/m
		newScorePerMinute = newScorePerMinute * 60;  //  scorePerminute = scorePerMinute * 60s

		float newSpm = 0;
		float moneyChange = getMoney () - moneySnapshot;
		if(threeSecondCounter >= 3){
			newSpm = ((moneyChange) / (threeSecondCounter)) * 20;
			threeSecondCounter = 0;
			moneySnapshot = getMoney ();
		} else {
			threeSecondCounter += Time.deltaTime;
		}
		newSpm = getSPM ();

		return (int)newSpm;
	}
    */
    /*
	public int getSPM(){
		return currentSPM;
	}
	

    public int getSPM()
    {
        int spm;
        float timeSinceStart = Time.time;
        int moneySixtySecondsAgo = 0;
        if (minimumUpdateTime)
        {
            moneySixtySecondsAgo = money;
        }
        int moneyDifference = money - moneyAtStart;
        int moneyLastSixty = money - moneySixtySecondsAgo;
        spm = moneyDifference / timeSinceStart;
        return spm;
    }
    */

    public float getSPM()
    {
        //print(moneyAtStart);
        float moneyGain = money - moneyAtStart;
        float timeSinceStart = Time.time;
        float spm = (float)(Math.Round((moneyGain / timeSinceStart)*100f)/100f)*60;
        //print(moneyGain);
        return spm;
    }
	/*
    public void calculateSPM(){
		float newSpm = 0;
		float moneyChange;
		if(tenSecondCounter >= 10){
			moneyChange = Mathf.Ceil(getMoney () - moneySnapshot);
			tenSecondCounter = Time.deltaTime;
			moneySnapshot = getMoney ();
		} else {
			tenSecondCounter += Time.deltaTime;
			//newSpm = getSPM ();
			//moneyChange = Mathf.Ceil(getMoney () - moneySnapshot);
		} 
		//newSpm = ((moneyChange)*6)/60;
		currentSPM = (int)newSpm;
	}

	public void setSPM(float spm){
		this.spm
	}
*/
	public void showBeans(){
		black.SetActive(true);
		pinto.SetActive(true);
		//handSound.Play ();
	}

	public void hideBeans(){
		black.SetActive (false);
		pinto.SetActive (false);
		//handSound.Play ();
	}

	private void correct(){
		moneySoundWin.Play();  //#note: sound tech?
		if (currentStreak <= 0) {
			//negative streak stuff
			currentStreak = 1;
		} else {
			currentStreak++;
		}
		if (currentStreak >= topStreak) {
			topStreak = currentStreak;
		}
        //WIN TEXT
        //string winnerText = randomWinText();
        //WinLose.text = winnerText;				//set correct message

        
        //streakAnimation.Play();
        /*  WIP!!!
        animationStreakText.SetActive(true);
        streakAnimation.Play();
        animationStreakText.SetActive(false);
        */

        //ADD MONEY
        if (gambling)
        {
            int prize = calculateGamblingProfit(getStreak(), true);
            setPrize(prize);
            updateMoneyGainText(prize);
            print("GAMBLING: CORRECT - Prize: " + getPrize());
        }
        else
        {
            int profit = calculateProfit(getBeanScore(), getStreak(), true);
            addMoney(profit);
            updateMoneyGainText(profit);
            updateMoneyText();
        }

        WinLose.text = "Correct!";
        //animationStreakText.text = currentStreak.ToString();
        //animationStreakText.enabled = true;
        //an.enabled = true;
        //an.StopPlayback();
        //an.Play("ShakeStreak");
        //Invoke("hideStreakPopupText", 2f);

    }

    private void incorrect(){
		moneySoundLose.Play();
	
		lastStreak = currentStreak;
		if (currentStreak > 0) {
			currentStreak = 0;
		} else if (currentStreak <= 0) {

		}

        if (gambling)
        {
            rewardPrize();
            setStreak(0);
            gambling = false;
            print("GAMBLING: INCORRECT - Prize: " + getPrize());
        }

        setStreak(0);
        int profit = calculateProfit(getBeanScore(), getStreak(), false);
		addMoney (profit);
		updateMoneyGainText (profit);
		updateMoneyText();

        //string loserText = randomLoseText();
        //WinLose.text = loserText;
        WinLose.text = "Incorrect!";
        hideStreakPopupText();
    }

    private int calculateProfit(int beanScore, int streak, bool win){
		int profit = 0;
		if (!win) {
			profit = (int)Mathf.Pow (beanScore, 0);
		} else if (win) {
			profit = (int)Mathf.Pow (beanScore, streak);
		} else {
			profit = 0;
			print ("Error: calculateProfit. improve your exception handling skills lol");
		}
		return profit;
	}

    // Gambling game //

    public void startGamblingGame(int bet)
    {
        setStreak(0);
        hideBeans();
        WinLose.text = "";
        updateMoneyGainText(0);
        gambling = true;
        setBet(bet);
    }

    private int calculateGamblingProfit(int streak, bool win)
    {
        int prize = streak*(getBet() / 2);

        return prize;
    }

    private void rewardPrize()
    {
        //give them prize and maybe stuff
        addMoney(getPrize());
    }

    private void setBet(int bet)
    {
        this.bet = bet;
    }

    private int getBet()
    {
        return bet;
    }

    private void setPrize(int prize)
    {
        this.prize = prize;
    }

    private int getPrize()
    {
        return prize;
    }

    // End Gambling game //


    private void updateMoneyGainText(int moneyGain){
		moneyGainText.text = "+" + moneyGain.ToString();
	}

	private void updateMoneyText(){
		moneyText.text = money.ToString();
	}

	private void updateTopStreak(){
		topStreakText.text = topStreak.ToString();
	}

	private void updateCurrentStreak(){
		currentStreakText.text = currentStreak.ToString();
	}

	private void updateLastStreak(){
		lastStreakText.text = lastStreak.ToString();

	}

	private void updateOdds(){
		float multipliedOdds = (float)(getOdds ()*100);
		oddsText.text = multipliedOdds.ToString() + "%";
	}

	private void updateSPM(){
		spmText.text = getSPM() + "\n$/min";
	}

	private void updateLuckText(){
		luckText.text = "Luck: " + getLuck ().ToString();
	}

	private void upgradeBeanScoreText(){
		beanScoreText.text = "Beans: " + getBeanScore ().ToString();

	}

	private void updateAllStats(){
		//calculateSPM ();
		updateSPM ();
		updateOdds ();
		updateTopStreak ();
		updateCurrentStreak ();
		updateLastStreak ();
		updateMoneyText ();
		updateLuckText ();
		upgradeBeanScoreText();
		//save money
	}

	private string randomWinText() {
		string[] winTexts = { "Nice Job!", "Correct!", "You're on a streak!" };
		return winTexts [UnityEngine.Random.Range (0, winTexts.Length)];
	}

	private string randomLoseText() {
		string[] loseTexts = { "Not Today!", "Incorrect", "C'mon try again!", "Next one's a winner!" };
		return loseTexts [UnityEngine.Random.Range (0, loseTexts.Length)];
	}

    private void hideStreakPopupText() {
        //animationStreakText.enabled = false;
        //an.enabled = false;
    }

//	public void NewSave(){
//		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream file = File.Create (Application.persistentDataPath + "/playertestsaveinfo.dat");
//		PlayerData data = new PlayerData ();
//		data.coins = getMoney ();
//		data.spm = getSPM ();
//		data.lastStreak = getLastStreak ();
//		data.topStreak = getTopStreak ();
//		//setMoney(data.coins);
//
//		bf.Serialize (file, data);
//		file.Close ();//Takes serizalable class "file" and writes to our class "data"
//		print ("You Saved");
//	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playertestsaveinfo.dat");
		PlayerData data = new PlayerData ();
		data.coins = getMoney ();
		data.spm = getSPM ();
		data.lastStreak = getLastStreak ();
		data.topStreak = getTopStreak ();
		data.luck = getLuck();
		data.beanScore = getBeanScore();
        data.bet = getBet();
        data.gambling = this.gambling;
        data.prize = getPrize();
        data.streak = getStreak();
		//setMoney(data.coins);

		bf.Serialize (file, data);
		file.Close ();//Takes serizalable class "file" and writes to our class "data"
		//print ("You Saved");
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playertestsaveinfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playertestsaveinfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			setMoney(data.coins);
            print(data.coins);
			setLastStreak (data.lastStreak);
			setTopStreak (data.topStreak);
			setLuck (data.luck);
			setBeanScore (data.beanScore);
            setStreak(data.streak);
            setBet(data.bet);
            setPrize(data.prize);
            this.gambling = data.gambling;
			//setSPM ();  //? do we want this

			print ("You loaded!");
		}
	}

	//Delete file path
	private string SaveFilePatch {
		get { return Application.persistentDataPath + "/playertestsaveinfo.dat"; }
	}

	public void deleteSaveData() {
		try {
			File.Delete(SaveFilePatch);
		}
		catch(Exception ex) {
			Debug.LogException (ex);
		}
	}

	public void clearStats() {
		try {
			if (File.Exists (Application.persistentDataPath + "/playertestsaveinfo.dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/playertestsaveinfo.dat", FileMode.Open);
				PlayerData data = (PlayerData)bf.Deserialize (file);

				data.coins = 0;
                print(data.coins);
                data.spm = 0;
				data.beanScore = 2;
				data.luck = 50;
				data.lastStreak = 0;
				data.topStreak = 0;
				print("Cleared Stats");
                bf.Serialize(file, data);
                file.Close();
            }
            Load();
		}catch(Exception ex) {
			Debug.LogException (ex);
		}
	}
		

}

[Serializable]
class PlayerData {
	public float coins;
	public float spm;
	public int lastStreak;
	public int topStreak;
	public float luck;
	public int beanScore;
    public bool gambling;
    public int prize;
    public int bet;
    public int streak;
	//float/int game time
}