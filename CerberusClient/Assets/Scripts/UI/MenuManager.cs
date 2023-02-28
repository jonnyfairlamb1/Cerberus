using NovaCoreNetworking.Utils;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager instance;

    [Header("Login Menu Items")]
    public Button _loginButton;

    public TMP_Text _loginText;
    public Color _errorTextColor;
    public Color _defaultTextColor;

    [Header("Menu Panels")]
    public GameObject _loginMenu;

    public GameObject _JoinGameMenu;
    public GameObject _MainMenuPanel;
    public GameObject _ShopPanel;
    public GameObject _OptionsPanel;
    public GameObject _LoadoutsPanel;
    public GameObject _CharacterManagementPanel;

    public GameObject _LoadingScreenPanel;

    [Header("MenuComponents")]
    public TMP_Text _player_Currency;

    public TMP_Text _player_Username;
    public RawImage _steam_Icon;

    [Header("Player Variables")]
    public string _availableCurrency = string.Empty;

    public string _battlePassLevel = string.Empty;

    private void Awake() {
        instance = this;
        _loginMenu.SetActive(true);
        _MainMenuPanel.SetActive(false);
        _OptionsPanel.SetActive(false);
        _ShopPanel.SetActive(false);
        _JoinGameMenu.SetActive(false);
        _LoadoutsPanel.SetActive(false);
        _CharacterManagementPanel.SetActive(false);
        _LoadingScreenPanel.SetActive(false);
    }

    public void LoginMenuLoginButton() {
        _loginButton.interactable = false;
        if (GameManager.instance.RunWithoutLoginServer) {
            Debug.LogError("Starting client in [Run Without Login Server] enabled");
            GoToMainMenu();
            UpdatePlayerSteamData();
        } else {
            if (string.IsNullOrEmpty(GameManager.instance._localPlayerData.steamID)) {
                _loginButton.interactable = true;
                Debug.LogError("Steam not initialized please check to see if it is running");
                return;
            }

            NetworkManager.instance.Connect();
        }
    }

    public void ExitGameButton() {
        Application.Quit();
    }

    public void GoToMainMenu() {
        _MainMenuPanel.SetActive(true);
        _loginMenu.SetActive(false);
        _OptionsPanel.SetActive(false);
        _ShopPanel.SetActive(false);
        _JoinGameMenu.SetActive(false);
        _LoadoutsPanel.SetActive(false);
        _CharacterManagementPanel.SetActive(false);
    }

    public void OpenShopButton() {
        _ShopPanel.SetActive(true);
        _OptionsPanel.SetActive(false);
        _JoinGameMenu.SetActive(false);
        _LoadoutsPanel.SetActive(false);
        _CharacterManagementPanel.SetActive(false);
    }

    public void OpenOptionsButton() {
        _OptionsPanel.SetActive(true);
        _ShopPanel.SetActive(false);
        _JoinGameMenu.SetActive(false);
        _LoadoutsPanel.SetActive(false);
        _CharacterManagementPanel.SetActive(false);
    }

    public void PanelBackButton() {
        _MainMenuPanel.SetActive(true);
        _OptionsPanel.SetActive(false);
        _ShopPanel.SetActive(false);
        _JoinGameMenu.SetActive(false);
        _LoadoutsPanel.SetActive(false);
        _CharacterManagementPanel.SetActive(false);
    }

    public void OpenLoadoutsButton() {
        _OptionsPanel.SetActive(false);
        _ShopPanel.SetActive(false);
        _JoinGameMenu.SetActive(false);
        _LoadoutsPanel.SetActive(true);
        _CharacterManagementPanel.SetActive(false);
    }

    public void OpenCharacterManagementButton() {
        _OptionsPanel.SetActive(false);
        _ShopPanel.SetActive(false);
        _JoinGameMenu.SetActive(false);
        _LoadoutsPanel.SetActive(false);
        _CharacterManagementPanel.SetActive(true);
    }

    public void UpdatePlayerSteamData() {
        _player_Currency.text = _availableCurrency;
        _player_Username.text = GameManager.instance._localPlayerData.steamName;
        _steam_Icon.texture = GetSteamImageAsTexture2D(SteamFriends.GetLargeFriendAvatar(new CSteamID(ulong.Parse(GameManager.instance._localPlayerData.steamID))));
    }

    public void JoinGameButton() {
        NovaCoreLogger.Log(NovaCoreNetworking.Utils.LogType.Debug, "Sending join game request");
        NetworkSend.SendJoinGameRequest();
    }

    public void StartLoadingScreen(string mapName, string gamemodeName, string modeAbbreviation) {
        _LoadingScreenPanel.SetActive(true);
        var loadingScreen = _LoadingScreenPanel.GetComponent<LoadingScreen>();
        loadingScreen.StartLoadingScreen(mapName, gamemodeName, modeAbbreviation);
    }

    public static Texture2D GetSteamImageAsTexture2D(int iImage) {
        Texture2D ret = null;
        uint ImageWidth;
        uint ImageHeight;
        bool bIsValid = SteamUtils.GetImageSize(iImage, out ImageWidth, out ImageHeight);

        if (bIsValid) {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];

            bIsValid = SteamUtils.GetImageRGBA(iImage, Image, (int)(ImageWidth * ImageHeight * 4));
            if (bIsValid) {
                ret = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
                ret.LoadRawTextureData(Image);
                ret.Apply();
            }
        }

        return ret;
    }

    public void ShowErrorMessage(string errorMessage) {
        _loginText.color = _errorTextColor;
        _loginText.text = errorMessage;
    }
}