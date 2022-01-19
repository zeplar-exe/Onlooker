namespace Onlooker.ObjectProperties.Animation;

public class AnimationSettings
{
    public TimeSpan Length { get; set; }
    public TimeSpan Interval { get; set; }
    public AnimationType Type { get; set; }
    public ObjectPropertyMetadata Metadata { get; }

    public AnimationSettings()
    {
        Metadata = new ObjectPropertyMetadata();
    }
}