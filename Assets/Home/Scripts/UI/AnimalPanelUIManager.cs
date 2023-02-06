using Celeste.Memory;
using Celeste.Tools;
using Rover.Core.Record;
using UnityEngine;

namespace Rover.Home.UI
{
    [CreateAssetMenu(fileName = nameof(AnimalPanelUIManager), menuName = "Rover/Home/UI/Animal Panel UI Manager")]
    public class AnimalPanelUIManager : MonoBehaviour
    {
        #region Properties and Fields

        [Header("Data")]
        [SerializeField] private AnimalRecord animalRecord;

        [Header("UI Elements")]
        [SerializeField] private GameObjectAllocator animalPanelUIControllerAllocator;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            this.TryGet(ref animalPanelUIControllerAllocator);
        }

        private void Start()
        {
            RebuildUI();
        }

        #endregion

        private void RebuildUI()
        {
            animalPanelUIControllerAllocator.DeallocateAll();

            for (int i = 0, n = animalRecord.MaxNumAnimals; i < n; ++i)
            {
                GameObject animalPanelUIGameObject = animalPanelUIControllerAllocator.Allocate();
                AnimalPanelUIController animalPanelUIController = animalPanelUIGameObject.GetComponent<AnimalPanelUIController>();

                if (i < animalRecord.NumCurrentAnimals)
                {
                    animalPanelUIController.Hookup(animalRecord.GetAnimal(i));
                }
                else
                {
                    animalPanelUIController.HookupEmpty();
                }

                animalPanelUIGameObject.SetActive(true);
            }
        }

        #region Callbacks

        public void OnAnimalRecordChanged()
        {
            RebuildUI();
        }

        #endregion
    }
}
