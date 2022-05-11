#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace DTT.Utils.EditorUtilities
{
    /// <summary>
    /// Provides a simple fade group layout implementation for giving your foldouts
    /// a nice foldout animation.
    /// </summary>
    public class AnimatedFoldout
    {
        #region Variables
        #region Public
        /// <summary>
        /// The target for which this foldout element is drawn.
        /// </summary>
        public readonly UnityObject target;
        #endregion
        #region Private
        /// <summary>
        /// The foldout animation.
        /// </summary>
        private readonly AnimBool _animation;

        /// <summary>
        /// Whether to use an indent on the foldout or not.
        /// </summary>
        private bool _indent;
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="editor">The editor to draw the foldout for.</param>
        /// <param name="opened">Whether the foldout is opened or not.</param>
        public AnimatedFoldout(Editor editor, bool opened = false, bool indent = false)
        {
            target = editor;

            _indent = indent;
            _animation = new AnimBool(opened);
            _animation.valueChanged.AddListener(editor.Repaint);
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="window">The editor window to draw the foldout for.</param>
        /// <param name="opened">Whether the foldout is opened or not.</param>
        public AnimatedFoldout(EditorWindow window, bool opened = false, bool indent = false)
        {
            target = window;

            _animation = new AnimBool(opened);
            _animation.valueChanged.AddListener(window.Repaint);
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Draws the foldout with inside it the GUI based
        /// on the given draw action.
        /// </summary>
        /// <param name="headerName">The name of the foldout header.</param>
        /// <param name="drawAction">The method drawing the GUI inside the foldout.</param>
        /// <returns>Whether the foldout is opened or not.</returns>
        public bool OnGUI(string headerName, Action drawAction)
        {
            _animation.target = EditorGUILayout.Foldout(_animation.target, headerName);
            if (EditorGUILayout.BeginFadeGroup(_animation.faded))
            {
                int indentLevel = EditorGUI.indentLevel;
                if (_indent)
                    EditorGUI.indentLevel += 1;

                drawAction?.Invoke();

                EditorGUI.indentLevel = indentLevel;
            }

            EditorGUILayout.EndFadeGroup();

            return _animation.target;
        }

        /// <summary>
        /// Draws the foldout with inside it the GUI based
        /// on the given draw action.
        /// </summary>
        /// <param name="headerName">The name of the foldout header.</param>
        /// <param name="drawAction">The method drawing the GUI inside the foldout.</param>
        /// <returns>Whether the foldout is opened or not.</returns>
        public bool OnGUI(Rect rect, string headerName, Action drawAction)
        {
            _animation.target = EditorGUI.Foldout(rect, _animation.target, headerName);
            if (EditorGUILayout.BeginFadeGroup(_animation.faded))
            {
                int indentLevel = EditorGUI.indentLevel;
                if (_indent)
                    EditorGUI.indentLevel += 1;

                drawAction?.Invoke();

                EditorGUI.indentLevel = indentLevel;
            }

            EditorGUILayout.EndFadeGroup();

            return _animation.target;
        }
        #endregion
        #endregion
    }
}

#endif