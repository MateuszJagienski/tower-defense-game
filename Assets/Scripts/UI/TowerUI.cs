using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class TowerUI : MonoBehaviour
{
    public Text towerName;
    public Text description;
    // add upgrade desc on hover later
    public Text upgradeDescription;
    public Button sellButton;
    public Button upgradeButton;
    [SerializeField]
    private GameUI gameUI;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject rangeGhost;

    private Tower tower;
    private TowerController towerController;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("inside Start, TowerUI");
        gameUI.selectionChanged += OnSelectionChanged;
     //   GameUI.Instance.selectionChanged += OnSelectionChanged;
     //   canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        rangeGhost = Instantiate(rangeGhost);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        towerName.text = tower.TowerName;
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
        towerController.SellTower(tower);
        Hide();
    }

    public void UpgradeButtonClicked()
    {
        towerController.UpgradeTower(tower);
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
