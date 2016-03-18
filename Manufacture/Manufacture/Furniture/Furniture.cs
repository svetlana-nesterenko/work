using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manufacture.Materials;

namespace Manufacture.Furniture
{
    public abstract class Furniture : IFurniture
    {
        public string Name { get; set; }
        public float Weight { get; set; }
        public float Size { get; set; }
        public List<Materials.MaterialSpecification> Materials { get; set; }

        public Furniture()
        {
            Materials = new List<Materials.MaterialSpecification>();
        }

        public virtual string GenerateOrder()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(String.Format("ORDER FOR {0}", this));
            stringBuilder.AppendLine(String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(String.Format("Name: {0}", this.Name));
            stringBuilder.AppendLine(String.Format("Weight: {0}", this.Weight));
            stringBuilder.AppendLine(String.Format("Size: {0}", this.Size));
            stringBuilder.AppendLine(String.Format("Total Price: {0}", this.Materials.Select(m => m.Material.Price * m.Count).Sum()));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("*** USED MATERIALS ***");
            stringBuilder.AppendLine();
            foreach (Manufacture.Materials.MaterialSpecification materialSpecification in Materials)
            {
                stringBuilder.AppendLine(String.Format("MATERIAL {0}", materialSpecification.Material.GetType()));
                stringBuilder.AppendLine(String.Format("Color: {0}", materialSpecification.Material.Color));
                stringBuilder.AppendLine(String.Format("Sort: {0}", materialSpecification.Material.Sort));

                if (materialSpecification.Material is Metal)
                {
                    stringBuilder.AppendLine(String.Format("Metal Type: {0}", ((Metal)materialSpecification.Material).Type));
                }
                else if (materialSpecification.Material is Wood)
                {
                    stringBuilder.AppendLine(String.Format("Wood Type: {0}", ((Wood)materialSpecification.Material).Type));
                }
                stringBuilder.AppendLine(String.Format("Count: {0}", materialSpecification.Count));
                stringBuilder.AppendLine(String.Format("Price: {0}", materialSpecification.Count * materialSpecification.Material.Price));
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}