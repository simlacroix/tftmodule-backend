// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : UserNotFoundException.cs
//           Exception when Summoner has not been found

namespace tft_module.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message)
    {
        
    }
}