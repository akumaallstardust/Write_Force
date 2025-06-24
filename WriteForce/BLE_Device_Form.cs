using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Bluetooth;         // NuGet: InTheHand.BluetoothLE

namespace WriteForce
{
    public partial class BLE_Device_Form : Form
    {

        // ---- BLE 関連フィールド ------------------------------------------------
        private BluetoothDevice? connectedDevice;
        private CancellationTokenSource? scanCts;
        private readonly Guid ServiceUuid = Guid.Parse("XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"); // 要置換
        private readonly ConcurrentDictionary<string, bool> seen = new();

        private BluetoothLEScan? currentScan; // ← RequestLEScanAsyncの戻り値を保持

        // ---- 公開プロパティ ----------------------------------------------------
        /// <summary>接続済みデバイス ID。未接続時は空文字。</summary>
        public string SelectedDeviceId => connectedDevice?.Id ?? string.Empty;

        // ---- コンストラクタ ----------------------------------------------------
        public BLE_Device_Form()
        {
            InitializeComponent();
            AcceptButton = btnOk;       // Enter キーで OK
        }

        // ========================================================================
        // イベント ハンドラ
        // ========================================================================
        private async void BtnScan_Click(object sender, EventArgs e)
            => await ScanAsync();

        private async void BtnConnect_Click(object sender, EventArgs e)
            => await ToggleConnectionAsync();

        private void LstDevices_SelectedIndexChanged(object? sender, EventArgs e)
            => UpdateButtons();

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
            => DisposeResources();

        // ========================================================================
        // スキャン処理
        // ========================================================================
        private async Task ScanAsync()
        {
            DisposeScan();  // スキャン中なら停止・イベント解除
            lstDevices.Items.Clear();
            seen.Clear();
            Log("スキャン開始…");

            btnScan.Enabled = false;
            btnConnect.Enabled = false;

            scanCts = new();
            var ct = scanCts.Token;

            if (!await Bluetooth.GetAvailabilityAsync().ConfigureAwait(false))
            {
                Log("BLE非対応デバイスです");
                btnScan.Enabled = true;
                return;
            }

            var scanOptions = new BluetoothLEScanOptions();
            scanOptions.Filters.Add(new BluetoothLEScanFilter { Services = { ServiceUuid } });

            try
            {
                // AdvertisementReceived イベント登録
                Bluetooth.AdvertisementReceived += OnAdvReceived;

                // スキャン開始
                currentScan = await Bluetooth.RequestLEScanAsync(scanOptions).ConfigureAwait(false);

                // タイムアウト処理（30秒後にキャンセル）
                _ = Task.Delay(TimeSpan.FromSeconds(30), ct)
                        .ContinueWith(_ => BeginInvoke(() => StopScan()), TaskScheduler.Default);
            }
            catch (Exception ex)
            {
                Log("スキャン開始失敗: " + ex.Message);
                StopScan();
            }
        }

        // ==== 広告イベントハンドラ ====
        private void OnAdvReceived(object? sender, BluetoothAdvertisingEvent e)
        {
            if (!seen.TryAdd(e.Device.Id, true)) return;

            // UI スレッドへマーシャリングしてリスト追加
            BeginInvoke(() =>
            {
                var dispName = string.IsNullOrWhiteSpace(e.Device.Name)
                               ? e.Device.Id
                               : e.Device.Name;

                lstDevices.Items.Add(new DeviceItem(dispName, e.Device.Id));
                Log($"発見: {dispName}");
                UpdateButtons();
            });
        }

        // ==== スキャン停止処理 ====
        private void StopScan()
        {
            currentScan?.Stop();
            currentScan = null;

            Bluetooth.AdvertisementReceived -= OnAdvReceived;

            scanCts?.Cancel();
            scanCts?.Dispose();
            scanCts = null;

            btnScan.Enabled = true;
            Log($"検出総数: {lstDevices.Items.Count} 台");
        }



        // ========================================================================
        // 接続／切断トグル
        // ========================================================================
        private async Task ToggleConnectionAsync()
        {
            if (lstDevices.SelectedItem is not DeviceItem item)
            {
                Log("デバイスを選択してください");
                return;
            }

            if (connectedDevice == null)
            {
                // ------ 接続処理 ------
                btnConnect.Enabled = false;
                btnConnect.Text = "接続中…";
                try
                {
                    var dev = await BluetoothDevice.FromIdAsync(item.Id).ConfigureAwait(false)
                              ?? throw new InvalidOperationException("デバイス取得失敗");

                    await dev.Gatt.ConnectAsync().ConfigureAwait(false);

                    var svc = (await dev.Gatt.GetPrimaryServicesAsync())
                              .FirstOrDefault(s => s.Uuid == ServiceUuid)
                              ?? throw new InvalidOperationException("対象サービス未検出");

                    _ = await svc.GetCharacteristicsAsync();  // RX/TX 検証省略

                    connectedDevice = dev;
                    btnConnect.Text = "切断";
                    Log($"接続成功: {dev.Name}");
                }
                catch (Exception ex)
                {
                    Log("接続失敗: " + ex.Message);
                    connectedDevice?.Gatt.Disconnect();
                    connectedDevice = null;
                    btnConnect.Text = "接続";
                }
                finally
                {
                    btnConnect.Enabled = true;
                }
            }
            else
            {
                // ------ 切断処理 ------
                connectedDevice.Gatt.Disconnect();
                Log("切断しました");
                connectedDevice = null;
                btnConnect.Text = "接続";
            }
            UpdateButtons();
        }

        // ========================================================================
        // 補助メソッド
        // ========================================================================
        private void UpdateButtons()
        {
            btnConnect.Enabled = lstDevices.SelectedItem is not null;
        }

        private void Log(string message) => lblStatus.Text = message;

        private void DisposeScan()
        {
            StopScan();  // StopScan() にすべて集約
        }

        private void DisposeResources()
        {
            DisposeScan();
            connectedDevice?.Gatt.Disconnect();
            connectedDevice = null;
        }

        // ========================================================================
        // 内部クラス
        // ========================================================================
        private sealed record DeviceItem(string Name, string Id)
        {
            public override string ToString() => Name;
        }
    }
}
