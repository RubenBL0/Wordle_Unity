namespace DTT.UI.ProceduralUI
{
    /// <summary>
    /// Should be implemented by objects where errors can occur that are fixable.
    /// </summary>
    public interface IFixableCanvasException
    {
        #region Methods
        /// <summary>
        /// Should resolve a problem related to a problem with the canvas.
        /// </summary>
        void Fix();
        #endregion
    }
}