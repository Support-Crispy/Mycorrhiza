using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

//Why is the sprite fucked

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
	public class WoodApple : ModItem
	{

		public override void SetStaticDefaults()
		{
			ItemID.Sets.IsFood[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 34;
			Item.maxStack = 30;
			Item.value = Item.buyPrice(copper: 50);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item2;

			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useTime = 17;
			Item.useAnimation = 17;

			Item.consumable = true;
			Item.potion = false;

			Item.DefaultToFood(32, 34, BuffID.WellFed, 18000, false);
		}
	}
}