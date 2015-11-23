namespace Vulpecula.Mobile.Models.Interfaces
{
    public interface IConfiguration
    {
        void SetString(string key, string value);

        string GetString(string key);

        void SetInt(string key, int value);

        int GetInt(string key);

        void SetDouble(string key, double value);

        double GetDouble(string key);

        void SetBool(string key, bool value);

        bool GetBool(string key);

        void SetArray(string key, string[] value);

        string[] GetArray(string key);
    }
}