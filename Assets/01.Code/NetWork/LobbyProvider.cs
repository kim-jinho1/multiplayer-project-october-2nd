using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PurrLobby;
using PurrNet.Logging;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Code.NetWork
{
    public enum LobbyType
    {
        Private,
        FriendsOnly,
        Public,
    }

    public class LobbyProvider : MonoBehaviour, ILobbyProvider
    {
        public LobbyType lobbyType = LobbyType.Public;
        public int maxLobbiesToFind = 10;

        [SerializeField] private bool handleSteamInit;

        private CSteamID _currentLobby = CSteamID.Nil;


        private CallResult<LobbyCreated_t> _lobbyCreated;
        private CallResult<LobbyEnter_t> _lobbyEnter;
        private CallResult<LobbyMatchList_t> _lobbyMatchList;

        private bool IsSteamClientAvailable
        {
            get
            {
                try
                {
                    InteropHelp.TestIfAvailableClient();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public event UnityAction<string> OnLobbyJoinFailed;
        public event UnityAction OnLobbyLeft;
        public event UnityAction<Lobby> OnLobbyUpdated;
        public event UnityAction<List<LobbyUser>> OnLobbyPlayerListUpdated;
        public event UnityAction<List<FriendUser>> OnFriendListPulled;
        public event UnityAction<string> OnError;

        public async Task<Lobby> CreateLobbyAsync(int maxPlayers, Dictionary<string, string> lobbyProperties = null)
        {
            if (!IsSteamClientAvailable)
                return default;

            _lobbyCreated ??= CallResult<LobbyCreated_t>.Create();

            var tcs = new TaskCompletionSource<bool>();
            CSteamID lobbyId = CSteamID.Nil;
            var lobbyName = $"{SteamFriends.GetPersonaName()}의 방";

            var handle = SteamMatchmaking.CreateLobby((ELobbyType)lobbyType, maxPlayers);
            _lobbyCreated.Set(handle, (result, ioError) =>
            {
                if (!ioError && result.m_eResult == EResult.k_EResultOK)
                {
                    lobbyId = new CSteamID(result.m_ulSteamIDLobby);
                    tcs.TrySetResult(true);
                    SteamMatchmaking.SetLobbyData(lobbyId, "Name", lobbyName);
                    SteamMatchmaking.SetLobbyData(lobbyId, "Started", "False");
                }
                else
                    tcs.TrySetResult(false);
            });

            if (!await tcs.Task)
                return new Lobby { IsValid = false };

            _currentLobby = lobbyId;

            if (lobbyProperties != null)
            {
                foreach (var prop in lobbyProperties)
                {
                    SteamMatchmaking.SetLobbyData(lobbyId, prop.Key, prop.Value);
                }
            }

            return LobbyFactory.Create(
                lobbyName,
                lobbyId.m_SteamID.ToString(),
                maxPlayers,
                true,
                GetLobbyUsers(lobbyId),
                lobbyProperties
            );
        }

        public Task<List<FriendUser>> GetFriendsAsync(LobbyManager.FriendFilter filter)
        {
            if (!IsSteamClientAvailable)
                return default;

            var friends = new List<FriendUser>();
            int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);

            for (int i = 0; i < friendCount; i++)
            {
                var steamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
                bool shouldAdd = filter switch
                {
                    LobbyManager.FriendFilter.InThisGame =>
                        SteamFriends.GetFriendGamePlayed(steamID, out var gameInfo) &&
                        gameInfo.m_gameID.AppID() == SteamUtils.GetAppID(),
                    LobbyManager.FriendFilter.Online => SteamFriends.GetFriendPersonaState(steamID) ==
                                                        EPersonaState.k_EPersonaStateOnline,
                    LobbyManager.FriendFilter.All => true,
                    _ => false
                };

                if (shouldAdd)
                    friends.Add(CreateFriendUser(steamID));
            }

            return Task.FromResult(friends);
        }

        public Task<string> GetLobbyDataAsync(string key)
        {
            if (!IsSteamClientAvailable)
                return Task.FromResult(string.Empty);

            return Task.FromResult(SteamMatchmaking.GetLobbyData(_currentLobby, key));
        }

        public Task<List<LobbyUser>> GetLobbyMembersAsync()
        {
            if (!IsSteamClientAvailable)
                return Task.FromResult(new List<LobbyUser>());

            return Task.FromResult(GetLobbyUsers(SteamUser.GetSteamID()));
        }

        public Task<string> GetLocalUserIdAsync()
        {
            if (!IsSteamClientAvailable)
                return Task.FromResult(string.Empty);

            return Task.FromResult(SteamUser.GetSteamID().m_SteamID.ToString());
        }

        public Task InitializeAsync()
        {
            _avatarImageLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
            _lobbyDataUpdateCallback = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdate);
            _lobbyChatUpdateCallback = Callback<LobbyChatUpdate_t>.Create(OnLobbyChatUpdate);
            _gameLobbyJoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);

            if (handleSteamInit)
                HandleSteamInit();

            return Task.CompletedTask;
        }

        public Task InviteFriendAsync(FriendUser user)
        {
            if (IsSteamClientAvailable && !string.IsNullOrEmpty(user.Id) && ulong.TryParse(user.Id, out var id))
            {
                var steamID = new CSteamID(id);
                SteamMatchmaking.InviteUserToLobby(_currentLobby, steamID);
            }

            return Task.FromResult(Task.CompletedTask);
        }

        public async Task<Lobby> JoinLobbyAsync(string lobbyId)
        {
            if (!IsSteamClientAvailable || string.IsNullOrEmpty(lobbyId))
                return default;

            _lobbyEnter ??= CallResult<LobbyEnter_t>.Create();

            var tcs = new TaskCompletionSource<bool>();
            var cLobbyId = new CSteamID(ulong.Parse(lobbyId));
            var handle = SteamMatchmaking.JoinLobby(cLobbyId);

            _lobbyEnter.Set(handle, (result, ioError) =>
            {
                if (result.m_EChatRoomEnterResponse ==
                    (uint)EChatRoomEnterResponse.k_EChatRoomEnterResponseSuccess)
                {
                    _currentLobby = new CSteamID(result.m_ulSteamIDLobby);
                    tcs.TrySetResult(true);
                }
                else
                {
                    tcs.TrySetResult(false);
                }
            });

            if (!await tcs.Task)
            {
                OnLobbyJoinFailed?.Invoke($"Failed to join lobby {lobbyId}.");
                return new Lobby { IsValid = false };
            }

            var lobby = LobbyFactory.Create(
                SteamMatchmaking.GetLobbyData(_currentLobby, "Name"),
                lobbyId,
                SteamMatchmaking.GetLobbyMemberLimit(_currentLobby),
                false,
                GetLobbyUsers(cLobbyId),
                GetLobbyProperties(_currentLobby)
            );

            OnLobbyUpdated?.Invoke(lobby);
            return lobby;
        }

        public Task LeaveLobbyAsync()
        {
            if (!IsSteamClientAvailable || _currentLobby == CSteamID.Nil)
                return Task.CompletedTask;

            SteamMatchmaking.LeaveLobby(_currentLobby);
            _currentLobby = default;
            OnLobbyLeft?.Invoke();
            return Task.CompletedTask;
        }

        public Task LeaveLobbyAsync(string lobbyId)
        {
            if (IsSteamClientAvailable && !string.IsNullOrEmpty(lobbyId) && ulong.TryParse(lobbyId, out var id))
            {
                var cLobbyId = new CSteamID(ulong.Parse(lobbyId));
                SteamMatchmaking.LeaveLobby(cLobbyId);
            }

            return Task.CompletedTask;
        }

        public async Task<List<Lobby>> SearchLobbiesAsync(int maxRoomsToFind = 10,
            Dictionary<string, string> filters = null)
        {
            if (!IsSteamClientAvailable)
                return new List<Lobby>();

            var tcs = new TaskCompletionSource<List<Lobby>>();
            var results = new List<Lobby>();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    SteamMatchmaking.AddRequestLobbyListStringFilter(filter.Key, filter.Value,
                        ELobbyComparison.k_ELobbyComparisonEqual);
                }
            }

            SteamMatchmaking.AddRequestLobbyListStringFilter("Started", "False",
                ELobbyComparison.k_ELobbyComparisonEqual);
            SteamMatchmaking.AddRequestLobbyListResultCountFilter(maxLobbiesToFind);

            _lobbyMatchList ??= CallResult<LobbyMatchList_t>.Create();
            _lobbyMatchList.Set(SteamMatchmaking.RequestLobbyList(), (result, ioError) =>
            {
                int totalLobbies = (int)result.m_nLobbiesMatching;

                for (int i = 0; i < totalLobbies; i++)
                {
                    var lobbyId = SteamMatchmaking.GetLobbyByIndex(i);
                    var lobbyProperties = GetLobbyProperties(lobbyId);
                    int maxPlayers = SteamMatchmaking.GetLobbyMemberLimit(lobbyId);

                    results.Add(new Lobby
                    {
                        Name = SteamMatchmaking.GetLobbyData(lobbyId, "Name"),
                        IsValid = true,
                        LobbyId = lobbyId.m_SteamID.ToString(),
                        MaxPlayers = maxPlayers,
                        Properties = lobbyProperties,
                        Members = GetLobbyUsers(lobbyId)
                    });
                }

                tcs.TrySetResult(results);
            });

            return await tcs.Task;
        }

        public Task SetIsReadyAsync(string userId, bool isReady)
        {
            //You can only set the ready state for your own user
            if (IsSteamClientAvailable && !string.IsNullOrEmpty(userId) && ulong.TryParse(userId, out var id)
                && SteamUser.GetSteamID().m_SteamID == id)
            {
                SteamMatchmaking.SetLobbyMemberData(_currentLobby, "IsReady", isReady.ToString());
                SteamMatchmaking.SetLobbyData(_currentLobby, "UpdateTrigger", DateTime.UtcNow.Ticks.ToString());
            }

            return Task.FromResult(Task.CompletedTask);
        }

        public Task SetLobbyDataAsync(string key, string value)
        {
            if (IsSteamClientAvailable)
                SteamMatchmaking.SetLobbyData(_currentLobby, key, value);

            return Task.FromResult(Task.CompletedTask);
        }

        public Task SetLobbyStartedAsync()
        {
            if (IsSteamClientAvailable)
            {
                SteamMatchmaking.SetLobbyGameServer(_currentLobby, 0, 0, SteamUser.GetSteamID());
                SteamMatchmaking.SetLobbyData(_currentLobby, "Started", "True");
            }

            return Task.FromResult(Task.CompletedTask);
        }

        public void Shutdown()
        {
            //Not needed
        }

        public Task SetAllReadyAsync()
        {
            return Task.FromResult(Task.CompletedTask);
        }

        private void HandleSteamInit()
        {
            if (handleSteamInit)
            {
                if (!SteamAPI.Init())
                {
                    PurrLogger.LogError("SteamAPI initialization failed.");
                    OnError?.Invoke("SteamAPI initialization failed.");
                    return;
                }

                RunSteamCallbacks();
            }
        }

        private async void RunSteamCallbacks()
        {
            var runCallbacks = true;
            while (runCallbacks)
            {
                SteamAPI.RunCallbacks();
                await Task.Delay(16);
            }
        }

        public void SetLobbyStarted(CSteamID serverId)
        {
            if (IsSteamClientAvailable)
            {
                SteamMatchmaking.SetLobbyGameServer(_currentLobby, 0, 0, serverId);
                SteamMatchmaking.SetLobbyData(_currentLobby, "Started", "True");
            }
        }

        public void SetLobbyStarted(string address, short port)
        {
            if (IsSteamClientAvailable)
            {
                var ipAddress = System.Net.IPAddress.Parse(address);
                var ipBytes = ipAddress.GetAddressBytes();
                var ip = (uint)ipBytes[0] << 24;
                ip += (uint)ipBytes[1] << 16;
                ip += (uint)ipBytes[2] << 8;
                ip += (uint)ipBytes[3];
                SteamMatchmaking.SetLobbyGameServer(_currentLobby, ip, (ushort)port, CSteamID.Nil);
                SteamMatchmaking.SetLobbyData(_currentLobby, "Started", "True");
            }
        }

        public void SetLobbyStarted(string address, short port, CSteamID serverId)
        {
            if (IsSteamClientAvailable)
            {
                var ipAddress = System.Net.IPAddress.Parse(address);
                var ipBytes = ipAddress.GetAddressBytes();
                var ip = (uint)ipBytes[0] << 24;
                ip += (uint)ipBytes[1] << 16;
                ip += (uint)ipBytes[2] << 8;
                ip += (uint)ipBytes[3];
                SteamMatchmaking.SetLobbyGameServer(_currentLobby, ip, (ushort)port, CSteamID.Nil);
                SteamMatchmaking.SetLobbyData(_currentLobby, "Started", "True");
            }
        }

        private List<LobbyUser> GetLobbyUsers(CSteamID lobbyId)
        {
            var users = new List<LobbyUser>();
            int memberCount = SteamMatchmaking.GetNumLobbyMembers(lobbyId);

            for (int i = 0; i < memberCount; i++)
            {
                var steamId = SteamMatchmaking.GetLobbyMemberByIndex(lobbyId, i);
                users.Add(CreateLobbyUser(steamId, lobbyId));
            }

            return users;
        }

        private LobbyUser CreateLobbyUser(CSteamID steamId, CSteamID lobbyId)
        {
            _avatarImageLoadedCallback ??=
                Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);

            var displayName = SteamFriends.GetFriendPersonaName(steamId);
            var isReadyString = SteamMatchmaking.GetLobbyMemberData(lobbyId, steamId, "IsReady");
            var isReady = !string.IsNullOrEmpty(isReadyString) && isReadyString == "True";

            var avatarHandle = SteamFriends.GetLargeFriendAvatar(steamId);
            Texture2D avatar = null;

            if (avatarHandle != -1 && SteamUtils.GetImageSize(avatarHandle, out uint width, out uint height))
            {
                byte[] imageBuffer = new byte[width * height * 4];
                if (SteamUtils.GetImageRGBA(avatarHandle, imageBuffer, imageBuffer.Length))
                {
                    avatar = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                    avatar.LoadRawTextureData(imageBuffer);
                    FlipTextureVertically(avatar);
                    avatar.Apply();
                }
            }

            return new LobbyUser
            {
                Id = steamId.m_SteamID.ToString(),
                DisplayName = displayName,
                IsReady = isReady,
                Avatar = avatar
            };
        }

        private void FlipTextureVertically(Texture2D texture)
        {
            var pixels = texture.GetPixels();
            int width = texture.width;
            int height = texture.height;

            for (int y = 0; y < height / 2; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var topPixel = pixels[y * width + x];
                    var bottomPixel = pixels[(height - 1 - y) * width + x];

                    pixels[y * width + x] = bottomPixel;
                    pixels[(height - 1 - y) * width + x] = topPixel;
                }
            }

            texture.SetPixels(pixels);
        }

        private FriendUser CreateFriendUser(CSteamID steamId)
        {
            var displayName = SteamFriends.GetFriendPersonaName(steamId);

            var avatarHandle = SteamFriends.GetLargeFriendAvatar(steamId);
            Texture2D avatar = null;

            if (avatarHandle != -1 && SteamUtils.GetImageSize(avatarHandle, out uint width, out uint height))
            {
                byte[] imageBuffer = new byte[width * height * 4];
                if (SteamUtils.GetImageRGBA(avatarHandle, imageBuffer, imageBuffer.Length))
                {
                    avatar = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                    avatar.LoadRawTextureData(imageBuffer);
                    FlipTextureVertically(avatar);
                    avatar.Apply();
                }
            }

            return new FriendUser()
            {
                Id = steamId.m_SteamID.ToString(),
                DisplayName = displayName,
                Avatar = avatar
            };
        }

        private void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
        {
            var steamId = callback.m_steamID;
            if (callback.m_iImage == -1)
            {
                PurrLogger.LogWarning($"Failed to load avatar for user {steamId}");
                return;
            }

            if (SteamUtils.GetImageSize(callback.m_iImage, out uint width, out uint height))
            {
                byte[] imageBuffer = new byte[width * height * 4];
                if (SteamUtils.GetImageRGBA(callback.m_iImage, imageBuffer, imageBuffer.Length))
                {
                    Texture2D avatar = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                    avatar.LoadRawTextureData(imageBuffer);
                    FlipTextureVertically(avatar);
                    avatar.Apply();

                    UpdateUserAvatar(steamId, avatar);
                }
            }
        }

        private void UpdateUserAvatar(CSteamID steamId, Texture2D avatar)
        {
            if (!_currentLobby.IsValid())
                return;
            var updatedMembers = GetLobbyUsers(_currentLobby);
            if (updatedMembers == null || updatedMembers.Count <= 0)
                return;

            for (int i = 0; i < updatedMembers.Count; i++)
            {
                if (updatedMembers[i].Id == steamId.m_SteamID.ToString())
                {
                    var updatedUser = updatedMembers[i];
                    updatedUser.Avatar = avatar;
                    updatedMembers[i] = updatedUser;
                    break;
                }
            }

            var updatedLobby = new Lobby
            {
                Name = SteamMatchmaking.GetLobbyData(_currentLobby, "Name"),
                IsValid = true,
                LobbyId = _currentLobby.m_SteamID.ToString(),
                MaxPlayers = SteamMatchmaking.GetLobbyMemberLimit(_currentLobby),
                Properties = new Dictionary<string, string>(), // Use existing properties if needed
                Members = updatedMembers
            };

            OnLobbyUpdated?.Invoke(updatedLobby);
        }

        private void OnLobbyDataUpdate(LobbyDataUpdate_t callback)
        {
            if (_currentLobby.m_SteamID != callback.m_ulSteamIDLobby)
                return;

            var ownerId = SteamMatchmaking.GetLobbyOwner(_currentLobby).m_SteamID.ToString();
            var localId = SteamUser.GetSteamID().m_SteamID.ToString();
            var isOwner = localId == ownerId;

            var updatedLobbyUsers = GetLobbyUsers(_currentLobby);
            var updatedLobby = LobbyFactory.Create(
                SteamMatchmaking.GetLobbyData(_currentLobby, "Name"),
                _currentLobby.m_SteamID.ToString(),
                SteamMatchmaking.GetLobbyMemberLimit(_currentLobby),
                isOwner,
                updatedLobbyUsers,
                GetLobbyProperties(_currentLobby)
            );

            OnLobbyUpdated?.Invoke(updatedLobby);
        }

        private Dictionary<string, string> GetLobbyProperties(CSteamID lobbyId)
        {
            var properties = new Dictionary<string, string>();
            int propertyCount = SteamMatchmaking.GetLobbyDataCount(lobbyId);

            for (int i = 0; i < propertyCount; i++)
            {
                string key = string.Empty;
                string value = string.Empty;
                int keySize = 256;
                int valueSize = 256;

                bool success = SteamMatchmaking.GetLobbyDataByIndex(
                    lobbyId,
                    i,
                    out key,
                    keySize,
                    out value,
                    valueSize
                );

                if (success)
                {
                    key = key.TrimEnd('\0');
                    value = value.TrimEnd('\0');
                    properties[key] = value;
                }
            }

            return properties;
        }

        private void OnLobbyChatUpdate(LobbyChatUpdate_t callback)
        {
            if (_currentLobby.m_SteamID != callback.m_ulSteamIDLobby)
                return;

            var stateChange = (EChatMemberStateChange)callback.m_rgfChatMemberStateChange;

            if (stateChange.HasFlag(EChatMemberStateChange.k_EChatMemberStateChangeEntered))
            {
                //PurrLogger.Log($"User {callback.m_ulSteamIDUserChanged} joined the lobby.");
            }

            if (stateChange.HasFlag(EChatMemberStateChange.k_EChatMemberStateChangeLeft) ||
                stateChange.HasFlag(EChatMemberStateChange.k_EChatMemberStateChangeDisconnected))
            {
                //PurrLogger.Log($"User {callback.m_ulSteamIDUserChanged} left the lobby.");
            }

            var ownerId = SteamMatchmaking.GetLobbyOwner(_currentLobby).m_SteamID.ToString();
            var localId = SteamUser.GetSteamID().m_SteamID.ToString();
            var isOwner = localId == ownerId;

            var data = SteamMatchmaking.GetLobbyData(_currentLobby, "Name");
            var properties = GetLobbyProperties(_currentLobby);
            var updatedLobbyUsers = GetLobbyUsers(_currentLobby);

            var updatedLobby = LobbyFactory.Create(
                data,
                _currentLobby.m_SteamID.ToString(),
                SteamMatchmaking.GetLobbyMemberLimit(_currentLobby),
                isOwner,
                updatedLobbyUsers,
                properties
            );

            OnLobbyUpdated?.Invoke(updatedLobby);
        }

        private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
        {
            var lobbyId = callback.m_steamIDLobby;
            //PurrLogger.Log($"Invite accepted. Joining lobby {lobbyId.m_SteamID}");

            _ = JoinLobbyAsync(lobbyId.m_SteamID.ToString());
        }

#pragma warning disable IDE0052 // Remove unread private members
        private Callback<LobbyDataUpdate_t> _lobbyDataUpdateCallback;
        private Callback<AvatarImageLoaded_t> _avatarImageLoadedCallback;
        private Callback<LobbyChatUpdate_t> _lobbyChatUpdateCallback;
        private Callback<GameLobbyJoinRequested_t> _gameLobbyJoinRequestedCallback;
#pragma warning restore IDE0052 // Remove unread private members
    }
}