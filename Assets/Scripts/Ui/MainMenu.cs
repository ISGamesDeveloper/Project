using TanksEngine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TanksEngine.Data.PlayerTankData;
using static TanksEngine.AngarsSytem.AngarsSystem;

/// <summary>
/// Реализует функционал, связанный 
/// с главным меню.
/// </summary>
namespace TanksEngine.UI
{
    public static class MainMenu
    {
        private static AngarSlot CurrentAngarSlot;
        public static MainMenu_UI mainMenu_UI;
        public static bool SkipMenu = false;
        private static InGameDataBaseSO dataBase;
        /// <summary>
        /// Инициализация, вызов отображения
        /// главного меню.
        /// </summary>
        /// <param name="menu"></param>
        public static void InitMenu(MainMenu_UI menu)
        {
            mainMenu_UI = menu;
            GetGameData();
            dataBase = Resources.Load<InGameDataBaseSO>("/GlobalDB/StdGlobalDataBase");
            if (SkipMenu)
            {
                mainMenu_UI.LoadingScreen.SetActive(false);
                SelectPanel(1);
            }
            else
            {
                mainMenu_UI.LoadingScreen.SetActive(false);
                SelectPanel(0);
            }

            UpdateAngarPanel();
            CheckTankParts();
            
        }
        public static void NextAngarSlot()
        {
            SelectNextAngarSlot();
            UpdateAngarPanel();
        }
        public static void PrevAngarSlot()
        {
            SelectPrevAngarSlot();
            UpdateAngarPanel();
        }
        private static void GetGameData()
        {
            CurrentAngarSlot = GetCurrentAngarSlot();
        }
        /// <summary>
        /// Обновляет панель ангара.
        /// Закрывает все панельки
        /// </summary>
        public static void UpdateAngarPanel()
        {
            UpdateCurrentDetailsPanel();
            int idx = GameData.CurSlot + 1;
            mainMenu_UI.AngarSlotText.text = "Ангар: "+idx.ToString();

            if (GameData.AngarSlots[GameData.CurSlot].slotType == AngarSlotType.Empty)
            {
                //Debug.Log("Empty slot!");
                MenuTankDisplayer.ClearTankViusal(mainMenu_UI.playerModel);
                mainMenu_UI.ShowCurrentDetailsButton.SetActive(false);
                mainMenu_UI.CommanderButton.SetActive(false);
            }
            else
            {
                MenuTankDisplayer.UpdateTankVisual(GameData.CurSlot, mainMenu_UI.playerModel);
                mainMenu_UI.ShowCurrentDetailsButton.SetActive(true);
                mainMenu_UI.CommanderButton.SetActive(true);
            }
            CheckTankParts();
        }
        /// <summary>
        /// Обновляет панель с теущими деталями.
        /// </summary>
        /// <param name="typeDetail"></param>
        private static void UpdateCurrentDetailsPanel(DBElementType DBdetailType = DBElementType.Main)
        {
            ClearPlayerDetailsPanel();

            CurrentAngarSlot.Details_Main = GameData.AngarSlots[GameData.CurSlot].detailMainList;
            CurrentAngarSlot.Commanders = GameData.AngarSlots[GameData.CurSlot].commanderList;

            int detailsCount = 4;
            Tank_Detail detail = new();
            string detailType = "";
            //Обновляет инфу на кнопках текущих деталей
            DBElementType _DBdetailType = DBElementType.Main;
            for (int i = 0; i < detailsCount; i++)
            {
                switch (i)
                {
                    case 0:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailMain.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailMain;
                            detailType = "Основа";
                            _DBdetailType = DBElementType.Main;
                        }
                        break;
                    case 1:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailEngine.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailEngine;
                            detailType = "Движок";
                            _DBdetailType = DBElementType.Engine;
                        }
                        break;
                    case 2:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailSuspension.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailSuspension;
                            detailType = "Подвеска";
                            _DBdetailType = DBElementType.Suspension;
                        }
                        break;
                    case 3:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailTurret.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailTurret;
                            detailType = "Пушка";
                            _DBdetailType = DBElementType.Turret;
                        }
                        break;
                    default:
                        break;
                }
                if (detail.Name != null)
                {
                    mainMenu_UI.current_details[i].text.text = $"{detailType}: {detail.Name}";
                    mainMenu_UI.current_details[i].image.sprite = DBManager.FindSprite(detail.Icon, _DBdetailType);
                    MenuTankDisplayer.UpdateTankVisual(GameData.CurSlot, mainMenu_UI.playerModel);
                }
                else
                {
                    mainMenu_UI.current_details[i].text.text = $"Деталь {detailType.ToLower()}";
                }
            }

            //Обновление информации о выбранном командире
            if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Icon != null)
            {
                mainMenu_UI.myCommanderNameText.GetComponent<Text>().text = $"{GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Name}";
                mainMenu_UI.myCommanderImage.GetComponent<Image>().sprite =   DBManager.FindSprite(GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Icon, DBElementType.Commander);
            }
            else
            {
                mainMenu_UI.myCommanderNameText.GetComponent<Text>().text = "Выберите командира.";
                mainMenu_UI.myCommanderImage.GetComponent<Image>().sprite = mainMenu_UI.ClearImage;
            }
            //Обновление панели со списком деталей для выбора
            UpdatePlayerDetails(DBdetailType);
        }
        /// <summary>
        /// Очищает панельку с 
        /// деталями для выбора.
        /// </summary>
        public static void ClearPlayerDetailsPanel()
        {
            foreach (Transform child in mainMenu_UI.DetailsPanel.transform) //Очистка панели со списком деталей
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        /// <summary>
        /// Показывает панель с деталями для выбора.
        /// </summary>
        public static void ShowPlayerDetailsPanel()
        {
            if (mainMenu_UI.PlayerDetailsPanel.activeSelf)
            {
                mainMenu_UI.CurrentDetailsPanel.SetActive(false);
                mainMenu_UI.PlayerDetailsPanel.SetActive(false);
            }
            else
            {
                mainMenu_UI.CurrentDetailsPanel.SetActive(true);
                mainMenu_UI.PlayerDetailsPanel.SetActive(true);
                UpdateCurrentDetailsPanel(DBElementType.Main);
            }
        }
        /// <summary>
        /// Меняет текущую деталь на выбранную.
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="detailType"></param>
        public static void SelectDetail(Tank_Detail detail, DBElementType DBdetailType)
        {
            Debug.Log("Click" + detail.Name + " " + DBdetailType);
            PlayerTankData newData = GameData.AngarSlots[GameData.CurSlot];
            switch (DBdetailType)
            {
                case DBElementType.Main:
                    newData.tankDataStruct.detailMain = detail;
                    break;
                case DBElementType.Engine:
                    newData.tankDataStruct.detailEngine = detail;
                    break;
                case DBElementType.Suspension:
                    newData.tankDataStruct.detailSuspension = detail;
                    break;
                case DBElementType.Turret:
                    newData.tankDataStruct.detailTurret = detail;
                    break;
                default:
                    break;
            }
            GameData.AngarSlots[GameData.CurSlot] = newData;
            CheckTankParts();
            UpdateCurrentDetailsPanel(DBdetailType);
        }
        /// <summary>
        /// Создает новую кнопку для выбора детали.
        /// </summary>
        /// <param name="item">Структура с инфой о детали</param>
        /// <param name="name">Название этой детали</param>
        /// <param name="type">Тип детали.</param>
        private static void SpawnButtonDetail(Tank_Detail item, string name, DBElementType DBdetailType)
        {
            Debug.Log("Spawn " + name + "/" + DBdetailType);
            mainMenu_UI.panel_button_obj = GameObject.Instantiate(mainMenu_UI.detailButton);
            mainMenu_UI.panel_button_obj.transform.SetParent(mainMenu_UI.DetailsPanel.transform);
            mainMenu_UI.panel_button_obj.transform.localScale = new Vector3(1, 1, 1);

            mainMenu_UI.panel_button_obj.transform.GetChild(0).GetComponent<Text>().text = $"{name}: {item.Name}";
            mainMenu_UI.panel_button_obj.transform.GetChild(1).GetComponent<Image>().sprite =  DBManager.FindSprite(item.Icon, DBdetailType);
            mainMenu_UI.panel_button_obj.GetComponent<Button>().onClick.AddListener(() => SelectDetail(item, DBdetailType));
        }
        private static void ShowDetailsMain()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in CurrentAngarSlot.Details_Main)
            {
                SpawnButtonDetail(item, "Основа", 0);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailMain.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        private static void ShowDetailsEngine()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in CurrentAngarSlot.Details_Engine)
            {
                SpawnButtonDetail(item, "Двигатель", DBElementType.Engine);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailEngine.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        private static void ShowDetailsSuspension()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in CurrentAngarSlot.Details_Suspension)
            {
                SpawnButtonDetail(item, "Подвеска", DBElementType.Suspension);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailSuspension.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        private static void ShowDetailsTurret()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in CurrentAngarSlot.Details_Turret)
            {
                SpawnButtonDetail(item, "Пушка", DBElementType.Turret);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailTurret.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        /// <summary>
        /// Реалзиует переключение между
        /// панелями главного меню.
        /// </summary>
        /// <param name="id"></param>
        public static void SelectPanel(int id)
        {
            foreach (GameObject item in mainMenu_UI.MenuPanels)
            {
                item.SetActive(false);
            }

            switch (id)
            {
                case 0://Главное меню
                    mainMenu_UI.MenuPanels[id].SetActive(true);
                    break;
                case 1://Ангар
                    mainMenu_UI.MenuPanels[id].SetActive(true);
                    UpdateAngarPanel();
                    break;
                case 2://Настройки
                    mainMenu_UI.MenuPanels[id].SetActive(true);
                    break;
            }
        }
        /// <summary>
        /// Активирует кнопку "В бой" если
        /// все компоненты танка установлены.
        /// </summary>
        private static void CheckTankParts()
        {
            
            var data = GameData.AngarSlots[GameData.CurSlot].tankDataStruct;
            if (data.detailSuspension.Name != null && data.detailMain.Name != null && data.detailEngine.Name != null && data.detailTurret.Name != null && data.commander.Name != null)
            {
                foreach (var item in mainMenu_UI.startButtons)
                {
                    item.interactable = true;
                }
            }
            else
            {
                foreach (var item in mainMenu_UI.startButtons)
                {
                    item.interactable = false;
                }
            }
            
        }
        public static void UpdatePlayerDetails(DBElementType DBdetailType)
        {
            switch (DBdetailType)
            {
                case DBElementType.Main :
                    ShowDetailsMain();
                    break;
                case DBElementType.Engine:
                    ShowDetailsEngine();
                    break;
                case DBElementType.Suspension:
                    ShowDetailsSuspension();
                    break;
                case DBElementType.Turret:
                    ShowDetailsTurret();
                    break;
                case DBElementType.Commander:
                    ShowCommanders();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Выводит список доступных командиров
        /// </summary>
        public static void ShowCommanders()
        {
            ClearPlayerDetailsPanel();

            mainMenu_UI.CurrentDetailsPanel.SetActive(false);
            mainMenu_UI.PlayerDetailsPanel.SetActive(true);

            //Обновление панели со списком деталей
            foreach (Commander item in CurrentAngarSlot.Commanders) //спавн кнопок для выбора танков
            {
                //Debug.Log(item);
                mainMenu_UI.panel_button_obj = GameObject.Instantiate(mainMenu_UI.detailButton);
                mainMenu_UI.panel_button_obj.transform.SetParent(mainMenu_UI.DetailsPanel.transform);
                mainMenu_UI.panel_button_obj.transform.localScale = new Vector3(1, 1, 1);

                mainMenu_UI.panel_button_obj.transform.GetChild(0).GetComponent<Text>().text = $"{item.Name}";
                mainMenu_UI.panel_button_obj.transform.GetChild(1).GetComponent<Image>().sprite =  DBManager.FindSprite(item.Icon, DBElementType.Commander);
                mainMenu_UI.panel_button_obj.GetComponent<Button>().onClick.AddListener(() => SelectCommander(item));

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        /// <summary>
        /// Заменяет текущего командира
        /// на выбранного.
        /// </summary>
        /// <param name="commander"></param>
        private static void SelectCommander(Commander commander)
        {
            PlayerTankData newData = GameData.AngarSlots[GameData.CurSlot];
            newData.tankDataStruct.commander = commander;
            GameData.AngarSlots[GameData.CurSlot] = newData;
            CheckTankParts();
            UpdateCurrentDetailsPanel(DBElementType.Commander);
            //mainMenu_UI.PlayerDetailsPanel.SetActive(false);
        }
    }

}
