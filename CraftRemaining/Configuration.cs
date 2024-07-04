using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace CraftRemaining;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;
    

    // the below exist just to make saving less cumbersome
    public void Save()
    {
    }

}
