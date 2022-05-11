using DTT.UI.ProceduralUI.Exceptions;
using UnityEditor;
using UnityEngine;

namespace DTT.UI.ProceduralUI.Editor
{
    /// <summary>
    /// Draws a section in the <see cref="RoundedImage"/> inspector if there are errors present.
    /// </summary>
    public class ErrorHandlingSection : IDrawable
    {
        #region Variables
        #region Private
        /// <summary>
        /// The info message shown when a fixable error has occured.
        /// </summary>
        private const string FIX_INFO_MESSAGE = "To start using the rounded image component the canvas settings " +
            "need to be updated.";

        /// <summary>
        /// Reference to the error handler, that checks for the errors.
        /// </summary>
        private RoundedImageErrorHandler _errorHandler;
        #endregion
        #endregion

        #region Initialization
        /// <summary>
        /// Creates a new error handling section.
        /// </summary>
        /// <param name="errorHandler">
        /// Reference to the error handler, that checks for the errors.
        /// </param>
        public ErrorHandlingSection(RoundedImageErrorHandler errorHandler) => this._errorHandler = errorHandler;
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Draws the error handling section 
        /// if <see cref="RoundedImageErrorHandler.CheckForErrors"/> throws an exception.
        /// </summary>
        public void Draw()
        {
            try
            {
                _errorHandler.CheckForErrors();
            }
            catch (ProceduralUIException exception)
            {
                if (exception is IFixableCanvasException)
                {
                    EditorGUILayout.HelpBox(FIX_INFO_MESSAGE, MessageType.Info);

                    // If the exception implements the IFixible interface 
                    // draw a button to provide the user with the option
                    // to fix all errors.
                    if (GUILayout.Button("Update Canvas Settings"))
                        _errorHandler.FixFixableErrors();
                }
                else
                {
                    EditorGUILayout.HelpBox(exception.ErrorMessage, MessageType.Error);
                }
            }
        }
        #endregion
        #endregion
    }
}