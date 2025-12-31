//PlayersRecord.cs 
//save and load players for hi-score saved as jsonformat

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

namespace TetrisV1.models
{
    public static class PlayersRecord
    {
        public static List<Player> Players=new();
        public static string FilePath="data/scoretable.json";
        public static void AddPlayer(Player player)
        {
            Players.Add(player);            
        }
        //save
        public static async Task SaveToFileAsync()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            var options=new JsonSerializerOptions{WriteIndented=true};
            string json=JsonSerializer.Serialize(Players,options);
            await File.WriteAllTextAsync(FilePath,json);
        }
        //load 
        public static async Task LoadFromFileAsync()
        {
            if (!File.Exists(FilePath))
            {
                Players=new List<Player>();
                return;
            }
            string json=await File.ReadAllTextAsync(FilePath);
            Players=JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
        }




        
    }
}