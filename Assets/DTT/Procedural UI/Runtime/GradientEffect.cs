using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTT.UI.ProceduralUI
{
    /// <summary>
    /// Main component for the Gradient Effect.
    /// </summary>
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UI/Gradient Effect")]
    [ExecuteInEditMode]
    public class GradientEffect : MonoBehaviour
    {
        #region InnerClasses
        /// <summary>
        /// The type of the gradient.
        /// </summary>
        public enum GradientType : int
        {
            /// <summary>
            /// Rotational gradient.
            /// </summary>
            ANGULAR = 0,
            /// <summary>
            /// Radial gradient.
            /// </summary>
            RADIAL = 1,
            /// <summary>
            /// Linear gradient.
            /// </summary>
            LINEAR = 2,
            /// <summary>
            /// Reflected gradient.
            /// </summary>
            REFLECTED = 3,
            /// <summary>
            /// Diamond gradient.
            /// </summary>
            DIAMOND = 4
        }
        #endregion

        #region variables
        #region private
        /// <summary>
        /// The type of the gradient.
        /// </summary>
        [SerializeField]
        private GradientType _type;

        /// <summary>
        /// The gradient of the gradient.
        /// </summary>
        [SerializeField]
        private Gradient _gradient;

        /// <summary>
        /// The horizontal and vertical offset of the gradient, local to the gradient.
        /// </summary>
        [SerializeField]
        private Vector2 _offset;

        /// <summary>
        /// The rotation of the gradient in degrees.
        /// </summary>
        [SerializeField]
        private float _rotation;

        /// <summary>
        /// The scale of the gradient, local to the gradient.
        /// </summary>
        [SerializeField]
        private float _scale = 1;

        /// <summary>
        /// Enables / disables batching.
        /// When batching is disabled doing calls to propertys wont work.
        /// </summary>
        [SerializeField]
        private bool _batching = true;

        /// <summary>
        /// The image refrence.
        /// </summary>
        private Image _image;

        /// <summary>
        /// The sprite refrence.
        /// only used when batching is dissabled
        /// </summary>
        private Texture2D _texture;

        /// <summary>
        /// The material Refrence
        /// only used when batching is dissabled
        /// </summary>
        private Material _gradientMaterial;
        
        #endregion
        #region public
        /// <summary>
        /// The type of the gradient.
        /// </summary>
        public GradientType Type {
            get => _type;
            set
            {
                if (_batching) return;
                _type = value;
                UpdateGradient();
            }
        }

        /// <summary>
        /// The gradient to use.
        /// </summary>
        public Gradient Gradient
        {
            get => _gradient;
            set
            {
                if (_batching) return;
                _gradient = value;
                UpdateGradient();
            }
        }

        /// <summary>
        /// The horizontal and vertical offset of the gradient, local to the gradient.
        /// </summary>
        public Vector2 Offset
        {
            get => _offset;
            set
            {
                if (_batching) return;
                _offset = value;
                UpdateGradient();
            }
        }

        /// <summary>
        /// The rotation of the gradient in degrees.
        /// </summary>
        public float Rotation
        {
            get => _rotation;
            set
            {
                if (_batching) return;
                _rotation = value;
                UpdateGradient();
            }
        }

        /// <summary>
        /// The scale of the gradient, local to the gradient.
        /// </summary>
        public float Scale {
            get => _scale;
            set
            {
                if (_batching) return;
                _scale = value;
                UpdateGradient();
            }
        }

        /// <summary>
        /// is batching is enabled or disabled
        /// </summary>
        public bool Batching
        {
            get => _batching;
            set
            {
                _batching = value;
                UpdateGradient();
            }
        }

        #endregion
        #endregion

        #region methods
        #region Unity
        /// <summary>
        /// The start of the monobehavior.
        /// Initilizes the gradient.
        /// </summary>
        public void Start()
        {
            _image = GetComponent<Image>();

            if (Gradient == null)
                Gradient = new Gradient();

            UpdateGradient();
        }

        /// <summary>
        /// Resets the Component to defaults
        /// </summary>
        public void Reset() => UpdateGradient();
        #endregion
        #region public
        /// <summary>
        /// Updates the gradient.
        /// </summary>
        public void UpdateGradient()
        {
            if (_batching)
            {
                if(_image == null) _image = GetComponent<Image>();

                Material material = GradientImageMaterialManager.GetMaterialFromCache(new GradientImageMaterialManager.MaterialData()
                {
                    isForUnityImage = _image.GetType() != typeof(RoundedImage),
                    offset = Offset,
                    rotationScale = new Vector2(Rotation, Scale),
                    type = (int)Type,
                    texture = GradientImageMaterialManager.GetTextureForCache(Gradient)
                });
                _image.material = material;
            }
            else
            {
                if (_image == null) _image = GetComponent<Image>();

                _texture = GradientImageMaterialManager.GetTextureFromParameters(Gradient, _texture);

                _gradientMaterial = GradientImageMaterialManager.GetMaterialFromParameters(new GradientImageMaterialManager.MaterialData()
                {
                    isForUnityImage = _image.GetType() != typeof(RoundedImage),
                    offset = Offset,
                    rotationScale = new Vector2(Rotation, Scale),
                    type = (int)Type,
                    texture = _texture
                }, _gradientMaterial);

                _image.material = _gradientMaterial;
            }
        }
        #endregion
        #endregion
    }
}