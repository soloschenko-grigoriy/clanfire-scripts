using System;
using GS.Hex;
using GS.UI;
using UnityEngine;

namespace GS.Structures
{
    public class Structure : HexCellObject
    {
        [SerializeField] private StructureSO config;
        [SerializeField] private ConstructionProgressUI constructionProgressUI;
        [SerializeField] private GameObject model;
        [SerializeField] private GameObject modelConstructionEarly;
        [SerializeField] private GameObject modelConstructionLate;
        
        private bool _isConstructing;
        private float _constructionElapsed;
        private bool _underConstructionPopupIsOpen;
        private HexCell _cell;
        private UnderConstructionPopup _underConstructionPopup;
        private StructureMenuPopup _structureMenuPopup;

        private void Awake()
        {
            // TODO find more performant way to locate the popup
            _underConstructionPopup = FindObjectOfType<UnderConstructionPopup>();
            _structureMenuPopup = FindObjectOfType<StructureMenuPopup>();
        }

        private void Update()
        {
            if (_isConstructing)
            {
                ProgressConstruction();
            }
        }

        public void Build(HexCell cell, bool immediateBuild)
        {
            _cell = cell;
            if (!immediateBuild)
            {
                model.SetActive(false);
                modelConstructionEarly.SetActive(true);
                _isConstructing = true;
            }
        }
        
        public void OnTouch()
        {
            if (_isConstructing && !_underConstructionPopupIsOpen)
            {
                _underConstructionPopupIsOpen = true;
                _underConstructionPopup.Show("Town Hall", _cell.RemoveContents, () => _underConstructionPopupIsOpen = false);
                
                return;
            }
            
            _structureMenuPopup.Show(config.Title);
        }
        

        private void ProgressConstruction()
        {
            var delta = _constructionElapsed / config.ConstructionTime;
            if (delta >= 1)
            {
                _isConstructing = false;
                constructionProgressUI.gameObject.SetActive(false);
                
                // audioManager.StopAll();
                modelConstructionLate.SetActive(false);
                model.SetActive(true);
                
                return;
            } 
            
            if (delta >= 0.5)
            {
                modelConstructionEarly.SetActive(false);
                modelConstructionLate.SetActive(true);
                constructionProgressUI.MoveUp();
            }

            constructionProgressUI.SetValue(delta);
            _constructionElapsed += Time.deltaTime;

            if (_underConstructionPopupIsOpen)
            {
                _underConstructionPopup.SetValue(delta);
            }
        }
    }
}
