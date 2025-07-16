namespace ic_ASPnet.Utils
{
    public class FakeReader
    {
        public static string GetCardUid(string manualInput)
        {
            return manualInput.Trim().ToUpper();
        }
    }
}
