using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using Mycorrhiza.Content;
using Mycorrhiza.Content.MycorrhizaBiome.OrbLoot;

namespace Mycorrhiza.Content.MycorrhizaBiome.FishingLoot
{
    public class MycorrhizalCrate : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 5;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<MycorrhizalCrateTile>());
            Item.rare = ItemRarityID.Green;
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            int[] dropOptions = [ModContent.ItemType<Cordycep>(),
                ModContent.ItemType<DispersionWand>(),
                ModContent.ItemType<FruitingBody>(),
                ModContent.ItemType<PlatedRing>()];

            var main = ItemDropRule.OneFromOptions(1, dropOptions);

            CrateHelper.BiomeCrate(itemLoot, main, ItemDropRule.NotScalingWithLuck(ItemID.SoulofNight, 2, 2, 5));
        }
    }

    public class MycorrhizalCrateTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = [16, 18];
            TileObjectData.addTile(Type);

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            AddMapEntry(new Color(123, 104, 84));
            DustType = -1;
        }
    }
}