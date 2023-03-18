using Newtonsoft.Json.Linq;
using System.Text;

namespace ConsoleApp1;

public class JsonApp
{
    private string json = @"{
    'Customer': {
        'FirstName': 'Amy',
        'LastName': 'HomeOwnder'
    },
    'Address': {
        'Street1': '1234 Main Street',
        'City': 'Dallas',
        'State': 'TX',
        'Zip': '77555'
    },
    'Employer': 'Boeing, Inc',
    'Postition': 'Flight Instructor'
}";

    public string GetJsonValue(JObject jObj, string path)
    {
        try
        {
            if (path.Contains('|'))
            {
                StringBuilder sb = new StringBuilder();
                string[] items = path.Split('|');
                foreach (string item in items)
                {
                    sb.Append($"{GetJsonValue(jObj, item)} ");
                }
                return sb.ToString().Trim();
            }

            var result = jObj.SelectToken(path, true).ToString();
            return result;
        }
        catch (Exception ex)
        {
            return ($"Error: {ex.Message}");
        }
    }

    public void Run()
    {
        // mimic configuration settings from database
        string config1 = "Customer";
        string config2 = "Customer.FirstName";
        string config3 = "Address.State";
        string config4 = "Employer";
        string config5 = "Address.Street1|Address.City|Address.State|Address.Zip";

        // create JObject
        var jo = JObject.Parse(json);

        // query the json based on config settings
        var customer = jo[config1];
        var firstname = GetJsonValue(jo, config2);
        var state = GetJsonValue(jo, config3);
        var employer = GetJsonValue(jo, config4);
        var address = GetJsonValue(jo, config5);
    }
}