using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Furniture
{
	public class WitchDoctorShop : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			if (shop.NpcType == NPCID.WitchDoctor)
			{
				shop.Add<MycorrhizaWaterFountain>();
			}
		}
	}
}