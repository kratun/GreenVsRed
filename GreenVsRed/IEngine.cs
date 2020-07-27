namespace GreenVsRed
{
    using System;

    /// <summary>
    /// Provide method to run game.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Method that run game.
        /// </summary>
        /// <exception cref="Exception">Can throw exception.</exception>
        void Run();
    }
}
