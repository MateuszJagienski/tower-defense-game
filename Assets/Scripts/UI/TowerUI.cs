using Assets.Scripts.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Canvas))]
    public class TowerUI : MonoBehaviour
    {
        public Text TowerName;
        public Text Description;
        // add upgrade desc on hover later
        public Text UpgradeDescription;
        public Button SellButton;
        public Button UpgradeButton;
        [SerializeField] private GameUI gameUi;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject rangeGhost;

        private Tower tower;
        private TowerController towerController;

        // Start is called before the first frame update
        void Start()
        {
            gameUi.SelectionChanged += OnSelectionChanged;
            canvas.enabled = false;
            rangeGhost = Instantiate(rangeGhost);
        }

        void OnSelectionChanged(Tower tower)
        {
            if (tower != null)
            {
                this.tower = tower;
                this.towerController = tower.GetComponent<TowerController>();
                Show(tower);
            }
            else
            {
                Hide();
            }
        }

        public void Show(Tower tower)
        {
            canvas.enabled = true;
            TowerName.text = tower.TowerName;
            rangeGhost.SetActive(true);
            rangeGhost.transform.position = tower.transform.position;
            rangeGhost.transform.localScale = new Vector3(tower.Range * 2, 0.1f, tower.Range * 2);

        }

        public void Hide()
        {
            canvas.enabled = false;
            rangeGhost.SetActive(false);

        }

        public void SellButtonClicked()
        {
            towerController.SellTower();
            Hide();
        }

        public void UpgradeButtonClicked()
        {
            towerController.UpgradeTower();
            var range = tower.GetComponent<Tower>().Range * 2;
            rangeGhost.transform.localScale = new Vector3(range, 0.1f, range);
        }

        public void First()
        {
            towerController.SetAttackType(AttackType.First);
        }

        public void Last()
        {
            towerController.SetAttackType(AttackType.Last);
        }

        public void Strong()
        {
            towerController.SetAttackType(AttackType.Strong);
        }

        public void Close()
        {
            towerController.SetAttackType(AttackType.Close);
        }
    }
}
