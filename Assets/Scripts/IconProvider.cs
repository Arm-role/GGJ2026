using UnityEngine;

public class IconProvider
{
  private readonly string _iconPath;

  public IconProvider(string iconPath)
  {
    _iconPath = iconPath;
  }

  public Sprite Get(EmotionType type)
      => Resources.Load<Sprite>($"{_iconPath}/{type}");
}