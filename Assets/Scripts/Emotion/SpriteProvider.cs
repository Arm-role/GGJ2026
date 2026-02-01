using UnityEngine;

public class SpriteProvider
{
  private readonly string _spritePath;

  public SpriteProvider(string spritePath)
  {
    _spritePath = spritePath;
  }

  public Sprite Get(string type)
      => Resources.Load<Sprite>($"{_spritePath}/{type}");
}