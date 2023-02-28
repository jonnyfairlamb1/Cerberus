using System.Text.Encodings.Web;
using System.Text.Json;

namespace CerberusLoginServer.General {

    public static class Helpers {

        public static string SerializeObject(Object obj) {
            JsonSerializerOptions options = new() {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(obj, options);
        }

        public static Object DeserializeObject(string json, Type returnType) {
            return JsonSerializer.Deserialize(json, returnType);
        }

        public static void SaveJson(string jsonString, string fileName) {
            File.WriteAllText(@"C:\Users\Jonny\Documents\UnityProjects\ProjectPheonix\Pheonix\TestJsonData\" + fileName + ".json", jsonString);
        }
    }
}