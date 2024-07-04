using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Game.Text;
using Dalamud.Game.Addon.Events;
using Lumina.Excel.GeneratedSheets;
using System;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Dalamud.Logging.Internal;

namespace CraftRemaining;

public unsafe class Plugin : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IAddonLifecycle AddonLifecycle { get; private set; } = null!;
    [PluginService] internal static IAddonEventManager AddonEventManager { get; private set; } = null!;
    [PluginService] internal static IPluginLog IPluginLog { get; private set; } = null!;
    [PluginService] internal static IDisposable Disposable { get; private set; } = null!;

    private const string CommandName = "/pmycommand";

    public Configuration Configuration { get; init; }

    public readonly WindowSystem WindowSystem = new("CraftRemaining");

    public Plugin()
    {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "Synthesis", OnPostUpdate);
    }
    private void OnPostUpdate(AddonEvent eventType, AddonArgs addonInfo)
    {
        var addon = (AtkUnitBase*) addonInfo.Addon;
        var progressNode = (AtkTextNode*)addon->GetNodeById(53);
        var qualityNode = (AtkTextNode*)addon->GetNodeById(59);
        var currentProgress = addon->AtkValues[5].UInt;
        var maxProgress = addon->AtkValues[6].UInt;
        var currentQuality = addon->AtkValues[9].UInt;
        var maxQuality = addon->AtkValues[17].UInt;
        var remainingProgress = maxProgress - currentProgress;
        var remainingQuality = maxQuality - currentQuality;
        IPluginLog.Debug($" {remainingProgress} {remainingQuality}");
        progressNode->SetText("Progress - Remaining: " + remainingProgress);
        qualityNode->SetText("Quality - Remaining: " + remainingQuality);
        
    }
    public void Dispose()
    {
        
    }
}
