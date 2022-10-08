// See https://aka.ms/new-console-template for more information


using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BirdMessenger;
using BirdMessenger.Collections;
using BirdMessenger.Infrastructure;
using BunnyCDN.Net.Storage;
using Microsoft.Extensions.DependencyInjection;

// var bunnyCDNStorage = new BunnyCDNStorage("smartstudy", "d43880ae-6577-4ead-8dda30522db1-7887-46e0", "de");
// await bunnyCDNStorage.UploadAsync("sample_video.mp4", $"/smartstudy/{Guid.NewGuid().ToString("N")}.mp4");

// var results = bunnyCDNStorage.GetStorageObjectsAsync("/smartstudy/");

//var isUploaded = UploadVideoAsync("sample_video.mp4", "c92bcee6-28f8-4598-bdc6a4a9141b-c57c-48d0", "64975", Guid.NewGuid().ToString("N"));

var baseUrl = "https://video.bunnycdn.com/library/";
var client = new HttpClient();
client.DefaultRequestHeaders.Add("AccessKey", "c92bcee6-28f8-4598-bdc6a4a9141b-c57c-48d0");
var data = new StringContent("{\"title\":\"" + System.Web.HttpUtility.JavaScriptStringEncode(Guid.NewGuid().ToString("N")) + "\"}", Encoding.UTF8, "application/json");
var reqCreate = await client.PostAsync(baseUrl + 64975 + "/videos", data);
var reqCreateResponse = JsonSerializer.Deserialize<Dictionary<String, Object>>(await reqCreate.Content.ReadAsStringAsync());
if (reqCreate.StatusCode == HttpStatusCode.OK)
{
    using (var stream = File.OpenRead("sample_video.mp4"))
    {
        var reqUpload = await client.PutAsync(baseUrl + "64975" + "/videos/" + reqCreateResponse["guid"], new StreamContent(stream));
        if (reqUpload.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine(reqUpload.Content.ToString());
            Console.WriteLine(reqUpload.StatusCode);
        }
    }
}
Console.WriteLine("Hello, World!");


/// <summary>Uploads video file to Bunny.net Stream.
/// </summary>
/// <exception cref="FileNotFoundException"></exception>
// static async Task<bool> UploadVideoAsync(String videoPath, String authKey, String libraryId, String videoName)
// {

//     return false;
// }
