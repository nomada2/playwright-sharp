using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaywrightSharp
{
    /// <summary>
    /// It represents an in-page DOM element.
    /// </summary>
    public interface IElementHandle : IJSHandle
    {
        /// <summary>
        /// Focuses the element, and then uses <see cref="IKeyboard.DownAsync(string, DownOptions)"/> and <see cref="IKeyboard.UpAsync(string)"/>.
        /// </summary>
        /// <param name="key">Name of key to press, such as <c>ArrowLeft</c>. See <see cref="KeyDefinitions"/> for a list of all key names.</param>
        /// <param name="options">press options.</param>
        /// <remarks>
        /// If <c>key</c> is a single character and no modifier keys besides <c>Shift</c> are being held down, a <c>keypress</c>/<c>input</c> event will also be generated. The <see cref="DownOptions.Text"/> option can be specified to force an input event to be generated.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the message is confirmed by the browser.</returns>
        Task PressAsync(string key, PressOptions options = null);

        /// <summary>
        /// Focuses the element, and sends a <c>keydown</c>, <c>keypress</c>/<c>input</c>, and <c>keyup</c> event for each character in the text.
        /// </summary>
        /// <param name="text">A text to type into a focused element.</param>
        /// <param name="options">Type options.</param>
        /// <remarks>
        /// To press a special key, like <c>Control</c> or <c>ArrowDown</c> use <see cref="IElementHandle.PressAsync(string, PressOptions)"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// elementHandle.TypeAsync("#mytextarea", "Hello"); // Types instantly
        /// elementHandle.TypeAsync("#mytextarea", "World", new TypeOptions { Delay = 100 }); // Types slower, like a user
        /// </code>
        /// An example of typing into a text field and then submitting the form:
        /// <code>
        /// var elementHandle = await page.GetElementAsync("input");
        /// await elementHandle.TypeAsync("some text");
        /// await elementHandle.PressAsync("Enter");
        /// </code>
        /// </example>
        /// <returns>A <see cref="Task"/> that completes when the message is confirmed by the browser.</returns>
        Task TypeAsync(string text, TypeOptions options = null);

        /// <summary>
        /// Takes a screenshot of the element.
        /// </summary>
        /// <param name="options">Screenshot options.</param>
        /// <returns>
        /// A <see cref="Task"/> that completes when the screenshot is done, yielding the screenshot as a <see cref="T:byte[]" />.
        /// </returns>
        Task<byte[]> ScreenshotAsync(ScreenshotOptions options = null);

        /// <summary>
        /// Focuses the element and triggers an `input` event after filling.
        /// If element is not a text `&lt;input&gt;`, `&lt;textarea&gt;` or `[contenteditable]` element, the method throws an error.
        /// </summary>
        /// <param name="text">Value to set for the `&lt;input&gt;`, `&lt;textarea&gt;` or `[contenteditable]` element.</param>
        /// <returns>A <see cref="Task"/> that completes when the fill action is done.</returns>
        Task FillAsync(string text);

        /// <summary>
        /// Content frame for element handles referencing iframe nodes, or null otherwise.
        /// </summary>
        /// <returns>A <see cref="Task"/> that completes when the frame is resolved, yielding element's parent <see cref="IFrame" />.</returns>
        Task<IFrame> GetContentFrameAsync();

        /// <summary>
        /// Scrolls element into view if needed, and then uses <see cref="IPage.Mouse"/> to hover over the center of the element.
        /// </summary>
        /// <returns>A <see cref="Task"/> that completes when the element is successfully hovered.</returns>
        Task HoverAsync();

        /// <summary>
        /// Tries to scroll element into view, unless it is completely visible as defined by <see href="https://developer.mozilla.org/en-US/docs/Web/API/Intersection_Observer_API"/>'s <b>ratio</b>.
        /// </summary>
        /// <seealso cref="IElementHandle.GetVisibleRatioAsync"/>
        /// <returns>A <see cref="Task"/> that completes when the element is successfully scrolled into view.</returns>
        Task ScrollIntoViewIfNeededAsync();

        /// <summary>
        /// Returns the frame containing the given element.
        /// </summary>
        /// <returns>A <see cref="Task"/> that completes when the frame is resolved, yielding element's owner <see cref="IFrame" />.</returns>
        Task<IFrame> GetOwnerFrameAsync();

        /// <summary>
        /// Gets the bounding box of the element (relative to the main frame), or null if the element is not visible.
        /// </summary>
        /// <returns>A <see cref="Task"/> that completes when the <see cref="BoundingBox"/> is resolved, yielding element's <see cref="BoundingBox"/>.</returns>
        Task<BoundingBox> GetBoundingBoxAsync();

        /// <summary>
        /// Gets the element's visible ratio.
        /// </summary>
        /// <remarks>
        /// Positive ratio means that some part of the element is visible in the current viewport. Ratio equal to one means that element is completely visible.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the visible ratio is retrieved successfully, yielding element's visible ratio.</returns>
        Task<double> GetVisibleRatioAsync();

        /// <summary>
        /// Scrolls element into view if needed, and then uses <see cref="IPage.Mouse"/> to click in the center of the element.
        /// </summary>
        /// <param name="options">click options.</param>
        /// <returns>A <see cref="Task"/> that completes when the element is successfully clicked.</returns>
        Task ClickAsync(ClickOptions options = null);

        /// <summary>
        /// Executes a function in browser context, passing the current <see cref="IElementHandle"/> as the first argument.
        /// </summary>
        /// <param name="script">Script to be evaluated in browser context.</param>
        /// <param name="args">Arguments to pass to script.</param>
        /// <typeparam name="T">Type to parse the result to.</typeparam>
        /// <remarks>
        /// If the script, returns a Promise, then the method would wait for the promise to resolve and return its value.
        /// <see cref="IJSHandle"/> instances can be passed as arguments.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the script is executed, yieling the return value of that script.</returns>
        Task<T> EvaluateAsync<T>(string script, params object[] args);

        /// <summary>
        /// Executes a function in browser context, passing the current <see cref="IElementHandle"/> as the first argument.
        /// </summary>
        /// <param name="script">Script to be evaluated in browser context.</param>
        /// <param name="args">Arguments to pass to script.</param>
        /// <remarks>
        /// If the script, returns a Promise, then the method would wait for the promise to resolve and return its value.
        /// <see cref="IJSHandle"/> instances can be passed as arguments.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the script is executed, yieling the return value of that script.</returns>
        Task<JsonElement?> EvaluateAsync(string script, params object[] args);

        /// <summary>
        /// Sets the value of the file input to these file paths or files. If some of the  <paramref name="filePath"/> are relative paths, then they are resolved relative to the <see cref="Directory.GetCurrentDirectory"/>.
        /// </summary>
        /// <param name="filePath">The file paths or files.</param>
        /// <remarks>
        /// This method expects <see cref="IElementHandle"/> to point to an <see href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input"/>.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the files are successfully set.</returns>
        Task SetInputFilesAsync(params string[] filePath);

        /// <summary>
        /// The method runs <c>document.querySelector</c> within the element. If no element matches the selector, the return value resolve to <c>null</c>.
        /// </summary>
        /// <param name="selector">A selector to query element for.</param>
        /// <returns>
        /// A <see cref="Task"/> that completes when the javascript function finishes, yielding an <see cref="IElementHandle"/>.
        /// </returns>
        Task<IElementHandle> QuerySelectorAsync(string selector);

        /// <summary>
        /// The method runs <c>Array.from(document.querySelectorAll(selector))</c> within the element.
        /// </summary>
        /// <param name="selector">A selector to query element for.</param>
        /// <returns>
        /// A <see cref="Task"/> that completes when the javascript function finishes, yielding an array of <see cref="IElementHandle"/>.
        /// </returns>
        Task<IElementHandle[]> QuerySelectorAllAsync(string selector);

        /// <summary>
        /// This method runs <c>document.querySelector</c> within the page and passes it as the first argument to <paramref name="script"/>.
        /// If there's no element matching selector, the method throws an error.
        /// </summary>
        /// <param name="selector">A selector to query element for.</param>
        /// <param name="script">Script to be evaluated in browser context.</param>
        /// <param name="args">Arguments to pass to script.</param>
        /// <remarks>
        /// If the script, returns a Promise, then the method would wait for the promise to resolve and return its value.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the script finishes or the promise is resolved, yielding the result of the script.</returns>
        Task QuerySelectorEvaluateAsync(string selector, string script, params object[] args);

        /// <summary>
        /// This method runs <c>document.querySelector</c> within the element and passes it as the first argument to <paramref name="script"/>.
        /// If there's no element matching selector, the method throws an error.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="selector">A selector to query element for.</param>
        /// <param name="script">Script to be evaluated in browser context.</param>
        /// <param name="args">Arguments to pass to script.</param>
        /// <remarks>
        /// If the script, returns a Promise, then the method would wait for the promise to resolve and return its value.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the script finishes or the promise is resolved, yielding the result of the script.</returns>
        Task<T> QuerySelectorEvaluateAsync<T>(string selector, string script, params object[] args);

        /// <summary>
        /// This method runs <c>Array.from(document.querySelectorAll(selector))</c> within the page and passes it as the first argument to <paramref name="script"/>.
        /// </summary>
        /// <param name="selector">A selector to query element for.</param>
        /// <param name="script">Script to be evaluated in browser context.</param>
        /// <param name="args">Arguments to pass to script.</param>
        /// <remarks>
        /// If the script, returns a Promise, then the method would wait for the promise to resolve and return its value.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the script finishes or the promise is resolved, yielding the result of the script.</returns>
        Task QuerySelectorAllEvaluateAsync(string selector, string script, params object[] args);

        /// <summary>
        /// This method runs <c>Array.from(document.querySelectorAll(selector))</c> within the element and passes it as the first argument to <paramref name="script"/>.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="selector">A selector to query element for.</param>
        /// <param name="script">Script to be evaluated in browser context.</param>
        /// <param name="args">Arguments to pass to script.</param>
        /// <remarks>
        /// If the script, returns a Promise, then the method would wait for the promise to resolve and return its value.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that completes when the script finishes or the promise is resolved, yielding the result of the script.</returns>
        Task<T> QuerySelectorAllEvaluateAsync<T>(string selector, string script, params object[] args);
    }
}
