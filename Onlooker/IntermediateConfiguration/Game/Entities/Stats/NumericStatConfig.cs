using YASF.Serialization;

namespace Onlooker.IntermediateConfiguration.Game.Entities.Stats;

public class NumericStatConfig : StatConfig
{
    [SettingsSerializer.SerializationName("default_minimum")]
    [SettingsDeserializer.SerializationName("default_minimum")]
    public double DefaultMinimum { get; set; }
    
    [SettingsSerializer.SerializationName("default_maximum")]
    [SettingsDeserializer.SerializationName("default_maximum")]
    public double DefaultMaximum { get; set; }
    
    [SettingsSerializer.SerializationName("minimum_can_change")]
    [SettingsDeserializer.SerializationName("minimum_can_change")]
    public bool MinimumCanChange { get; set; }
    
    [SettingsSerializer.SerializationName("maximum_can_change")]
    [SettingsDeserializer.SerializationName("maximum_can_change")]
    public bool MaximumCanChange { get; set; }
    
    [SettingsSerializer.SerializationName("default_stat")]
    [SettingsDeserializer.SerializationName("default_stat")]
    public bool IsDefault { get; set; }
    
    [SettingsSerializer.SerializationName("on_reached_minimum_handler")]
    [SettingsDeserializer.SerializationName("on_reached_minimum_handler")]
    public OnReachLimitHandlerEnum OnReachedMinimumHandler { get; set; }
    
    [SettingsSerializer.SerializationName("on_reached_maximum_handler")]
    [SettingsDeserializer.SerializationName("on_reached_maximum_handler")]
    public OnReachLimitHandlerEnum OnReachedMaximumHandler { get; set; }
    
    public NumericStatConfig(FileInfo source) : base(source)
    {
        
    }
    
    public enum OnReachLimitHandlerEnum
    {
        None = 0,
        
        Kill
    }
}