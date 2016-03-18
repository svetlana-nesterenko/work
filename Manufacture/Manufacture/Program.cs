using Manufacture.Furniture;
using Manufacture.Materials;
using System;
using System.Collections.Generic;
using System.IO;

namespace Manufacture
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IFurniture> furnitures = GenerateRandomFurnitureList(10);
            foreach (IFurniture furniture in furnitures)
            {
                string order = furniture.GenerateOrder();
                File.WriteAllText("order_for_" + furniture.Name + ".txt", order);
            }
        }

        private static List<IFurniture> GenerateRandomFurnitureList(int count)
        {
            List<IFurniture> list = new List<IFurniture>();

            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                IFurniture f = CreateRandomFurniture(random);
                f.Name = f.GetType() + " #" + i;
                f.Size = random.Next(1, 1000) / 10;
                f.Weight = random.Next(1, 1000) / 10;

                for (int j = 0; j < random.Next(1, 10); j++)
                {
                    IMaterial material = CreateRandomMaterial(random);
                    f.Materials.Add(new Materials.MaterialSpecification() { Material = material, Count = random.Next(1000, 3000) / 100 });
                }

                list.Add(f);
            }
            return list;
        }

        private static IFurniture CreateRandomFurniture(Random random)
        {
            IFurniture f = null;

            int a = random.Next(1, 6);
            switch (a)
            {
                case 1:
                    f = new AdultBed();
                    ((IBed)f).SeatPlacesNumber = random.Next(1, 2);
                    break;
                case 2:
                    f = new AdultCabinet();
                    ((ICabinet)f).SectionsNumber = random.Next(2, 10);
                    break;
                case 3:
                    f = new AdultTable();
                    break;
                case 4:
                    f = new ChildBed();
                    ((IBed)f).SeatPlacesNumber = random.Next(1, 3);
                    ((IChild) f).CertificateNumber = "N-" + random.Next(1000, 30000);
                    break;
                case 5:
                    f = new ChildCabinet();
                    ((ICabinet)f).SectionsNumber = random.Next(1, 5);
                    ((IChild)f).CertificateNumber = "N-" + random.Next(1000, 30000);
                    break;
                case 6:
                    f = new ChildTable();
                    ((IChild)f).CertificateNumber = "N-" + random.Next(1000, 30000);
                    break;
            }

            return f;
        }

        private static IMaterial CreateRandomMaterial(Random random)
        {
            IMaterial material = null;
            switch (random.Next(1, 3))
            {
                case 1:
                    material = new Wood();
                    switch (random.Next(1, 3))
                    {
                        case 1:
                            ((Wood)material).Type = WoodType.Oak;
                            break;
                        case 2:
                            ((Wood)material).Type = WoodType.Birch;
                            break;
                        case 3:
                            ((Wood)material).Type = WoodType.Aspen;
                            break;
                    }
                    break;
                case 2:
                    material = new Metal();
                    switch (random.Next(1, 3))
                    {
                        case 1:
                            ((Metal)material).Type = MetalType.Alluminium;
                            break;
                        case 2:
                            ((Metal)material).Type = MetalType.Ferrum;
                            break;
                        case 3:
                            ((Metal)material).Type = MetalType.Steel;
                            break;
                    }
                    break;
                case 3:
                    material = new Textile();
                    break;
            }

            switch (random.Next(1, 3))
            {
                case 1:
                    material.Color = MaterialColor.Black;
                    break;
                case 2:
                    material.Color = MaterialColor.Red;
                    break;
                case 3:
                    material.Color = MaterialColor.White;
                    break;
            }

            switch (random.Next(1, 3))
            {
                case 1:
                    material.Sort = MaterialSort.High;
                    break;
                case 2:
                    material.Sort = MaterialSort.Middle;
                    break;
                case 3:
                    material.Sort = MaterialSort.Low;
                    break;
            }

            material.Price = random.Next(1000, 3000) / 10;
            return material;
        }
    }
}