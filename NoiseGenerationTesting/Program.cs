using Onlooker.Common;
using Onlooker.Common._2D;
using Onlooker.Generation;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

var generation = new NoiseGenerator();

generation.Frequencies.Add(new NoiseFrequency(1, 1));
generation.Frequencies.Add(new NoiseFrequency(0.25, 16));
generation.FudgeFactor = 1.2;

const int sizeX = 100;
const int sizeY = 100;
var map = generation.Generate(new Vector2Int(sizeX, sizeY), 1);
var colors = new Color[sizeX, sizeY];

for (var x = 0; x < sizeX; x++)
{
    for (var y = 0; y < sizeY; y++)
    {
        var noise = map[x, y];
        var color = (byte)Math.Ceiling(Math2.Lerp(0d, 255d, noise));
        
        colors[x, y] = new Color(color, color, color);
    }
}

var window = new RenderWindow(VideoMode.DesktopMode, "Noise Test", Styles.Default);
window.Size = new Vector2u(800, 800);
var image = new Image(colors);
var texture = new Texture(image);
var sprite = new Sprite { Texture = texture };

window.Closed += delegate { window.Close(); };

while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear();

    var windowSize = window.GetView().Size;
    sprite.Scale = new Vector2f(
        windowSize.X / sprite.GetLocalBounds().Width, 
        windowSize.Y / sprite.GetLocalBounds().Height);
    
    window.Draw(sprite);
    window.Display();
}