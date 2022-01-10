using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Extensions
{
    public static class SeedExtension
    {
        public static List<TEntity> SeedDataFromJson<TEntity>(string fileName, string ext="json")
        {
            string currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "Project.DAL", "InitialData", $"{fileName}.{ext}");
            string fullPath = Path.GetFullPath(currentDirectory);

            #region Test
            //string read_json = System.IO.File.ReadAllText(fullPath);
            //var result = JsonConvert.DeserializeObject<List<TEntity>>(read_json);
            #endregion

            var result = new List<TEntity>();
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<TEntity>>(json);
            }

            return result;
        }
    }
}
