using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spotify_Stats
{
    public class Exporter
    {

        public static void ExportToJson(List<PlaylistTrackItem> items, string filePath)
        {
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static void ExportToXml(List<PlaylistTrackItem> items, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlaylistTrackItem>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, items);
            }
        }

        public static void ExportToTxt(List<PlaylistTrackItem> items, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var item in items)
                {
                    string artists = string.Join(", ", item.Artists ?? new List<string>());
                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine($"Name       : {item.Name}");
                    writer.WriteLine($"Artists    : {artists}");
                    writer.WriteLine($"Album      : {item.AlbumName}");
                    writer.WriteLine($"Duration   : {item.DurationMs} ms");
                    writer.WriteLine($"Popularity : {item.Popularity}");
                    writer.WriteLine($"Added At   : {item.AddedAt:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"Added By   : {item.AddedBy}");
                    writer.WriteLine($"Track ID   : {item.Id}");
                    writer.WriteLine($"Artist ID  : {item.ArtistId}");
                    writer.WriteLine($"Image URL  : {item.AlbumImageUrl}");
                    writer.WriteLine();
                }

                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine($"Total tracks: {items.Count}");
            }
        }
        public static void ExportToCsv(List<PlaylistTrackItem> items, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Escribir encabezado
                writer.WriteLine("Id,Name,DurationMs,Artists,AlbumName,AlbumImageUrl,AddedAt,AddedBy,ArtistId,Popularity");

                foreach (var item in items)
                {
                    string artists = string.Join(";", item.Artists ?? new List<string>());

                    // Escapar comas y comillas
                    string[] fields = new string[]
                    {
                item.Id,
                item.Name,
                item.DurationMs.ToString(),
                artists,
                item.AlbumName,
                item.AlbumImageUrl,
                item.AddedAt.ToString("o"), // formato ISO 8601
                item.AddedBy,
                item.ArtistId,
                item.Popularity.ToString()
                    };

                    // Escapar comillas y envolver campos con comas
                    string line = string.Join(",", fields.Select(f => $"\"{f?.Replace("\"", "\"\"")}\""));
                    writer.WriteLine(line);
                }
            }
        }




    }
}
