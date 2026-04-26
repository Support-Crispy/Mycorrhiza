using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace Mycorrhiza.Content
{
    internal static class FurnitureCommon
    {

        /// <summary>
        /// Extension which initializes a ModTile to be a bar.
        /// </summary>
        /// <param name="mt">The ModTile which is being initialized.</param>
        /// <param name="itemDropID">The ID of the item this tile drops when broken.</param>
        /// <param name="mapColor">The color of the bar on the minimap.</param>
        /// <param name="lavaImmune">Whether this tile is supposed to be immune to lava. Defaults to true like vanilla bars.</param>
        internal static void SetUpBar(this ModTile mt, int itemDropID, Color mapColor, bool lavaImmune = true)
        {
            mt.RegisterItemDrop(itemDropID);

            Main.tileShine[mt.Type] = 1100;
            Main.tileSolid[mt.Type] = true;
            Main.tileSolidTop[mt.Type] = true;
            Main.tileFrameImportant[mt.Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = !lavaImmune;
            TileObjectData.newTile.LavaPlacement = lavaImmune ? LiquidPlacement.Allowed : LiquidPlacement.NotAllowed;
            TileObjectData.addTile(mt.Type);

            // Vanilla bars are labeled as "Metal Bar" on the minimap
            mt.AddMapEntry(mapColor, Language.GetText("MapObject.MetalBar"));
        }

    }
}