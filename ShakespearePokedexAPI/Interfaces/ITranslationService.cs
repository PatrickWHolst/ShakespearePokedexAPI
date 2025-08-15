namespace ShakespearePokedexAPI.Interfaces
{
    /// <summary>
    /// service interface for translating text to Shakespearean English.
    /// </summary>
    public interface ITranslationService
    {
        /// <summary>
        /// Translates the specified text into shakespearean English.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<string> TranslateToShakespeareAsync(string text);
    }
}