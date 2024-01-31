using System;

[Serializable]
public class ScoreElement
{
    public string playerName;
    public string points;
    public string time;
    public string mistakes;
  
  public ScoreElement (string name, string points, string time, string mistakes)
  {
    this.playerName = name;
    this.points = points;
    this.time = time;
    this.mistakes = mistakes;
  }
}
