namespace PlaywrightSharp
{
    /// <summary>
    /// Navigation options used by <see cref="IPage.WaitForNavigationAsync(WaitForNavigationOptions)"/> and <see cref="IPage.SetContentAsync(string, NavigationOptions)"/>.
    /// </summary>
    public class NavigationOptions
    {
        /// <summary>
        /// Maximum navigation time in milliseconds, defaults to 30 seconds, pass <c>0</c> to disable timeout.
        /// </summary>
        /// <remarks>
        /// The default value can be changed by setting the <see cref="IPage.DefaultNavigationTimeout"/> or <see cref="IPage.DefaultTimeout"/> property.
        /// </remarks>
        public int? Timeout { get; set; }

        /// <summary>
        /// When to consider navigation succeeded, defaults to <see cref="WaitUntilNavigation.Load"/>. Given an array of <see cref="WaitUntilNavigation"/>, navigation is considered to be successful after all events have been fired.
        /// </summary>
        public WaitUntilNavigation[] WaitUntil { get; set; }
    }
}
