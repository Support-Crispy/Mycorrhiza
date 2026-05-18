using Microsoft.Xna.Framework;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Ice;
using Mycorrhiza.Content.MycorrhizaBiome.Dusts;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
    public class VitriolicPowder : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.PurificationPowder;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<VitriolicPowderSpray>();
            Item.shootSpeed = 5;
            Item.value = Item.sellPrice(copper: 20);
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ModContent.ItemType<VitriolicMushroom>(), 1)
                .Register();
        }
    }

    internal class VitriolicPowderSpray : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_0";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PurificationPowder);
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    ModContent.DustType<Dusts.MycoMoldDust>(),
                    Projectile.velocity.X * 0.2f,
                    Projectile.velocity.Y * 0.2f,
                    100,
                    default,
                    1.2f
                );
                Main.dust[dust].noGravity = true;
            }

            Point pt = Projectile.Center.ToTileCoordinates();
            WorldGen.Convert(pt.X, pt.Y, MycorrhizaConversion.ConversionType, 3);
        }

        public override bool? CanCutTiles() => false;
        public override bool? CanDamage() => false;
    }

    public class MycorrhizaConversion : ModBiomeConversion
    {
        public static int ConversionType => ModContent.GetInstance<MycorrhizaConversion>().Type;

        public override void SetStaticDefaults()
        {

            //I wish there was an easier way to do this Yandev image

            TileLoader.RegisterConversion(TileID.Stone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileustoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Ebonstone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileustoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Crimstone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileustoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Pearlstone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileustoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Grass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CorruptGrass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CrimsonGrass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.HallowedGrass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Sand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Ebonsand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Crimsand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Pearlsand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.HardenedSand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<HardenedPileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CorruptHardenedSand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<HardenedPileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CrimsonHardenedSand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<HardenedPileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.HallowHardenedSand, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<HardenedPileusandBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.Sandstone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandstoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CorruptSandstone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandstoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CrimsonSandstone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandstoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.HallowSandstone, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<PileusandstoneBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.JungleGrass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyJungleGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.MushroomGrass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyJungleGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CorruptJungleGrass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyJungleGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CrimsonJungleGrass, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<MoldyJungleGrassPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.IceBlock, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<GreyIceBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.CorruptIce, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<GreyIceBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.FleshIce, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<GreyIceBlockPlaced>());
                return true;
            });

            TileLoader.RegisterConversion(TileID.HallowedIce, ConversionType, (i, j, type, conversionType) =>
            {
                WorldGen.ConvertTile(i, j, ModContent.TileType<GreyIceBlockPlaced>());
                return true;
            });
        }
    }
}