using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Mycorrhiza.Content;

internal class Recipes : ModSystem
{
    private static Dictionary<int, int> groupEntries = [];

    /// <summary> Add the given item <paramref name="type"/> to the existing recipe group of <paramref name="groupID"/>. </summary>
    /// <param name="groupID"> The recipe group to add to. </param>
    /// <param name="type"> The item type to add. </param>
    public static void AddToGroup(int groupID, int type) => groupEntries.Add(type, groupID);

    /// <summary> Provides an array of all item types included in groups <paramref name="groupIDs"/>. </summary>
    public static int[] GetTypesFromGroup(params int[] groupIDs)
    {
        List<int> allTypes = [];
        foreach (int id in groupIDs)
        {
            allTypes.AddRange(RecipeGroup.recipeGroups[id].ValidItems);
        }

        return [.. allTypes];
    }

    public override void AddRecipeGroups()
    {
        foreach (var pair in groupEntries)
        {
            var group = RecipeGroup.recipeGroups[pair.Value];
            group.ValidItems.Add(pair.Key);
        }

        groupEntries = null;

        RecipeGroup.RegisterGroup("IronBars", BaseGroup(ItemID.IronBar, [ItemID.IronBar, ItemID.LeadBar]));
    }

    public static RecipeGroup BaseGroup(object GroupName, int[] Items)
    {
        string Name = "";
        Name += GroupName switch
        {
            //modcontent items
            int i => Lang.GetItemNameValue((int)GroupName),
            //vanilla item ids
            short s => Lang.GetItemNameValue((short)GroupName),
            //custom group names
            _ => GroupName.ToString(),
        };
        return new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Name, Items);
    }
}