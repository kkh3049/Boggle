namespace Boggle
{
    public static class Utilities
    {
        public static string DiceView(this char letter) => letter == 'q' ? "qu" : letter.ToString();
    }
}