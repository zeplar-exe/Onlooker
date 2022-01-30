namespace Onlooker.IntermediateConfiguration.Game.Entities.Personality;

public class PersonalityConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("")] 
    public List<PersonalityConfig> PersonalityConfigs { get; }

    public PersonalityConfigGroup()
    {
        PersonalityConfigs = new List<PersonalityConfig>();
    }
}