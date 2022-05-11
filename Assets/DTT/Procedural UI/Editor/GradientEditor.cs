using DTT.PublishingTools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DTT.UI.ProceduralUI.Editor
{
    /// <summary>
    /// The custom inspector for <see cref="GradientEffect"/>.
    /// </summary>
    [CustomEditor(typeof(GradientEffect))]
    [CanEditMultipleObjects]
    [DTTHeader("dtt.proceduralui")]
    public class GradientEditor : DTTInspector
    {
        #region Variables
        #region Private
        /// <summary>
        /// The gradient effect object
        /// </summary>
        private GradientEffect _gradientEffect;

        /// <summary>
        /// The serialized object's for the gradient
        /// </summary>
        private GradientEffectSerializedProperties _serializedProperties;

        /// <summary>
        /// All the sections that inspector should draw.
        /// </summary>
        private readonly List<IDrawable> _sections = new List<IDrawable>();
        #endregion
        #endregion

        #region Unity Methods
        /// <summary>
        /// Initializes the sections.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            serializedObject.Update();

            // Obtain references.
            _gradientEffect = (GradientEffect)target;
            _gradientEffect.UpdateGradient();
            _serializedProperties = new GradientEffectSerializedProperties(serializedObject);

            CreateSections();
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Draws the inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            // Draw all the sections.
            for (int i = 0; i < _sections.Count; i++)
                _sections[i].Draw();

            if (EditorGUI.EndChangeCheck() || 
                (Event.current.type == EventType.ValidateCommand &&
                 Event.current.commandName == "UndoRedoPerformed"))
            {
                serializedObject.ApplyModifiedProperties();
                _gradientEffect.UpdateGradient();
            }
        }

        #endregion

        #region Private
        /// <summary>
        /// Creates the different sections that are in the inspector.
        /// </summary>
        private void CreateSections()
        {
            UnityAction repaintAction = new UnityAction(Repaint);

            // Create the Image Settings section.
            _sections.Add(new GradientSection(_gradientEffect, repaintAction,
                _serializedProperties
                ));
        }
        #endregion
        #endregion
    }
}