using System.Text;

namespace Bikiran.Utils;

public class C
{
    public static void P(params string?[] values)
    {
        // Foreach with index
        for (int i = 0; i < values.Length; i++)
        {
            var val = values[i];
            Console.WriteLine(" ");
            Console.WriteLine($"++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++[{i}]");
            Console.WriteLine(val);
            Console.WriteLine(" ");
        }
    }

    public static void X(params string?[] values)
    {
        P(values);
        throw new Exception(values.ToJson());
    }

    public static void Print(params string?[] values)
    {
        P(values);
    }

    public static async Task AddLogAsync(string data)
    {
        try
        {
            // Request API Call
            string url = "https://api2.bikiran.win/test/add";

            // Payload
            var payload = new
            {
                Key = "KC6URpHk6GUCMkRbev8Emj3bpgdGARuj",
                Value = data
            };

            using (HttpClient client = new HttpClient())
            {
                // Convert payload to JSON
                string jsonPayload = JsonConvert.SerializeObject(payload);

                // Create content with proper headers
                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Read response
                string responseString = await response.Content.ReadAsStringAsync();

                // Output response
                P($"Response Code: {response.StatusCode}");
                P($"Response Body: {responseString}");
            }
        }
        catch (Exception ex)
        {
            P($"Error: {ex.Message}");
        }
    }
}
