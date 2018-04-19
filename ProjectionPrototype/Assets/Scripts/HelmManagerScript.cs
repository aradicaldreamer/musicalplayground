using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmManagerScript : MonoBehaviour {
	[Header("Synth Section")]
	public AudioHelm.HelmController Drone; //ref to helm controller to play synth
	public AudioHelm.HelmController Bass; //ref to helm controller to play synth
	public AudioHelm.HelmController Arp;
	public AudioHelm.HelmController AirDrone;
	public AudioHelm.HelmController Lead;
	public AudioHelm.HelmController Snare;
	public AudioHelm.HelmController Hihat;
	public AudioHelm.HelmController CymbalHit;
	[Header("Sequencer Section")]
	public AudioHelm.Sequencer KickSeq;
	public AudioHelm.Sequencer SnareSeq;
	public AudioHelm.Sequencer HihatSeq;
	public AudioHelm.Sequencer ArpSeq;
	public AudioHelm.Sequencer AirDroneSeq;
	public AudioHelm.Sequencer LeadSeq;
	[Header("Chord Picker")]
	public float ChordCmaj69, ChordAmaj69, ChordGbmaj69, ChordEbmaj69, ChordA6B, ChordAbmin9, ChordFmin9 = 0;
//	public int Chordii = 0;
//	public int Chordiv = 0;
//	public int Chordv = 0;
//	public int Chordvi = 0;
//	public int Chordbvii = 0;
//	public int Chordvii = 0;
	[Header("Drum Parameters")]
	//Snare Parameters
	public float SnareDelayMix = 0.0f; // posX
	public float SnareDelayFeedback = 0.0f; //posY
	public float SnareDelaySync = 0.4f; //velX
	//Hihat Parameters
	public float HihatDelayMix = 0.0f; //posX combined
	public float HihatDelayFeedback = 0.0f; //posY combined
	public float HihatDelaySync = 0.4f; //velY
	[Header("Bass Parameters")]
	//Bass Parameters
	public float BassFeedbackTune = 0.0f; //posX
	public float BassFeedbackAmount = 0.0f; //posY
	public float BassSubShuffle = 0.0f; //velX
	public float BassOSC2tune = 0.0f; //velY
	public float BassReso = 0.0f; //collision 0-1
	[Header("Drone Parameters")]
	//Drone	Parameters
	public float DroneX = 0.0f; // posX
	public float DroneY = 0.5f; // posY
	public float DroneFeedback = 0.0f; // velX
	public float DroneMod = 0.0f; // velY
	public float DronefilterBlend = 1.0f; //collision
	[Header("AirDrone Parameters")]
	//AirDrone Parameters	
	public float AirDroneX = 1.0f; // posX
	public float AirDroneY = 0.0f; // posY
	public float AirDronefilterBlend = 1.0f; //velX
	public float AirDroneArpOn = 0.0f; //velY
	public float AirDroneDelayTempo = 0.5f; //collision

	[Header("Arp Parameters")]
	//Arp Parameters
	public float ArpStutter = 1.0f; // posX
	public float ArpSub = 0.0f; // posY
	public float ArpFeedback = 0.5f; // velX
	public float ArpDelayFeedback = 0.5f; //velY
	public float ArpSustain = 0.0f; //collision 0-1
	[Header("Lead Parameters")]
	//Lead Parameters
	public float LeadMix = 0.0f; //posX
	public float LeadSub = 0.0f; //posY
	public float LeadSustain = 0.2f; // velX 
	public float LeadAttack = 0.1f; //velY
	public float LeadDelaySync = 0.5f; // collision 0.5 - 1.0?
	public float LeadDelayTempo = 0.5f; // collision combined 0.5 - 1.0?


	[Header("Startup invoke timer")]
	//Timer to invoke sequencers/synths on startup
	public float time = 1.5f;

	//Is the midi note 0 to 127 = to music note
	private int C2= 48;
	private int Db2 = 49;
	private int D2 = 50;
	private int Eb2= 51;
	private int E2= 52;
	private int F2 = 53;
	private int Gb2= 54;
	private int G2= 55;
	private int Ab2= 56;
	private int A2= 57;
	private int Bb2= 58;
	private int B2= 59;

	private int C3 = 60;
	private int Db3= 61;
	private int D3= 62;
	private int Eb3 = 63;
	private int E3 = 64;
	private int F3 = 65;
	private int Gb3 = 66;
	private int G3 = 67;
	private int Ab3 = 68;
	private int A3 = 69;
	private int Bb3 = 70;
	private int B3 = 71;

	private int C4 = 72;
	private int Db4 = 73;
	private int D4 = 74;
	private int Eb4 = 75;
	private int E4= 76;
	private int F4= 77;
	private int Gb4 = 78;
	private int G4= 79;
	private int Ab4= 80;
	private int A4= 81;
	private int Bb4 = 82;
	private int B4= 83;

	private int C5= 84;
	private int Db5= 85;
	private int D5= 86;
	private int Eb5= 87;
	private int E5 = 88;
	private int F5 = 89;
	private int Gb5 = 90;
	private int G5 = 91;
	private int Ab5= 92;
	private int A5= 93;
	private int Bb5 = 94;
	private int B5= 95;

	private int C6= 96;
	private int Db6 = 97;
	private int D6= 98;
	private int Eb6= 99;
	private int E6 = 100;
	private int F6= 101;
	private int Gb6= 102;
	private int G6= 103;
	private int Ab6= 104;
	private int A6= 105;
	private int Bb6= 106;
	private int B6= 107;
	private int C7= 108;

	public static HelmManagerScript main;

	void Awake() {
		main = this;
	}

	// Use this for initialization
	void Start () {
		//string[] notes = { "C-2", "Db-2", "D-2", "Eb-2", "E-2", "F-2", "Gb-2", "G-2", "Ab-2","A-2","Bb-2","B-2","C-1", "Db-1", "D-1", "Eb-1", "E-1", "F-1", "Gb-1", "G-1", "Ab-1","A-1","Bb-1","B-1","C0", "Db0", "D0", "Eb0", "E0", "F0", "Gb0", "G0", "Ab0","A0","Bb0","B0","C1", "Db1", "D1", "Eb1", "E1", "F1", "Gb1", "G1", "Ab1","A1","Bb1","B1","C2", "Db2", "D2", "Eb2", "E2", "F2", "Gb2", "G2", "Ab2","A2","Bb2","B2","C3", "Db3", "D3", "Eb3", "E3", "F3", "Gb3", "G3", "Ab3","A3","Bb3","B3","C4", "Db4", "D4", "Eb4", "E4", "F4", "Gb4", "G4", "Ab4","A4","Bb4","B4","C5", "Db5", "D5", "Eb5", "E5", "F5", "Gb5", "G5", "Ab5","A5","Bb5","B5","C6", "Db6", "D6", "Eb6", "E6", "F6", "Gb6", "G6", "Ab6","A6","Bb6","B6"};
		Invoke("DroneEnable", time);
		Invoke("AirDroneEnable", time*4);
		Invoke("BassEnable", time*8);
		Invoke("DrumsEnable", time*12);
		Invoke("ArpEnable", time*16);
		Invoke ("LeadEnable", time*20);
		Invoke ("CymbalHitEnable", time*12);
	}

	public void DrumsEnable()
	{
		KickSeq.Clear ();
		SnareSeq.Clear ();
		HihatSeq.Clear ();

		KickSeq.AddNote (C3,0,1);
		KickSeq.AddNote (C3,8,9);

		SnareSeq.AddNote (D2,4,5);
		SnareSeq.AddNote (D2,10,11);
		SnareSeq.AddNote (D2,14,15);
		SnareSeq.AddNote (D2,15,16,0.5f);

		HihatSeq.AddNote (G3, 1, 2, 0.5f);
		HihatSeq.AddNote (G3, 2, 3);
		HihatSeq.AddNote (G3, 6, 7, 0.7f);
		HihatSeq.AddNote (G3, 8, 9);
		HihatSeq.AddNote (G3, 12, 13,0.7f);
		HihatSeq.AddNote (G3, 14, 15);

	}
	public void DrumsDisable()
	{
		KickSeq.Clear ();
		SnareSeq.Clear ();
		HihatSeq.Clear ();
	}
	public void BassEnable()
	{
		if (ChordCmaj69 == 1) {
			//Bass Chordi
			Bass.AllNotesOff();
			Bass.NoteOn (C2);
			Bass.NoteOn (C3);
		}
		if (ChordAmaj69 == 1) {
			//Bass Chordii
			Bass.AllNotesOff();
			Bass.NoteOn (A2);
			Bass.NoteOn (A3);			
		}
		if (ChordGbmaj69 == 1) {
			//Bass Chordiv
			Bass.AllNotesOff();
			Bass.NoteOn (Gb2);
			Bass.NoteOn (Gb3);			
		}
		if (ChordEbmaj69 == 1) {
			//Bass Chordv
			Bass.AllNotesOff();
			Bass.NoteOn (Eb2);
			Bass.NoteOn (Eb3);			
		}
		if (ChordA6B == 1) {
			//Bass Chordvi
			Bass.AllNotesOff();
			Bass.NoteOn (A2);
			Bass.NoteOn (A3);			
		}
		if (ChordAbmin9 == 1) {
			//Bass Chordbvii
			Bass.AllNotesOff();
			Bass.NoteOn (Ab2);
			Bass.NoteOn (Ab3);			
		}
		if (ChordFmin9 == 1) {
			//Bass Chordvii
			Bass.AllNotesOff();
			Bass.NoteOn (F2);
			Bass.NoteOn (F3);			
		}
	}
	public void BassDisable()
	{
		Bass.AllNotesOff();
	}
	public void DroneEnable()
	{
		if (ChordCmaj69 == 1) {
			//Drone Chordi
			Drone.AllNotesOff();
			Drone.NoteOn (C2, 1.0f);
			Drone.NoteOn (E2, 1.0f);
			Drone.NoteOn (A2, 1.0f); 
			Drone.NoteOn (D3, 1.0f);
		}
		if (ChordAmaj69 == 1) {
			//Drone Chordii
			Drone.AllNotesOff();
			Drone.NoteOn (A2, 1.0f);
			Drone.NoteOn (Db2, 1.0f);
			Drone.NoteOn (Gb3, 1.0f); 
			Drone.NoteOn (B3, 1.0f);
		}
		if (ChordGbmaj69 == 1) {
			//Drone Chordiv
			Drone.AllNotesOff();
			Drone.NoteOn (Gb2, 1.0f);
			Drone.NoteOn (Bb2, 1.0f);
			Drone.NoteOn (Eb3, 1.0f); 
			Drone.NoteOn (Ab3, 1.0f);
		}
		if (ChordEbmaj69 == 1) {
			//Drone Chordv
			Drone.AllNotesOff();
			Drone.NoteOn (Eb2, 1.0f);
			Drone.NoteOn (G2, 1.0f);
			Drone.NoteOn (C3, 1.0f); 
			Drone.NoteOn (F3, 1.0f);
		}
		if (ChordA6B == 1) {
			//Drone Chordvi
			Drone.AllNotesOff();
			Drone.NoteOn (B2, 1.0f);
			Drone.NoteOn (A3, 1.0f);
			Drone.NoteOn (Bb3, 1.0f); 
			Drone.NoteOn (E3, 1.0f);
		}
		if (ChordAbmin9 == 1) {
			//Drone Chordbvii
			Drone.AllNotesOff();
			Drone.NoteOn (Ab2, 1.0f);
			Drone.NoteOn (B3, 1.0f);
			Drone.NoteOn (Gb3, 1.0f); 
			Drone.NoteOn (Bb4, 1.0f);
		}
		if (ChordFmin9 == 1) {
			//Drone Chordvii
			Drone.AllNotesOff();
			Drone.NoteOn (F2, 1.0f);
			Drone.NoteOn (Ab3, 1.0f);
			Drone.NoteOn (Eb3, 1.0f); 
			Drone.NoteOn (G4, 1.0f);
		}
	}
	public void DroneDisable()
	{
		Drone.AllNotesOff();
	}
	public void AirDroneEnable()
	{
		if (ChordCmaj69 == 1) {
		//AirDrone Chordi
		AirDroneSeq.Clear ();
		AirDroneSeq.AddNote (B5, 0, 17);
		AirDroneSeq.AddNote (D6, 0, 17);
		AirDroneSeq.AddNote (E6, 0, 17);
		}
		if (ChordAmaj69 == 1) {
			//AirDrone Chordii
			AirDroneSeq.Clear ();
			AirDroneSeq.AddNote (Ab5, 0, 17);
			AirDroneSeq.AddNote (B6, 0, 17);
			AirDroneSeq.AddNote (Db6, 0, 17);
		}
		if (ChordGbmaj69 == 1) {
			//AirDrone Chordiv
			AirDroneSeq.Clear ();
			AirDroneSeq.AddNote (F5, 0, 17);
			AirDroneSeq.AddNote (Ab6, 0, 17);
			AirDroneSeq.AddNote (Bb6, 0, 17);
		}
		if (ChordEbmaj69 == 1) {
			//AirDrone Chordv
			AirDroneSeq.Clear ();
			AirDroneSeq.AddNote (D5, 0, 17);
			AirDroneSeq.AddNote (F6, 0, 17);
			AirDroneSeq.AddNote (G6, 0, 17);
		}
		if (ChordA6B == 1) {
			//AirDrone Chordvi
			AirDroneSeq.Clear ();
			AirDroneSeq.AddNote (Gb5, 0, 17);
			AirDroneSeq.AddNote (Db6, 0, 17);
			AirDroneSeq.AddNote (E6, 0, 17);
		}
		if (ChordAbmin9 == 1) {
			//AirDrone Chordbvii
			AirDroneSeq.Clear ();
			AirDroneSeq.AddNote (Gb5, 0, 17);
			AirDroneSeq.AddNote (Bb6, 0, 17);
			AirDroneSeq.AddNote (B6, 0, 17);
		}
		if (ChordFmin9 == 1) {
			//AirDrone Chordvii
			AirDroneSeq.Clear ();
			AirDroneSeq.AddNote (Eb5, 0, 17);
			AirDroneSeq.AddNote (G6, 0, 17);
			AirDroneSeq.AddNote (Ab6, 0, 17);
		}
	}
	public void AirDroneDisable()
	{
		AirDroneSeq.Clear ();
	}
	public void ArpEnable()
	{
		if (ChordCmaj69 == 1) {
			//Arp Chordi
			ArpSeq.Clear ();
			ArpSeq.AddNote (E4, 0, 17);
			ArpSeq.AddNote (A4, 0, 17);
			ArpSeq.AddNote (B4, 0, 17);
			ArpSeq.AddNote (D5, 0, 17);
		}
		if (ChordAmaj69 == 1) {
			//Arp Chordii
			ArpSeq.Clear ();
			ArpSeq.AddNote (Db4, 0, 17);
			ArpSeq.AddNote (Gb4, 0, 17);
			ArpSeq.AddNote (Ab4, 0, 17);
			ArpSeq.AddNote (B5, 0, 17);
		}
		if (ChordGbmaj69 == 1) {
			//Arp Chordiv
			ArpSeq.Clear ();
			ArpSeq.AddNote (Bb4, 0, 17);
			ArpSeq.AddNote (Eb4, 0, 17);
			ArpSeq.AddNote (F4, 0, 17);
			ArpSeq.AddNote (Ab5, 0, 17);
		}
		if (ChordEbmaj69 == 1) {
			//Arp Chordv
			ArpSeq.Clear ();
			ArpSeq.AddNote (G4, 0, 17);
			ArpSeq.AddNote (C4, 0, 17);
			ArpSeq.AddNote (D4, 0, 17);
			ArpSeq.AddNote (F5, 0, 17);
		}
		if (ChordA6B == 1) {
			//Arp Chordvi
			ArpSeq.Clear ();
			ArpSeq.AddNote (Db4, 0, 17);
			ArpSeq.AddNote (Gb4, 0, 17);
			ArpSeq.AddNote (E4, 0, 17);
			ArpSeq.AddNote (B5, 0, 17);
		}
		if (ChordAbmin9 == 1) {
			//Arp Chordbvii
			ArpSeq.Clear ();
			ArpSeq.AddNote (B4, 0, 17);
			ArpSeq.AddNote (Eb4, 0, 17);
			ArpSeq.AddNote (Gb5, 0, 17);
			ArpSeq.AddNote (Bb5, 0, 17);
		}
		if (ChordFmin9 == 1) {
			//Arp Chordvii
			ArpSeq.Clear ();
			ArpSeq.AddNote (Ab4, 0, 17);
			ArpSeq.AddNote (C4, 0, 17);
			ArpSeq.AddNote (Eb5, 0, 17);
			ArpSeq.AddNote (G5, 0, 17);
		}
	}
	public void ArpDisable()
	{
		ArpSeq.Clear ();
	}
	public void LeadEnable()
	{
		if (ChordCmaj69 == 1) {
		//Lead Chordi
		LeadSeq.Clear ();
	//	LeadSeq.AddNote (A5, 0, 1);
		LeadSeq.AddNote (D5, 5, 6); //9
		LeadSeq.AddNote (A5, 8, 9); //6
		LeadSeq.AddNote (E5, 13, 14); //3
		}
		if (ChordAmaj69 == 1) {
			//Lead Chordii
			LeadSeq.Clear ();
			LeadSeq.AddNote (Db5, 0, 1); //3
			LeadSeq.AddNote (Gb6, 5, 6); //6
			LeadSeq.AddNote (B5, 8, 9); //9
			//LeadSeq.AddNote (F5, 13, 14);
		}
		if (ChordGbmaj69 == 1) {
			//Lead Chordiv
			LeadSeq.Clear ();
			LeadSeq.AddNote (Eb5, 0, 1); //6
		//	LeadSeq.AddNote (G6, 5, 6);
			LeadSeq.AddNote (Bb5, 8, 9); //3
			LeadSeq.AddNote (Ab5, 13, 14); //9
		}
		if (ChordEbmaj69 == 1) {
			//Lead Chordv
			LeadSeq.Clear ();
			//LeadSeq.AddNote (C5, 0, 1);
			LeadSeq.AddNote (G5, 0, 1); //3
			LeadSeq.AddNote (F6, 5, 6); //9
			//LeadSeq.AddNote (D5, 8, 9);
			LeadSeq.AddNote (C5, 13, 14); //6
		}
		if (ChordA6B == 1) {
			//Lead Chordvi
			LeadSeq.Clear ();
			LeadSeq.AddNote (B5, 0, 1); //9
			LeadSeq.AddNote (Db6, 5, 6); //3
			LeadSeq.AddNote (Gb5, 8, 9); //6
			//LeadSeq.AddNote (C5, 13, 14);
		}
		if (ChordAbmin9 == 1) {
			//Lead Chordbvii
			LeadSeq.Clear ();
			LeadSeq.AddNote (Gb5, 0, 1); //7
			LeadSeq.AddNote (Eb6, 5, 6); //5
			LeadSeq.AddNote (B5, 8, 9); //3
			LeadSeq.AddNote (Bb5, 13, 14); //9
		}
		if (ChordFmin9 == 1) {
			//Lead Chordvii
			LeadSeq.Clear ();
			LeadSeq.AddNote (G5, 0, 1); //9
			LeadSeq.AddNote (Ab6, 5, 6); //3
			LeadSeq.AddNote (C5, 8, 9); //5
			LeadSeq.AddNote (Eb5, 13, 14); //7
		}
	}
	public void LeadDisable()
	{
		LeadSeq.Clear ();
	}
	public void CymbalHitEnable()
	{
		CymbalHit.NoteOn (C4, 1.0f, 2.0f);
	}
	// Update is called once per frame
	void Update () {
		
	//Drums	
		//Snare
		Snare.SetParameterPercent(AudioHelm.Param.kDelayDryWet, SnareDelayMix); //posX
		Snare.SetParameterValue(AudioHelm.Param.kDelayFeedback, SnareDelayFeedback); //posY 
		Snare.SetParameterPercent(AudioHelm.Param.kDelaySync, SnareDelaySync); //velX
		//Hihat
		Hihat.SetParameterPercent(AudioHelm.Param.kDelayDryWet, HihatDelayMix); //posY combined
		Hihat.SetParameterValue(AudioHelm.Param.kDelayFeedback, HihatDelayFeedback); //posX combined
		Hihat.SetParameterPercent(AudioHelm.Param.kDelaySync, HihatDelaySync); //velY

	//Bass
		Bass.SetParameterValue(AudioHelm.Param.kOscFeedbackTune,BassFeedbackTune); //posX
		Bass.SetParameterPercent (AudioHelm.Param.kFilterDrive, BassFeedbackTune);//posX combined
		Bass.SetParameterValue(AudioHelm.Param.kOscFeedbackAmount, BassFeedbackAmount); //posY
		Bass.SetParameterValue(AudioHelm.Param.kDelayFeedback, BassFeedbackAmount);//posY combined
		Bass.SetParameterPercent(AudioHelm.Param.kSubShuffle, BassSubShuffle); //velX
		Bass.SetParameterValue(AudioHelm.Param.kOsc2Tune, BassOSC2tune); //velY
		Bass.SetParameterPercent(AudioHelm.Param.kResonance, BassReso); //collision 0-1

	//Drone
		Drone.SetParameterPercent(AudioHelm.Param.kFormantX, DroneX); //posX
		Drone.SetParameterPercent(AudioHelm.Param.kFormantY, DroneY); //posY
		Drone.SetParameterValue(AudioHelm.Param.kDelayFeedback, DroneFeedback); //velX
		Drone.SetParameterPercent(AudioHelm.Param.kCrossMod, DroneMod); //velY
		Drone.SetParameterPercent(AudioHelm.Param.kFilterBlend, DronefilterBlend); //collision

	//AirDrone
		AirDrone.SetParameterPercent(AudioHelm.Param.kFormantX, AirDroneX); //posX
		AirDrone.SetParameterPercent(AudioHelm.Param.kFormantY, AirDroneY); //posY
		AirDrone.SetParameterPercent(AudioHelm.Param.kFilterBlend, AirDronefilterBlend); //velX
		AirDrone.SetParameterPercent(AudioHelm.Param.kArpOn, AirDroneArpOn); //velY
		AirDrone.SetParameterPercent (AudioHelm.Param.kDelayTempo, AirDroneDelayTempo); //collision
		AirDrone.SetParameterPercent (AudioHelm.Param.kDelaySync, AirDroneDelayTempo-0.2f); //collision combined
 //collision 0-1

	//Arp
		Arp.SetParameterPercent(AudioHelm.Param.kStutterTempo, ArpStutter); // posX
		Arp.SetParameterPercent(AudioHelm.Param.kStutterResampleSync, ArpStutter-1.0f); // posX combined
		Arp.SetParameterPercent(AudioHelm.Param.kSubOctave, ArpSub); //posY
		Arp.SetParameterPercent(AudioHelm.Param.kSubVolume, ArpSub+0.3f); // posY combined
		Arp.SetParameterPercent (AudioHelm.Param.kOscFeedbackAmount, ArpFeedback);
		Arp.SetParameterPercent (AudioHelm.Param.kDelayFeedback, ArpDelayFeedback); //velX
		Arp.SetParameterPercent (AudioHelm.Param.kAmplitudeSustain, ArpSustain); // collision 0-1

	//Lead
		Lead.SetParameterPercent(AudioHelm.Param.kArpOn, LeadMix); //posX
		Lead.SetParameterPercent(AudioHelm.Param.kSubVolume, LeadMix+0.3f); //posX combined
		Lead.SetParameterPercent(AudioHelm.Param.kSubShuffle, LeadSub-0.5f); //posY
		Lead.SetParameterPercent(AudioHelm.Param.kSubOctave, LeadSub); //posY combined
		Lead.SetParameterPercent(AudioHelm.Param.kAmplitudeRelease, LeadSustain); //velX
		Lead.SetParameterPercent(AudioHelm.Param.kAmplitudeAttack, LeadAttack); //velY
		//	Lead.SetParameterPercent(AudioHelm.Param.kDelaySync, LeadDelaySync); //collision 0.5 -1
		//	Lead.SetParameterPercent(AudioHelm.Param.kDelayTempo, LeadDelayTempo); //collision combined 0.5 -1 
}
}
