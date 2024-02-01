using System;

[Serializable]
public class ScoreElement
{
    public string playerName;
    public int score;
    public string time;
    public int mistakes;
  
  public ScoreElement (string name, int score, string time, int mistakes)
  {
    this.playerName = name;
    this.score = score;
    this.time = time;
    this.mistakes = mistakes;
  }
}
