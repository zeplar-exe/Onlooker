namespace Onlooker.IntermediateConfiguration.Game.Entities.Personality;

public class PersonalityConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("personalities")] 
    public List<PersonalityConfig> PersonalityConfigs { get; }

    public PersonalityConfigGroup()
    {
        PersonalityConfigs = new List<PersonalityConfig>();
    }
}